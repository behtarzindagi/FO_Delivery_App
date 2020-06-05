using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FarmerOrderListForDeliverViewModel
    {
        public List<FarmerOrderListForDeliverModel> OrderList { get; set; }
    }
    public class FarmerOrderListForDeliverModel
    {
        public int orderId { get; set; }
        public string orderRefNo { get; set; }
        public string deliveryDate { get; set; }
        public string mobile { get; set; }
        public decimal orderAmount { get; set; }
        public List<FarmerProductListForDeliverableOrders> orderProductList { get; set; }

    }
    public class FarmerProductListForDeliverableOrders
    {
        public int packageId { get; set; }
        public string packageName { get; set; }
        public string productName { get; set; }
        public int Qty { get; set; }
        public decimal unitPrice { get; set; }
    }
}
