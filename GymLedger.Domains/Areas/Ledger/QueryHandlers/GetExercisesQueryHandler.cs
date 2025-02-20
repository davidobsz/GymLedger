using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Helpers.CookieAuth;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetExercisesQueryValidator : IQueryHandler<GetExercisesQuery, GetExerciseView>
    {
        readonly IQueryHandler<GetExercisesQuery, GetExerciseView> decorated;
        GetExercisesQuery Query;

        public GetExercisesQueryValidator(IQueryHandler<GetExercisesQuery, GetExerciseView> decorated, GetExercisesQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetExerciseView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetExercisesQueryHandler : IQueryHandler<GetExercisesQuery, GetExerciseView>
    {
        internal GetExercisesQuery Query;

        public GetExercisesQueryHandler(GetExercisesQuery query)
        {
            Query = query;
        }

        public GetExerciseView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {
                GetExerciseView view = new GetExerciseView();

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);

                view.Exercises = (from e in db.Exercises.AsNoTracking()
                                  where e.UserId == user.Id
                                  select new ExerciseDetailView()
                                  {
                                      Name = e.Name,
                                      DateAdded = e.DateAdded,
                                      UniqueId = e.UniqueId,
                                  }).OrderByDescending(d => d.Name).ToList();

                foreach (var exercise in view.Exercises)
                {
                    exercise.DateAddedAsString = $"{exercise.DateAdded.Value.Day}/{exercise.DateAdded.Value.Month}/{exercise.DateAdded.Value.Year}";
                }
                return view;
            }
        }
    }
}
