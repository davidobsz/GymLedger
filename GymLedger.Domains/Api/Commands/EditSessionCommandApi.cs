using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymLedger.Domains.Api.Commands
{
    public class EditSessionCommandApi: ICommand<ApiDataCommandResponse>
    {
        public EditSessionView View { get; set; }
        public User UserIdentity { get; set; }

        public EditSessionCommandApi(EditSessionView view, string token)
        {
            this.View = view;
            this.UserIdentity = Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }

        public void ValidateMe()
        {
            if (this.View.UniqueId == null)
            {
                throw new Exception("UniqueId of session not found");
            }
        }
    }
}
