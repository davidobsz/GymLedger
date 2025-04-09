using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class AddOneRepMaxView
    {
        public string ExerciseName { get; set; }  // Selected Exercise
        public DateTime? Date { get; set; }
        public List<Exercise> Exercises { get; set; }  // List of Exercises for dropdown
        public float Weight { get; set; }
    }
}
