using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrderDistributorModel
    {
        public string OrderID { set; get; }
        public string OrderRefNo { set; get; }
        public string InvoiceNo { set; get; }
        public string OrderStatus { set; get; }
        public string FarmerAddress { set; get; }
        public string Amount { set; get; }

    }
    public class OrderDistributorViewModel
    {
        public List<OrderDistributorModel> OrderList { set; get; }
    }
    public class OrderDistributor
    {
        public string DistributorId { set; get; }
        public string DistributorName { set; get; }
        public string DistributorAddress { set; get; }
        public OrderDistributorViewModel Orders { set; get; }
        public int IsPrime { get; set; }
        public int IsAppAccess { get; set; }
    }
    public class DistributorViewModel
    {
        public List<OrderDistributor> DistributorList { set; get; }
    }
    public class Distributor
    {
        public DistributorViewModel Distributors { set; get; }
    }
}
