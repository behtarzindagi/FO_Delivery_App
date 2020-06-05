using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LoginModel
    {
        public string apiKey { get; set; }
        public string userName { get; set; }
        public string password { get; set; }        
        public string deviceid { get; set; }
        public string fcmid { get; set; }
        public string modelName { get; set; }
        public string imei { get; set; }
    }
}
