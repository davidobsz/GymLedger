using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.Querys
{
    public class GetOneRepMaxQueryApi : IQuery<OneRepMaxApiView>
    {
        public User UserIdentity { get; set; }
        public string UniqueId { get; set; }

        public GetOneRepMaxQueryApi(string token, string uniqueId)
        {
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
            UniqueId = uniqueId;
        }
        public void ValidateMe()
        {

        }
    }
}
