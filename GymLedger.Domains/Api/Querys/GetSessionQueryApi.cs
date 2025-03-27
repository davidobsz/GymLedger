using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Api.Querys
{
    public class GetSessionQueryApi : IQuery<SessionDetailView>
    {
        public User UserIdentity { get; set; }

        public string UniqueId { get; set; }

        public GetSessionQueryApi(string token, string uniqueId)
        {
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
            this.UniqueId = uniqueId;
        }
        public void ValidateMe()
        {

        }
    }
}
