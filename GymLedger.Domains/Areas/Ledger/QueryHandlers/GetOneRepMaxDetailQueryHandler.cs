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
using GymLedger.Models.Models;

namespace GymLedger.Domains.Areas.Ledger.QueryHandlers
{
    public class GetOneRepMaxDetailQueryValidator : IQueryHandler<GetOneRepMaxDetailQuery, OneRepMaxView>
    {
        readonly IQueryHandler<GetOneRepMaxDetailQuery, OneRepMaxView> decorated;
        GetOneRepMaxDetailQuery Query;

        public GetOneRepMaxDetailQueryValidator(IQueryHandler<GetOneRepMaxDetailQuery, OneRepMaxView> decorated, GetOneRepMaxDetailQuery query)
        {
            this.decorated = decorated;
            this.Query = query;
        }

        public OneRepMaxView Get()
        {
            Query.ValidateMe();

            var response = this.decorated.Get();

            return response;
        }
    }

    public class GetOneRepMaxDetailQueryHandler : IQueryHandler<GetOneRepMaxDetailQuery, OneRepMaxView>
    {
        internal GetOneRepMaxDetailQuery Query;

        public GetOneRepMaxDetailQueryHandler(GetOneRepMaxDetailQuery query)
        {
            Query = query;
        }

        public OneRepMaxView Get()
        {

            using (Data.DataContext db = new Data.DataContext())
            {

                var user = db.Users.FirstOrDefault(u => u.Username == this.Query.UserIdentity.Username);
                var orm = db.OneRepMaxes.Where(e => e.UserId == user.Id && e.UniqueId == this.Query.UniqueId).SingleOrDefault();

                OneRepMaxView view = new OneRepMaxView
                {
                    Weight = orm.Weight,
                    Date = orm.Date,
                    UniqueId = orm.UniqueId,
                    Exercise = db.Exercises.SingleOrDefault(e => e.Id == orm.ExerciseId).Name
                };

                view.DateAsString = $"{view.Date.Value.Day}/{view.Date.Value.Month}/{view.Date.Value.Year}";

                return view;
            }
        }
    }
}
