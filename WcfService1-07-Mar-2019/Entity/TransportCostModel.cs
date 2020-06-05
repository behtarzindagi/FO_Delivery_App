using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TransportCostModel
    {
        public string Id { set; get; }
        public string TripId { set; get; }
        public string VehicleID { set; get; }
        public string VehicleName { set; get; }
        public string TranspoterName { set; get; }
        public string Mobile { set; get; }
        public string TotalKm { set; get; }
        public string TotalCost { get; set; }
    }
}
