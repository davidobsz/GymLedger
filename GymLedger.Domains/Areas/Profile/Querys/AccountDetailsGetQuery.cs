using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Areas.Profile.Querys
{
    public class AccountDetailsGetQuery : IQuery<MyProfileView>
    {
        public User UserIdentity { get; set; }

        public AccountDetailsGetQuery(HttpContextBase httpContext)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }

        public void ValidateMe()
        {

        }
    }
}
