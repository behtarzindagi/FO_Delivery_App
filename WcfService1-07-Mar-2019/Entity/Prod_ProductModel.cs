using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Prod_ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string TechnicalName { get; set; }
        public string CompanyName { get; set; }
    }

    public class Prod_ProductViewModel
    {
        public List<Prod_ProductModel> Product { get; set; }

    }
}
