using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Account.Login;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.Commands
{
    public class AddExerciseCommand : ICommand<ApiDataCommandResponse>
    {
        public AddExerciseView View { get; set; }
        public User UserIdentity { get; set; }

        public AddExerciseCommand(AddExerciseView view, string token)
        {
            this.View = view;
            this.UserIdentity = Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }

        public void ValidateMe()
        {
            if (string.IsNullOrWhiteSpace(View.Name))
            {
                throw new Exception("Exercise name is empty");
            }
        }
    }
}
