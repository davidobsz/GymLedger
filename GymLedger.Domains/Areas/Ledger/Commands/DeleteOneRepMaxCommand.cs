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
    public class DeleteOneRepMaxCommand : ICommand<DataCommandResponse>
    {
        public string UniqueId { get; set; }
        public User UserIdentity { get; set; }

        public DeleteOneRepMaxCommand(string uniqueId, HttpContextBase context)
        {
            this.UniqueId = uniqueId;

            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();

        }
        public void ValidateMe()
        {
            if (UniqueId == null)
            {
                throw new Exception("UniqueId is empty");
            }


        }
    }
}
