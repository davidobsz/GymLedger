using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Models
{
    public class Set : BaseModel
    {
        public string UniqueId { get; set; }
        public int SetNumber { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }

        // Foreign Key
        public int SessionId { get; set; }

        // Navigator Property
        public Session Session { get; set; }
    }
}
