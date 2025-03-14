using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;//Mapping as not allowing null into DB - But in APP it allow null because it's of type "string" reference type.
        public int? Age { get; set; }
        public string? Address { get; set; }
    }
}
