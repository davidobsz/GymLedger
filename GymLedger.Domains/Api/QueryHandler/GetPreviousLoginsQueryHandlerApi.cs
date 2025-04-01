using GymLedger.Data;
using GymLedger.Domains.Api.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.QueryHandler
{
    public class GetPreviousLoginsQueryValidatorApi : IQueryHandler<GetPreviousLoginsQueryApi, PreviousLoginsView>
    {
        readonly IQueryHandler<GetPreviousLoginsQueryApi, PreviousLoginsView> decorated;
        GetPreviousLoginsQueryApi Query;

        public GetPreviousLoginsQueryValidatorApi(IQueryHandler<GetPreviousLoginsQueryApi, PreviousLoginsView> decorated, GetPreviousLoginsQueryApi query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public PreviousLoginsView Get()
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

    public class GetPreviousLoginsQueryHandlerApi : IQueryHandler<GetPreviousLoginsQueryApi, PreviousLoginsView>
    {
        internal GetPreviousLoginsQueryApi Query;

        public GetPreviousLoginsQueryHandlerApi(GetPreviousLoginsQueryApi query)
        {
            Query = query;
        }

        public PreviousLoginsView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var previousLogins = db.PreviousLogins.Where(u => u.UserId == user.Id).OrderByDescending(u => u.LoginDate).ToList().Take(5).ToList();
                PreviousLoginsView view = new PreviousLoginsView();
                List<PreviousLoginDetailView> logins = new List<PreviousLoginDetailView>();



                if (previousLogins.Count > 0)
                {
                    foreach (var item in previousLogins)
                    {
                        PreviousLoginDetailView login = new PreviousLoginDetailView
                        {
                            LoginDate = item.LoginDate,
                        };

                        logins.Add(login);
                    }
                }

                view.Logins = logins;

                return view;
            }
        }
    }
}
