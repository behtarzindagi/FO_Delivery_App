using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PodDetail
    {
        public string OrderRefNo { get; set; }
        public int OrderID { get; set; }
        public int FarmerID { get; set; }
        public string FarmerRefNo { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string NickName { get; set; }
        public string FatherName { get; set; }
        public byte StateID { get; set; }
        public string StateName { get; set; }
        public short DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int BLockID { get; set; }
        public string BlockName { get; set; }
        public int VillageID { get; set; }
        public string VillageName { get; set; }
        public string NearByVillage { get; set; }
        public string Chaupal { get; set; }
        public string ShippingAddress { get; set; }
        public string MobNo { get; set; }
        public int ProductID { get; set; }
        public string BrandName { get; set; }
        public string OrganisationName { get; set; }
        public string TechnicalName { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string UnitName { get; set; }
        public decimal OurPrice { get; set; }
        public int Quantity { get; set; }
        public decimal OtherCharges { get; set; }
        public string DiscType { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal Subtotal { get; set; }
        public decimal HandleChages { get; set; }
        public decimal Total { get; set; }
        public string Total_InWord { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string Time { get; set; }
        public string DeliveryInstruction { get; set; }
        public string CreatedDate { get; set; }
        public string ProcessedDate { get; set; }
    }
}
