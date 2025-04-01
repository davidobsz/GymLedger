using GymLedger.Data;
using GymLedger.Domains.Api.Querys;
using GymLedger.Domains.Areas.Profile.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Profile;
using GymLedger.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.QueryHandler
{
    public class AccountDetailsGetQueryValidatorApi : IQueryHandler<AccountDetailsGetApiQuery, MyProfileApiView>
    {
        readonly IQueryHandler<AccountDetailsGetApiQuery, MyProfileApiView> decorated;
        AccountDetailsGetApiQuery Query;

        public AccountDetailsGetQueryValidatorApi(IQueryHandler<AccountDetailsGetApiQuery, MyProfileApiView> decorated, AccountDetailsGetApiQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public MyProfileApiView Get()
        {
            Query.ValidateMe();
            using (DataContext db = new DataContext())
            {
                var exists = db.Users.Any(u => u.Username == this.Query.UserIdentity.Username);

                if (!exists)
                {
                    throw new Exception("user does not exist");
                }
            }

            var response = this.decorated.Get();

            return response;
        }
    }

    public class AccountDetailsGetQueryHandlerApi : IQueryHandler<AccountDetailsGetApiQuery, MyProfileApiView>
    {
        internal AccountDetailsGetApiQuery Query;

        public AccountDetailsGetQueryHandlerApi(AccountDetailsGetApiQuery query)
        {
            Query = query;
        }

        public MyProfileApiView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var previousLogins = db.PreviousLogins.Where(u => u.UserId == user.Id).OrderByDescending(u => u.LoginDate).ToList().Take(5).ToList();
                MyProfileApiView view = new MyProfileApiView
                {
                    Username = user.Username,
                    DateAdded = user.DateAdded,
                };

                return view;
            }
        }
    }
}
