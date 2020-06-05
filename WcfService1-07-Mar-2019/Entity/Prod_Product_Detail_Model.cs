using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Prod_Product_Detail_Model
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
        public GenericModel Quality { get; set; }
        public List<Prod_Package> Packahes { get; set; }
    }
    public class Prod_Package
    {
        public int PackageId { get; set; }
        public decimal Qty { get; set; }
        public decimal MRP { get; set; }
        public GenericModel Unit { get; set; }
    }
    
}
