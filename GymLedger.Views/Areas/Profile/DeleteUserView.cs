using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Profile
{
    public class DeleteUserView
    {
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }
    }
}
