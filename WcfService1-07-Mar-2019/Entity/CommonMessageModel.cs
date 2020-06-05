using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CommonMessageModel
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public string mobiles { get; set; }
        public string message { get; set; }
        public char language { get; set; }

    }
}
