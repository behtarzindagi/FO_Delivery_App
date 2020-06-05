using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class AgreementDetails
    {
        public string apiKey { get; set; }
        public string CompanyName { get; set; }
        public string EnterpenueName { get; set; }
        public string MobileNo { get; set; }
        public string AltMobile { get; set; }
        public string GST { get; set; }
        public string PAN { get; set; }
        public string ShopAddress { get; set; }

        public int StateId { get; set; }
        public int DistrictId { get; set; }

        public string Pincode { get; set; }
    }

}
