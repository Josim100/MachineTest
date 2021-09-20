using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineTest.Models
{
    public class SalesModel
    {
        public string CustomerName { get; set; }
        public string CountryCode { get; set; }
        public string RegionCode { get; set; }
        public int CityCode { get; set; }
        public DateTime DateofSale { get; set;}
        public int ProductID { get; set; }
        public int Quanty { get; set; }

    }
    
}