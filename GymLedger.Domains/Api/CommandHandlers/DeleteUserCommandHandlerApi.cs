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
    public class DeleteUserCommandValidatorApi : ICommandHandler<DeleteUserCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteUserCommandApi, ApiDataCommandResponse> decorated;
        DeleteUserCommandApi Command;

        public DeleteUserCommandValidatorApi(ICommandHandler<DeleteUserCommandApi, ApiDataCommandResponse> decorated, DeleteUserCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            Command.ValidateMe();

            using (DataContext db = new DataContext())
            {
                var exists = db.Users.Where(e => e.UniqueId == this.Command.View.UniqueId).Any();

                if (!exists)
                {
                    throw new Exception("This user does not exist");
                }
            };

            return this.decorated.Execute();
        }
    }

    public class DeleteUserCommandHandlerApi : ICommandHandler<DeleteUserCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<DeleteUserCommandApi, ApiDataCommandResponse> decorated;
        DeleteUserCommandApi Command;

        public DeleteUserCommandHandlerApi(ICommandHandler<DeleteUserCommandApi, ApiDataCommandResponse> decorated, DeleteUserCommandApi command)
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

                db.Users.Remove(user);
                db.SaveChanges();

                return new ApiDataCommandResponse
                {
                    Success = true,
                    Data = new
                    {
                        Username = user.Username,
                        UniqueId = user.UniqueId,
                    },
                    Message = "User deleted successfully"
                };
            }
        }
    }
}
