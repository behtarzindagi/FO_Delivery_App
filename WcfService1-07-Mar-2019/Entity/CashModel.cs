using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class CashModel
    {
        public string CashCollectToday { get; set; }
        public string TotalCashInHand { get; set; }
        public string TotalClearingAmount { get; set; }
    }

    public class CashViewMoodel
    {
        public CashModel Cash { get; set; }
    }
}
