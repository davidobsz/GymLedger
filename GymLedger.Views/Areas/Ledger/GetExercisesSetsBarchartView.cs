using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetExercisesSetsBarchartView
    {
        public List<GetExercisesSetsBarchartDetailView> GetExercisesSetsBarchartDetailViews { get; set; }
    }

    public class GetExercisesSetsBarchartDetailView
    {
        public int SetNumber { get; set; }
        public int SetProgress { get; set; }
    }
}
