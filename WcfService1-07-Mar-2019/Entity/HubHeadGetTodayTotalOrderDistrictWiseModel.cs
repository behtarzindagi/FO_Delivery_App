using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HubHeadGetTodayTotalOrderDistrictWiseViewModel
    {
        public HubHeadGetTodayTotalOrderDistrictWiseModel List { get; set; }
    }
    public class HubHeadGetTodayTotalOrderDistrictWiseModel
    {
        public int TotalOrder { get; set; }
        public int TotalOrderAssigned { get; set; }
        public int TotalOrderRemaian { get; set; }
        public List<HubHeadGetTodayTotalOrderDistrictWiseFoDetailModel> FODetailList { get; set; }
    }
    public class HubHeadGetTodayTotalOrderDistrictWiseFoDetailModel
    {
        public int FoId { get; set; }
        public string FoName { get; set; }
        public long Mobile { get; set; }
        public int TotalOrderAssignedToFo { get; set; }
        public decimal TotalAmountCollectedToday { get; set; }
        public decimal TotalAmountToBeCollect { get; set; }
        public decimal Cash { get; set; }
        public decimal PayU { get; set; }
        public decimal POS { get; set; }
        public decimal UPI { get; set; }
        public int TripId { get; set; }
        public int TripReleasable { get; set; }
    }
}
