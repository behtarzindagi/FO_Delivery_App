using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class LogisticActionTakenTripsModel
    {
        public int TripId { get; set; }
        public string DistrictName { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedTime { get; set; }
        public string ActionBy { get; set; }
        public string ActionTime { get; set; }
        public string ActionStatus { get; set; }
        public string Vehicle { get; set; }
        public int RuleId { get; set; }
        public string Rule { get; set; }
        public decimal FixRateCharge { get; set; }
        public decimal UpCharge { get; set; }
        public decimal DownCharge { get; set; }
        public int MinKmRange { get; set; }
        public decimal MinRatePerKm { get; set; }
        public decimal RatePerKm { get; set; }
        public decimal FixRatePerDay { get; set; }
        public decimal FuelCharge { get; set; }

    }

    public class LogisticActionTakenTripsViewModel
    {
        public List<LogisticActionTakenTripsModel> List { set; get; }
    }
}
