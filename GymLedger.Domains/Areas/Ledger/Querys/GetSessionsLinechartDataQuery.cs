using GymLedger.Domains.BaseQuerys;
using GymLedger.Models;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Querys
{
    public class GetSessionsLinechartDataQuery : IQuery<GetSessionsLinechartDataView>
    {
        public User UserIdentity { get; set; }

        public GetSessionsLinechartDataQuery(HttpContextBase httpContext)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }
        public void ValidateMe()
        {

        }
    }
}
