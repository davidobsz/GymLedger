using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Models.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Linq;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetExerciseQueryValidatorApi : IQueryHandler<GetExerciseQueryApi, ExerciseDetailView>
    {
        private readonly IQueryHandler<GetExerciseQueryApi, ExerciseDetailView> _decorated;
        private readonly GetExerciseQueryApi _query;

        public GetExerciseQueryValidatorApi(IQueryHandler<GetExerciseQueryApi, ExerciseDetailView> decorated, GetExerciseQueryApi query)
        {
            _decorated = decorated;
            _query = query;
        }

        public ExerciseDetailView Get()
        {
            // Validate the query before executing it
            _query.ValidateMe();

            return _decorated.Get();
        }
    }
    public class GetExerciseQueryHandlerApi : IQueryHandler<GetExerciseQueryApi, ExerciseDetailView>
    {
        private readonly GetExerciseQueryApi _query;

        public GetExerciseQueryHandlerApi(GetExerciseQueryApi query)
        {
            _query = query;
        }

        public ExerciseDetailView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                // Get the user from the database based on the username from the token
                var user = db.Users.FirstOrDefault(u => u.Username == _query.UserIdentity.Username);

                var exercise = (from e in db.Exercises.AsNoTracking()
                                  where e.UserId == user.Id && this._query.UniqueId == e.UniqueId
                                  select new ExerciseDetailView()
                                  {
                                      Name = e.Name,
                                      DateAdded = e.DateAdded,
                                      UniqueId = e.UniqueId,
                                  }).SingleOrDefault();

               exercise.DateAddedAsString = $"{exercise.DateAdded.Value.Day}/{exercise.DateAdded.Value.Month}/{exercise.DateAdded.Value.Year}";
                
                return exercise;
            }
        }
    }
}
