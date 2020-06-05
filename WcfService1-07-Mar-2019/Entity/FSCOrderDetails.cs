using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FSCOrderList
    {
        public string CreateDate { get; set; }
        public string Deliverydate { get; set; }
        public string FarmerName { get; set; }
        public string FatherName { get; set; }
        public int OrderStatus { get; set; }
        public string  Status { get; set; }
        public int OrderId { get; set; }
        public string MobileNo { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        public int FarmerId { get; set; }
        public string OrderRefNo { get; set; }
        public decimal TotalPrice { get; set; }

        public decimal DiscAmount { get; set; }
    }
    public class FSCOrderDetails
    {
        public List<FSCOrderList> OrderList { set; get; }
    }
    public class SaleOrderList
    {
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string SaleCount { get; set; }
        public decimal SaleValue { get; set; }

    }
    public class SaleOrderDetail
    {
        public List<SaleOrderList> SaleList { set; get; }
    }
}
