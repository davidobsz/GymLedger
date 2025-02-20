using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Account.Login;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;
using GymLedger.Helpers.CookieAuth;
namespace GymLedger.Domains.Areas.Ledger.Commands
{
    public class AddExerciseCommand : ICommand<DataCommandResponse>
    {
        public AddExerciseView View { get; set; }
        public User UserIdentity { get; set; }

        public AddExerciseCommand(AddExerciseView view, HttpContextBase context)
        {
            this.View = view;

            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();

        }
        public void ValidateMe()
        {
            if (View.Name == null)
            {
                throw new Exception("Exercise name is empty");
            }

            
        }
    }
}
