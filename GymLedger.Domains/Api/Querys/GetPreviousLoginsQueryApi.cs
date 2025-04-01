using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.Querys
{
    public class GetPreviousLoginsQueryApi: IQuery<PreviousLoginsView>
    {
        public User UserIdentity { get; set; }

        public GetPreviousLoginsQueryApi(string token) 
        {
            this.UserIdentity = Helpers.JwtAuth.JwtTokenHelper.GetUserFromToken(token);
        }

        public void ValidateMe()
        {

        }
    }
}
