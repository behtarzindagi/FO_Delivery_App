using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract()]
    public class OrderDeliveryModel
    {
        [DataMember]
        public string apiKey { set; get; }
        [DataMember]
        public int userid { set; get; }
        [DataMember]
        public int orderid { set; get; }
        
        [DataMember]
        public string statusid { set; get; }
    }

    public class OfflinePODModel
    {
        [DataMember]
        public string apiKey { set; get; }
        [DataMember]
        public int userid { set; get; }
        [DataMember]
        public int orderid { set; get; }
        [DataMember]
        public string imageBase64String { set; get; }
    }
}
