using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Api.Commands
{
    public class AddOneRepMaxCommandApi : ICommand<ApiDataCommandResponse>
    {
        public AddOneRepMaxApiView View { get; set; }
        public User UserIdentity { get; set; }

        public AddOneRepMaxCommandApi(AddOneRepMaxApiView view, string token)
        {
            this.View = view;

            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);

        }
        public void ValidateMe()
        {
            if (View.Weight < 0)
            {
                throw new Exception("Weight cannot be empty");
            }

            if (View.ExerciseName == null)
            {
                throw new Exception("Exercise Name cannot be empty");
            }
        }
    }
}
