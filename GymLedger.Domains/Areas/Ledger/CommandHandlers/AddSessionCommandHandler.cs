using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Helpers.CookieAuth;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;
using System.Data.Entity.Validation;
using GymLedger.Models;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class AddSessionCommandValidator : ICommandHandler<AddSessionCommand, DataCommandResponse>
    {
        readonly ICommandHandler<AddSessionCommand, DataCommandResponse> decorated;
        AddSessionCommand Command;

        public AddSessionCommandValidator(ICommandHandler<AddSessionCommand, DataCommandResponse> decorated, AddSessionCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();
            using (DataContext db = new DataContext())
            {



            }

            return this.decorated.Execute();
        }
    }

    public class AddSessionCommandHandler : ICommandHandler<AddSessionCommand, DataCommandResponse>
    {
        readonly ICommandHandler<AddSessionCommand, DataCommandResponse> decorated;
        AddSessionCommand Command;

        public AddSessionCommandHandler(ICommandHandler<AddSessionCommand, DataCommandResponse> decorated, AddSessionCommand command)
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

                var view = this.Command.View;
                var exerciseId = db.Exercises.Where(e => e.Name == this.Command.View.ExerciseName && e.UserId == user.Id).FirstOrDefault().Id;

                Session exercise = new Session
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

                foreach( var set in this.Command.View.Sets)
                {
                    Set setModel = new Set
                    {
                        UniqueId = Guid.NewGuid().ToString("N"),
                        Reps = set.Reps,
                        SetNumber = set.SetNumber,
                        Weight = set.Weight,
                        SessionId = exercise.Id,
                        DateAdded = DateTime.UtcNow,
                        DateModified= DateTime.UtcNow,
                        
                    };
                    sets.Add(setModel);
                }

                exercise.Sets = sets;
                

                db.Sessions.Add(exercise);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Exercise added successfully";

                return response;
            }
        }
    }
}
