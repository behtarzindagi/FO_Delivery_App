using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LogisticGetActionTakenOrderDetailModel
    {
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public string Package { get; set; }
    }
    public class LogisticGetActionTakenOrderDetailViewModel
    {
        public List<LogisticGetActionTakenOrderDetailModel> List { set; get; }
    }
}
