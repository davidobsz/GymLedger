using GymLedger.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Models.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Username { get; set; }
        public UserRole UserRole { get; set; }
        [Required]
        public string Password { get; set; }
        public string UniqueId { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<Session> Sessions { get; set; }

        public int FailedLoginAttempts { get; set; }
        public DateTime? LockedOutDate { get; set; }
        public bool IsLockedOut { get; set; }

        public ICollection<PreviousLogin> PreviousLogins { get; set; }

        public ICollection<PreviousPassword> PreviousPasswords { get; set; }

        public ICollection<OneRepMax> OneRepMaxes { get; set; }
    }
}
