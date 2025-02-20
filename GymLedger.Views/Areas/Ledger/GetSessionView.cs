using GymLedger.Models;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetSessionView
    {
        public GetSessionView()
        {
            Sessions = new List<SessionDetailView>();
        }

        public List<SessionDetailView> Sessions { get; set; }
    }

    public class SessionDetailView
    {
        public int SessionId { get; set; }
        public string Exercise { get; set; }
        public DateTime? DateAdded { get; set; }
        public string DateAddedAsString { get; set; }
        public string UniqueId { get; set; }
        public List<SetView> Sets { get; set; }
        public DateTime? Date { get; set; }
        public int NumOfSets { get; set; }

    }

    public class SetView
    {
        public int SessionId { get; set; }
        public string UniqueId { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public int SetNumber { get; set; }
    }
}
