using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.Querys
{
    public class GetExercisesPiechartDataQuery : IQuery<GetExercisesPiechartDataView>
    {
        public User UserIdentity { get; set; }

        public GetExercisesPiechartDataQuery(HttpContextBase httpContext)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }
        public void ValidateMe()
        {

        }
    }
}
