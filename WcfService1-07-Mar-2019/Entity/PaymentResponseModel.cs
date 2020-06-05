using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract]
    public class PaymentResponseModel
    {
        [DataMember]
        public string apiKey { set; get; }
        [DataMember]
        public int userid { set; get; }
        [DataMember]
        public int orderid { set; get; }

        [DataMember]
        public string cash { set; get; }

        [DataMember]
        public string PaymentGateway { set; get; }

        [DataMember]
        public string UPI { set; get; }
        [DataMember]
        public string POS { set; get; }
        [DataMember]
        public string POS_MachineNo { set; get; }
        [DataMember]
        public string POS_BatchNo { set; get; }
    }

    public class PaymentRequestParam
    {
        public List<PaymentRequestParamList> Farmerc { set; get; }
    }
    public class PaymentRequestParamList
    {
        public string EncKey { set; get; }
        public string PGMerchant_ID { set; get; }
        public string User_Name { get; set; }
        public string OAuth_Password { set; get; }
        public string Name { set; get; }
        public string VPA { get; set; }
        public string merchantCategoryCode { set; get; }
        public string PaymentType { set; get; }    
        public string TxnType { get; set; }
      
    }

}
