using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class Product_Add_Dealer_Temp
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int TechnicalID { get; set; }
        //public int BrandID { get; set; }

        public int BrandID { get; set; }

        public int CompanyID { get; set; }
        public int PackageTypeID { get; set; }
        public int StateID { get; set; }
        public int ProductType { get; set; }
        public int CropID { get; set; }

        public string apikey { get; set; }


        public int QualityID { get; set; }
        public List<Prod_Package_Add_dealer> Packages { get; set; }


        public int OldProductId { get; set; }
      
        public string DosageID { get; set; }
        public string ApplyOnCrop { get; set; }
        public string Target { get; set; }
        public string Remark { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
     
     
        public string Disease { get; set; }
        public int FeedType { get; set; }
        public int FeedState { get; set; }
        public int status { get; set; }
      //  public string Technical_Name { get; set; }
      //  public string Company_Name { get; set; }
      //  public string Brand_Name { get; set; }

        public string otherCompanyName { get; set; }
        public string otherBrandName { get; set; }
        public string otherTechnicalName { get; set; }

        public List<DealerTbl_ProductUseFor> ProductUserFor { get; set; }
        public List<NutrientValue> otherCompoundCattleFeed { get; set; }


    }

    public class Prod_Package_Add_dealer
    {
        // public int ProductId { get; set; }
        public int packageId { get; set; }
        public decimal size { get; set; }
        public decimal mrp { get; set; }
        public int unitID { get; set; }
        public int dealerID { get; set; }
        public decimal dealerPrice { get; set; }
        public decimal ourPrice { get; set; }
        public decimal othercharges { get; set; }

        public int qty { get; set; }
        public int isActive { get; set; }

    }
}
