using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetOneRepMaxLineChartQueryValidator : IQueryHandler<GetOneRepMaxLineChartQuery, GetOneRepMaxLineChartView>
    {
        readonly IQueryHandler<GetOneRepMaxLineChartQuery, GetOneRepMaxLineChartView> decorated;
        GetOneRepMaxLineChartQuery Query;

        public GetOneRepMaxLineChartQueryValidator(IQueryHandler<GetOneRepMaxLineChartQuery, GetOneRepMaxLineChartView> decorated, GetOneRepMaxLineChartQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetOneRepMaxLineChartView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            if (string.IsNullOrEmpty(this.Query.UniqueId))
            {
                throw new Exception("uniqueId not found for the sets barchart");
            }

            return response;
        }
    }
    public class GetOneRepMaxLineChartQueryHandler : IQueryHandler<GetOneRepMaxLineChartQuery, GetOneRepMaxLineChartView>
    {
        private readonly GetOneRepMaxLineChartQuery Query;

        public GetOneRepMaxLineChartQueryHandler(GetOneRepMaxLineChartQuery query)
        {
            Query = query;
        }

        public GetOneRepMaxLineChartView Get()
        {
            using (var db = new Data.DataContext())
            {
                var view = new GetOneRepMaxLineChartView { DataPoints = new List<GetOneRepMaxLineChartDetailView>() };

                var user = db.Users.FirstOrDefault(u => u.Username == Query.UserIdentity.Username);
                if (user == null) return view;

                var exercise = db.Exercises.FirstOrDefault(e => e.UniqueId == Query.UniqueId);
                if (exercise == null) return view;

                var sessions = db.Sessions
                                .Where(s => s.ExerciseId == exercise.Id)
                                .Include(s => s.Sets)
                                .OrderBy(s => s.Date)
                                .ToList();

                if (Query.DatesReport?.StartDate.HasValue == true)
                {
                    sessions = sessions.Where(s => s.Date >= Query.DatesReport.StartDate.Value).ToList();
                }
                if (Query.DatesReport?.EndDate.HasValue == true)
                {
                    sessions = sessions.Where(s => s.Date <= Query.DatesReport.EndDate.Value).ToList();
                }

                var oneRepMaxData = sessions
                    .Select(session =>
                    {
                        var maxSet = session.Sets.OrderByDescending(s => s.Weight).FirstOrDefault();
                        if (maxSet == null) return null;

                        double estimatedOneRepMax = maxSet.Weight * (1 + (maxSet.Reps / 30.0));

                        return new GetOneRepMaxLineChartDetailView
                        {
                            Date = session.Date,
                            EstimatedOneRepMax = estimatedOneRepMax
                        };
                    })
                    .Where(data => data != null)
                    .ToList();

                view.DataPoints = oneRepMaxData;
                return view;
            }
        }
    }
}
