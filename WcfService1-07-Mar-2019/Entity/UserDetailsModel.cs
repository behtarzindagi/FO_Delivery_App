using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   
    public class UserDetailsModel
    {

        public long FarmerID { get; set; }
        public string FarmerRefNo { get; set; }
        public string FName { get; set; }
        public string FatherName { get; set; }
       
        public decimal MobNo { get; set; }

        public Nullable<int> StateID { get; set; }
        public string StateName { get; set; }

        public Nullable<int> DistrictID { get; set; }
        public string DistrictName { get; set; }

        public Nullable<int> BlockID { get; set; }
        public string BlockName { get; set; }

        public Nullable<int> NearestMandi { get; set; }
        public string NearestMandi_Name { get; set; }
      
        public Nullable<long> VillageID { get; set; }
        public string VillageName { get; set; }

        public string Address { get; set; }

        public Nullable<int> CreateBy { get; set; }
        public string CreateBy_Name { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy_Name { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public string LandMark { get; set; }
        public string NickName { get; set; }
        public string Chaupal { get; set; }
        public string NearByVillage { get; set; }

    }
    public class OrderDetailsModel
    {
        public long ItemID { get; set; }

        public long OrderId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal OurPrice { get; set; }
        public Nullable<decimal> OtherCharges { get; set; }
        public Nullable<decimal> DiscAmt { get; set; }
        public string DiscType { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> TechnicalName { get; set; }
        public decimal Amount { get; set; }
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string OrganisationName { get; set; }
        public string BrandName { get; set; }
        public string Status { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public Nullable<bool> IsUpdated { get; set; }
        public int PackageID { get; set; }
        public string TechnicalName_Name { get; set; }

    }
    public class UserDetails
    {
        public string MobileNo { set; get; }
        public string UserStatus { set; get; }
        public UserDetailsModel UserDetail { set; get; }

        public List<OrderDetailsModel> OrderDetails { set; get; }
    }
    public class UserLoginDetails
    {
        public string Name { set; get; }
        public string MobileNO { set; get; }
        public int RoleId { set; get; }
        public string UserName { set; get; }
        public int UserID { set; get; }

        public string AppVersionCode { set; get; }
        public string OzontelapiKey { set; get; }

        public string OzonteluserName { set; get; }

        public string Ozonteldid { set; get; }

    }
}
