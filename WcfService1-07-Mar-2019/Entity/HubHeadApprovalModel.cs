using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HubHeadApprovalModel
    {
        public int FoId { get; set; }
        public string FoName { get; set; }
        public decimal Amount { get; set; }
        public string Desc { get; set; }
        public int RecordId { get; set; }
        public string Date { get; set; }
        public string Mobile { get; set; }
    }

    public class HubHeadApprovalViewModel
    {
        public List<HubHeadApprovalModel> FOList { get; set; }
    }
    
}