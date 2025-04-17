using GymLedger.Domains.BaseCommands;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Api.Commands
{
    public class DeleteOneRepMaxCommandApi : ICommand<ApiDataCommandResponse>
    {
        public string UniqueId { get; set; }
        public User UserIdentity { get; set; }

        public DeleteOneRepMaxCommandApi(string uniqueId, string token)
        {
            this.UniqueId = uniqueId;

            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);

        }
        public void ValidateMe()
        {
            if (UniqueId == null)
            {
                throw new Exception("UniqueId is empty");
            }


        }
    }
}
