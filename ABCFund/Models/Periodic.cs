using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class Periodic
    {
        public int Id { get; set; }
        public int StockId { get; set; }

        [Display(Name = "Trade Date")]
        [DataType(DataType.Date)]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Daily")]
        public decimal OneDay { get; set; }

        [Display(Name = "1 Month")]
        public decimal OneMonth { get; set; }

        [Display(Name = "3 Months")]
        public decimal ThreeMonth { get; set; }

        [Display(Name = "6 Months")]
        public decimal SixMonth { get; set; }

        [Display(Name = "One Year")]
        public decimal OneYear { get; set; }

        [Display(Name = "Year To Date")]
        public decimal YearToDate { get; set; }

        [Display(Name = "Since Inception")]
        public decimal SinceInception { get; set; }

        [Display(Name = "Annualised Returns")]
        public decimal AnnualisedReturns { get; set; }
    }
}
