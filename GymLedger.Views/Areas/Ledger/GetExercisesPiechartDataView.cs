using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Models.Models;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetExercisesPiechartDataView
    {
        public List<string> Exercise { get; set; } = new List<string>();
        public List<int> Sessions { get; set; } = new List<int>();
    }
}
