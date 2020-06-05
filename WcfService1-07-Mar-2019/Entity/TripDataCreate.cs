using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public  class TripDataCreate
    {
        public string apiKey { get; set; }
        public string mobile { get; set; }

        public string orderid { get; set; }
        public string vehicletypeid { get; set; }

        public string vehiclename { get; set; }
        public string createdby { get; set; }
        public string transpoterId { get; set; }
        public string ruleId { get; set; }
    }
}
