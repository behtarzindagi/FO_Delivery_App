using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Today_Order_Delivery_Model
    {
        public int OrderId { set; get; }
        public string OrderRef { set; get; }
        public double Amount { set; get; }
        public int POD { set; get; }
        public string Status { set; get; }
        public string CashAmount { set; get; }
        public string PayUAmount { set; get; }
        public string POSAmount { set; get; }
        public string UPIAmount { set; get; }
    }
    public class Today_Order_Delivery_ViewModel
    {
        public List<Today_Order_Delivery_Model> List { get; set; }
    }
}