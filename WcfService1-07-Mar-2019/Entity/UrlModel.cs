using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class UrlModel
    {
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string Url { get; set; }
    }
    public class UrlViewModel
    {
        public List<UrlModel> List { get; set; }

    }
}
