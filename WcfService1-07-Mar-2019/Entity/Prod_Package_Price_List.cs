using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class Prod_Package_Price_List
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public GenericModel Company { get; set; }
        public GenericModel Brand { get; set; }
        public GenericModel Category { get; set; }
        public GenericModel SubCategory { get; set; }
        public GenericModel TechnicalName { get; set; }
        public bool isActive { get; set; }

        // public List<Prod_Package> Packages { get; set; }

        //  public   Prod_Product_Detail_Model lstProductDetail { get; set; }
        //   public List<Dealers> dealers { get; set; }
        public Prod_Get_Package_Details Package { get; set; }

    }
}
