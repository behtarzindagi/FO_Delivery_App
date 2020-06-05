using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DealerScanModel
    {

    }
    public class DS_DeaalerList
    {
        public List<DS_DealerDetail> DealerList { get; set; }
    }
    public class DS_DealerDetail
    {
        public int dealerId { get; set; }
        public string dealerName { get; set; }
        public int acceptRejectFlag { get; set; }
        public List<DS_DealerPackageList> ProdDealerList { set; get; }
    }
    public class DS_DealerPackageList
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Product { get; set; }
        public int Qty { get; set; }
        public List<DS_DealerPackageDetail> Detail { get; set; }
    }
    public class DS_DealerPackageDetail
    {
        public int PackageId { get; set; }
        public int RecordId { get; set; }
        public int DealerId { get; set; }
        public int Qty { get; set; }
    }
        public class DS_ProductDealerViewModel
    {
        public DS_ProductDealerModel DealerData { get; set; }
    }
    public class DS_ProductDealerModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public  string actionTaken { get; set; }
        public int dealerId { get; set; }
        public List<DS_ProductPostDetail> ProductList { get; set; }
    }
    public class DS_ProductPostDetail
    {
        public int RecordId { get; set; }
        public int PackageId { get; set; }
        public int Quantity { get; set; }
    }
    public class DS_ProductAssignModel
    {
        public string apiKey { get; set; }
        public int FoID { get; set; }
        public int NewFoId { get; set; }
        public int HubHeadId { get; set; }
        public List<DS_ProductPostDetail> ProductList { get; set; }
    }
}
