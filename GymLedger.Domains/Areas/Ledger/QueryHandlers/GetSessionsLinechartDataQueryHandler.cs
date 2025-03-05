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
    public class GetSessionsLinechartDataQueryValidator: IQueryHandler<GetSessionsLinechartDataQuery, GetSessionsLinechartDataView>
    {
        readonly IQueryHandler<GetSessionsLinechartDataQuery, GetSessionsLinechartDataView> decorated;
        GetSessionsLinechartDataQuery Query;

        public GetSessionsLinechartDataQueryValidator(IQueryHandler<GetSessionsLinechartDataQuery, GetSessionsLinechartDataView> decorated, GetSessionsLinechartDataQuery query)
        {
            this.decorated = decorated;
            Query = query;
        }

        public GetSessionsLinechartDataView Get()
        {
            Query.ValidateMe();
            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetSessionsLinechartDataQueryHandler : IQueryHandler<GetSessionsLinechartDataQuery, GetSessionsLinechartDataView>
    {
        internal GetSessionsLinechartDataQuery Query;

        public GetSessionsLinechartDataQueryHandler(GetSessionsLinechartDataQuery query)
        {
            Query = query;
        }

        public GetSessionsLinechartDataView Get()
        {
            using (var db = new Data.DataContext())
            {
                var view = new GetSessionsLinechartDataView
                {
                    Exercises = new List<string>(),
                    Progress = new List<float>()
                };

                var user = db.Users.FirstOrDefault(u => u.Username == Query.UserIdentity.Username);
                if (user == null) return view;

                var exercises = db.Exercises.Where(e => e.User.Id == user.Id).ToList();
                var sessions = db.Sessions
                                .Where(s => s.UserId == user.Id)
                                .Include(s => s.Sets)
                                .ToList();

                // Apply StartDate and EndDate filtering if provided
                if (Query.DatesReport != null)
                {
                    if (Query.DatesReport.StartDate.HasValue)
                    {
                        sessions = sessions.Where(s => s.Date >= Query.DatesReport.StartDate.Value).ToList();
                    }
                    if (Query.DatesReport.EndDate.HasValue)
                    {
                        sessions = sessions.Where(s => s.Date <= Query.DatesReport.EndDate.Value).ToList();
                    }
                }

                foreach (var exercise in exercises)
                {
                    var exerciseSessions = sessions.Where(s => s.ExerciseId == exercise.Id).ToList();

                    if (exerciseSessions.Count() > 1)
                    {
                        view.Exercises.Add(exercise.Name);

                        var firstSession = exerciseSessions.FirstOrDefault()?.Sets.Sum(s => s.Reps * s.Weight) ?? 0;
                        var lastSession = exerciseSessions.LastOrDefault()?.Sets.Sum(s => s.Reps * s.Weight) ?? 0;

                        if (firstSession + lastSession > 0)
                        {
                            var progress = ((float)lastSession / (float)(firstSession + lastSession)) * 100;
                            view.Progress.Add(progress);
                        }
                        else
                        {
                            view.Progress.Add(0);
                        }
                    }
                }

                return view;
            }
        }
    }
}
