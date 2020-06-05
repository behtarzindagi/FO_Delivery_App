using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HubHeadGetTodayOrderByFoModel
    {
        public int OrderId { get; set; }
        public string OrderRefNo { get; set; }
        public string Status { get; set; }
        public string StatusId { get; set; }
    }
    public class HubHeadGetTodayOrderByFoViewModel
    {
        public List<HubHeadGetTodayOrderByFoModel> List { get; set; }
    }
}
