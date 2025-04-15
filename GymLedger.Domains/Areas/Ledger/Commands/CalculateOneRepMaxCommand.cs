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
    public class CalculateOneRepMaxCommand : ICommand<DataCommandResponse>
    {
        public CalculateOneRepMaxView View { get; set; }
        public User UserIdentity { get; set; }
        public CalculateOneRepMaxCommand(CalculateOneRepMaxView view, HttpContextBase httpContext)
        {
            this.View = view;
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }

        public void ValidateMe()
        {
            if (this.View.Reps < 0)
            {
                throw new Exception("Reps cannot be less than 0");
            }

            if (this.View.Weight < 0)
            {
                throw new Exception("Weight cannot be less than 0");
            }
        }
    }
}
