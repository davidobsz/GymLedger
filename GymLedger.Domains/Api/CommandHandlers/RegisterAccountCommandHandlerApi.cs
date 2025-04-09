using GymLedger.Common.Enums;
using GymLedger.Data;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class RegisterAccountCommandValidatorApi : ICommandHandler<RegisterAccountCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<RegisterAccountCommandApi, ApiDataCommandResponse> decorated;
        RegisterAccountCommandApi Command;

        public RegisterAccountCommandValidatorApi(ICommandHandler<RegisterAccountCommandApi, ApiDataCommandResponse> decorated, RegisterAccountCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
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

    public class RegisterAccountCommandHandlerApi : ICommandHandler<RegisterAccountCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<RegisterAccountCommandApi, ApiDataCommandResponse> decorated;
        RegisterAccountCommandApi Command;

        public RegisterAccountCommandHandlerApi(ICommandHandler<RegisterAccountCommandApi, ApiDataCommandResponse> decorated, RegisterAccountCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
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

                var response = new ApiDataCommandResponse();

                response.Success = true;
                response.Message = "Registration Successful";
                response.Data = user;

                return response;
            }
        }
    }
}
