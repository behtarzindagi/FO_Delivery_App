using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Transporter
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

    }

    public class TripForApprove: Transporter
    {
        public string VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string RuleId { get; set; }
        public string RuleName { get; set; }
        public string Fixedrateperday { get; set; }
        public string FuelCharges { get; set; }
        public string Priceperkm { get; set; }
        public string Mincharges { get; set; }
        public string MinchargeUptoKm { get; set; }
        public string UpPrice { get; set; }
        public string DownPrice { get; set; }
        public string FixedPrice { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedToName { get; set; }
        public string TripStatus { get; set; }
        public string TripTempId { get; set; }
        public string IsUpDown { get; set; }
        public string ReasonId { get; set; }
        public string ReasonDesc { get; set; }
        public string Remarks { get; set; }
        public string District { get; set; }
        public string DistrictId { get; set; }
    }

    public class TransporterVehicleTrip 
    {
       
        public Transporter Transporter { get; set; }
        public string Orderid { get; set; }
        public string VehicleId { get; set; }
        public string Fixedrateperday { get; set; }
        public string FuelCharges { get; set; }
        public string Priceperkm { get; set; }
        public string Mincharges { get; set; }
        public string MinchargeUptoKm { get; set; }
        public string UpPrice { get; set; }
        public string DownPrice { get; set; }
        public string FixedPrice { get; set; }
        public string VehicleName { get; set; }
        public string CreatedBy { get; set; }
        public string Mobile { get; set; }
        public string RuleId { get; set; }
        public string apiKey { get; set; }
        public string IsUpDown { get; set; }
        public string ReasonId { get; set; } 
        public string ReasonDesc { get; set; }
        public string Remarks { get; set; }

    }

    public class TransporterVehicleTripWithDate
    {

        public Transporter Transporter { get; set; }
        public string Orderid { get; set; }
        public string VehicleId { get; set; }
        public string Fixedrateperday { get; set; }
        public string FuelCharges { get; set; }
        public string Priceperkm { get; set; }
        public string Mincharges { get; set; }
        public string MinchargeUptoKm { get; set; }
        public string UpPrice { get; set; }
        public string DownPrice { get; set; }
        public string FixedPrice { get; set; }
        public string VehicleName { get; set; }
        public string CreatedBy { get; set; }
        public string Mobile { get; set; }
        public string RuleId { get; set; }
        public string apiKey { get; set; }
        public string IsUpDown { get; set; }
        public string ReasonId { get; set; }
        public string ReasonDesc { get; set; }
        public string Remarks { get; set; }
        public DateTime Date { get; set; }

    }


    public class TempTransporterVehicleTrip
    {
        public string TransUserId { get; set; }
        public string TransName { get; set; }
        public string TransMobile { get; set; }
        public string TransAddress { get; set; }
        public string Orderid { get; set; }
        public string VehicleId { get; set; }
        public string UpPrice { get; set; }
        public string DownPrice { get; set; }
        public string FixedPrice { get; set; }
        public string VehicleName { get; set; }
        public string CreatedBy { get; set; }
        public string Mobile { get; set; }
        public string RuleId { get; set; }
        public string apiKey { get; set; }
    }


    public class ResponseObj
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public string Value { get; set; }
    }



}

