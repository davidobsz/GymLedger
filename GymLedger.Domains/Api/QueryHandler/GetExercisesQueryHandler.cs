using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Linq;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetExercisesQueryValidatorApi : IQueryHandler<GetExercisesQueryApi, GetExerciseView>
    {
        private readonly IQueryHandler<GetExercisesQueryApi, GetExerciseView> _decorated;
        private readonly GetExercisesQueryApi _query;

        public GetExercisesQueryValidatorApi(IQueryHandler<GetExercisesQueryApi, GetExerciseView> decorated, GetExercisesQueryApi query)
        {
            _decorated = decorated;
            _query = query;
        }

        public GetExerciseView Get()
        {
            // Validate the query before executing it
            _query.ValidateMe();

            return _decorated.Get();
        }
    }
    public class GetExercisesQueryHandlerApi : IQueryHandler<GetExercisesQueryApi, GetExerciseView>
    {
        private readonly GetExercisesQueryApi _query;

        public GetExercisesQueryHandlerApi(GetExercisesQueryApi query)
        {
            _query = query;
        }

        public GetExerciseView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                GetExerciseView view = new GetExerciseView();

                // Get the user from the database based on the username from the token
                var user = db.Users.FirstOrDefault(u => u.Username == _query.UserIdentity.Username);

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
