using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Exchange { get; set; }

        public string Sector { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }
    }
}
