using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetVolumePerSessionQueryValidator : IQueryHandler<GetVolumePerSessionQuery, GetVolumePerSessionView>
    {
        readonly IQueryHandler<GetVolumePerSessionQuery, GetVolumePerSessionView> decorated;
        GetVolumePerSessionQuery Query;

        public GetVolumePerSessionQueryValidator(IQueryHandler<GetVolumePerSessionQuery, GetVolumePerSessionView> decorated, GetVolumePerSessionQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetVolumePerSessionView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            if (string.IsNullOrEmpty(this.Query.UniqueId))
            {
                throw new Exception("uniqueId not found for the volume per session chart");
            }

            return response;
        }
    }

    public class GetVolumePerSessionQueryHandler : IQueryHandler<GetVolumePerSessionQuery, GetVolumePerSessionView>
    {
        private readonly GetVolumePerSessionQuery Query;

        public GetVolumePerSessionQueryHandler(GetVolumePerSessionQuery query)
        {
            Query = query;
        }

        public GetVolumePerSessionView Get()
        {
            using (var db = new Data.DataContext())
            {
                var view = new GetVolumePerSessionView { DataPoints = new List<GetVolumePerSessionDetailView>() };

                var user = db.Users.FirstOrDefault(u => u.Username == Query.UserIdentity.Username);
                if (user == null) return view;

                var exercise = db.Exercises.FirstOrDefault(e => e.UniqueId == Query.UniqueId);
                if (exercise == null) return view;

                var sessions = db.Sessions
                                .Where(s => s.ExerciseId == exercise.Id)
                                .Include(s => s.Sets)
                                .OrderByDescending(s => s.Date)
                                .Take(10)
                                .ToList()
                                .OrderBy(s => s.Date) // Reorder chronologically
                                .ToList();

                if (Query.DatesReport?.StartDate.HasValue == true)
                {
                    sessions = sessions.Where(s => s.Date >= Query.DatesReport.StartDate.Value).ToList();
                }
                if (Query.DatesReport?.EndDate.HasValue == true)
                {
                    sessions = sessions.Where(s => s.Date <= Query.DatesReport.EndDate.Value).ToList();
                }

                var volumeData = sessions
                    .Select(session =>
                    {
                        double totalVolume = session.Sets.Sum(s => s.Weight * s.Reps); // Calculate total volume for the session

                        return new GetVolumePerSessionDetailView
                        {
                            Date = session.Date,
                            TotalVolume = totalVolume
                        };
                    })
                    .Where(data => data != null)
                    .ToList();

                view.DataPoints = volumeData;
                return view;
            }
        }
    }
}
