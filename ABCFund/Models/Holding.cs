using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class Holding
    {
        public int Id { get; set; }

        [Display(Name = "Stock Id")]
        public int StockId { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal Weight { get; set; }

        public string Sector { get; set; }

        public string Industry { get; set; }

        public decimal Shares { get; set; }

        [Display(Name = "Initial Value")]
        public decimal InitialValue { get; set; }

        [Display(Name = "Fund Id")]
        public int FundId { get; set; }

    }
}
