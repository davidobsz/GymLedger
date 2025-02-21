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
    public class EditExerciseCommandValidator : ICommandHandler<EditExerciseCommand, DataCommandResponse>
    {
        readonly ICommandHandler<EditExerciseCommand, DataCommandResponse> decorated;
        EditExerciseCommand Command;

        public EditExerciseCommandValidator(ICommandHandler<EditExerciseCommand, DataCommandResponse> decorated, EditExerciseCommand command)
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

    public class EditExerciseCommandHandler : ICommandHandler<EditExerciseCommand, DataCommandResponse>
    {
        readonly ICommandHandler<EditExerciseCommand, DataCommandResponse> decorated;
        EditExerciseCommand Command;

        public EditExerciseCommandHandler(ICommandHandler<EditExerciseCommand, DataCommandResponse> decorated, EditExerciseCommand command)
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

                var exercise = db.Exercises.Where(e => e.UniqueId == this.Command.View.UniqueId).SingleOrDefault();

                // update exercise value
                exercise.Name = this.Command.View.Name;
                exercise.DateModified = DateTime.UtcNow;

                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Exercise edited successfully";

                return response;
            }
        }
    }
}
