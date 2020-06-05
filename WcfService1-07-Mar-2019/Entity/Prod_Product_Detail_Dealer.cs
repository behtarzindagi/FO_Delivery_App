using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class Prod_Product_Detail_Dealer
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string Dosage { get; set; }
        public GenericModel Category { get; set; }
        public GenericModel SubCategory { get; set; }
        public GenericModel TechnicalName { get; set; }
        public GenericModel Brand { get; set; }
        public GenericModel Company { get; set; }
        public GenericModel PackageType { get; set; }
        public GenericModel State { get; set; }
        public GenericModel Crop { get; set; }
        public GenericModel ProductType { get; set; }

        public GenericModel Quality { get; set; }
        public List<Prod_Package_Add_dealer> Packages { get; set; }
        public List<DealerTbl_ProductUseFor> ProductUserFor { get; set; }

        public string otherCompanyName { get; set; }
        public string otherBrandName { get; set; }
        public string otherTechnicalName { get; set; }

    }

}



 

