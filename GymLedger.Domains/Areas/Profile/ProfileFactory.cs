using GymLedger.Domains.Areas.Ledger.QueryHandlers;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.Areas.Profile.QueryHandlers;
using GymLedger.Domains.Areas.Profile.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using GymLedger.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Profile
{
    public static partial class ProfileFactory
    {
        #region Querys

        public static IQueryHandler<AccountDetailsGetQuery, MyProfileView> AccountDetailsGetQueryHandler(AccountDetailsGetQuery query) 
        {
            return new AccountDetailsGetQueryValidator(new  AccountDetailsGetQueryHandler(query), query);
        }

        #endregion
    }
}
