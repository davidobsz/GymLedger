using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.Areas.Profile.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using GymLedger.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Profile.QueryHandlers
{
    public class AccountDetailsGetQueryValidator : IQueryHandler<AccountDetailsGetQuery, MyProfileView>
    {
        readonly IQueryHandler<AccountDetailsGetQuery, MyProfileView> decorated;
        AccountDetailsGetQuery Query;

        public AccountDetailsGetQueryValidator(IQueryHandler<AccountDetailsGetQuery, MyProfileView> decorated, AccountDetailsGetQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public MyProfileView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class AccountDetailsGetQueryHandler : IQueryHandler<AccountDetailsGetQuery, MyProfileView>
    {
        internal AccountDetailsGetQuery Query;

        public AccountDetailsGetQueryHandler(AccountDetailsGetQuery query)
        {
            Query = query;
        }

        public MyProfileView Get()
        { 
            using (Data.DataContext db = new Data.DataContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                MyProfileView view = new MyProfileView
                {
                    Username = user.Username,
                    DateAdded = user.DateAdded,
                };

                return view;
            }
        }
    }
}
