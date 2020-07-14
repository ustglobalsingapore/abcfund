using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class Fund
    {
        public int Id { get; set; }

        [Display(Name = "Stock Id")]
        public int StockId { get; set; }

        [Display(Name = "Benchmark Id")]
        public int BenchmarkId { get; set; }

        [Display(Name = "Manager Id")]
        public int ManagerId { get; set; }

        public decimal Size { get; set; }

        [Display(Name = "Initial Fee")]
        public decimal InitialFee { get; set; }

        public string Scheme { get; set; }

        [Display(Name = "All-In-Fee")]
        public decimal AllInFee { get; set; }

        [Display(Name = "Expense Ratio")]
        public decimal ExpenseRatio { get; set; }

        public decimal NAV { get; set; }

        [Display(Name = "Dividend Frequency")]
        public string DividendFrequency { get; set; }

        [Display(Name = "Inception Date")]
        [DataType(DataType.Date)]
        public DateTime InceptionDate { get; set; }
    }
}
