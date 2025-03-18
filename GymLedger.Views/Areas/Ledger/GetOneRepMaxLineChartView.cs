using System;
using System.Collections.Generic;

namespace GymLedger.Views.Areas.Ledger
{
    public class GetOneRepMaxLineChartView
    {
        public List<GetOneRepMaxLineChartDetailView> DataPoints { get; set; }
    }

    public class GetOneRepMaxLineChartDetailView
    {
        public DateTime? Date { get; set; }
        public double EstimatedOneRepMax { get; set; }
    }
}
