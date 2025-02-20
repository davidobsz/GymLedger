using GymLedger.Domains.BaseCommands;
using GymLedger.Views.Account.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymLedger.Domains.Account.Commands
{
    public class RegisterAccountCommand: ICommand<DataCommandResponse>
    {
        public RegisterAccountView View { get; set; }

        public RegisterAccountCommand(RegisterAccountView view)
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
