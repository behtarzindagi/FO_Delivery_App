using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;
namespace Entity
{
    public class OrderModel
    {
        public string OrderId { set; get; }
        public string OrderName { set; get; }
        public string Village { set; get; }
        public string Block { set; get; }
        public string Distributor { set; get; }
        public int IsPrime { get; set; }

    }


    public class OrderViewModel
    {
        public List<OrderModel> OrderList { set; get; }
    }

    public class Order
    {
        public OrderViewModel Orders { set; get; }

    }

    public class OrderFo
    {
        public OrderViewModel Orders { set; get; }
        public int Status { get; set; }
        public string Msg { get; set; }
    }


    public class OrderTripModel
    {
        public string OrderID { set; get; }
        public string OrderRefNo { set; get; }
        public string Status { set; get; }
        public string Village { set; get; }
        public string VillageToUse { set; get; }
        public string Farmer { set; get; }
        public string CashCollected { set; get; }
        public string Mobile { set; get; }
        public int OrderReprocessFlag { set; get; }
        public int VillageMapflag { set; get; }
    }
    public class DealerOrderTripModel
    {
        public string DealerID { set; get; }
        public string DealerName { set; get; }
        public string DealerAddress { set; get; }
    }

    public class OrderTripViewModel
    {
        public List<OrderTripModel> OrderList { set; get; }
        public List<DealerOrderTripModel> DealerList { set; get; }
    }


    public class OrderDetail
    {
        public string CurrentSeriesNumber { get; set; }
        public List<PodDetail> OrderDetails { get; set; }
    }
}

