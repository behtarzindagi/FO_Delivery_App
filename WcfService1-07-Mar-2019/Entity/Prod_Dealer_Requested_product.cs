using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class Prod_Dealer_Requested_product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public int productType { get; set; }
        public string Description { get; set; }
        public string DosageDescription { get; set; }
        public string companyName { get; set; }
        public int companyId { get; set; }

        public string brandName { get; set; }
        public int brandId { get; set; }

        public string technicalName { get; set; }
        public int technicalId { get; set; }

        public string subCategoryName { get; set; }
        public int subCategoryID { get; set; }
        public string categoryName { get; set; }
        public int categoryId { get; set; }

        
        public int packageID { get; set; }
        public string packetName { get; set; }
        public int pckTypeId { get; set; }
        public int ProductState { get; set; }
        public string ProStateName { get; set; }
        public decimal size { get; set; }
        public decimal mrp { get; set; }
        public string unitName { get; set; }
        public int unitId { get; set; }
        public string dealerName { get; set; }
        public string districtName { get; set; }
        public int districtId { get; set; }

        public int dealerID { get; set; }
        public decimal dealerPrice { get; set; }
        public int qty { get; set; }
        public bool isActive { get; set; }
        public int status { get; set; }
        public string remarks { get; set; }
        public string applyOnCrop { get; set; }
        public string target { get; set; }
        public decimal ourPrice  {get; set; }
        public decimal otherCharges { get; set; }
        public int packageTypeId { get; set; }
        public int QualityId { get; set; }
        public int cropID { get; set; }
        public string Disease { get; set; }
        public int FeedType { get; set; }
        public int FeedState { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public decimal previousDealerPrice { get; set; }

        public string otherCompanyName { get; set; }
        public string otherBrandName { get; set; }
        public string otherTechnicalName { get; set; }





    }
}
