using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Querys
{
    public class GetVolumePerSessionQuery : IQuery<GetVolumePerSessionView>
    {
        public User UserIdentity { get; set; }
        public GetDatesReportView DatesReport { get; set; }
        public string UniqueId { get; set; }

        public GetVolumePerSessionQuery(HttpContextBase httpContext, GetDatesReportView datesReport, string uniqueId)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            DatesReport = datesReport;
            this.UniqueId = uniqueId;
        }

        public void ValidateMe()
        {
            if (string.IsNullOrEmpty(UniqueId))
                throw new Exception("UniqueId is required for the Volume chart.");
        }
    }
}
