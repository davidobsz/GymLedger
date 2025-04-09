using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Models.Models
{
    public class OneRepMax: BaseModel
    {
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public string UniqueId { get; set; }
        public DateTime? Date { get; set; }
        public User User { get; set; }
        public float Weight { get; set; }
    }
}
