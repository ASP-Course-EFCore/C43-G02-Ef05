using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    class PartTimeEmployee:Employee
    {
        public decimal HourRate { get; set; }
        public int CountOfHours { get; set; }
    }
}
