using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class EditSessionCommandValidatorApi : ICommandHandler<EditSessionCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<EditSessionCommandApi, ApiDataCommandResponse> decorated;
        EditSessionCommandApi Command;

        public EditSessionCommandValidatorApi(ICommandHandler<EditSessionCommandApi, ApiDataCommandResponse> decorated, EditSessionCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();

            return this.decorated.Execute();
        }
    }

    public class EditSessionCommandHandlerApi : ICommandHandler<EditSessionCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<EditSessionCommandApi, ApiDataCommandResponse> decorated;
        EditSessionCommandApi Command;

        public EditSessionCommandHandlerApi(ICommandHandler<EditSessionCommandApi, ApiDataCommandResponse> decorated, EditSessionCommandApi command)
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

                var session = db.Sessions.Where(e => e.UniqueId == this.Command.View.UniqueId).Include(s => s.Sets).FirstOrDefault();

                if (session == null)
                {
                    return new ApiDataCommandResponse
                    {
                        Success = false,
                        Message = "could not find session with the given UniqueId"
                    };
                }

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

                var response = new ApiDataCommandResponse();

                response.Success = true;
                response.Message = $"Session with UniqueId: {session.UniqueId} edited successfully";

                return response;
            }
        }
    }
}
