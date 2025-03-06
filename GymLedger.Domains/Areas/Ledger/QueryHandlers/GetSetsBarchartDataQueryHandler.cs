using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetSetsBarchartDataQueryValidator : IQueryHandler<GetSetsBarchartDataQuery, GetSetsBarchartDataView>
    {
        readonly IQueryHandler<GetSetsBarchartDataQuery, GetSetsBarchartDataView> decorated;
        GetSetsBarchartDataQuery Query;

        public GetSetsBarchartDataQueryValidator(IQueryHandler<GetSetsBarchartDataQuery, GetSetsBarchartDataView> decorated, GetSetsBarchartDataQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetSetsBarchartDataView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetSetsBarchartDataQueryHandler : IQueryHandler<GetSetsBarchartDataQuery, GetSetsBarchartDataView>
    {
        internal GetSetsBarchartDataQuery Query;

        public GetSetsBarchartDataQueryHandler(GetSetsBarchartDataQuery query)
        {
            Query = query;
        }

        public GetSetsBarchartDataView Get()
        {
            using (Data.DataContext db = new Data.DataContext())
            {
                GetSetsBarchartDataView view = new GetSetsBarchartDataView();

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);

                if (user == null) return view;

                // Get the dates from the query and filter the exercises by the date range if provided
                DateTime? startDate = Query.DatesReport?.StartDate;
                DateTime? endDate = Query.DatesReport?.EndDate;

                var exercises = db.Exercises.Where(e => e.User.Id == user.Id).ToList();

                foreach (var exercise in exercises)
                {
                    var setsQuery = db.Sessions.Where(e => e.ExerciseId == exercise.Id && e.UserId == user.Id).Include(s => s.Sets);

                    // Apply the date filter if startDate and endDate are provided
                    if (startDate.HasValue)
                    {
                        setsQuery = setsQuery.Where(s => s.Date >= startDate.Value);
                    }
                    if (endDate.HasValue)
                    {
                        setsQuery = setsQuery.Where(s => s.Date <= endDate.Value);
                    }
                    int totalSets = setsQuery.SelectMany(s => s.Sets).Count();

                    view.Exercise.Add(exercise.Name.ToString());
                    view.Sets.Add(totalSets);
                }

                return view;
            }
        }
    }
}
