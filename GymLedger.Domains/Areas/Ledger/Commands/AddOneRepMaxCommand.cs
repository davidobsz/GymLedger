using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Commands
{
    public class AddOneRepMaxCommand : ICommand<DataCommandResponse>
    {
        public AddOneRepMaxView View { get; set; }
        public User UserIdentity { get; set; }

        public AddOneRepMaxCommand(AddOneRepMaxView view, HttpContextBase context)
        {
            this.View = view;

            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();

        }
        public void ValidateMe()
        {
            if (View.Weight == null)
            {
                throw new Exception("Weight cannot be empty");
            }


        }
    }
}
