using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class TripSheetModel
    {
        public int OrderNo { get; set; }
        public string Product { get; set; }
        public string Weight { get; set; }
        public string Farmer { get; set; }
        public string Block { get; set; }
        public string Village { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public string InvoicePODStatus { get; set; }
    }
}
