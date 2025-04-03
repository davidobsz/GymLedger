using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace GymLedger.Domains.Areas.Profile.Commands
{
    public class DeleteUserCommand : ICommand<DataCommandResponse>
    {
        public User UserIdentity { get; set; }
        public DeleteUserView View { get; set; }

        public DeleteUserCommand(HttpContextBase httpContext, DeleteUserView view) 
        {
            this.View = view;
            this.UserIdentity = Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }

        public void ValidateMe() 
        {
            if (string.IsNullOrEmpty(this.View.CurrentPassword))
            {
                throw new Exception("Must enter current password");
            }
        }
    }
}
