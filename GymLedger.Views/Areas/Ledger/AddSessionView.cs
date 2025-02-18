using GymLedger.Models;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Views.Areas.Ledger
{
    public class AddSessionView
    {
        public Exercise Exercise { get; set; }
        public List<Set> Sets { get; set; } 
    }
}
