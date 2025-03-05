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
    public class GetUniqueExerciseQuery: IQuery<ExerciseDetailView>
    {
        public User UserIdentity { get; set; }
        public string UniqueId { get; set; }
        public GetUniqueExerciseQuery(HttpContextBase httpContext, string UniqueId)
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
            this.UniqueId = UniqueId;
        }
        public void ValidateMe()
        {

        }
    }
}
