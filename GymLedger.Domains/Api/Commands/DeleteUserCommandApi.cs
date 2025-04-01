using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using GymLedger.Views.Areas.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymLedger.Domains.Api.Commands
{
    public class DeleteUserCommandApi : ICommand<ApiDataCommandResponse>
    {
        public DeleteUserApiView View { get; set; }
        public User UserIdentity { get; set; }

        public DeleteUserCommandApi(DeleteUserApiView view, string token)
        {
            this.View = view;

            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);

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
