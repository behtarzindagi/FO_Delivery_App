using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HubHeadGetTodayOrderStatusByOrderIdViewModel
    {
        public HubHeadGetTodayOrderStatusByOrderIdModel OrderDetail { set; get; }
    }
    public class HubHeadGetTodayOrderStatusByOrderIdModel
    {

        public string DeliverTo { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cash { get; set; }
        public decimal PayU { get; set; }
        public decimal POS { get; set; }
        public decimal UPI { get; set; }
        public List<HubHeadGetTodayOrderStatusByOrderId> ProductList { get; set; }
    }

    public class HubHeadGetTodayOrderStatusByOrderId
    {
        public string DealerName { get; set; }
        public string PickupTime { get; set; }
        public string InvoiceNo { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int OriginalQty { get; set; }
        public string Packaging { get; set; }
        public string Reason { get; set; }
    }
}
