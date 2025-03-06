using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetSetsBarchartDataView
    {
        public List<string> Exercise { get; set; } = new List<string>();
        public List<int> Sets { get; set; } = new List<int>();
    }
}
