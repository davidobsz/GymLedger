using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetOneRepMaxesView
    {
        public GetOneRepMaxesView()
        {
            OneRepMaxes = new List<OneRepMaxView>();
        }

        public List<OneRepMaxView> OneRepMaxes { get; set; }
    }

    public class OneRepMaxView
    {
        public string Exercise { get; set; }

        public DateTime? DateAdded { get; set; }
        public string DateAddedAsString { get; set; }
        public string DateAsString { get; set; }
        public string UniqueId { get; set; }
        public DateTime? Date { get; set; }
        public float Weight { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserId { get; set; }
    }
}
