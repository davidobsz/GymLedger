using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Models;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetOneRepMaxesQueryValidator : IQueryHandler<GetOneRepMaxesQuery, GetOneRepMaxesView>
    {
        readonly IQueryHandler<GetOneRepMaxesQuery, GetOneRepMaxesView> decorated;
        GetOneRepMaxesQuery Query;

        public GetOneRepMaxesQueryValidator(IQueryHandler<GetOneRepMaxesQuery, GetOneRepMaxesView> decorated, GetOneRepMaxesQuery query)
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

    public class GetOneRepMaxesQueryHandler : IQueryHandler<GetOneRepMaxesQuery, GetOneRepMaxesView>
    {
        internal GetOneRepMaxesQuery Query;

        public GetOneRepMaxesQueryHandler(GetOneRepMaxesQuery query)
        {
            Query = query;
        }

        public GetOneRepMaxesView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {
                GetOneRepMaxesView view = new GetOneRepMaxesView();

                if (this.Query.UniqueId != "0000")
                {
                    var user1 = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                    var exercises1 = db.Exercises.Where(e => e.User.Username == this.Query.UserIdentity.Username && e.UniqueId == this.Query.UniqueId).SingleOrDefault();

                    var oneRepMaxes1 = (from s in db.OneRepMaxes
                                     where s.User.Id == user1.Id && s.ExerciseId == exercises1.Id
                                     select new OneRepMaxView
                                     {
                                         Exercise = exercises1.Name,
                                         DateAdded = s.DateAdded,
                                         Date = s.Date,
                                         UniqueId = s.UniqueId,
                                         Weight = s.Weight,
                                         UserId = s.UserId,
                                     }).ToList();

                    return view;
                }

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
