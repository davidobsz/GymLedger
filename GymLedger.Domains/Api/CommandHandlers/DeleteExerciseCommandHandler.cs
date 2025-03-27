using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class DeleteExerciseCommandValidatorApi : ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse> decorated;
        DeleteExerciseCommandApi Command;

        public DeleteExerciseCommandValidatorApi(ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse> decorated, DeleteExerciseCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();

            using (DataContext db = new DataContext())
            {
                var exists = db.Exercises.Where(e => e.UniqueId == this.Command.View.UniqueId).Any();

                if (!exists)
                {
                    throw new Exception("This exercise does not exist");
                }
            };

            return this.decorated.Execute();
        }
    }

    public class DeleteExerciseCommandHandlerApi : ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse> decorated;
        DeleteExerciseCommandApi Command;

        public DeleteExerciseCommandHandlerApi(ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse> decorated, DeleteExerciseCommandApi command)
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

                var exercise = db.Exercises.Where(e => e.UniqueId == this.Command.View.UniqueId).SingleOrDefault();

                db.Exercises.Remove(exercise);
                db.SaveChanges();

                return new ApiDataCommandResponse
                {
                    Success = true,
                    Data = new
                    {
                        Name = exercise.Name,
                        UniqueId = exercise.UniqueId,
                        UserId = exercise.UserId,
                        DateAdded = exercise.DateAdded,
                        DateModified = exercise.DateModified
                    },
                    Message = "Exercise deleted successfully"
                };
            }
        }
    }
}
