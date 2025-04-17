using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class OneRepMaxApiView
    {
        public string ExerciseName { get; set; }
        public float Weight { get; set; }
        public DateTime? Date { get; set; }
    }
}
