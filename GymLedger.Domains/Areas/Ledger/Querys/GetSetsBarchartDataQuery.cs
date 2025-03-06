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
    public class GetSetsBarchartDataQuery : IQuery<GetSetsBarchartDataView>
    {
        public User UserIdentity { get; set; }
        public GetDatesReportView DatesReport { get; set; }

        public GetSetsBarchartDataQuery(HttpContextBase httpContext, GetDatesReportView datesReport)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            DatesReport = datesReport;
        }
        public void ValidateMe()
        {

        }
    }
}
