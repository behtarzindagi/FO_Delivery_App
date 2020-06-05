using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TempOrderDetailsModel
    {
       public List<TempOrderDetail> List { get; set; }
       
    }

    public class TempOrderDetail
    {
        public int Quantity { get; set; }

        public BZProductDescription product { get; set; }
      
    }

}
