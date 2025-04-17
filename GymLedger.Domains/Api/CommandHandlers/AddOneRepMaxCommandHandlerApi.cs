using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class AddOneRepMaxCommandValidatorApi : ICommandHandler<AddOneRepMaxCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<AddOneRepMaxCommandApi, ApiDataCommandResponse> decorated;
        AddOneRepMaxCommandApi Command;

        public AddOneRepMaxCommandValidatorApi(ICommandHandler<AddOneRepMaxCommandApi, ApiDataCommandResponse> decorated, AddOneRepMaxCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();

            using (DataContext db = new DataContext()) 
            {
                var exists = db.Exercises.Any(e => e.Name == this.Command.View.ExerciseName);

                if (!exists)
                {
                    throw new Exception("The exercise name given is not one of your added exercises");   
                }
            }

            return this.decorated.Execute();
        }
    }

    public class AddOneRepMaxCommandHandlerApi : ICommandHandler<AddOneRepMaxCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<AddOneRepMaxCommandApi, ApiDataCommandResponse> decorated;
        AddOneRepMaxCommandApi Command;

        public AddOneRepMaxCommandHandlerApi(ICommandHandler<AddOneRepMaxCommandApi, ApiDataCommandResponse> decorated, AddOneRepMaxCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            using (DataContext db = new DataContext())
            {

                // get user
                var user = db.Users.FirstOrDefault(u => u.Username == this.Command.UserIdentity.Username);
                var command = this.Command;

                OneRepMax oneRepMax = new OneRepMax
                {
                    UniqueId = Guid.NewGuid().ToString("N"),
                    UserId = user.Id,
                    User = user,
                    DateAdded = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    ExerciseId = db.Exercises.SingleOrDefault(e => e.Name == this.Command.View.ExerciseName && e.UserId == user.Id).Id,
                    Weight = this.Command.View.Weight,
                    Date = this.Command.View.Date,
                };

                db.OneRepMaxes.Add(oneRepMax);
                db.SaveChanges();

                var response = new ApiDataCommandResponse();

                response.Success = true;
                response.Message = "One Rep Max added successfully";
                return response;
            }
        }
    }
}
