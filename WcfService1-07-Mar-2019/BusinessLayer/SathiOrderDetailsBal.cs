using DataLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class SathiOrderDetailsBal
    {


        public SathiOrderDetails GetSathiSalesReport(string apikey, int UserId, int DateMode, DateTime? SDATE, DateTime? EDATE)
        {
            SathiOrderDetails _sathiOrderDetails = new SathiOrderDetails();
            SathiOrderDetail _objData = new SathiOrderDetail();
            
            List<FarmerOrderDetails> _objFarmerDataList = new List<FarmerOrderDetails>();
            List<FarmerOrderDetails> _objFarmerCancellDataList = new List<FarmerOrderDetails>();
            List<FarmerOrderDetails> _objFarmerDeliveredDataList = new List<FarmerOrderDetails>();
            BZOfferBanner ObjOfferBanner = new BZOfferBanner();
            BookedOrder _bookedOrder = new BookedOrder();
            DeliveredOrder _deliveredOrder = new DeliveredOrder();
            CancelledOrder _cancelledOrder = new CancelledOrder();

            DataSet ds = new SathiOrderDetailsDal().GetSathiSalesReport(apikey, UserId, DateMode, SDATE, EDATE);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    _objData.SathiName = ds.Tables[0].Rows[i]["SathiName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SathiName"]);
                    _objData.ValueIncentive = ds.Tables[0].Rows[i]["ValueIncentive"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["ValueIncentive"]);
                    _objData.OrderIncentive = ds.Tables[0].Rows[i]["OrderIncentive"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderIncentive"]);
                    _objData.TotalIncentive = ds.Tables[0].Rows[i]["TotalIncentive"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalIncentive"]);
                    _bookedOrder.BookedQuantity = ds.Tables[0].Rows[i]["BookedQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BookedQuantity"]);
                    _bookedOrder.BookedValue = ds.Tables[0].Rows[i]["BookedValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BookedValue"]);
                    _bookedOrder.FarmerOrderDetails = new List<FarmerOrderDetails>();

                    _deliveredOrder.DelieveredQuantity = ds.Tables[0].Rows[i]["DelieveredQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DelieveredQuantity"]);
                    _deliveredOrder.DeliveredValue = ds.Tables[0].Rows[i]["DelieveredValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DelieveredValue"]);
                    _deliveredOrder.FarmerOrderDetails = new List<FarmerOrderDetails>();

                    _cancelledOrder.CancelledQuantity = ds.Tables[0].Rows[i]["CancelledQuantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CancelledQuantity"]);
                    _cancelledOrder.CancelledValue = ds.Tables[0].Rows[i]["CancelledValue"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CancelledValue"]);

                    _cancelledOrder.FarmerOrderDetails = new List<FarmerOrderDetails>();

                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    FarmerOrderDetails _objFarmerData = new FarmerOrderDetails();
                    _objFarmerData.FarmerId = ds.Tables[1].Rows[i]["FarmerId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["FarmerId"]);
                    _objFarmerData.FarmerName = ds.Tables[1].Rows[i]["FarmerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["FarmerName"]);
                    _objFarmerData.FatherName = ds.Tables[1].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["FatherName"]);
                    _objFarmerData.Village = ds.Tables[1].Rows[i]["Village"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["Village"]);
                    _objFarmerData.Block = ds.Tables[1].Rows[i]["Block"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["Block"]);
                    _objFarmerData.State = ds.Tables[1].Rows[i]["State"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["State"]);
                    _objFarmerData.orderId = ds.Tables[1].Rows[i]["orderId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["orderId"]);
                    _objFarmerData.ProductName = ds.Tables[1].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["ProductName"]);
                    _objFarmerData.Quantity = ds.Tables[1].Rows[i]["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[i]["Quantity"]);
                    _objFarmerData.DelieveredAmount = ds.Tables[1].Rows[i]["DelieveredAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[1].Rows[i]["DelieveredAmount"]);
                    _objFarmerData.DeliveryDate = ds.Tables[1].Rows[i]["DeliveryDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["DeliveryDate"]).Substring(0, 10);
                    _objFarmerData.DeliveryDate = (_objFarmerData.DeliveryDate.Trim().Split('/')[1]+"/"+ _objFarmerData.DeliveryDate.Trim().Split('/')[0]+"/"+ _objFarmerData.DeliveryDate.Trim().Split('/')[2]);
                    _objFarmerData.PaymentStatus = Convert.ToInt32(ds.Tables[1].Rows[i]["OrderStatus"]) == 0 ? false : Convert.ToInt32(ds.Tables[1].Rows[i]["OrderStatus"]) == 2?false: true;
                    _objFarmerData.District = ds.Tables[1].Rows[i]["District"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[1].Rows[i]["District"]);
                    _objFarmerDataList.Add(_objFarmerData);
                    if (ds.Tables[1].Rows[i]["OrderStatus"].ToString() == "2")
                    {
                        _objFarmerCancellDataList.Add(_objFarmerData);
                    }
                    if (ds.Tables[1].Rows[i]["OrderStatus"].ToString() == "5") { _objFarmerDeliveredDataList.Add(_objFarmerData); }
                }
                _sathiOrderDetails.SathiOrderDetail = _objData;
                _sathiOrderDetails.SathiOrderDetail.BookedOrder = _bookedOrder;
                _sathiOrderDetails.SathiOrderDetail.DeliveredOrder = _deliveredOrder;
                _sathiOrderDetails.SathiOrderDetail.CancelledOrder = _cancelledOrder;
                _sathiOrderDetails.SathiOrderDetail.BookedOrder.FarmerOrderDetails = _objFarmerDataList;

                _sathiOrderDetails.SathiOrderDetail.DeliveredOrder.FarmerOrderDetails = _objFarmerDeliveredDataList;
                _sathiOrderDetails.SathiOrderDetail.CancelledOrder.FarmerOrderDetails = _objFarmerCancellDataList;
            }
            return _sathiOrderDetails;
        }

        public SathiPaymentDetails GetSathiPaymentReport(string apikey, int UserId, int DateMode, DateTime? SDATE, DateTime? EDATE)
        {
            SathiPaymentDetails objPaymentDetail = new SathiPaymentDetails();


            DataSet ds = new SathiOrderDetailsDal().GetSathiPaymentReport(apikey, UserId, DateMode, SDATE, EDATE);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<SathiPaymentDetail> PaymentList = new List<SathiPaymentDetail>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SathiPaymentDetail _objData = new SathiPaymentDetail();

                    //_objData.SathiName = ds.Tables[0].Rows[i]["SathiName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SathiName"]);
                    _objData.PaymentDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["PaymentDate"]);
                    _objData.Amount = ds.Tables[0].Rows[i]["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]);
                    _objData.TxnId = ds.Tables[0].Rows[i]["TxnId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TxnId"]);

                    PaymentList.Add(_objData);
                }
                objPaymentDetail.SathiName = ds.Tables[0].Rows[0]["SathiName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[0]["SathiName"]);

                objPaymentDetail.SathiPaymentDetail = PaymentList;
            }
            return objPaymentDetail;
        }
    }
}