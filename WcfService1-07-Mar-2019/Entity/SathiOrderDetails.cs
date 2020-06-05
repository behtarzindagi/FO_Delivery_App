using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class SathiOrderDetails
    {
        public SathiOrderDetail SathiOrderDetail { get; set; }
       
    }
    public class SathiOrderDetail
    {
        public string SathiName { get; set; }
        public BookedOrder BookedOrder { get; set; }
        public DeliveredOrder DeliveredOrder { get; set; }
        public CancelledOrder CancelledOrder { get; set; }
        public decimal ValueIncentive { get; set; }
        public decimal OrderIncentive { get; set; }
        public decimal TotalIncentive { get; set; }
    }
    public class BookedOrder
    {
        public int BookedQuantity { get; set; }
        public int BookedValue { get; set; }
        public List<FarmerOrderDetails> FarmerOrderDetails { get; set; }
    }
    public class DeliveredOrder
    {
        public int DelieveredQuantity { get; set; }
        public int DeliveredValue { get; set; }
        public List<FarmerOrderDetails> FarmerOrderDetails { get; set; }
    }
    public class CancelledOrder
    {
        public int CancelledQuantity { get; set; }
        public int CancelledValue { get; set; }
        public List<FarmerOrderDetails> FarmerOrderDetails { get; set; }
    }
    public class FarmerOrderDetails
    {
        public int FarmerId { get; set; }
        public string FarmerName { get; set; }
        public string FatherName { get; set; }
        public string Village { get; set; }
        public string Block { get; set; } 
        public string District { get; set; }
        public string State { get; set; }
        public int orderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal DelieveredAmount { get; set; }
        public string DeliveryDate { get; set; } 
        public bool PaymentStatus { get; set; }
    }

    public class SathiPaymentDetails
    {
        public List<SathiPaymentDetail> SathiPaymentDetail { get; set; }
        public string SathiName { get; set; }
    }
    public class SathiPaymentDetail
    {
        
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string  TxnId { get; set; }
    }
}