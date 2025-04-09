using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Api.Commands
{
    public class ChangePasswordCommandApi : ICommand<ApiDataCommandResponse>
    {
        public ChangePasswordView View { get; set; }
        public User UserIdentity { get; set; }

        public ChangePasswordCommandApi(string token, ChangePasswordView view)
        {
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
            this.View = view;
        }

        public void ValidateMe()
        {
            if (View.CurrentPassword == null)
            {
                throw new Exception("Current password cannot be empty");
            }

            if (View.NewPassword == null)
            {
                throw new Exception("New password field cannot be empty");
            }
            if (View.ConfirmNewPassword == null)
            {
                throw new Exception("Confirm new password field cannot be empty");
            }

            if (View.NewPassword != View.ConfirmNewPassword)
            {
                throw new Exception("New Password and Confirm New Password do not match");
            }
        }
    }
}
