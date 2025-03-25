using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Profile
{
    public class ChangePasswordView
    {
        public string UniqueId { get; set; }

        [Display(Name="Current Password")]
        public string CurrentPassword { get; set; }

        [Display(Name ="New Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
