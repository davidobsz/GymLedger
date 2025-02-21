using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetSessionDetailQueryValidator : IQueryHandler<GetSessionDetailQuery, SessionDetailView>
    {
        readonly IQueryHandler<GetSessionDetailQuery, SessionDetailView> decorated;
        GetSessionDetailQuery Query;

        public GetSessionDetailQueryValidator(IQueryHandler<GetSessionDetailQuery, SessionDetailView> decorated, GetSessionDetailQuery query)
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

    public class GetSessionDetailQueryHandler : IQueryHandler<GetSessionDetailQuery, SessionDetailView>
    {
        internal GetSessionDetailQuery Query;

        public GetSessionDetailQueryHandler(GetSessionDetailQuery query)
        {
            Query = query;
        }

        public SessionDetailView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);

                var Session = db.Sessions
                        .Where(e => e.UniqueId == this.Query.UniqueId)
                        .Include(s => s.Sets)
                        .AsNoTracking()
                        .SingleOrDefault();

                SessionDetailView view = new SessionDetailView
                {
                    Exercise = db.Exercises.Where(e => e.Id == Session.ExerciseId).FirstOrDefault().Name,
                    SessionId = Session.Id,
                    Date = Session.Date,
                    DateAdded = Session.DateAdded,
                    DateModified = Session.DateModified,
                    UniqueId = Session.UniqueId,

                };
                view.Sets = new List<SetView>();

                // for each set add it to the viewmodel
                foreach (var set in Session.Sets)
                {
                    SetView sv = new SetView
                    {
                        SessionId = set.Id,
                        UniqueId = set.UniqueId,
                        Reps = set.Reps,
                        Weight = set.Weight,
                        SetNumber = set.SetNumber,
                    };
                    view.Sets.Add(sv);
                }


                return view;
            }
        }
    }
}
