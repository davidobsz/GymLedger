using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class AddOneRepMaxCommandValidator : ICommandHandler<AddOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<AddOneRepMaxCommand, DataCommandResponse> decorated;
        AddOneRepMaxCommand Command;

        public AddOneRepMaxCommandValidator(ICommandHandler<AddOneRepMaxCommand, DataCommandResponse> decorated, AddOneRepMaxCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();

            return this.decorated.Execute();
        }
    }

    public class AddOneRepMaxCommandHandler : ICommandHandler<AddOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<AddOneRepMaxCommand, DataCommandResponse> decorated;
        AddOneRepMaxCommand Command;

        public AddOneRepMaxCommandHandler(ICommandHandler<AddOneRepMaxCommand, DataCommandResponse> decorated, AddOneRepMaxCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
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

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "One Rep Max added successfully";

                return response;
            }
        }
    }
}
