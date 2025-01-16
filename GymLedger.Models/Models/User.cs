using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Models.Models
{
    public class User: BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
