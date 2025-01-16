using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Models.Models
{
    public class Exercise : BaseModel
    {
        public string UniqueId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}
