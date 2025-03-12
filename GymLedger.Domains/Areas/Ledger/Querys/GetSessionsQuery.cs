using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Querys
{
    public class GetSessionsQuery : IQuery<GetSessionView>
    {
        public User UserIdentity { get; set; }

        public string UniqueId { get; set; }

        public GetSessionsQuery(HttpContextBase httpContext, string uniqueId) 
        { 
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            this.UniqueId = uniqueId;
        }
        public void ValidateMe()
        {

        }
    }
}
