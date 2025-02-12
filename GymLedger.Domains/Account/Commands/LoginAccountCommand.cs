using GymLedger.Domains.BaseCommands;
using GymLedger.Views.Account.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymLedger.Domains.Account.Commands
{
    public class LoginAccountCommand: ICommand<DataCommandResponse>
    {
        public LoginAccountView View { get; set; }

        public LoginAccountCommand(LoginAccountView view) 
        { 
            this.View = view;
        }
        public void ValidateMe()
        {
            if (View.Password == null)
            {
                throw new Exception("Password is empty");
            }

            if (View.Username == null)
            {
                throw new Exception("Username is empty");
            }
        }
    }
}
