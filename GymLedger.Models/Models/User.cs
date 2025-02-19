using GymLedger.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Models.Models
{
    public class User: BaseModel
    {
        [Required]
        public string Username { get; set; }
        public UserRole UserRole { get; set; }
        [Required]
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
