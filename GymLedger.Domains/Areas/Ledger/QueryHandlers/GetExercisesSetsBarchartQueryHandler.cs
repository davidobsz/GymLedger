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
    public class GetExercisesSetsBarchartQueryValidator : IQueryHandler<GetExercisesSetsBarchartQuery, GetExercisesSetsBarchartView>
    {
        readonly IQueryHandler<GetExercisesSetsBarchartQuery, GetExercisesSetsBarchartView> decorated;
        GetExercisesSetsBarchartQuery Query;

        public GetExercisesSetsBarchartQueryValidator(IQueryHandler<GetExercisesSetsBarchartQuery, GetExercisesSetsBarchartView> decorated, GetExercisesSetsBarchartQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetExercisesSetsBarchartView Get()
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

    public class GetExercisesSetsBarchartQueryHandler : IQueryHandler<GetExercisesSetsBarchartQuery, GetExercisesSetsBarchartView>
    {
        internal GetExercisesSetsBarchartQuery Query;

        public GetExercisesSetsBarchartQueryHandler(GetExercisesSetsBarchartQuery query)
        {
            Query = query;
        }

        public GetExercisesSetsBarchartView Get()
        {
            using (var db = new Data.DataContext())
            {
                var view = new GetExercisesSetsBarchartView
                {
                    GetExercisesSetsBarchartDetailViews = new List<GetExercisesSetsBarchartDetailView>()
                };

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                if (user == null) return view;

                var exercise = db.Exercises.FirstOrDefault(e => e.UniqueId == this.Query.UniqueId);
                if (exercise == null) return view;

                var sessions = db.Sessions
                                .Where(s => s.ExerciseId == exercise.Id)
                                .Include(s => s.Sets)
                                .OrderBy(s => s.Date) // Ensure sessions are in chronological order
                                .ToList();

                // Apply date filtering if datesreport has value
                if (Query.DatesReport?.StartDate.HasValue == true)
                {
                    sessions = sessions.Where(s => s.Date >= Query.DatesReport.StartDate.Value).ToList();
                }
                if (Query.DatesReport?.EndDate.HasValue == true)
                {
                    sessions = sessions.Where(s => s.Date <= Query.DatesReport.EndDate.Value).ToList();
                }

                var sets = sessions.SelectMany(s => s.Sets).ToList();

                var groupedSets = sets
                    .GroupBy(s => s.SetNumber)
                    .Select(g =>
                    {
                        var orderedSets = g.OrderBy(s => s.Session.Date).ToList(); // Sort by session date

                        var firstSessionVolume = orderedSets.FirstOrDefault()?.Reps * orderedSets.FirstOrDefault()?.Weight ?? 0;
                        var lastSessionVolume = orderedSets.LastOrDefault()?.Reps * orderedSets.LastOrDefault()?.Weight ?? 0;

                        float progress = 0;
                        if (firstSessionVolume > 0)
                        {
                            progress = ((float)(lastSessionVolume - firstSessionVolume) / firstSessionVolume) * 100;
                        }

                        return new GetExercisesSetsBarchartDetailView
                        {
                            SetNumber = g.Key,
                            SetProgress = (int)progress // Percentage increase in volume
                        };
                    })
                    .OrderBy(g => g.SetNumber)
                    .ToList();

                view.GetExercisesSetsBarchartDetailViews = groupedSets;

                return view;
            }
        }


    }
}
