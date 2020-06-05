using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PodDetailReport
    {
        public string OrderRefNo { get; set; }
        public int OrderID { get; set; }
        public long FarmerID { get; set; }
        public string FarmerRefNo { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string NickName { get; set; }
        public string FatherName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int BLockID { get; set; }
        public string BlockName { get; set; }
        public int VillageID { get; set; }
        public string VillageName { get; set; }
        public string NearByVillage { get; set; }
        public string Chaupal { get; set; }
        public string ShippingAddress { get; set; }
        public decimal MobNo { get; set; }
        public int ProductID { get; set; }
        public string BrandName { get; set; }
        public string OrganisationName { get; set; }
        public string TechnicalName { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public string UnitName { get; set; }
        public decimal OurPrice { get; set; }
        public int Quantity { get; set; }
        public decimal OtherCharges { get; set; }
        public string DiscType { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal Subtotal { get; set; }
        public decimal HandleChages { get; set; }
        public decimal DiscountedAmount { get; set; }
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public System.DateTime DeliveryInstruction { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ProcessedDate { get; set; }
        public string DiscText { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public string TotalPayableAmount_In_Word { get; set; }
        public int SubCategoryId { get; set; }
        public string HsnCode { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public string PanNo { get; set; }
        public string GstNumber { get; set; }
        public string DealerStateName { get; set; }
        public string DealerDistrictName { get; set; }
        public string DealerBlockName { get; set; }
        public string DealerVillageName { get; set; }
        public string DealerAddress { get; set; }
        public string Invoice_No { get; set; }
        public long RecordId { get; set; }
        //public byte[] QrCode { get; set; }

    }

    public class _VmInvoiceOrder
    {
        public long OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public int DealerId { get; set; }
    }
}
