using GymLedger.Domains.BaseCommands;
using GymLedger.Views.Account.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.Commands
{
    public class RegisterAccountCommandApi : ICommand<ApiDataCommandResponse>
    {
        public RegisterAccountView View { get; set; }

        public RegisterAccountCommandApi(RegisterAccountView view)
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

            // password validation
            if (View.Password.Count() < 7)
            {
                throw new Exception("Password must have at least 8 characters");
            }

            if (!Regex.IsMatch(View.Password, @"\d"))
            {
                throw new Exception("Password must contain at least 1 number");
            }

            if (!Regex.IsMatch(View.Password, @"[\W_]+"))
            {
                throw new Exception("Password must contain at least 1 special character");
            }
        }
    }
}
