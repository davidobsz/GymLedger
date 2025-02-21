using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.BaseQuerys;
using System.Web;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;

namespace GymLedger.Domains.Areas.Ledger.Querys
{
    public class GetSessionDetailQuery : IQuery<SessionDetailView>
    {
        public User UserIdentity { get; set; }
        public string UniqueId { get; set; }

        public GetSessionDetailQuery(HttpContextBase httpContext, string uniqueId)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            this.UniqueId = uniqueId;
        }
        public void ValidateMe()
        {

        }
    }
}
