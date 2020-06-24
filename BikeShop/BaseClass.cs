using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop
{
    public class BaseClass
    {
        public int Money { get; set; }
        public string Car { get; set; }
        public void Spend()
        {
            Debug.WriteLine("Do something");
        }
    }
}
