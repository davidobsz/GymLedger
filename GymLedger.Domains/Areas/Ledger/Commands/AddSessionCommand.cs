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
    public class AddSessionCommand : ICommand<DataCommandResponse>
    {
        public AddSessionView View { get; set; }
        public User UserIdentity { get; set; }

        public AddSessionCommand(AddSessionView view, HttpContextBase httpContext)
        {
            this.View = view;
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }
        public void ValidateMe()
        {
            if (View.Sets.Count < 1)
            {
                throw new Exception("You must add at least one set");
            }
        }
    }
}
