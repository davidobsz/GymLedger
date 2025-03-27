using GymLedger.Domains.Api.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Models;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.QueryHandler
{
    public class GetSessionQueryValidatorApi : IQueryHandler<GetSessionQueryApi, SessionDetailView>
    {
        readonly IQueryHandler<GetSessionQueryApi, SessionDetailView> decorated;
        GetSessionQueryApi Query;

        public GetSessionQueryValidatorApi(IQueryHandler<GetSessionQueryApi, SessionDetailView> decorated, GetSessionQueryApi query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public SessionDetailView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetSessionQueryHandlerApi : IQueryHandler<GetSessionQueryApi, SessionDetailView>
    {
        internal GetSessionQueryApi Query;

        public GetSessionQueryHandlerApi(GetSessionQueryApi query)
        {
            Query = query;
        }

        public SessionDetailView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {

                var user = db.Users.SingleOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var exercises = db.Exercises.Where(e => e.User.Username == user.Username);

                var session = (from s in db.Sessions
                                where s.User.Id == user.Id && s.UniqueId == this.Query.UniqueId
                                select new SessionDetailView
                                {
                                    SessionId = s.Id,
                                    Exercise = exercises.Where(e => e.Id == s.ExerciseId).FirstOrDefault().Name,
                                    DateAdded = s.DateAdded,
                                    Date = s.Date,
                                    UniqueId = s.UniqueId,
                                    UserId = user.Id
                                }).SingleOrDefault();
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

                var sets2 = sets.Where(set => set.SessionId == session.SessionId)
                                       .Select(set => set.Set)
                                       .ToList();
                session.Sets = sets2;

                return session;

            }
        }
    }
}
