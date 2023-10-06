using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager
{
    internal class Projection
    {
        public Projection()
        {
        }

        public static decimal Inflation { get; set; }
        public static decimal Interest { get; set; }
        public static Int16 Years { get; set;}
    }
}
