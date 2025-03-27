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
    public class GetExercisesQueryApi : IQuery<GetExerciseView>
    {
        public User UserIdentity { get; set; }

        public GetExercisesQueryApi(string token)
        {
            // Decode and validate the JWT token to get user info
            this.UserIdentity = GymLedger.Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }

        public void ValidateMe()
        {
            // Add any validation logic if needed
        }
    }
}
