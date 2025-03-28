using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System.Web;

namespace GymLedger.Domains.Api.Commands
{
    public class AddSessionCommandApi : ICommand<ApiDataCommandResponse>
    {
        public AddSessionApiView View { get; set; }
        public User UserIdentity { get; set; }

        public AddSessionCommandApi(AddSessionApiView view, string token)
        {
            this.View = view;
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }
        public void ValidateMe()
        {
            if (View.Sets == null)
            {
                throw new Exception("You must add at least one set");
            }
            if (View.Sets.Count < 1)
            {
                throw new Exception("You must add at least one set");
            }
            if (View.ExerciseName == null)
            {
                throw new Exception("you must add an exercise");
            }
        }
    }
}
