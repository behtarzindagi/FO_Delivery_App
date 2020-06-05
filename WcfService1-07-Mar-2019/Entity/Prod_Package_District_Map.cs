using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Entity
{
   public class Prod_Package_District_Map
    {
         public string Packages { get; set; }
         public string Districts { get; set; } 
     

    }

    public class Prod_Get_Package_District_Details
    {
        public int productId { get; set; }

        public int priceId { get; set; }
        public int packageId { get; set; }
        public int districtId { get; set; }
      //  public string blockName { get; set; }

        public string districtName { get; set; }

        public decimal ourPrice { get; set; }
        public decimal otherCharges { get; set; }
    //    public int blockID { get; set; }

        // public List<Prod_Get_Package_Details> packageDetail { get; set; }
        public List<Dealers> dealers { get; set; }
        public List<blocks> blocks { get; set; }

        //     public Dealers dealers { get; set; }

    }

    public class Prod_Get_Package_Details
    {
        public int productId { get; set; }

        public int priceId { get; set; }
        public int packageId { get; set; }
        public int districtId { get; set; }
        public GenericModel district { get; set; }
        public GenericModel block { get; set; }

        public decimal ourPrice { get; set; }
        public decimal otherCharges { get; set; }
        public int blockID { get; set; }        
        public List<Dealers> dealers { get; set; }
        public PackageDetail packageUnit { get; set; }



    }

    public class Dealers
    {
        public int dealerId { get; set; }
        public decimal dealerPrice { get; set; }
        public int dealerAvailableQuantity { get; set; }
        public int packageId { get; set; }
        public string dealerName { get; set; }



    }


    public class blocks
    {
        public int blockID { get; set; }
        public string blockName { get; set; }
        public decimal otherCharges { get; set; }
        public decimal ourPrice { get; set; }
        public int priceId { get; set; }

    }
}
