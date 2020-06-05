using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    class Prod_DemandProduct
    {
        public int technicalId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public string productName { get; set; }
        public string remarks { get; set; }
        public int createdBy { get; set; }

    }
}
