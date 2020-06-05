using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ReprocessItem
    {

        public int PackageId { get; set; }
       // public int RecordId { get; set; }
        public string PackageName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ToatlPrice { get; set; }
        public int Qty { get; set; }
        public string Company { get; set; }

    }

    public class ReprocessItemsListViewModel
    {
        public List<ReprocessItem> ProdctList { get; set; }
    }

    public class ReprocessProductPostDetail
    {
        //public int RecordId { get; set; }
        public int PackageId { get; set; }
        public int Quantity { get; set; }
    }

    public class ReprocessOrderCreateModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public int tripid { get; set; }
        public string DeliveryDate { get; set; }
        public string ModeOfPayment { get; set; }
        public FarmerDetailModel Farmer { set; get; }
        public List<ReprocessProductPostDetail> Product { set; get; }
    }

    public class GetOrderReprocessDetailModel
    {
        public int OrderId { get; set; }
        public string OrderRefNo { get; set; }
        public decimal Amount { get; set; }
        public string FarmerName { get; set; }
        public long Mobile { get; set; }
        public string Vilage { get; set; }
    }

}
