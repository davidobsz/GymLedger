using GymLedger.Domains.Api.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.QueryHandler
{
    public class GetOneRepMaxQueryValidatorApi : IQueryHandler<GetOneRepMaxQueryApi, OneRepMaxApiView>
    {
        readonly IQueryHandler<GetOneRepMaxQueryApi, OneRepMaxApiView> decorated;
        GetOneRepMaxQueryApi Query;

        public GetOneRepMaxQueryValidatorApi(IQueryHandler<GetOneRepMaxQueryApi, OneRepMaxApiView> decorated, GetOneRepMaxQueryApi query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public OneRepMaxApiView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetOneRepMaxQueryHandlerApi : IQueryHandler<GetOneRepMaxQueryApi, OneRepMaxApiView>
    {
        internal GetOneRepMaxQueryApi Query;

        public GetOneRepMaxQueryHandlerApi(GetOneRepMaxQueryApi query)
        {
            Query = query;
        }

        public OneRepMaxApiView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                OneRepMaxApiView view = new OneRepMaxApiView();

                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var exercises = db.Exercises.Where(e => e.User.Username == this.Query.UserIdentity.Username);

                var oneRepMax = (from s in db.OneRepMaxes
                                 where s.User.Id == user.Id && s.UniqueId == this.Query.UniqueId
                                 select new OneRepMaxApiView
                                 {
                                     ExerciseName = exercises.Where(e => e.Id == s.ExerciseId).FirstOrDefault().Name,
                                     Date = s.Date,
                                     Weight = s.Weight,
                                 }).SingleOrDefault();

                return oneRepMax;
            }
        }
    }
}
