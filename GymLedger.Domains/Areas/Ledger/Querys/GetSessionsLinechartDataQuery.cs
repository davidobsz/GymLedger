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
        public GetDatesReportView DatesReport { get; set; }

        public GetSessionsLinechartDataQuery(HttpContextBase httpContext, GetDatesReportView datesReportView)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            this.DatesReport = datesReportView;
        }

        public void ValidateMe()
        {
        }
    }
}

