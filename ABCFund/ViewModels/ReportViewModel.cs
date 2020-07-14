using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class ReportViewModel
    {
        public Fund PrestigeFund { get; set; }
        public Fund GlobalFund { get; set; }
        public List<Holding> PrestigeHoldings { get; set; }
        public List<Holding> GlobalHoldings { get; set; }
        public Periodic PrestigePeriodic { get; set; }
        public Periodic StiPeriodic { get; set; }
        public Periodic GlobalPeriodic { get; set; }
        public Periodic DjiPeriodic { get; set; }
        public Periodic NasaqPeriodic { get; set; }
    }
}
