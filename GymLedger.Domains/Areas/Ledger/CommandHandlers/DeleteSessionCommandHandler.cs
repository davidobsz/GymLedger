using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class DeleteSessionCommandValidator : ICommandHandler<DeleteSessionCommand, DataCommandResponse>
    {
        readonly ICommandHandler<DeleteSessionCommand, DataCommandResponse> decorated;
        DeleteSessionCommand Command;

        public DeleteSessionCommandValidator(ICommandHandler<DeleteSessionCommand, DataCommandResponse> decorated, DeleteSessionCommand command)
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

    public class DeleteSessionCommandHandler : ICommandHandler<DeleteSessionCommand, DataCommandResponse>
    {
        readonly ICommandHandler<DeleteSessionCommand, DataCommandResponse> decorated;
        DeleteSessionCommand Command;

        public DeleteSessionCommandHandler(ICommandHandler<DeleteSessionCommand, DataCommandResponse> decorated, DeleteSessionCommand command)
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

                var session = db.Sessions.Where(e => e.UniqueId == this.Command.View.UniqueId).SingleOrDefault();

                db.Sessions.Remove(session);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Exercise Deleted successfully";

                return response;
            }
        }
    }
}
