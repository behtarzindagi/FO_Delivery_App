using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BZProduct
    {
        public List<BzProduct> BzProduct { get; set; }
        public int TotalCount { get; set; }
    }
    public class BzProduct
    {
        public int BzProductId { get; set; }
        public int PackID { get; set; }
        public decimal OfferPrice { get; set; }
        public string Districtlist { get; set; }

        public string DistrictlistName { get; set; }
        public string ImagePath { get; set; }

        public string ProductName { get; set; }

        public string ProductHindiName { get; set; }
        public string OrganisationName { get; set; }
        public string BrandName { get; set; }
        public string TechnicalName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryHindi { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string UnitName { get; set; }

        public decimal OurPrice { get; set; }
        public string status { get; set; }
        public string CretedBy { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int OfferPrice_Qty { get; set; }
        public decimal OnlinePrice { get; set; }
        public int COD { get; set; }
        public int MRP { get; set; }

        public string OfferDiscount { get; set; }
        public bool IsDetails { get; set; }
        // public int TotalCount { get; set; } //By Lalit
    }

    public class ComboBZProduct
    {
        public int ComboId { get; set; }

        public string ComboName { get; set; }

        public int OfferPrice { get; set; }

        public int MRP { get; set; }

        public int DiscountAmt { get; set; }

        public string ImagePath { get; set; }

        public string ComboHindiName { get; set; }
    }

    public class BZProductBanner
    {
        public int BannerId { get; set; }

        public string BannerName { get; set; }

        public string BannerType { get; set; }

        public string ImagePath { get; set; }

       
    }

    public class BZProductInterestCount
    {
        public int TotalCount { get; set; }
    }

    public class BZFCMId
    {
        public string FCMId { get; set; }
    }
    public class BZFCMIdList
    {
        public List<BZFCMId> FcmList { get; set; }
        public string FCMId { get; set; }
    }

    public class StateDistrictBlockVillage
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StateDistrictBlockVillageList
    {
        public List<StateDistrictBlockVillage> List { get; set; }
    }
}
