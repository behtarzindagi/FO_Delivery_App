using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public  class GetPODOrderDetailModel
    {
        public string ProductName { set; get; }
        public int ProductID { get; set; }
        public int couponStatus { get; set; }
        public string Company { get; set; }
        public int CompanyId { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Package { set; get; }
        public string PricePerUnit { set; get; }
        public string TotalPrice { set; get; }
        public string RecordId { set; get; }
        public string Qty { get; set; }
        public string AmountAfterDiscount { get; set; }
        public string DiscAmount { get; set; }
        public string OtherCharges { get; set; }
        public string HSNCode { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string TaxValue { get; set; }
        public string UnitPrice { get; set; }
    
        public string CouponCode { set; get; }
        public int CouponID { set; get; }
        public int DiscType { set; get; }

        public string PackageId { set; get; }

        public int DistrictID { get; set; }
    }

    public class GetPODOrderDetailViewModel
    {
        public string OrderRefNo { set; get; }
        public string DealerName { set; get; }
        public string FarmerId { set; get; }
        public string FarmerName { set; get; }
        public string FatherName { set; get; }
        public string FarmerAddress { set; get; }
        public string StateName { set; get; }
        public int StateId { get; set; }
        public string DistrictName { set; get; }
        public int DistrictId { get; set; }
        public string BlockName { set; get; }
        public string VillageName { set; get; }
        public string NearByVillageName { set; get; }
        public string FarmerContact { set; get; }
        public string GrandTotal { set; get; }
        public string OrderDate { set; get; }
        public string DeliveryDate { set; get; }
        public string DeliveryRemark { set; get; }
        public int PaymentMode { get; set; }
        public List<GetPODOrderDetailModel> ProductList { set; get; }
    }
}
