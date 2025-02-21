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
    public class DeleteExerciseCommand : ICommand<DataCommandResponse>
    {
        public DeleteExerciseView View { get; set; }
        public User UserIdentity { get; set; }

        public DeleteExerciseCommand(DeleteExerciseView view, HttpContextBase context)
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
