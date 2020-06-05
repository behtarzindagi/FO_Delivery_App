using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Reason
    {
        public ReasonViewModel ReasonJson { set; get; }
    }
    public class ReasonModel
    {
        public string ReasonId { set; get; }
        public string ReasonName { set; get; }
    }
    public class ReasonViewModel
    {
        public List<ReasonModel> CancelReason { set; get; }
        public List<ReasonModel> PendingReason { set; get; }
        public List<ReasonModel> ModifyReason { set; get; }
        public List<ReasonModel> ModifyReasonForDealer { set; get; }
        public List<ReasonModel> DealerChangeReason { set; get; }
        public List<ReasonModel> TransportReason { set; get; }
    }
}
