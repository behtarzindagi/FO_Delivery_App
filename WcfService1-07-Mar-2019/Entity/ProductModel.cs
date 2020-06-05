using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProductModel
    {

        public string RecordId { set; get; }
        public string ProductId { set; get; }
        public string ProductName { set; get; }
        public string Quantity { set; get; }
        public string PackageName { set; get; }
        public string PackageId { set; get; }
    }
    public class ProductViewModel
    {
        public List<ProductModel> ProductList { set; get; }
    }
    public class Product
    {
        public ProductViewModel Products { set; get; }
    }
}
