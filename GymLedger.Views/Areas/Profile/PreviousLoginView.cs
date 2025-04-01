using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Profile
{
    public class PreviousLoginsView
    {
        public List<PreviousLoginDetailView> Logins { get; set; }
    }

    public class PreviousLoginDetailView
    {
        public DateTime? LoginDate { get; set; }
    }
}
