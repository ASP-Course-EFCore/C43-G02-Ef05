using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstEFCorePowerTool.Models
{
    partial class CustOrderHistResult
    {
        public override string ToString()
        {
            return $"{ProductName} :: {Total}";
        }
    }
}
