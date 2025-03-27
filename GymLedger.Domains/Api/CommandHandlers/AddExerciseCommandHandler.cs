using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class AddExerciseCommandValidatorApi : ICommandHandler<AddExerciseCommand, ApiDataCommandResponse>
    {
        readonly ICommandHandler<AddExerciseCommand, ApiDataCommandResponse> decorated;
        AddExerciseCommand Command;

        public AddExerciseCommandValidatorApi(ICommandHandler<AddExerciseCommand, ApiDataCommandResponse> decorated, AddExerciseCommand command)
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

    public class AddExerciseCommandHandlerApi : ICommandHandler<AddExerciseCommand, ApiDataCommandResponse>
    {
        readonly ICommandHandler<AddExerciseCommand, ApiDataCommandResponse> decorated;
        AddExerciseCommand Command;

        public AddExerciseCommandHandlerApi(ICommandHandler<AddExerciseCommand, ApiDataCommandResponse> decorated, AddExerciseCommand command)
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
                    Message = "Exercise added successfully"
                };
            }
        }
    }
}
