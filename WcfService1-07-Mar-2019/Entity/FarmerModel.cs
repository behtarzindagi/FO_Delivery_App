using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class FarmerModel
    {
           public string FarmerId { set; get; }
          public string FarmerName { set; get; }
         public string District { set; get; }
         public string MobileNo { set; get; }

        public string DistrictId { set; get; }

        public string StateId { set; get; }

        public int StatusID { set; get; }
        public string CallStatus { set; get; }

    }
    public class FarmerViewModel
    {
        public List<FarmerModel> FarmerList { set; get; }
    }
    public class Farmer
    {
        public FarmerViewModel Farmers { set; get; }
    }
    public class FarmerDetails
    {
        public string FarmerId { set; get; }
        public string RefernceSource { get; set; }
        public string FarmerName { set; get; }
        public string FatherName { get; set; }
        public decimal MobNo { get; set; }
        public string Email { get; set; }
        public int StateId { set; get; }
        public string StateName { get; set; }
        public int DistrictId { set; get; }
        public string DistrictName { set; get; }
        public string MobileNo { set; get; }
        public Nullable<int> BlockID { get; set; }
        public string BlockName { get; set; }
        public Nullable<long> VillageID { get; set; }
        public string VillageName { get; set; }
        public string Address { get; set; }

        public string NearByVillage { get; set; }

        public Nullable<bool> IsActive { get; set; }
    }

    public class FarmerData
    {

        public string apiKey { set; get; }
        public string FarmerName { set; get; }
        public string FatherName { get; set; }
        public int StateId { set; get; }   
        public int DistrictId { set; get; }   
        public string MobileNo { set; get; }
        public int  BlockID { get; set; }   
        public int VillageID { get; set; }
        public string Address { get; set; }
        public string NearByVillage { get; set; }
        public int UserID { set; get; }
        public string RefrenceSource { get; set; }

        public int statusId { set; get; }


    }

    public class FarmecallHistory
    {
        public List<FarmerCall> Farmerc { set; get; }
    }
    public class FarmerCall
    {
        public string FarmerId { set; get; }
        public string FarmerName { set; get; }
        public string FatherName { get; set; }
        public string MobileNo { set; get; }
        public int StateId { set; get; }
        public string StateName { get; set; }
        public int DistrictId { set; get; }
        public string DistrictName { set; get; }    
        public Nullable<int> BlockID { get; set; }
        public string BlockName { get; set; }
        public Nullable<long> VillageID { get; set; }
        public string VillageName { get; set; }
        public string Address { get; set; }
        public string NearByVillage { get; set; }
        public string CallDate { get; set; }
        public string CallDuration { get; set; }
        public string CallStatus { get; set; }
        public string RescheduledDate { get; set; }
        public string Message { get; set; }
    }

}
