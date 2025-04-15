using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace GymLedger.Domains.Areas.Ledger.Commands
{
    public class EditOneRepMaxCommand : ICommand<DataCommandResponse>
    {
        public EditOneRepMaxView View { get; set; }
        public User UserIdentity { get; set; }
        public EditOneRepMaxCommand(EditOneRepMaxView view, HttpContextBase httpContext)
        {
            this.View = view;
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }

        public void ValidateMe()
        {
            if (this.View.UniqueId == null)
            {
                throw new Exception("UniqueId of one rep max not found");
            }
        }
    }
}