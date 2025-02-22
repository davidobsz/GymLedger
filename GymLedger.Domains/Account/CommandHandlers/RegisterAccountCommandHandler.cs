using GymLedger.Common.Enums;
using GymLedger.Data;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Account.CommandHandlers
{
    public class RegisterAccountCommandValidator: ICommandHandler<RegisterAccountCommand, DataCommandResponse>
    {
        readonly ICommandHandler<RegisterAccountCommand, DataCommandResponse> decorated;
        RegisterAccountCommand Command;

        public RegisterAccountCommandValidator(ICommandHandler<RegisterAccountCommand, DataCommandResponse> decorated, RegisterAccountCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();
            using (DataContext db = new DataContext())
            {
                bool exists = db.Users.Any(i => i.Username == this.Command.View.Username);
                if (exists)
                {
                    throw new Exception("This account already exists"); // against owasp i think. TODO: need to change this.
                }
            }

            return this.decorated.Execute();
        }
    }

    public class RegisterAccountCommandHandler: ICommandHandler<RegisterAccountCommand, DataCommandResponse>
    {
        readonly ICommandHandler<RegisterAccountCommand, DataCommandResponse> decorated;
        RegisterAccountCommand Command;

        public RegisterAccountCommandHandler(ICommandHandler<RegisterAccountCommand, DataCommandResponse> decorated, RegisterAccountCommand command) 
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            using (DataContext db = new DataContext())
            {
                var users = db.Users;
                User user = new User
                {
                    Username = this.Command.View.Username,
                    Password = Helpers.PasswordHelper.PasswordHelper.HashPassword(this.Command.View.Password),
                    UserRole = UserRole.User,
                    UniqueId = Guid.NewGuid().ToString("N"),   
                    DateAdded = DateTime.UtcNow,
                };

                users.Add(user);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Registration Successful";

                return response;
            }
        }
    }
}
