using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FarmerDetailModel
    {
        public int FarmerId { get; set; }
        public string FarmerName { get; set; }
        public string FatherName { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int VillageId { get; set; }
        public string OtherVillageName { get; set; }
        public long Mobile { get; set; }
        public string Address { get; set; }
    }
  
    public class CategoryDetail
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class DealerDetail
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
    }
    public class BZProductDescription
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandID { get; set; }
        public string Company { get; set; }
        public string TechnicalName { get; set; }

        public int CompanyId { get; set; }
        public int DealerId { get; set; }
        public string DealerName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public int IsTrending { get; set; }

    }
    public class CategogyProductViewModel
    {
        public List<CategoryDetail> _categoryList { get; set; }
        public List<DealerDetail> _dealerList { get; set; }
        public List<BZProductDescription> _productList { get; set; }

    }
    public class ProductPostDetail
    {
        public int PackageId { get; set; }
        public int Quantity { get; set; }
        //public Nullable<int> DistrictID { get; set; }
    }
    public class TempProductPostDetail
    {
        public int PackageId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int DealerId { get; set; }
    }
    public class OrderCreateModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public string DeliveryDate { get; set; }
        public string ModeOfPayment { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; } //OfferDiscount
        public FarmerDetailModel Farmer { set; get; }
        public ProductPostDetail Product { set; get; }

        public int DiscountPrice { get; set; }
        public string DiscountCode { get; set; }
        public decimal Amount { get; set; }
        public int OfferDiscount { get; set; }
        public string yonoTransactionID { get; set; }
    }
    public class AgentOrderCreateModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public string DeliveryDate { get; set; }
        public string ModeOfPayment { get; set; }
        public FarmerDetailModel Farmer { set; get; }
        public List<ProductAgentDetail> Product { set; get; }
       
    }
    public class ProductAgentDetail
    {
        public int PackageId { get; set; }
        public int Quantity { get; set; }
        public int? CouponId { get; set; }
        public string CouponName { get; set; }
        public int? DiscTypeId { get; set; }
        public int? DiscoutValue { get; set; }
        public Nullable<int> DistrictID { get; set; }
    }
    public class TempOrderCreateModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public int FarmerId { set; get; }
        public List<TempProductPostDetail> Product { set; get; }
    }
   
    public class ProductPopularDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }
    public class BZAgentProductViewModel
    {
        
        public List<BZProductDescription> _productList { get; set; }

        public List<ProductPopularDetail> _PopularProductList { get; set; }

    }
    public class DemandCreateModel
    {
        public string apiKey { get; set; }
        public string FarmerName { get; set; }
        public long Mobile { get; set; }
        public int DistrictId { set; get; }
        public int CropID { set; get; }
        public int CategoryID { set; get; }
        public string Product { set; get; }
        public int PackageID { set; get; }
        public int Qty { set; get; }
        public decimal FarmerPrice { set; get; }
        public string AadharOrLoanNo { set; get; }  
        public int UserId { set; get; }
    }

    public class UserValidation
    {

        public int Userid { get; set; }

        public int UserStatus { get; set; }

        public List<PromoBanner> lstBanner { get; set; }

    }

    public class PromoBanner
    {
        public string Imagepath { get; set; }
    }

    public class RemoveItemFromCart
    {
        public string apikey { get; set; }
        public int OrderID { get; set; }
        public int RecordId { get; set; }
        public int DeletedBy { get; set; }
     
    }

    public class OrderWiseProduct
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }

    }
    public class OrderWiseProductViewModel
    {
          public List<OrderWiseProduct> _OrderWiseProductList { get; set; }

    }

    public class AgentOrderUpdateModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public int OrderID { get; set; }
        public int CancelReasonID { get; set; }
        public int statusId { get; set; }
        public string DeliveryDate { get; set; }
        public string ModeOfPayment { get; set; }
        public string Remark { get; set; }
        public FarmerDetailModel Farmer { set; get; }
        public List<ProductAgentDetail> Product { set; get; }

    }
    public class LeadCreateModel
    {
        public string apiKey { get; set; }
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string MobNo { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public string NearbyVillage { get; set; }
        public string AdditionalAddress { get; set; }
    }

    public class LoanDetail
    {
        public  string apiKey { get; set; }
        public string ParameterValue { get; set; }

        public int LoanId { get; set; }
        public string FarmerName { get; set; }
        public string MobileNumber { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int VillageId { get; set; }
        public string LandMark { get; set; }
        public string PanCardNo { get; set; }
        public string UIDNo { get; set; }
        public string VehicleModelNo { get; set; }
        public DateTime VehiclePurchaseDate { get; set; }
        public string LoanType { get; set; }
        public decimal RequiredLoanAmount { get; set; }
        public bool IsLastLoan { get; set; }            
    }

    public class QuestionAns
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string AnsText { get; set; }
        public string AnsValue { get; set; }
        public string FarmerName { get; set; }
        public string MobileNo { get; set; }
    }
    
    [DataContract]
    public class QuestionAnsTest
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Question { get; set; }
        [DataMember]
        public string AnsText { get; set; }
        [DataMember]
        public string AnsValue { get; set; }
        [DataMember]
        public string FarmerName { get; set; }
        [DataMember]
        public string MobileNo { get; set; }
    }
    public class Contest
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int RightAnsID { get; set; }
        public List<Option> Option { get; set; }
    }
    public class Option
    {
        public int OptionId { get; set; }
        public int QuetionId { get; set; }
        public int OptionValueId { get; set; }
        public string OptionText { get; set; }
    }

    public class SathiOrderCreateModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public string DeliveryDate { get; set; }
        public string ModeOfPayment { get; set; }
        public FarmerDetailModel Farmer { set; get; }
        public List<ProductPostDetail> Product { set; get; }
    }

    public class SathiCreatedOrderResult
    {
        public int Status { get; set; }
        public int OrderId { get; set; }
        public decimal OrderAmount { get; set; } 
    }

}
