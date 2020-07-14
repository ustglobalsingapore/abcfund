using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class Historical
    {
        public int Id { get; set; }

        [Display(Name = "Stock Id")]
        public int StockId { get; set; }

        [Display(Name = "Trade Date")]
        [DataType(DataType.Date)]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Open")]
        public decimal OpenPrice { get; set; }

        [Display(Name = "High")]
        public decimal HighPrice { get; set; }

        [Display(Name = "Low")]
        public decimal LowPrice { get; set; }

        [Display(Name = "Close")]
        public decimal ClosePrice { get; set; }

        [Display(Name = "Adjusted Close")]
        public decimal AdjPrice { get; set; }

        public decimal Volume { get; set; }
    }
}
