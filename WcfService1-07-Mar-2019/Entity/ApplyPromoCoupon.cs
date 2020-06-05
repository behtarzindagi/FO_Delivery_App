using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ApplyPromoCoupon
    {
        public int CatID { get; set; }
        public int SubCatID { get; set; }
        public int CompanyID { get; set; }
        public int BrandID { get; set; }
        public int ProductID { get; set; }
        public int PkgID { get; set; }
        public int Qty { get; set; }
        public decimal ActualAmt { get; set; }
        public int CouponID { get; set; }
        public string CouponCode { get; set; }
        public Int16 DiscType { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string CouponMsg { get; set; }

        public int CouponStatus { get; set; }
    }
}
