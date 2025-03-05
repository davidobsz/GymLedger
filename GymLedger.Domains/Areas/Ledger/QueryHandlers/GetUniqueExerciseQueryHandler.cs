using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetUniqueExerciseQueryValidator : IQueryHandler<GetUniqueExerciseQuery, ExerciseDetailView>
    {
        readonly IQueryHandler<GetUniqueExerciseQuery, ExerciseDetailView> decorated;
        GetUniqueExerciseQuery Query;

        public GetUniqueExerciseQueryValidator(IQueryHandler<GetUniqueExerciseQuery, ExerciseDetailView> decorated, GetUniqueExerciseQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public ExerciseDetailView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetUniqueExerciseQueryHandler : IQueryHandler<GetUniqueExerciseQuery, ExerciseDetailView>
    {
        internal GetUniqueExerciseQuery Query;

        public GetUniqueExerciseQueryHandler(GetUniqueExerciseQuery query)
        {
            Query = query;
        }

        public ExerciseDetailView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);

                var exercise = db.Exercises.AsNoTracking()
                        .Where(e => e.UniqueId == this.Query.UniqueId)
                        .SingleOrDefault();

                return new ExerciseDetailView
                {
                    Name = exercise.Name,
                    DateAdded = exercise.DateAdded,
                    UniqueId = exercise.UniqueId
                };
            }
        }
    }
}
