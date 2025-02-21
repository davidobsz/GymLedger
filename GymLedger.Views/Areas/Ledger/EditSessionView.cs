using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class EditSessionView
    {
        public string UniqueId { get; set; }
        public string Exercise { get; set; }
        public List<SetView> Sets { get; set; }
        public DateTime? Date { get; set; }
    }
}
