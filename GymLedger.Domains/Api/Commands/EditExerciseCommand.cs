using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Api.Commands
{
    public class EditExerciseCommandApi : ICommand<ApiDataCommandResponse>
    {
        public EditExerciseView View { get; set; }
        public User UserIdentity { get; set; }
        public EditExerciseCommandApi(EditExerciseView view, string token)
        {
            this.View = view;
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
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
