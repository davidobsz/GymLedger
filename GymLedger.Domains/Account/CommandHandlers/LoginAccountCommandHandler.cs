using GymLedger.Common.Enums;
using GymLedger.Data;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Helpers.CookieAuth;
using GymLedger.Helpers.PasswordHelper;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Account.CommandHandlers
{
    public class LoginAccountCommandValidator : ICommandHandler<LoginAccountCommand, DataCommandResponse>
    {
        readonly ICommandHandler<LoginAccountCommand, DataCommandResponse> decorated;
        LoginAccountCommand Command;

        public LoginAccountCommandValidator(ICommandHandler<LoginAccountCommand, DataCommandResponse> decorated, LoginAccountCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();
            using (DataContext db = new DataContext())
            {
                bool exists = db.Users.Any(u => u.Username == this.Command.View.Username);

                if (!exists)
                {
                    throw new Exception("Username or password incorrect");
                }

                if(exists)
                {
                    var user = db.Users.Where(u => u.Username == this.Command.View.Username).FirstOrDefault();

                    if (user.Password != PasswordHelper.HashPassword(this.Command.View.Password))
                    {
                        throw new Exception("Username or password is incorrect");
                    }
                }

            }

            return this.decorated.Execute();
        }
    }

    public class LoginAccountCommandHandler : ICommandHandler<LoginAccountCommand, DataCommandResponse>
    {
        readonly ICommandHandler<LoginAccountCommand, DataCommandResponse> decorated;
        LoginAccountCommand Command;

        public LoginAccountCommandHandler(ICommandHandler<LoginAccountCommand, DataCommandResponse> decorated, LoginAccountCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            using (DataContext db = new DataContext())
            {
                // log user in
                AuthCookieHelper.CreateAuthCookie(this.Command.View.Username, PasswordHelper.HashPassword(this.Command.View.Password), true);

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Login Successful";

                return response;
            }
        }
    }

}
