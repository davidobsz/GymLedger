using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Commands
{
    public class DeleteSessionCommand : ICommand<DataCommandResponse>
    {
        public DeleteSessionView View { get; set; }
        public User UserIdentity { get; set; }

        public DeleteSessionCommand(DeleteSessionView view, HttpContextBase context)
        {
            this.View = view;

            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();

        }
        public void ValidateMe()
        {
            if (View.UniqueId == null)
            {
                throw new Exception("UniqueId is empty");
            }


        }
    }
}
