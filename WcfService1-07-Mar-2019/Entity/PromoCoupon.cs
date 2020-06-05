using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class PromoCoupon
    {
        public int CouponID { get; set; }
        public string CouponCode { get; set; }

        public string CouponName { get; set; }

        public string CouponDesc{ get; set; }


    }
    public class PromoCouponModel
    {
        public List<PromoCoupon> List { get; set; }

    }
}
