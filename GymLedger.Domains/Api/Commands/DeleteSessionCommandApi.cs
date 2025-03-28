using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;

namespace GymLedger.Domains.Api.Commands
{
    public class DeleteSessionCommandApi : ICommand<ApiDataCommandResponse>
    {
        public DeleteSessionView View { get; set; }
        public User UserIdentity { get; set; }

        public DeleteSessionCommandApi(DeleteSessionView view, string token)
        {
            this.View = view;

            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);

        }
        public void ValidateMe()
        {
            if (View == null)
            {
                throw new Exception("UniqueId is empty");
            }
            if (View.UniqueId == null)
            {
                throw new Exception("UniqueId is empty");
            }
        }
    }
}
