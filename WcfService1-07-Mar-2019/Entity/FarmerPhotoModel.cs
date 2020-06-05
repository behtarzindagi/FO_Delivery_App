using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract()]
    public class FarmerPhotoModel
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
    [DataContract()]
    public class UploadPhotoModel
    {
        [DataMember]
        public string apiKey { set; get; }
        [DataMember]
        public int userid { set; get; }
        [DataMember]
        public string Lat { set; get; }
        [DataMember]
        public string Lang { set; get; }
        [DataMember]
        public string imageBase64String { set; get; }

    }
}
