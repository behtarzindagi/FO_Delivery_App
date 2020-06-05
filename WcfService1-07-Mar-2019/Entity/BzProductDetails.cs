using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BzProductDtl
    {
        public List<BzProductDetails> BzPrdctDtl { get; set; }
    }
    public class BzProductDetails
    {
        public int ProductId { get; set; }
        public string CropName { get; set; }
        public string BrandName { get; set; }
        public string TechnicalName { get; set; }
        public string Days { get; set; }
        public string Dose { get; set; }
        public string Method { get; set; }
        public string Control { get; set; }
        public string Symptoms { get; set; }
        public string TimeofUse { get; set; }

        public string AdditionalInfo { get; set; }

        public string Manufacturer { get; set; }

        public string ImageUrl { get; set; }
        public string  VideoUrl { get; set; }

        public bool IsActive { get; set; }

        public string Product { get; set; }
        public string Creator { get; set; }
        public string Purpose { get; set; }
        public string Features { get; set; }

        public string Weight { get; set; }
        public int RecordId { get; set; }
        public string  BladeType { get; set; }
        public string Blade { get; set; }
        public string Speciality { get; set; }
        public string TractorHPRequirement { get; set; }
        public string TractorPTO { get; set; }
        public string Transmission { get; set; }
        public string GearBox { get; set; }

        public string ProductName { get; set; }
        public string Capacity { get; set; }
        public string ModelNo { get; set; }
        public string UseMethod { get; set; }
        public string Description { get; set; }
    }

    public class BzVegetableDetail
    {
        public List<BzVegetableContent> BzVegetablesDtl { get; set; }
    }
    public class BzVegetableContent
    {
        public int ProductId { get; set; }
        public string CropName { get; set; }
        public string Species { get; set; }
        public string Manufacturer { get; set; }
        public string SeedingTime { get; set; }
        public string TransplantingTime { get; set; }
        public string SeedQty { get; set; }
        public string CropTime { get; set; }
        public string Distance { get; set; }
        public string YieldingPerAcre { get; set; }
        public string FruitWeight { get; set; }

        public string CropSpeciality { get; set; }

        public string FruitSize { get; set; }

        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public bool IsActive { get; set; }
    }
    public class BzShareLink
    {
        public string BzShareUrl { get; set; }
    }
        public class Referal: BasicUser
    {
        public string BzShareUrl { get; set; }
        public string ReferencedCode { get; set; }
        
        public string RandomCode { get; set; }
        public bool Status { get; set; }
        public string FarmerExist { get; set; }
        public string ReferdTo { get; set; }
        public string ReferdBy { get; set; }
        public string CouponCode { get; set; }
        public string CouponDesc { get; set; }
        public int DiscAmount { get; set; }
        public int TotalCoupons { get; set; }
        public int MinTransactionValue { get; set; }
       public List<Referal> DiscountInfoValue { get; set; }
        

        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string VillageName { get; set; }
        public int ShippingAddressId { get; set; }
        public bool IsChecked { get; set; }
        public string OfferType { get; set; }

        public string PaymentMode { get; set; }
        public decimal DeliveryCharges { get; set; }

        //Changes on 20-06-2019
        public string DeviceId { get; set; }
        public string apiKey { get; set; }
        public string Email { get; set; }

        public string LoginType { get; set; }
    }

    public class BasicUser
    {
        public long FarmerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public string BlockId { get; set; }
        public string VillageId { get; set; }
        public string Landmark { get; set; }
        public string PinCode { get; set; }
        public string MobileNumber { get; set; }
        public int RoleId { get; set; }
        public bool IsReset { get; set; }
        public string Address { get; set; }
        public string NearByVillage { get; set; }
    }

    public class CouponDetail
    {
        public int MinTransactionValue { get; set; }
        public int DiscAmount { get; set; }
        public string CouponCode { get; set; }
        public string CouponDesc { get; set; }
        public string ReferdBy { get; set; }
        public string ReferdTo { get; set; }
        public string OfferType { get; set; }
        public int TotalCoupons { get; set; }
    }


    public class PaymentOptionList
    {      
        public List<CouponDetail> CouponDetails { get; set; }
        public List<PaymentOptions> PaymentOptions { get; set; }

    }

    public class PaymentOptions
    {
        public string PaymentMode { get; set; }
        public decimal DeliveryCharges { get; set; }

        public string PaymentModeText { get; set; }
        public string DeliveryChargeText { get; set; }
    }
    public class BzAppLeftMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ImageUrl { get; set; }
        public string MenuHindiName { get; set; }
    }
    public class BzLeftMenuList
    {
        public List<BzAppLeftMenu> BzLeftMenu { get; set; }
    }

    public class BZOfferBanner
    {
        public int BannerId { get; set; }
        public int PackageId { get; set; }
        public string OfferName { get; set; }
        public string Weight { get; set; }
        public decimal MRP { get; set; }
        public decimal COD { get; set; }
        public decimal OnlinePrice { get; set; }
        public string ImageUrl { get; set; }
        
        public int DistrictId { get; set; }
        public string WebImageUrl { get; set; }
        public string MobImageUrl { get; set; }
    }

    public class BZOfferBannerList
    {
        public List<BZOfferBanner> BzOfferBannerList { get; set; }
    }


    public class BzFlowersDetail
    {
        public List<BzFlowersContent> BzVegetablesDtl { get; set; }
    }
    public class BzFlowersContent
    {
        public int BzProductId { get; set; }
        public string FlowerName { get; set; }
        public string NoOfSeeds { get; set; }
        public string SowingTemperature { get; set; }
        public string SowingTime { get; set; }
        public string PlantType { get; set; }
        public string Spacing { get; set; }
        public string GerminationRequirement { get; set; }
        public string SowingMethod { get; set; }
        public string BestFor { get; set; }
        public string GerminationTime { get; set; }

        public string FertilizerRecommended { get; set; }

        public string SpecialFeatures { get; set; }

        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public bool IsActive { get; set; }
    }

}
