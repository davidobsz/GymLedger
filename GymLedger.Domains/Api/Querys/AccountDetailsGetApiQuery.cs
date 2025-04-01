using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Profile;
using GymLedger.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Api.Querys
{
    public class AccountDetailsGetApiQuery : IQuery<MyProfileApiView>
    {
        public User UserIdentity { get; set; }

        public AccountDetailsGetApiQuery(string token) 
        {
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }

        public void ValidateMe()
        {

        }
    }
}
