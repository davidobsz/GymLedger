using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class DeleteSessionCommandValidatorApi : ICommandHandler<DeleteSessionCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteSessionCommandApi, ApiDataCommandResponse> decorated;
        DeleteSessionCommandApi Command;

        public DeleteSessionCommandValidatorApi(ICommandHandler<DeleteSessionCommandApi, ApiDataCommandResponse> decorated, DeleteSessionCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();

            using (DataContext db = new DataContext())
            {
                var exists = db.Sessions.Where(s => s.UniqueId == this.Command.View.UniqueId).Any();

                if (!exists)
                {
                    throw new Exception("This session does not exist");
                }
            };

            return this.decorated.Execute();
        }
    }

    public class DeleteSessionCommandHandlerApi : ICommandHandler<DeleteSessionCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteSessionCommandApi, ApiDataCommandResponse> decorated;
        DeleteSessionCommandApi Command;

        public DeleteSessionCommandHandlerApi(ICommandHandler<DeleteSessionCommandApi, ApiDataCommandResponse> decorated, DeleteSessionCommandApi command)
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

                var session = db.Sessions.Where(e => e.UniqueId == this.Command.View.UniqueId).SingleOrDefault();

                db.Sessions.Remove(session);
                db.SaveChanges();

                var response = new ApiDataCommandResponse();

                response.Success = true;
                response.Message = "Session Deleted successfully";
                response.Data = session;

                return response;
            }
        }
    }
}
