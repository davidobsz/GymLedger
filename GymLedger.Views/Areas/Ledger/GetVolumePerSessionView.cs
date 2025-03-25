using System.Collections.Generic;
using System;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetVolumePerSessionView
    {
        public List<GetVolumePerSessionDetailView> DataPoints { get; set; }
    }

    public class GetVolumePerSessionDetailView
    {
        public DateTime? Date { get; set; }
        public double TotalVolume { get; set; }
    }
}
