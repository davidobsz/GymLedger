using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class EditExerciseCommandValidatorApi : ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse> decorated;
        EditExerciseCommandApi Command;

        public EditExerciseCommandValidatorApi(ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse> decorated, EditExerciseCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
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

    public class EditExerciseCommandApiHandler : ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse> decorated;
        EditExerciseCommandApi Command;

        public EditExerciseCommandApiHandler(ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse> decorated, EditExerciseCommandApi command)
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

                // update exercise value
                exercise.Name = this.Command.View.Name;
                exercise.DateModified = DateTime.UtcNow;

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
                    Message = "Exercise edited successfully"
                };
            }
        }
    }
}
