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
    public class DeleteOneRepMaxCommandValidatorApi : ICommandHandler<DeleteOneRepMaxCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteOneRepMaxCommandApi, ApiDataCommandResponse> decorated;
        DeleteOneRepMaxCommandApi Command;

        public DeleteOneRepMaxCommandValidatorApi(ICommandHandler<DeleteOneRepMaxCommandApi, ApiDataCommandResponse> decorated, DeleteOneRepMaxCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();

            using (DataContext db = new DataContext())
            {
                var exists = db.OneRepMaxes.Any(e => e.UniqueId == this.Command.UniqueId);

                if (!exists)
                {
                    throw new Exception("Cannot find one rep max to delete");
                }
            }

            return this.decorated.Execute();
        }
    }

    public class DeleteOneRepMaxCommandHandlerApi : ICommandHandler<DeleteOneRepMaxCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteOneRepMaxCommandApi, ApiDataCommandResponse> decorated;
        DeleteOneRepMaxCommandApi Command;

        public DeleteOneRepMaxCommandHandlerApi(ICommandHandler<DeleteOneRepMaxCommandApi, ApiDataCommandResponse> decorated, DeleteOneRepMaxCommandApi command)
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

                var orm = db.OneRepMaxes.SingleOrDefault(r => r.UniqueId == this.Command.UniqueId);

                db.OneRepMaxes.Remove(orm);
                db.SaveChanges();

                var response = new ApiDataCommandResponse();

                response.Success = true;
                response.Message = "One rep max deleted successfully";

                return response;
            }
        }
    }
}
