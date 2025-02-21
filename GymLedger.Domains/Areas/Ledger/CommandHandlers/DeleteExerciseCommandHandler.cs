using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class DeleteExerciseCommandValidator : ICommandHandler<DeleteExerciseCommand, DataCommandResponse>
    {
        readonly ICommandHandler<DeleteExerciseCommand, DataCommandResponse> decorated;
        DeleteExerciseCommand Command;

        public DeleteExerciseCommandValidator(ICommandHandler<DeleteExerciseCommand, DataCommandResponse> decorated, DeleteExerciseCommand command)
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

    public class DeleteExerciseCommandHandler : ICommandHandler<DeleteExerciseCommand, DataCommandResponse>
    {
        readonly ICommandHandler<DeleteExerciseCommand, DataCommandResponse> decorated;
        DeleteExerciseCommand Command;

        public DeleteExerciseCommandHandler(ICommandHandler<DeleteExerciseCommand, DataCommandResponse> decorated, DeleteExerciseCommand command)
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

                db.Exercises.Remove(exercise);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Exercise Deleted successfully";

                return response;
            }
        }
    }
}
