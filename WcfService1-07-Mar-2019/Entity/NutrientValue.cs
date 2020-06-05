using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class NutrientValue
    {
            //public Nullable<short> NutrientID { get; set; }
            //public Nullable<decimal> Amount { get; set; }
            //public Nullable<byte> Unit { get; set; }
            //public Nullable<short> TechName { get; set; }


        public GenericModel Nutrient { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public GenericModel Unit { get; set; }
        public Nullable<short> TechName { get; set; }

    }
}
