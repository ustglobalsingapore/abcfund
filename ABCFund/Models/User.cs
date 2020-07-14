using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCFund.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Names { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
