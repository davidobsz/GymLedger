using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Models;
using GymLedger.Domains.Api.Commands;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class AddSessionCommandValidatorApi : ICommandHandler<AddSessionCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<AddSessionCommandApi, ApiDataCommandResponse> decorated;
        AddSessionCommandApi Command;

        public AddSessionCommandValidatorApi(ICommandHandler<AddSessionCommandApi, ApiDataCommandResponse> decorated, AddSessionCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();
            using (DataContext db = new DataContext())
            {
                // validate that exercise name actually exists.
                var user = db.Users.FirstOrDefault(u => u.Username == this.Command.UserIdentity.Username);
                var exercises = db.Exercises.Where(e => e.UserId == user.Id).ToList();
                var isExercise = false;
                foreach (var exercise in exercises)
                {
                    if (exercise.Name == this.Command.View.ExerciseName)
                    {
                        isExercise = true;
                    }
                }
                if (!isExercise) 
                {
                    throw new Exception("The exercise you have used does not exist");
                }

            }

            return this.decorated.Execute();
        }
    }

    public class AddSessionCommandHandlerApi : ICommandHandler<AddSessionCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<AddSessionCommandApi, ApiDataCommandResponse> decorated;
        AddSessionCommandApi Command;

        public AddSessionCommandHandlerApi(ICommandHandler<AddSessionCommandApi, ApiDataCommandResponse> decorated, AddSessionCommandApi command)
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
                var exerciseId = db.Exercises.Where(e => e.Name == this.Command.View.ExerciseName && e.UserId == user.Id).FirstOrDefault().Id;

                Session session = new Session
                {
                    UniqueId = Guid.NewGuid().ToString("N"),
                    UserId = user.Id,
                    User = user,
                    Date = this.Command.View.Date,
                    DateAdded = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    ExerciseId = exerciseId
                };

                List<Set> sets = new List<Set>();

                foreach (var set in this.Command.View.Sets)
                {
                    Set setModel = new Set
                    {
                        UniqueId = Guid.NewGuid().ToString("N"),
                        Reps = set.Reps,
                        SetNumber = set.SetNumber,
                        Weight = set.Weight,
                        SessionId = session.Id,
                        DateAdded = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,

                    };
                    sets.Add(setModel);
                }

                session.Sets = sets;


                db.Sessions.Add(session);
                db.SaveChanges();

                return new ApiDataCommandResponse
                {
                    Success = true,
                    Data = new
                    {
                        Id = session.Id,
                        ExerciseId = session.ExerciseId,
                        ExerciseName = this.Command.View.ExerciseName,
                        UniqueId = session.UniqueId,
                        DateAdded = session.DateAdded,
                        DateModified = session.DateModified,
                        Date = session.Date,
                        UserId = session.Id,
                        Sets = sets.Select(set => new
                        {
                            Id = set.Id,
                            UniqueId = set.UniqueId,
                            SetNumber = set.SetNumber,
                            Weight = set.Weight,
                            Reps = set.Reps,
                            DateAdded = set.DateAdded,
                            DateModified = set.DateModified
                        }).ToList()
                    },
                    Message = "Session added successfully"
                };
            }
        }
    }
}
