using GymLedger.Data;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Helpers.CookieAuth;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class AddExerciseCommandValidator : ICommandHandler<AddExerciseCommand, DataCommandResponse>
    {
        readonly ICommandHandler<AddExerciseCommand, DataCommandResponse> decorated;
        AddExerciseCommand Command;

        public AddExerciseCommandValidator(ICommandHandler<AddExerciseCommand, DataCommandResponse> decorated, AddExerciseCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();
            using (DataContext db = new DataContext())
            {
                bool exists = db.Exercises.Any(e => e.Name == this.Command.View.Name && e.User.Username == this.Command.UserIdentity.Username);

                if (exists) 
                {
                    throw new Exception("This exercise already exists");
                }
            }

            return this.decorated.Execute();
        }
    }

    public class AddExerciseCommandHandler : ICommandHandler<AddExerciseCommand, DataCommandResponse>
    {
        readonly ICommandHandler<AddExerciseCommand, DataCommandResponse> decorated;
        AddExerciseCommand Command;

        public AddExerciseCommandHandler(ICommandHandler<AddExerciseCommand, DataCommandResponse> decorated, AddExerciseCommand command)
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

                Exercise exercise = new Exercise
                {
                    Name = this.Command.View.Name,
                    UniqueId = Guid.NewGuid().ToString("N"),
                    UserId = user.Id,
                    User = user,
                    DateAdded = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                db.Exercises.Add(exercise);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Exercise added successfully";

                return response;
            }
        }
    }
}
