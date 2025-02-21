using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class EditSessionCommandValidator : ICommandHandler<EditSessionCommand, DataCommandResponse>
    {
        readonly ICommandHandler<EditSessionCommand, DataCommandResponse> decorated;
        EditSessionCommand Command;

        public EditSessionCommandValidator(ICommandHandler<EditSessionCommand, DataCommandResponse> decorated, EditSessionCommand command)
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

    public class EditSessionCommandHandler : ICommandHandler<EditSessionCommand, DataCommandResponse>
    {
        readonly ICommandHandler<EditSessionCommand, DataCommandResponse> decorated;
        EditSessionCommand Command;

        public EditSessionCommandHandler(ICommandHandler<EditSessionCommand, DataCommandResponse> decorated, EditSessionCommand command)
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

                var session = db.Sessions.Where(e => e.UniqueId == this.Command.View.UniqueId).Include(s => s.Sets).FirstOrDefault();

                session.Date = this.Command.View.Date;
                session.DateModified = DateTime.UtcNow;
                session.ExerciseId = db.Exercises.FirstOrDefault(e => e.Name == this.Command.View.Exercise)?.Id ?? session.ExerciseId;

                foreach (var set in this.Command.View.Sets)
                {
                    // Check if the set exists in the current session
                    var existingSet = session.Sets.FirstOrDefault(s => s.SetNumber == set.SetNumber);
                    if (existingSet != null)
                    {
                        // Update the existing set
                        existingSet.Reps = set.Reps;
                        existingSet.SetNumber = set.SetNumber;
                        existingSet.Weight = set.Weight;
                    }
                    else
                    {
                        // Add new set if it doesn't exist
                        var newSet = new Set
                        {
                            Reps = set.Reps,
                            SetNumber = set.SetNumber,
                            Weight = set.Weight,
                            UniqueId = set.UniqueId, // Make sure to preserve UniqueId for the new set
                            SessionId = session.Id
                        };
                        session.Sets.Add(newSet);
                    }
                }

                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Session edited successfully";

                return response;
            }
        }
    }
}
