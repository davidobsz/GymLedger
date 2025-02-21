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
    public class EditExerciseCommand: ICommand<DataCommandResponse>
    {
        public EditExerciseView View { get; set; }
        public User UserIdentity { get; set; }
        public EditExerciseCommand(EditExerciseView view, HttpContextBase httpContext) 
        {
            this.View = view;
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }

        public void ValidateMe()
        {
            if (this.View.UniqueId == null)
            {
                throw new Exception("UniqueId of exercise not found");
            }
        }
    }
}
