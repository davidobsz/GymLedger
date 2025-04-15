using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class DeleteOneRepMaxCommandValidator : ICommandHandler<DeleteOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<DeleteOneRepMaxCommand, DataCommandResponse> decorated;
        DeleteOneRepMaxCommand Command;

        public DeleteOneRepMaxCommandValidator(ICommandHandler<DeleteOneRepMaxCommand, DataCommandResponse> decorated, DeleteOneRepMaxCommand command)
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

    public class DeleteOneRepMaxCommandHandler : ICommandHandler<DeleteOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<DeleteOneRepMaxCommand, DataCommandResponse> decorated;
        DeleteOneRepMaxCommand Command;

        public DeleteOneRepMaxCommandHandler(ICommandHandler<DeleteOneRepMaxCommand, DataCommandResponse> decorated, DeleteOneRepMaxCommand command)
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

                var orm = db.OneRepMaxes.SingleOrDefault(r => r.UniqueId == this.Command.UniqueId);

                db.OneRepMaxes.Remove(orm);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "One rep max deleted successfully";

                return response;
            }
        }
    }
}
