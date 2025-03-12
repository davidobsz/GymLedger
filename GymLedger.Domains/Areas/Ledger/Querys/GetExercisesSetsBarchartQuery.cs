using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Querys
{
    public class GetExercisesSetsBarchartQuery : IQuery<GetExercisesSetsBarchartView>
    {
        public User UserIdentity { get; set; }
        public GetDatesReportView DatesReport { get; set; }
        public string UniqueId { get; set; }

        public GetExercisesSetsBarchartQuery(HttpContextBase httpContext, GetDatesReportView datesReport, string uniqueId)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            DatesReport = datesReport;
            this.UniqueId = uniqueId;
        }
        public void ValidateMe()
        {

        }
    }
}
