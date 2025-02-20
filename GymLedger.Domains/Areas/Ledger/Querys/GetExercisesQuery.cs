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
    public class GetExercisesQuery: IQuery<GetExerciseView>
    {
        public User UserIdentity { get; set; }

        public GetExercisesQuery(HttpContextBase httpContext) 
        {
            this.UserIdentity = GymLedger.Helpers.CookieAuth.AuthCookieHelper.getUserIdentity();
        }
        public void ValidateMe()
        {

        }
    }
}
