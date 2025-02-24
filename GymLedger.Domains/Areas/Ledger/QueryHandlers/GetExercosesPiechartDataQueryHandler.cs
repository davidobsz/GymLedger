using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetExercisesPiechartDataQueryValidator : IQueryHandler<GetExercisesPiechartDataQuery, GetExercisesPiechartDataView>
    {
        readonly IQueryHandler<GetExercisesPiechartDataQuery, GetExercisesPiechartDataView> decorated;
        GetExercisesPiechartDataQuery Query;

        public GetExercisesPiechartDataQueryValidator(IQueryHandler<GetExercisesPiechartDataQuery, GetExercisesPiechartDataView> decorated, GetExercisesPiechartDataQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetExercisesPiechartDataView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetExercisesPiechartDataQueryHandler : IQueryHandler<GetExercisesPiechartDataQuery, GetExercisesPiechartDataView>
    {
        internal GetExercisesPiechartDataQuery Query;

        public GetExercisesPiechartDataQueryHandler(GetExercisesPiechartDataQuery query)
        {
            Query = query;
        }

        public GetExercisesPiechartDataView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {
                GetExercisesPiechartDataView view = new GetExercisesPiechartDataView();

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);

                var exercises = db.Exercises.Where(e => e.User.Id == user.Id).ToList();
                
                foreach (var exercise in exercises)
                {
                    view.Exercise.Add(exercise.Name.ToString());
                    view.Sessions.Add(db.Sessions.Where(e => e.ExerciseId == exercise.Id && e.UserId == user.Id).Count());
                }
                

                return view;
            }
        }
    }
}
