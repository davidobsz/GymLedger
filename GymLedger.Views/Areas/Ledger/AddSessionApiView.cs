using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymLedger.Models.Models;

namespace GymLedger.Views.Areas.Ledger
{
    public class AddSessionApiView
    {
        public string ExerciseName { get; set; }  // Selected Exercise
        public DateTime? Date { get; set; }
        public List<SetViewApiModel> Sets { get; set; }
    }

    public class SetViewApiModel
    {
        public int SetNumber { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
    }
}
