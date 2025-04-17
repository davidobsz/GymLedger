using GymLedger.Domains.Api.Querys;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.QueryHandler
{
    public class GetOneRepMaxesQueryValidatorApi : IQueryHandler<GetOneRepMaxesQueryApi, GetOneRepMaxesView>
    {
        readonly IQueryHandler<GetOneRepMaxesQueryApi, GetOneRepMaxesView> decorated;
        GetOneRepMaxesQueryApi Query;

        public GetOneRepMaxesQueryValidatorApi(IQueryHandler<GetOneRepMaxesQueryApi, GetOneRepMaxesView> decorated, GetOneRepMaxesQueryApi query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetOneRepMaxesView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetOneRepMaxesQueryHandlerApi : IQueryHandler<GetOneRepMaxesQueryApi, GetOneRepMaxesView>
    {
        internal GetOneRepMaxesQueryApi Query;

        public GetOneRepMaxesQueryHandlerApi(GetOneRepMaxesQueryApi query)
        {
            Query = query;
        }

        public GetOneRepMaxesView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                GetOneRepMaxesView view = new GetOneRepMaxesView();

                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var exercises = db.Exercises.Where(e => e.User.Username == this.Query.UserIdentity.Username);

                var oneRepMaxes = (from s in db.OneRepMaxes
                                   where s.User.Id == user.Id
                                   select new OneRepMaxView
                                   {
                                       Exercise = exercises.Where(e => e.Id == s.ExerciseId).FirstOrDefault().Name,
                                       DateAdded = s.DateAdded,
                                       Date = s.Date,
                                       UniqueId = s.UniqueId,
                                       Weight = s.Weight,
                                   }).ToList();

                foreach (var maxes in oneRepMaxes)
                {
                    maxes.DateAddedAsString = $"{maxes.DateAdded.Value.Day}/{maxes.DateAdded.Value.Month}/{maxes.DateAdded.Value.Year}";
                    maxes.DateAsString = $"{maxes.Date.Value.Day}/{maxes.Date.Value.Month}/{maxes.Date.Value.Year}";
                }

                view.OneRepMaxes = oneRepMaxes;
                return view;
            }
        }
    }
}
