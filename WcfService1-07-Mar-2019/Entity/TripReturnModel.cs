using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TripReturItemModel
    {
        public string ProductId { set; get; }
        public string ProductName { set; get; }
        public string PackageId { set; get; }
        public string PackageName { set; get; }
        public string RecordId { set; get; }
        public string Qty { get; set; }
        public string ReturnedToDealer { set; get; }
        
    }
    public class TripReturnModel
    {
        public string OrderId { set; get; }
        public string OrderRef { set; get; }
        public string Invoice_No { set; get; }
        public string CancelDate { set; get; }
        public List<TripReturItemModel> Item { get; set; }
    }
    public class TripReturnViewModel
    {
        public string DistributorId { set; get; }
        public string DistributorName { set; get; }
        public List<TripReturnModel> OrderList { set; get; }
    }
    public class TripReturn
    {
        public List<TripReturnViewModel> DisOrders { set; get; }
    }
}
