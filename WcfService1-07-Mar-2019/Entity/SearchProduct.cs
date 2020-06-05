using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class SearchProductList
    {
     
        public string Company { get; set; }
        public string DealerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
   
        public int PackageId { get; set; }
      
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int DealerId { get; set; }
        public int CategoryId { get; set; }
        public string PackageName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
    public class SearchProduct
    {
        public List<SearchProductList> ProductList { set; get; }
    }
}
