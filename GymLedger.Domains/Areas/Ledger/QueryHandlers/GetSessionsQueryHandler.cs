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
using GymLedger.Models;
using System.Web.UI.WebControls;
using GymLedger.Models.Models;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetSessionsQueryValidator : IQueryHandler<GetSessionsQuery, GetSessionView>
    {
        readonly IQueryHandler<GetSessionsQuery, GetSessionView> decorated;
        GetSessionsQuery Query;

        public GetSessionsQueryValidator(IQueryHandler<GetSessionsQuery, GetSessionView> decorated, GetSessionsQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public GetSessionView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetSessionsQueryHandler : IQueryHandler<GetSessionsQuery, GetSessionView>
    {
        internal GetSessionsQuery Query;

        public GetSessionsQueryHandler(GetSessionsQuery query)
        {
            Query = query;
        }

        public GetSessionView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {
                GetSessionView getSessionView = new GetSessionView();

                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var exercises = db.Exercises.Where(e => e.User.Username == this.Query.UserIdentity.Username);

                var sessions = (from s in db.Sessions
                                where s.User.Id == user.Id
                                select new SessionDetailView
                                {
                                    SessionId = s.Id,
                                    Exercise = exercises.Where(e => e.Id == s.ExerciseId).FirstOrDefault().Name,
                                    DateAdded = s.DateAdded,
                                    Date = s.Date,
                                    UniqueId = s.UniqueId,
                                }).ToList();

                // Retrieve sets and map them to their corresponding session
                var sets = db.Sets
                            .Where(s => s.Session.User.Id == user.Id)
                            .Select(s => new
                            {
                                s.SessionId,
                                Set = new SetView
                                {
                                    SessionId = s.SessionId,
                                    Reps = s.Reps,
                                    SetNumber = s.SetNumber,
                                    Weight = s.Weight,
                                    UniqueId = s.UniqueId
                                }
                            }).ToList();

                // Assign sets to their respective sessions
                foreach (var session in sessions)
                {
                    var sets2 = sets.Where(set => set.SessionId == session.SessionId)
                                       .Select(set => set.Set)
                                       .ToList();
                    session.Sets = sets2;
                    session.NumOfSets = sets2.Count;
                    session.DateAddedAsString = $"{session.DateAdded.Value.Day}/{session.DateAdded.Value.Month}/{session.DateAdded.Value.Year}";
                }
                getSessionView.Sessions = sessions;

                return getSessionView;

            }
        }
    }
}
