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
    public class GetOneRepMaxesQueryApi : IQuery<GetOneRepMaxesView>
    {
        public User UserIdentity { get; set; }

        public GetOneRepMaxesQueryApi(string token)
        {
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }
        public void ValidateMe()
        {

        }
    }
}
