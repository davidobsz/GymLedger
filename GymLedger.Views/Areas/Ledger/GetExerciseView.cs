using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetExerciseView
    {
        public GetExerciseView() 
        {
            Exercises = new List<ExerciseDetailView>();
        }

        public List<ExerciseDetailView> Exercises { get; set; }
    }

    public class ExerciseDetailView
    {
        public string Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public string DateAddedAsString { get; set; }
        public string UniqueId { get; set; }

    }
}
