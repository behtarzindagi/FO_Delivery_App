using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrderReturItemModel
    {
        public string ProductId { set; get; }
        public string ProductName { set; get; }
        public string PackageId { set; get; }
        public string PackageName { set; get; }
        public string RecordId { set; get; }
        public string Qty { get; set; }
        public string ReturnedToDealer { set; get; }
    }
    public class OrderReturnModel
    {
        public string OrderId { set; get; }
        public string OrderRef { set; get; }
        public string OTP { set; get; }
        public string Invoice_No { set; get; }
        public List<OrderReturItemModel> Item { get; set; }
    }


    public class OrderReturnViewModel
    {
        public string DistributorId { set; get; }
        public string DistributorName { set; get; }
       // public string OTP { set; get; }
        public List<OrderReturnModel> OrderList { set; get; }
    }

    public class OrderReturn
    {
        public List<OrderReturnViewModel> DisOrders { set; get; }
    }
}
