using GymLedger.Domains.BaseCommands;
using GymLedger.Views.Account.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.Commands
{
    public class LoginApiAccountCommand : ICommand<ApiDataCommandResponse>
    {
        public LoginAccountView View { get; set; }

        public LoginApiAccountCommand(LoginAccountView view)
        {
            this.View = view;
        }

        public void ValidateMe()
        {
            if (string.IsNullOrWhiteSpace(View.Password))
            {
                throw new Exception("Password is empty");
            }

            if (string.IsNullOrWhiteSpace(View.Username))
            {
                throw new Exception("Username is empty");
            }
        }
    }
}
