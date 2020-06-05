using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UpdateFarmerDetails
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public int FarmerId { get; set; }
        public string RefSource { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int VillageId { get; set; }
        public string NearByVillage { get; set; }
        public string Address { get; set; }
        public  string Landmark { get; set; }
        public  string PinCode { get; set; }
        public string Email { get; set; }
    }
    public class ResponseStatus
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
    }


    public class BZAPPWebFarmerLogin
    {
        public string apiKey { get; set; }
        public string MobNo { get; set; }
        public string emailId { get; set; }
        public string ShareCode { get; set; }
        public int FarmerId { get;set;}
        public string DeviceId { get; set; }
        public string Name { get; set; }
    }
}
