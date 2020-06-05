using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
        public  class Tbl_ProductUseFor
        {
            public int Id { get; set; }
        //    public Nullable<int> ProductID { get; set; }
            public Nullable<int> UseFor { get; set; }
            public Nullable<int> Category { get; set; }
            public Nullable<int> Srange { get; set; }
            public Nullable<int> Erange { get; set; }
            public Nullable<int> RangeUnit { get; set; }
            public string Remark { get; set; }
            public Nullable<int> CreateBy { get; set; }
       //     public Nullable<System.DateTime> CreatedDate { get; set; }
            public string CreatedDate { get; set; }
             public Nullable<int> ModifiedBy { get; set; }
       //     public Nullable<System.DateTime> ModifiedDate { get; set; }

            public string ModifiedDate { get; set; }

           public Nullable<int> IsActive { get; set; }
        }

    public class DealerTbl_ProductUseFor : Tbl_ProductUseFor
    {
        public int status { get ; set;}

    }


}
