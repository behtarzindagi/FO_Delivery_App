using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;
using Entity;
using Helper;
using System.Configuration;
using System.Reflection;

namespace BusinessLayer
{
    public class ReasonStatusBal
    {
        ReasonStatusDal _rsdal;
        public ReasonStatusBal()
        {
            _rsdal = new ReasonStatusDal();

        }

        #region Login, Tracking, Leave 
        public Dictionary<string, string> FOLogin(string userName, string password)
        {

            string UserID = "0", Role = "0", FO_Name = "", DistrictID = "0", District = "", VehicleTypeID = "", RuleId = "";//Changed by Arpit
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            try
            {
                string encodepassword = Encode(password);
                DataSet DS = _rsdal.FOLogin(userName, encodepassword);
                if (DS != null)
                {
                    if (DS.Tables[0] != null && DS.Tables[0].Rows.Count > 0)
                    {
                        UserID = DS.Tables[0].Rows[0]["UserID"].ToString();
                        Role = DS.Tables[0].Rows[0]["Role"].ToString();

                        FO_Name = DS.Tables[0].Rows[0]["FO_Name"].ToString();
                        DistrictID = DS.Tables[0].Rows[0]["DistrictID"].ToString();
                        District = DS.Tables[0].Rows[0]["DistrictName"].ToString();
                        VehicleTypeID = DS.Tables[0].Rows[0]["VehicleTypeID"].ToString();//Changed by Arpit
                        RuleId = DS.Tables[0].Rows[0]["RuleId"].ToString();//Changed by Arpit
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                returndata.Add("UserID", UserID);
                returndata.Add("Role", Role);
                returndata.Add("FO_Name", FO_Name);
                returndata.Add("DistrictID", DistrictID);
                returndata.Add("District", District);
                returndata.Add("VehicleTypeID", VehicleTypeID);//Changed by Arpit
                returndata.Add("RuleId", RuleId);//Changed by Arpit
            }

            return returndata;
        }

        public int ChangePassword(int userid, string password, string newpassword)
        {
            int flag = 0;

            string encodepassword = Encode(password);
            string encodenewpassword = Encode(newpassword);
            flag = _rsdal.ChangePassword(userid, encodepassword, encodenewpassword);
            if (flag > 0) { flag = 1; }
            return flag;
        }
        #region User PassWordEncode
        private string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        #endregion

        public int InsertLatLong(int userid, string lat, string longitude, int tstatus)
        {
            int flag = _rsdal.InsertLatLong(userid, lat, longitude, tstatus);
            return flag;
        }

        public int FO_LeaveMark(int userid)
        {
            int flag = _rsdal.FO_LeaveMark(userid);
            return flag;
        }

        public int FO_CashPendingLeaveMark()
        {
            int flag = 0;
            DataSet DS = _rsdal.FO_CashPendingLeaveMark();
            if (DS != null)
            {
                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    string msg = ConfigurationManager.AppSettings["CashFCMMsg"];
                    string title = ConfigurationManager.AppSettings["CashFCMTitle"];
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        CommonBal.SendNotification(DS.Tables[0].Rows[i]["FCMId"].ToString(), msg, Convert.ToInt32(DS.Tables[0].Rows[i]["UserID"]), title);
                    }
                }
            }
            return flag;
        }

        public int FoNotUpdateApp()
        {
            int  flag=0;
            try {
                if (ConfigurationManager.AppSettings["FoNotUpdateApp"].ToString() == "1")
                {
                    DataSet DS = _rsdal.FoNotUpdateApp();
                    if (DS != null)
                    {
                        if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0 && DS.Tables[1].Rows.Count > 0)
                        {
                            var sendToList = DS.Tables[1].Rows[0]["ToList"].ToString();
                            var sendCCList = DS.Tables[1].Rows[0]["CCList"].ToString();

                            string fileName = DS.Tables[1].Rows[0]["Date"].ToString();

                            var mailSubject = DS.Tables[1].Rows[0]["Subject"].ToString();
                            var mailBody = DS.Tables[1].Rows[0]["Body"].ToString();

                            ReportInXMail.SendMailWithFoNotUpdateAppData(DS.Tables[0], sendToList, sendCCList, mailSubject, mailBody, fileName, 0);

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);

            }
            return flag;
        }

        #region Event Tracking
        public int EventTracking_SelectOrder(int userid, string orderIds, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_SelectOrder(userid, orderIds, lat, longitude);
            return flag;
        }
        public int EventTracking_PickReviseOrder(int userid, int dealerId, int orderId, string type, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_PickReviseOrder(userid, dealerId, orderId, type, lat, longitude);
            return flag;
        }
        public int EventTracking_StartDelivery(int userid, int tripId, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_StartDelivery(userid, tripId, lat, longitude);
            return flag;
        }
        public int EventTracking_ActionAtOrder(int userid, int orderId, string action, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_ActionAtOrder(userid, orderId, action, lat, longitude);
            return flag;
        }
        public int EventTracking_ProductReturn(int userid, int orderId, string recordIds, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_ProductReturn(userid, orderId, recordIds, lat, longitude);
            return flag;
        }
        public int EventTracking_DataCollMarket(int userid, string type, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_DataCollMarket(userid, type, lat, longitude);
            return flag;
        }
        public int EventTracking_ReprocessOrder(int userid, int orderId, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_ReprocessOrder(userid, orderId, lat, longitude);
            return flag;
        }
        public int EventTracking_BookNewOrder(int userid, int farmerId, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_BookNewOrder(userid, farmerId, lat, longitude);
            return flag;
        }
        public int EventTracking_PackageDealerChange(int userid, int tripid, int recordid, int olddealerid, int newdealerid, string lat, string longitude)
        {
            int flag = _rsdal.EventTracking_PackageDealerChange(userid, tripid, recordid, olddealerid, newdealerid, lat, longitude);
            return flag;
        }

        #endregion
        #endregion

        public ReasonViewModel GetReasons()
        {
            DataSet ds = _rsdal.GetReasons();

            ReasonViewModel a = new ReasonViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<ReasonModel> p = new List<ReasonModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReasonModel Reason = new ReasonModel();
                        Reason.ReasonId = ds.Tables[0].Rows[i]["Id"].ToString();
                        Reason.ReasonName = ds.Tables[0].Rows[i]["name"].ToString();
                        p.Add(Reason);
                    }
                    a.CancelReason = p;
                }
            }
            if (ds != null && ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    List<ReasonModel> p = new List<ReasonModel>();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        ReasonModel Reason = new ReasonModel();
                        Reason.ReasonId = ds.Tables[1].Rows[i]["Id"].ToString();
                        Reason.ReasonName = ds.Tables[1].Rows[i]["name"].ToString();
                        p.Add(Reason);
                    }
                    a.PendingReason = p;
                }
            }

            if (ds != null && ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    List<ReasonModel> p = new List<ReasonModel>();
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        ReasonModel Reason = new ReasonModel();
                        Reason.ReasonId = ds.Tables[2].Rows[i]["Id"].ToString();
                        Reason.ReasonName = ds.Tables[2].Rows[i]["name"].ToString();
                        p.Add(Reason);
                    }

                    a.ModifyReason = p;
                }
            }

            if (ds != null && ds.Tables[3] != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    List<ReasonModel> p = new List<ReasonModel>();
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        ReasonModel Reason = new ReasonModel();
                        Reason.ReasonId = ds.Tables[3].Rows[i]["Id"].ToString();
                        Reason.ReasonName = ds.Tables[3].Rows[i]["name"].ToString();
                        p.Add(Reason);
                    }

                    a.ModifyReasonForDealer = p;
                }
            }
            if (ds != null && ds.Tables[4] != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    List<ReasonModel> p = new List<ReasonModel>();
                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        ReasonModel Reason = new ReasonModel();
                        Reason.ReasonId = ds.Tables[4].Rows[i]["Id"].ToString();
                        Reason.ReasonName = ds.Tables[4].Rows[i]["name"].ToString();
                        p.Add(Reason);
                    }

                    a.DealerChangeReason = p;
                }
            }
            if (ds != null && ds.Tables[5] != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    List<ReasonModel> p = new List<ReasonModel>();
                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                    {
                        ReasonModel Reason = new ReasonModel();
                        Reason.ReasonId = ds.Tables[5].Rows[i]["Id"].ToString();
                        Reason.ReasonName = ds.Tables[5].Rows[i]["name"].ToString();
                        p.Add(Reason);
                    }

                    a.TransportReason = p;
                }
            }
            return a;
        }

        public OrderViewModel GetOrders(string userid)
        {
            DataSet ds = _rsdal.GetOrders(userid);

            OrderViewModel a = new OrderViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<OrderModel> p = new List<OrderModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderModel Reason = new OrderModel();
                        Reason.OrderId = ds.Tables[0].Rows[i]["Id"].ToString();
                        Reason.OrderName = ds.Tables[0].Rows[i]["ref_no"].ToString();
                        Reason.Village = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.Block = ds.Tables[0].Rows[i]["BlockName"].ToString();
                        Reason.Distributor = ds.Tables[0].Rows[i]["DistributorName"].ToString();
                        p.Add(Reason);
                    }
                    a.OrderList = p;
                }
            }
            return a;
        }

        public int UpdateOrders(string orderid, string userid)
        {
            int flag = _rsdal.UpdateOrders(orderid, userid);


            return flag;
        }

        public OrderTripViewModel GetOrdersByTrip(string tripid)
        {
            DataSet ds = _rsdal.GetOrdersByTrip(tripid);

            OrderTripViewModel a = new OrderTripViewModel();

            if (ds != null && ds.Tables[0] != null && ds != null && ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    List<OrderTripModel> p = new List<OrderTripModel>();
                    List<DealerOrderTripModel> d = new List<DealerOrderTripModel>();
                         
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderTripModel Reason = new OrderTripModel();
                        Reason.OrderID = ds.Tables[0].Rows[i]["Id"].ToString();
                        Reason.OrderRefNo = ds.Tables[0].Rows[i]["ref_no"].ToString();
                        Reason.Status = ds.Tables[0].Rows[i]["OrderStatus"].ToString();
                        Reason.Village = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.VillageToUse = ds.Tables[0].Rows[i]["VillageToUse"].ToString();
                        Reason.VillageMapflag = Convert.ToInt32(ds.Tables[0].Rows[i]["villageMapflag"].ToString());
                        Reason.Farmer = ds.Tables[0].Rows[i]["FarmerName"].ToString();
                        Reason.CashCollected = ds.Tables[0].Rows[i]["CashCollected"].ToString();
                        Reason.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                        Reason.OrderReprocessFlag = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderReprocess"].ToString());

                        p.Add(Reason);
                    }
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DealerOrderTripModel Reason = new DealerOrderTripModel();
                        Reason.DealerID = ds.Tables[1].Rows[i]["DealerID"].ToString();
                        Reason.DealerName = ds.Tables[1].Rows[i]["DealerName"].ToString();
                        Reason.DealerAddress = ds.Tables[1].Rows[i]["DealerAddress"].ToString();
                        

                        d.Add(Reason);
                    }
                    a.OrderList = p;
                    a.DealerList = d;
                }
            }
            return a;
        }

        public ProductViewModel GetProductsByOrderId(string orderid, string distributorid)
        {
            DataSet ds = _rsdal.GetProductsByOrderId(orderid, distributorid);

            ProductViewModel a = new ProductViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<ProductModel> p = new List<ProductModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ProductModel Reason = new ProductModel();
                        Reason.RecordId = ds.Tables[0].Rows[i]["RecordID"].ToString();
                        Reason.ProductId = ds.Tables[0].Rows[i]["ProductID"].ToString();
                        Reason.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        Reason.Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        Reason.PackageId = ds.Tables[0].Rows[i]["PackageID"].ToString();
                        Reason.PackageName = ds.Tables[0].Rows[i]["Package"].ToString();
                        p.Add(Reason);
                    }
                    a.ProductList = p;
                }
            }
            return a;
        }

        public PackageViewModel GetPackagesByProductId(string productid)
        {
            DataSet ds = _rsdal.GetPackagesByProductId(productid);

            PackageViewModel a = new PackageViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<PackageModel> p = new List<PackageModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PackageModel Reason = new PackageModel();
                        Reason.PackageId = ds.Tables[0].Rows[i]["PackageID"].ToString();
                        Reason.PackageName = ds.Tables[0].Rows[i]["Package"].ToString();
                        p.Add(Reason);
                    }
                    a.PackageList = p;
                }
            }
            return a;
        }

        public Distributor GetDistributorOrdersByTrip(string tripid)
        {
            Distributor dsbtr = new Distributor();
            DataSet ds = _rsdal.GetDistributorOrdersByTrip(tripid);

            DistributorViewModel p1 = new DistributorViewModel();

            var p1list = new List<OrderDistributor>();
            List<OrderDistributorModel> k2list = null;

            string distributorIdTemp = "";

            OrderDistributor k = null;// = new OrderDistributor();// Change
            OrderDistributorViewModel k1 = null;//= new OrderDistributorViewModel();// Change

            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int q = 0; q < ds.Tables[0].Rows.Count; q++)
                    {

                        if (q == 0)
                        {
                            distributorIdTemp = ds.Tables[0].Rows[q]["DealerID"].ToString();
                            k2list = new List<OrderDistributorModel>();

                            k = new OrderDistributor();//Change
                            k1 = new OrderDistributorViewModel();//Change
                        }

                        if (distributorIdTemp == ds.Tables[0].Rows[q]["DealerID"].ToString())
                        {
                            OrderDistributorModel k2 = new OrderDistributorModel();
                            k2.OrderID = ds.Tables[0].Rows[q]["OrderID"].ToString();
                            k2.OrderRefNo = ds.Tables[0].Rows[q]["OrderRefNo"].ToString();
                            k2.InvoiceNo = ds.Tables[0].Rows[q]["Invoice_no"].ToString();
                            k2.OrderStatus = ds.Tables[0].Rows[q]["OrderStatus"].ToString();
                            k2.FarmerAddress = ds.Tables[0].Rows[q]["FarmerAddress"].ToString();
                            k2.Amount = ds.Tables[0].Rows[q]["Amount"].ToString();

                            k2list.Add(k2);
                        }
                        else
                        {
                            k1.OrderList = k2list;

                            k.DistributorId = ds.Tables[0].Rows[q - 1]["DealerID"].ToString();
                            k.DistributorName = ds.Tables[0].Rows[q - 1]["DealerName"].ToString();
                            k.DistributorAddress = ds.Tables[0].Rows[q - 1]["DealerAddress"].ToString();

                            k.IsPrime = Convert.ToInt32(ds.Tables[0].Rows[q - 1]["IsPrime"]);
                            k.IsAppAccess = Convert.ToInt32(ds.Tables[0].Rows[q - 1]["IsAppAccess"]);

                            k.Orders = k1;
                            p1list.Add(k);
                            k = new OrderDistributor();
                            k1 = new OrderDistributorViewModel();

                            distributorIdTemp = ds.Tables[0].Rows[q]["DealerID"].ToString();
                            k2list = new List<OrderDistributorModel>();
                            OrderDistributorModel k2 = new OrderDistributorModel();
                            k2.OrderID = ds.Tables[0].Rows[q]["OrderID"].ToString();
                            k2.OrderRefNo = ds.Tables[0].Rows[q]["OrderRefNo"].ToString();
                            k2.InvoiceNo = ds.Tables[0].Rows[q]["Invoice_no"].ToString();
                            k2.OrderStatus = ds.Tables[0].Rows[q]["OrderStatus"].ToString();
                            k2.FarmerAddress = ds.Tables[0].Rows[q]["FarmerAddress"].ToString();
                            k2.Amount = ds.Tables[0].Rows[q]["Amount"].ToString();

                            k2list.Add(k2);
                        }

                    }

                    k1.OrderList = k2list;
                    k.DistributorId = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["DealerID"].ToString();
                    k.DistributorName = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["DealerName"].ToString();
                    k.DistributorAddress = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["DealerAddress"].ToString();

                    k.IsPrime = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["IsPrime"]);
                    k.IsAppAccess = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["IsAppAccess"]);

                    k.Orders = k1;
                    p1list.Add(k);

                    p1.DistributorList = p1list;
                    dsbtr.Distributors = p1;
                }
            }

            return dsbtr;
        }
        public int NotificationForReviseOrder(string userid, string recordids, string productids, string reasonid, string remark)
        {
            int flag = _rsdal.NotificationForReviseOrder(userid, recordids, productids, reasonid, remark);


            return flag;
        }

        public int NotificationForCancelPendingOrderByFarmer(string userid, string orderid, int statusid, string reasonid, string remark)
        {
            int flag = _rsdal.NotificationForCancelPendingOrderByFarmer(userid, orderid, statusid, reasonid, remark);


            return flag;
        }

        public DataSet GetDealerMobile(string userid, string orderid, int statusid, string reasonid)
        {
            DataSet ds = _rsdal.GetDealerMobile(userid, orderid, statusid, reasonid);

            return ds;
        }

        public int UpdateInvoiceOrder(string userid, string orderid, int dealerid, string invoiceno)
        {
            int flag = _rsdal.UpdateInvoiceOrder(userid, orderid, dealerid, invoiceno);
            if (flag > 0)
            {
                #region Send SMS to Farmer at Order Delivery

                if (ConfigurationManager.AppSettings["Farmersmsflag"] == "1")
                {
                    var DS = GetFarmerDetail(userid, orderid.ToString(), "P");
                    if (DS != null)
                    {
                        if (DS.Tables.Count > 0)
                        {
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                                {
                                    string mobile = DS.Tables[0].Rows[i]["MobNo"].ToString();
                                    string Name = DS.Tables[0].Rows[i]["Name"].ToString();
                                    string AlternateMobNo = DS.Tables[0].Rows[i]["AlternateMobNo"].ToString();
                                    string FO = DS.Tables[0].Rows[i]["FO_Name"].ToString();
                                    string FO_Mobile = DS.Tables[0].Rows[i]["FOMobileNo"].ToString();
                                    string OrderRef = DS.Tables[0].Rows[i]["OrderRefNo"].ToString();
                                    string msg = DS.Tables[0].Rows[i]["Msg"].ToString();

                                    msg = msg.Replace("{OrderRef}", OrderRef);
                                    msg = msg.Replace("{FOName}", FO);
                                    msg = msg.Replace("{FOMobile}", FO_Mobile);

                                    if (mobile != "0")
                                    {
                                        SmsHelper.MessageHindiSend(Convert.ToInt32(userid), mobile, msg);
                                    }
                                }
                            }
                        }
                    }

                }
                #endregion
            }
            return flag;
        }

        #region Return TO Dealer Items
        public OrderReturn GetDistributorReturnOrdersByTrip(string tripid, int userid)
        {
            string distributorIdTemp = "";
            string orderIdTemp = "";
            string smsProductList = "";

            DataSet ds = _rsdal.GetDistributorReturnOrdersByTrip(tripid, userid);

            OrderReturn ord = new OrderReturn();
            List<OrderReturnModel> ordlist = null;
            OrderReturnViewModel ordView = null;
            List<OrderReturnViewModel> disord = new List<OrderReturnViewModel>();
            OrderReturnModel ordReturn = null;
            OrderReturItemModel ordItem = null;
            List<OrderReturItemModel> ordItemlist = null;
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int rows = ds.Tables[0].Rows.Count;

                    for (int q = 0; q < ds.Tables[0].Rows.Count; q++)
                    {

                        if (q == 0)
                        {
                            distributorIdTemp = ds.Tables[0].Rows[q]["DealerID"].ToString();
                            ordlist = new List<OrderReturnModel>();
                            ordView = new OrderReturnViewModel();

                            //  orderIdTemp = ds.Tables[0].Rows[q]["OrderID"].ToString();
                        }

                        if (distributorIdTemp == ds.Tables[0].Rows[q]["DealerID"].ToString())
                        {
                            ordReturn = new OrderReturnModel();

                            ordReturn.OrderId = ds.Tables[0].Rows[q]["OrderID"].ToString();
                            ordReturn.OrderRef = ds.Tables[0].Rows[q]["OrderRefNo"].ToString();
                            ordReturn.Invoice_No = ds.Tables[0].Rows[q]["Invoice_no"].ToString();
                            ordReturn.OTP = ds.Tables[0].Rows[q]["OTP"].ToString();

                            if (orderIdTemp == ds.Tables[0].Rows[q]["OrderID"].ToString())
                            {
                                ordItem = new OrderReturItemModel();

                                ordItem.ProductId = ds.Tables[0].Rows[q]["ProductID"].ToString();
                                ordItem.ProductName = ds.Tables[0].Rows[q]["ProductName"].ToString();
                                ordItem.PackageId = ds.Tables[0].Rows[q]["PackageID"].ToString();
                                ordItem.PackageName = ds.Tables[0].Rows[q]["PackageName"].ToString();
                                ordItem.RecordId = ds.Tables[0].Rows[q]["RecordID"].ToString();
                                ordItem.Qty = ds.Tables[0].Rows[q]["Quantity"].ToString();
                                ordItem.ReturnedToDealer = ds.Tables[0].Rows[q]["ReturnedToDealer"].ToString();
                                ordItemlist.Add(ordItem);
                                ordlist.RemoveAt(ordlist.Count - 1);//temp ashish
                            }
                            else if (orderIdTemp != ds.Tables[0].Rows[q]["OrderID"].ToString())
                            {
                                if (orderIdTemp != "")
                                {
                                    #region SMS send Order wise
                                    string dealer_Mobile1 = ds.Tables[0].Rows[q - 1]["mobile"].ToString();
                                    string smscontent1 = ds.Tables[1].Rows[0][0].ToString();

                                    smscontent1 = smscontent1.Replace("{product_list}", smsProductList);
                                    smscontent1 = smscontent1.Replace("OTP", "OTP " + ordReturn.OTP);
                                    smscontent1 = smscontent1.Replace("{orderref}", ordReturn.OrderRef);
                                    smscontent1 = smscontent1.Replace("{Fo_Name}", ds.Tables[0].Rows[rows - 1]["FO"].ToString());//{Fo_Name} {orderref}

                                    if (ConfigurationManager.AppSettings["Dealersmsflag"] == "1")
                                    {
                                        if (dealer_Mobile1 != "0")
                                        {
                                            SmsHelper.MessageEnglishSend(1, dealer_Mobile1, smscontent1);
                                        }
                                    }

                                    #endregion
                                }
                                ordItem = new OrderReturItemModel();
                                ordItemlist = new List<OrderReturItemModel>();


                                orderIdTemp = ds.Tables[0].Rows[q]["OrderID"].ToString();

                                ordItem.ProductId = ds.Tables[0].Rows[q]["ProductID"].ToString();
                                ordItem.ProductName = ds.Tables[0].Rows[q]["ProductName"].ToString();
                                ordItem.PackageId = ds.Tables[0].Rows[q]["PackageID"].ToString();
                                ordItem.PackageName = ds.Tables[0].Rows[q]["PackageName"].ToString();
                                ordItem.RecordId = ds.Tables[0].Rows[q]["RecordID"].ToString();
                                ordItem.Qty = ds.Tables[0].Rows[q]["Quantity"].ToString();
                                ordItem.ReturnedToDealer = ds.Tables[0].Rows[q]["ReturnedToDealer"].ToString();
                                ordItemlist.Add(ordItem);
                            }
                            ordReturn.Item = ordItemlist;

                            ordlist.Add(ordReturn);

                            smsProductList += ordItem.ProductName + " " + ds.Tables[0].Rows[q]["PackageName"].ToString() + " " + ordItem.Qty + "Bag, ";

                        }
                        else
                        {
                            ordView.DistributorId = ds.Tables[0].Rows[q - 1]["DealerID"].ToString();
                            ordView.DistributorName = ds.Tables[0].Rows[q - 1]["DealerName"].ToString();
                            // ordView.OTP = ds.Tables[0].Rows[q - 1]["OTP"].ToString();
                            // ordView.OTP = "123";
                            ordView.OrderList = ordlist;
                            disord.Add(ordView);

                            string dealer_Mobile = ds.Tables[0].Rows[q - 1]["mobile"].ToString();
                            string smscontent = ds.Tables[1].Rows[0][0].ToString();
                            smscontent = smscontent.Replace("{product_list}", smsProductList);
                            smscontent = smscontent.Replace("OTP", "OTP " + ordReturn.OTP);
                            smscontent = smscontent.Replace("{orderref}", ordReturn.OrderRef);
                            smscontent = smscontent.Replace("{Fo_Name}", ds.Tables[0].Rows[q - 1]["FO"].ToString());//{Fo_Name}
                            smsProductList = "";
                            if (ConfigurationManager.AppSettings["Dealersmsflag"] == "1")
                            {
                                if (dealer_Mobile != "0")
                                {
                                    SmsHelper.MessageEnglishSend(1, dealer_Mobile, smscontent);
                                }
                            }

                            ordlist = new List<OrderReturnModel>();
                            ordView = new OrderReturnViewModel();
                            distributorIdTemp = ds.Tables[0].Rows[q]["DealerID"].ToString();
                            ordReturn = new OrderReturnModel();

                            ordReturn.OrderId = ds.Tables[0].Rows[q]["OrderID"].ToString();
                            ordReturn.OrderRef = ds.Tables[0].Rows[q]["OrderRefNo"].ToString();
                            ordReturn.Invoice_No = ds.Tables[0].Rows[q]["Invoice_no"].ToString();
                            ordReturn.OTP = ds.Tables[0].Rows[q]["OTP"].ToString();

                            ordItem = new OrderReturItemModel();
                            ordItemlist = new List<OrderReturItemModel>();
                            orderIdTemp = ds.Tables[0].Rows[q]["OrderID"].ToString();

                            ordItem.ProductId = ds.Tables[0].Rows[q]["ProductID"].ToString();
                            ordItem.ProductName = ds.Tables[0].Rows[q]["ProductName"].ToString();
                            ordItem.PackageId = ds.Tables[0].Rows[q]["PackageID"].ToString();
                            ordItem.PackageName = ds.Tables[0].Rows[q]["PackageName"].ToString();
                            ordItem.RecordId = ds.Tables[0].Rows[q]["RecordID"].ToString();
                            ordItem.Qty = ds.Tables[0].Rows[q]["Quantity"].ToString();
                            ordItem.ReturnedToDealer = ds.Tables[0].Rows[q]["ReturnedToDealer"].ToString();
                            ordItemlist.Add(ordItem);
                            ordReturn.Item = ordItemlist;
                            ordlist.Add(ordReturn);

                            smsProductList += ordItem.ProductName + " " + ds.Tables[0].Rows[q]["PackageName"].ToString() + " " + ordItem.Qty + "Bag, ";
                        }
                    }

                    ordView.DistributorId = ds.Tables[0].Rows[rows - 1]["DealerID"].ToString();
                    ordView.DistributorName = ds.Tables[0].Rows[rows - 1]["DealerName"].ToString();
                    //ordView.OTP = ds.Tables[0].Rows[rows - 1]["OTP"].ToString();

                    ordView.OrderList = ordlist;
                    disord.Add(ordView);

                    //string dealer_Mobile1 = ds.Tables[0].Rows[rows - 1]["mobile"].ToString();
                    //string smscontent1 = ds.Tables[1].Rows[0][0].ToString();

                    //smscontent1 = smscontent1.Replace("{product_list}", smsProductList);
                    //smscontent1 = smscontent1.Replace("OTP", "OTP " + ordView.OTP);
                    //smscontent1 = smscontent1.Replace("{Fo_Name}", ds.Tables[0].Rows[rows - 1]["FO"].ToString());//{Fo_Name}

                    //if (ConfigurationManager.AppSettings["Dealersmsflag"] == "1")
                    //{
                    //    if (dealer_Mobile1 != "0")
                    //    {
                    //        SmsHelper.MessageEnglishSend(1, dealer_Mobile1, smscontent1);
                    //    }
                    //}
                    ord.DisOrders = disord;
                }
            }
            return ord;
        }

        public int UpdateReturnOrder(string userid, int dealerid, string tripid, int orderid, string recordids)
        {
            int flag = _rsdal.UpdateReturnOrder(userid, dealerid, tripid, orderid, recordids);


            return flag;
        }

        public TripReturn GetDistributorReturnOrdersByFOid(int userid)
        {
            string distributorIdTemp = "";
            string orderIdTemp = "";
            string smsProductList = "";

            DataSet ds = _rsdal.GetDistributorReturnOrdersByFOid(userid);

            TripReturn ord = new TripReturn();
            List<TripReturnModel> ordlist = null;
            TripReturnViewModel ordView = null;
            List<TripReturnViewModel> disord = new List<TripReturnViewModel>();
            TripReturnModel ordReturn = null;
            TripReturItemModel ordItem = null;
            List<TripReturItemModel> ordItemlist = null;
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int rows = ds.Tables[0].Rows.Count;

                    for (int q = 0; q < ds.Tables[0].Rows.Count; q++)
                    {

                        if (q == 0)
                        {
                            distributorIdTemp = ds.Tables[0].Rows[q]["DealerID"].ToString();
                            ordlist = new List<TripReturnModel>();
                            ordView = new TripReturnViewModel();

                            //  orderIdTemp = ds.Tables[0].Rows[q]["OrderID"].ToString();
                        }

                        if (distributorIdTemp == ds.Tables[0].Rows[q]["DealerID"].ToString())
                        {
                            ordReturn = new TripReturnModel();

                            ordReturn.OrderId = ds.Tables[0].Rows[q]["OrderID"].ToString();
                            ordReturn.OrderRef = ds.Tables[0].Rows[q]["OrderRefNo"].ToString();
                            ordReturn.Invoice_No = ds.Tables[0].Rows[q]["Invoice_no"].ToString();
                            ordReturn.CancelDate = ds.Tables[0].Rows[q]["Cancel_Date"].ToString();

                            if (orderIdTemp == ds.Tables[0].Rows[q]["OrderID"].ToString())
                            {
                                ordItem = new TripReturItemModel();

                                ordItem.ProductId = ds.Tables[0].Rows[q]["ProductID"].ToString();
                                ordItem.ProductName = ds.Tables[0].Rows[q]["ProductName"].ToString();
                                ordItem.PackageId = ds.Tables[0].Rows[q]["PackageID"].ToString();
                                ordItem.PackageName = ds.Tables[0].Rows[q]["PackageName"].ToString();
                                ordItem.RecordId = ds.Tables[0].Rows[q]["RecordID"].ToString();
                                ordItem.Qty = ds.Tables[0].Rows[q]["Quantity"].ToString();
                                ordItem.ReturnedToDealer = ds.Tables[0].Rows[q]["ReturnedToDealer"].ToString();
                                
                                ordItemlist.Add(ordItem);
                                ordlist.RemoveAt(ordlist.Count - 1);//temp ashish
                            }
                            else if (orderIdTemp != ds.Tables[0].Rows[q]["OrderID"].ToString())
                            {
                                if (orderIdTemp != "")
                                {
                                   
                                }
                                ordItem = new TripReturItemModel();
                                ordItemlist = new List<TripReturItemModel>();


                                orderIdTemp = ds.Tables[0].Rows[q]["OrderID"].ToString();

                                ordItem.ProductId = ds.Tables[0].Rows[q]["ProductID"].ToString();
                                ordItem.ProductName = ds.Tables[0].Rows[q]["ProductName"].ToString();
                                ordItem.PackageId = ds.Tables[0].Rows[q]["PackageID"].ToString();
                                ordItem.PackageName = ds.Tables[0].Rows[q]["PackageName"].ToString();
                                ordItem.RecordId = ds.Tables[0].Rows[q]["RecordID"].ToString();
                                ordItem.Qty = ds.Tables[0].Rows[q]["Quantity"].ToString();
                                ordItem.ReturnedToDealer = ds.Tables[0].Rows[q]["ReturnedToDealer"].ToString();
                                
                                ordItemlist.Add(ordItem);
                            }
                            ordReturn.Item = ordItemlist;

                            ordlist.Add(ordReturn);

                            smsProductList += ordItem.ProductName + " " + ds.Tables[0].Rows[q]["PackageName"].ToString() + " " + ordItem.Qty + "Bag, ";

                        }
                        else
                        {
                            ordView.DistributorId = ds.Tables[0].Rows[q - 1]["DealerID"].ToString();
                            ordView.DistributorName = ds.Tables[0].Rows[q - 1]["DealerName"].ToString();
                            // ordView.OTP = ds.Tables[0].Rows[q - 1]["OTP"].ToString();
                            // ordView.OTP = "123";
                            ordView.OrderList = ordlist;
                            disord.Add(ordView);

                            string dealer_Mobile = ds.Tables[0].Rows[q - 1]["mobile"].ToString();
                            string smscontent = ds.Tables[1].Rows[0][0].ToString();
                            smscontent = smscontent.Replace("{product_list}", smsProductList);
                            smscontent = smscontent.Replace("{orderref}", ordReturn.OrderRef);
                            smscontent = smscontent.Replace("{Fo_Name}", ds.Tables[0].Rows[q - 1]["FO"].ToString());//{Fo_Name}
                            smsProductList = "";

                            ordlist = new List<TripReturnModel>();
                            ordView = new TripReturnViewModel();
                            distributorIdTemp = ds.Tables[0].Rows[q]["DealerID"].ToString();
                            ordReturn = new TripReturnModel();

                            ordReturn.OrderId = ds.Tables[0].Rows[q]["OrderID"].ToString();
                            ordReturn.OrderRef = ds.Tables[0].Rows[q]["OrderRefNo"].ToString();
                            ordReturn.Invoice_No = ds.Tables[0].Rows[q]["Invoice_no"].ToString();

                            ordItem = new TripReturItemModel();
                            ordItemlist = new List<TripReturItemModel>();
                            orderIdTemp = ds.Tables[0].Rows[q]["OrderID"].ToString();

                            ordItem.ProductId = ds.Tables[0].Rows[q]["ProductID"].ToString();
                            ordItem.ProductName = ds.Tables[0].Rows[q]["ProductName"].ToString();
                            ordItem.PackageId = ds.Tables[0].Rows[q]["PackageID"].ToString();
                            ordItem.PackageName = ds.Tables[0].Rows[q]["PackageName"].ToString();
                            ordItem.RecordId = ds.Tables[0].Rows[q]["RecordID"].ToString();
                            ordItem.Qty = ds.Tables[0].Rows[q]["Quantity"].ToString();
                            ordItem.ReturnedToDealer = ds.Tables[0].Rows[q]["ReturnedToDealer"].ToString();
                            
                            ordItemlist.Add(ordItem);
                            ordReturn.Item = ordItemlist;
                            ordlist.Add(ordReturn);

                            smsProductList += ordItem.ProductName + " " + ds.Tables[0].Rows[q]["PackageName"].ToString() + " " + ordItem.Qty + "Bag, ";
                        }
                    }

                    ordView.DistributorId = ds.Tables[0].Rows[rows - 1]["DealerID"].ToString();
                    ordView.DistributorName = ds.Tables[0].Rows[rows - 1]["DealerName"].ToString();

                    ordView.OrderList = ordlist;
                    disord.Add(ordView);
                    
                    ord.DisOrders = disord;
                }
            }
            return ord;
        }
        #endregion

        public CashModel GetFoCashCollect(string userid)
        {
            CashModel cash = new CashModel();
            DataSet ds = _rsdal.GetFoCashCollect(userid);

            if (ds != null && ds.Tables[0] != null && ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {


                    cash.CashCollectToday = ds.Tables[0].Rows[0]["cash_collect_today"].ToString();
                    cash.TotalCashInHand = ds.Tables[1].Rows[0]["total_cash_inhand"].ToString();
                    cash.TotalClearingAmount = ds.Tables[2].Rows[0]["ToatlClearingAmount"].ToString();

                }
            }
            return cash;
        }

        public int UpdateFoCashDebit(string userid, string amount, string transid)
        {
            int flag = _rsdal.UpdateFoCashDebit(userid, amount, transid);


            return flag;
        }

        public ProductViewModel GetProductsByOrderId(string orderid)
        {
            DataSet ds = _rsdal.GetProductsByOrderId(orderid);

            ProductViewModel a = new ProductViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<ProductModel> p = new List<ProductModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ProductModel Reason = new ProductModel();
                        Reason.RecordId = ds.Tables[0].Rows[i]["RecordID"].ToString();
                        Reason.ProductId = ds.Tables[0].Rows[i]["ProductID"].ToString();
                        Reason.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        Reason.Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        Reason.PackageId = ds.Tables[0].Rows[i]["PackageID"].ToString();
                        Reason.PackageName = ds.Tables[0].Rows[i]["Package"].ToString();
                        p.Add(Reason);
                    }
                    a.ProductList = p;
                }
            }
            return a;
        }

        public int NotificationForReviseOrderByFarmer(string userid, string recordid, int quantity, string productid, string reasonid, string remark)
        {
            int flag = _rsdal.NotificationForReviseOrderByFarmer(userid, recordid, quantity, productid, reasonid, remark);
            return flag;
        }

        public int CheckTodayAllOrdersAttended(string tripid)
        {
            int flag = _rsdal.CheckTodayAllOrdersAttended(tripid);


            return flag;
        }
        public DataSet GetFarmerDetail(string userid, string orderid, string type)
        {
            DataSet ds = _rsdal.GetFarmerDetail(userid, orderid, type);

            return ds;
        }

        public string Get_FO_Stage(int userid)
        {
            string serviceurl = _rsdal.Get_FO_Stage(userid);


            return serviceurl;
        }

        public double GetAmountCollectedAtDelivery(int orderid)
        {
            double amount = _rsdal.GetAmountCollectedAtDelivery(orderid);

            return amount;
        }

        public GetPODOrderDetailViewModel GetPODScreen(int userid, int orderid)
        {

            DataSet ds = _rsdal.GetPODScreen(userid, orderid);
            GetPODOrderDetailViewModel Order = new GetPODOrderDetailViewModel();
            List<GetPODOrderDetailModel> productlist = new List<GetPODOrderDetailModel>();
            GetPODOrderDetailModel product = null;
            if (ds != null && ds.Tables[0] != null && ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {/*
                    RecordID	Quantity	PricePerUnit	Total	ProductID	ProductName	PackageID	PackageName*/
                    Order.OrderRefNo = ds.Tables[0].Rows[0]["OrderRefNo"].ToString();
                    Order.DealerName = ds.Tables[0].Rows[0]["Dealer"].ToString();
                    Order.FarmerId = ds.Tables[0].Rows[0]["FarmerID"].ToString();
                    Order.FarmerName = ds.Tables[0].Rows[0]["FarmerName"].ToString();
                    Order.FarmerAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                    Order.OrderDate = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                    Order.FarmerContact = ds.Tables[0].Rows[0]["MobNo"].ToString();
                    Order.GrandTotal = ds.Tables[0].Rows[0]["GrandTotal"].ToString();
                    Order.DeliveryRemark = ds.Tables[0].Rows[0]["DeliveryRemark"].ToString();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        product = new GetPODOrderDetailModel();

                        product.ProductName = ds.Tables[1].Rows[i]["ProductName"].ToString();
                        product.Package = ds.Tables[1].Rows[i]["PackageName"].ToString();
                        product.Qty = ds.Tables[1].Rows[i]["Quantity"].ToString();
                        product.PricePerUnit = ds.Tables[1].Rows[i]["PricePerUnit"].ToString();
                        product.TotalPrice = ds.Tables[1].Rows[i]["Total"].ToString();
                        product.RecordId = ds.Tables[1].Rows[i]["RecordID"].ToString();
                        product.DiscAmount = ds.Tables[1].Rows[i]["DiscAmt"].ToString();
                        product.AmountAfterDiscount = ds.Tables[1].Rows[i]["NetAmount"].ToString();
                        product.OtherCharges = ds.Tables[1].Rows[i]["OtherCharges"].ToString();
                        //	NetDiscount	NetPrice					
                        product.HSNCode = ds.Tables[1].Rows[i]["HSNcode"].ToString();
                        product.CGST = ds.Tables[1].Rows[i]["CGST"].ToString();
                        product.SGST = ds.Tables[1].Rows[i]["SGST"].ToString();
                        product.TaxValue = ds.Tables[1].Rows[i]["GSTAmout"].ToString();
                        product.UnitPrice = ds.Tables[1].Rows[i]["UnitPrice"].ToString();
                        productlist.Add(product);
                    }
                    Order.ProductList = productlist;


                }
            }
            return Order;
        }

        public int OrderDelivery(int userid, int orderid,  string statusid)
        {
            int flag = _rsdal.OrderDelivery(userid, orderid,  statusid);
            return flag;
        }
        public int OfflinePODUpload(int userid, int orderid, string imageBase64String)
        {
            int flag = _rsdal.OfflinePODUpload(userid, orderid, imageBase64String);


            return flag;
        }

        public int Trip_Transportation_Cost_Insert(int userid, int tripid, int vehicleid, string name, string mobile, string distance, decimal cost, string remark)
        {
            int flag = _rsdal.Trip_Transportation_Cost_Insert(userid, tripid, vehicleid, name, mobile, distance, cost, remark);

            return flag;
        }

        //public List<TransportCostModel> Trip_Transportation_Cost_Select(int userid, int tripid)
        //{

        //    DataSet ds = _rsdal.Trip_Transportation_Cost_Select(userid, tripid);

        //    TransportCostModel transcost = new TransportCostModel();
        //    List<TransportCostModel> transcostlist = new List<TransportCostModel>();

        //    if (ds != null && ds.Tables[0] != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                transcost = new TransportCostModel();
        //                transcost.Id = ds.Tables[0].Rows[i]["Id"].ToString();
        //                transcost.TripId = ds.Tables[0].Rows[i]["TripId"].ToString();
        //                transcost.VehicleID = ds.Tables[0].Rows[i]["VehicleID"].ToString();
        //                transcost.VehicleName = ds.Tables[0].Rows[i]["VehicleName"].ToString();
        //                transcost.TranspoterName = ds.Tables[0].Rows[i]["TranspoterName"].ToString();
        //                transcost.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
        //                transcost.TotalKm = ds.Tables[0].Rows[i]["TotalKm"].ToString();
        //                transcost.TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString();

        //                transcostlist.Add(transcost);
        //            }

        //        }
        //    }
        //    return transcostlist;
        //}
        //public int Trip_Transportation_Cost_Delete(int userid, int id, int tripid)
        //{
        //    int flag = _rsdal.Trip_Transportation_Cost_Delete(userid, id, tripid);

        //    return flag;
        //}

        public int ResetOrdersByTrip(int userid, string tripid)
        {
            int flag = _rsdal.ResetOrdersByTrip(userid, tripid);


            return flag;
        }

        public List<NotificationModel> FSCToFONotification(int userid)
        {

            DataSet ds = _rsdal.FSCToFONotification(userid);

            NotificationModel _notification = new NotificationModel();
            List<NotificationModel> _notificationlist = new List<NotificationModel>();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _notification = new NotificationModel();
                        _notification.NotificationReceiverId = ds.Tables[0].Rows[i]["NotificationReceiversId"].ToString();
                        _notification.NotificationId = ds.Tables[0].Rows[i]["NotificationID"].ToString();
                        _notification.ItemRecordId = ds.Tables[0].Rows[i]["ItemRecordID"].ToString();
                        _notification.Remark = ds.Tables[0].Rows[i]["Remarks"].ToString();

                        _notificationlist.Add(_notification);
                    }
                }
            }
            return _notificationlist;
        }

        public int NotificationReadFlag(int userid, int notificationreceiverid)
        {
            int flag = _rsdal.NotificationReadFlag(userid, notificationreceiverid);


            return flag;
        }

        public int FCMDeviceIdInsert(int userid, string deviceid, string fcmid, string modelName, string imei)
        {
            int flag = _rsdal.FCMDeviceIdInsert(userid, deviceid, fcmid, modelName, imei);
            return flag;
        }

        public int FCMDeviceIdWithoutLoginInsert(string deviceid, string fcmid)
        {
            int flag = _rsdal.FCMDeviceIdWithoutLoginInsert(deviceid, fcmid);
            return flag;
        }

        public int OrderPaymentResponse(int userid, int orderid, string cash, string PaymentGateway, string UPI, string POS, string POS_MachineNo, string POS_BatchNo)
        {
            decimal? cashd = cash != "" ? Convert.ToDecimal(cash) : 0;
            decimal? PaymentGatewayd = PaymentGateway != "" ? Convert.ToDecimal(PaymentGateway) : 0;
            decimal? UPId = UPI != "" ? Convert.ToDecimal(UPI) : 0;
            decimal? POSd = POS != "" ? Convert.ToDecimal(POS) : 0;


            int flag = _rsdal.OrderPaymentResponse(userid, orderid, cashd, PaymentGatewayd, UPId, POSd, POS_MachineNo, POS_BatchNo);


            return flag;
        }

        public int RemoveUnderRevisionOrderFromTrip(int userid, int tripid)
        {
            int flag = _rsdal.RemoveUnderRevisionOrderFromTrip(userid, tripid);
            return flag;
        }

        public string FarmerDataCollect(int userid, string name, string fathername, Int64 mobile, int stateid, int districtid, int blockid, int villageid, string othervillagename)
        {
            string flag = _rsdal.FarmerDataCollect(userid, name, fathername, mobile, stateid, districtid, blockid, villageid, othervillagename);


            return flag;
        }

        public GenericViewModel GetDistrictBlockVilage(int id, char type)
        {
            DataSet ds = _rsdal.GetDistrictBlockVilage(id, type);

            GenericViewModel a = new GenericViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<GenericModel> p = new List<GenericModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GenericModel data = new GenericModel();
                        data.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        data.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        p.Add(data);
                    }
                    a.List = p;
                }
            }
            return a;
        }

        public int GetFarmerPhoto(int userid, int orderid, string path)
        {
            int flag = _rsdal.GetFarmerPhoto(userid, orderid, path);


            return flag;
        }

        public UrlViewModel GetWebUrl(string type)
        {
            DataSet ds = _rsdal.GetWebUrl(type);

            UrlViewModel a = new UrlViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<UrlModel> p = new List<UrlModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        UrlModel data = new UrlModel();
                        data.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        data.ConfigKey = ds.Tables[0].Rows[i]["ConfigKey"].ToString();
                        data.Url = ds.Tables[0].Rows[i]["Url"].ToString();
                        p.Add(data);
                    }
                    a.List = p;
                }
            }
            return a;
        }
        public WordMeaningViewModel GetWordMeaning(string type)
        {
            DataSet DS = _rsdal.GetWordMeaning(type);
            WordMeaningViewModel _viewModel = new WordMeaningViewModel();
            //List<WordMeaningModel> Model = new List<WordMeaningModel>();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {

                    var _listModel = (from p in DS.Tables[0].AsEnumerable()
                                      select new WordMeaningModel
                                      {
                                          Word = p.Field<string>("Word"),
                                          Meaning = p.Field<string>("Meaning"),

                                      }).ToList();

                    _viewModel.List = _listModel;
                }
            }

            return _viewModel;
        }

        public int TripEnd(int userid, int tripid)
        {
            int flag = _rsdal.TripEnd(userid, tripid);

            return flag;
        }

        #region Order Create by FO Services
        public CategogyProductViewModel GetCategoryProductDetail(int userid)
        {
            DataSet ds = _rsdal.GetCategoryProductDetail(userid);

            CategogyProductViewModel _catProList = new CategogyProductViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<CategoryDetail> _catList = new List<CategoryDetail>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CategoryDetail _category = new CategoryDetail();
                        _category.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"].ToString());
                        _category.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                        _catList.Add(_category);
                    }
                    _catProList._categoryList = _catList;
                }
            }
            /*
            productId	PackageId	ProductName	TechNameID	TechnicalName	Amount	ourprice	
            unitname	SubCategoryId	SubCategoryName	categoryId	categoryName	CompanyId	OrganisationName	dealerId
            */
            if (ds != null && ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    List<BZProductDescription> _prodList = new List<BZProductDescription>();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        BZProductDescription _product = new BZProductDescription();
                        _product.PackageId = Convert.ToInt32(ds.Tables[1].Rows[i]["PackageId"].ToString());
                        _product.PackageName = ds.Tables[1].Rows[i]["ProductName"].ToString() + " - " + ds.Tables[1].Rows[i]["TechnicalName"].ToString()
                            + "(" + ds.Tables[1].Rows[i]["Amount"].ToString() + ds.Tables[1].Rows[i]["unitname"].ToString()
                            + ds.Tables[1].Rows[i]["ourprice"].ToString() + ")";

                        _product.CategoryId = Convert.ToInt32(ds.Tables[1].Rows[i]["categoryId"].ToString());
                        _product.ProductId = Convert.ToInt32(ds.Tables[1].Rows[i]["productId"].ToString());
                        _product.ProductName = ds.Tables[1].Rows[i]["ProductName"].ToString();
                        _product.Company = ds.Tables[1].Rows[i]["OrganisationName"].ToString();
                        _product.Price = Convert.ToDecimal(ds.Tables[1].Rows[i]["ourprice"].ToString());
                        _product.DealerId = Convert.ToInt32(ds.Tables[1].Rows[i]["dealerId"].ToString());
                        _product.DealerName = ds.Tables[1].Rows[i]["DealerName"].ToString();

                        _prodList.Add(_product);
                    }
                    _catProList._productList = _prodList;
                }
            }

            if (ds != null && ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    List<DealerDetail> _dealerList = new List<DealerDetail>();
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        DealerDetail _dealer = new DealerDetail();
                        _dealer.DealerId = Convert.ToInt32(ds.Tables[2].Rows[i]["DealerId"].ToString());
                        _dealer.DealerName = ds.Tables[2].Rows[i]["DealerName"].ToString();
                        _dealerList.Add(_dealer);
                    }
                    _catProList._dealerList = _dealerList;
                }
            }


            return _catProList;
        }


        public int GetFarmerIdByMobile(long mobile)
        {
            int id = _rsdal.GetFarmerIdByMobile(mobile);
            return id;
        }


        public FarmerDetailModel GetFarmerDetailByFarmerId(int farmerid)
        {
            DataSet ds = _rsdal.GetFarmerDetailByFarmerId(farmerid);

            FarmerDetailModel _farmer = new FarmerDetailModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {/*
                        FarmerID	Name	FatherName	Address	MobNo	StateID	DistrictID	BlockID	VillageID	
                        Address	StateName	DistrictName	VillageName	BlockName
                        */
                        _farmer.FarmerId = Convert.ToInt32(ds.Tables[0].Rows[i]["FarmerID"].ToString());
                        _farmer.FarmerName = ds.Tables[0].Rows[i]["Name"].ToString();
                        _farmer.FatherName = ds.Tables[0].Rows[i]["FatherName"].ToString();
                        _farmer.Mobile = Convert.ToInt64(ds.Tables[0].Rows[i]["MobNo"].ToString());
                        _farmer.StateId = Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"].ToString());
                        _farmer.DistrictId = Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictID"].ToString());
                        _farmer.BlockId = Convert.ToInt32(ds.Tables[0].Rows[i]["BlockID"].ToString());
                        _farmer.VillageId = Convert.ToInt32(ds.Tables[0].Rows[i]["VillageID"].ToString());
                        _farmer.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    }
                }
            }

            return _farmer;
        }

        public int OrderCreate(OrderCreateModel obj)
        {
            //int id = _rsdal.GetFarmerIdByMobile(mobile);
            //DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            int flag = _rsdal.OrderCreate(obj.OfferDiscount, obj.Product.PackageId, obj.Product.Quantity, obj.Amount, obj.Farmer.FarmerId, obj.Farmer.Mobile,
                obj.Farmer.StateId, obj.Farmer.DistrictId, obj.Farmer.BlockId, obj.Farmer.VillageId, obj.Farmer.OtherVillageName, obj.Farmer.Address,
                obj.DeliveryDate, obj.ModeOfPayment,obj.DiscountPrice,obj.DiscountCode,obj.yonoTransactionID);
            return flag;
        }
        #endregion

        #region Item Reprocess
        public ReprocessItemsListViewModel GetItemsForReprocess(int userid, int trip)
        {
            DataSet ds = _rsdal.GetItemsForReprocess(userid, trip);

            ReprocessItemsListViewModel _ProdList = new ReprocessItemsListViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<ReprocessItem> _list = new List<ReprocessItem>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ReprocessItem _item = new ReprocessItem();
                        _item.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageID"].ToString());
                        // _item.RecordId = Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                        _item.PackageName = ds.Tables[0].Rows[i]["PackageName"].ToString();
                        _item.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"].ToString());
                        _item.UnitPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["UnitPrice"].ToString());
                        _item.ToatlPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["ToatlPrice"].ToString());
                        _item.Company = ds.Tables[0].Rows[i]["Company"].ToString();
                        _list.Add(_item);
                    }
                    _ProdList.ProdctList = _list;
                }
            }
            return _ProdList;
        }
        public int ReprocessOrderCreate(ReprocessOrderCreateModel obj)
        {
            DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            int flag = _rsdal.ReprocessOrderCreate(obj.userid, obj.Farmer.FarmerId, obj.tripid, obj.Farmer.FarmerName, obj.Farmer.FatherName, obj.Farmer.Mobile,
                obj.Farmer.StateId, obj.Farmer.DistrictId, obj.Farmer.BlockId, obj.Farmer.VillageId, obj.Farmer.OtherVillageName, obj.Farmer.Address,
                obj.DeliveryDate, obj.ModeOfPayment, DT);
            return flag;
        }

        public GetOrderReprocessDetailModel GetOrderReprocessDetail(int userid, int orderid)
        {
            DataSet ds = _rsdal.GetOrderReprocessDetail(userid, orderid);

            GetOrderReprocessDetailModel order = new GetOrderReprocessDetailModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    order.OrderId = Convert.ToInt32(ds.Tables[0].Rows[0]["OrderID"].ToString());
                    order.OrderRefNo = ds.Tables[0].Rows[0]["OrderRefNo"].ToString();
                    order.Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"].ToString());
                    order.FarmerName = ds.Tables[0].Rows[0]["FarmerName"].ToString();
                    order.Mobile = Convert.ToInt64(ds.Tables[0].Rows[0]["MobNo"].ToString());
                    order.Vilage = ds.Tables[0].Rows[0]["VillageName"].ToString();
                }
            }
            return order;
        }
        #endregion

        #region Dealer Change
        public DealerViewModel GetDealerList(int userid, int packageid)
        {
            DataSet ds = _rsdal.GetDealerList(userid, packageid);

            DealerViewModel model = new DealerViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var list = (from p in ds.Tables[0].AsEnumerable()
                                select new DealerModel
                                {
                                    // PriceID DealerId    DealerName  OurPrice    BlockName   DistrictID  DistrictName    PackageID
                                    DealerId = p.Field<int>("DealerId"),
                                    DealerName = p.Field<string>("DealerName"),
                                    PackageId = p.Field<int>("PackageID"),
                                    PriceId = p.Field<int>("PriceID"),
                                    Price = p.Field<decimal>("OurPrice"),
                                    DistrictName = p.Field<string>("DistrictName"),
                                    BlockName = p.Field<string>("BlockName"),
                                }).ToList();

                    model.Dealer = list;
                }
            }
            return model;
        }
        public int DealerChange(int userid, int dealerid, int recordid, int packageid, int priceid, int reasonid, string remark)
        {
            int flag = _rsdal.DealerChange(userid, dealerid, recordid, packageid, priceid, reasonid, remark);

            return flag;
        }
        #endregion

        public int InventoryItemInsert(InventroyItemViewModel obj)
        {
            DataTable DT = Helper.Helper.ToDataTable(obj.Items);
            int flag = _rsdal.InventoryItemInsert(obj.userId, obj.dealerId, DT);

            return flag;
        }

        public Today_Order_Delivery_ViewModel GetTodayOrderDeliver(int userid)
        {
            DataSet ds = _rsdal.GetTodayOrderDeliver(userid);
            Today_Order_Delivery_ViewModel viewmodel = new Today_Order_Delivery_ViewModel();
            List<Today_Order_Delivery_Model> model = new List<Today_Order_Delivery_Model>();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Today_Order_Delivery_Model data = new Today_Order_Delivery_Model();
                        data.OrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"].ToString());
                        data.OrderRef = ds.Tables[0].Rows[i]["OrderRefNo"].ToString();
                        data.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                        data.POD = Convert.ToInt32(ds.Tables[0].Rows[i]["POD"].ToString());
                        data.Amount = Convert.ToDouble(ds.Tables[0].Rows[i]["Amount"].ToString());
                        data.CashAmount = ds.Tables[0].Rows[i]["CashAmount"].ToString();
                        data.PayUAmount = ds.Tables[0].Rows[i]["PGAmount"].ToString();
                        data.POSAmount = ds.Tables[0].Rows[i]["POSAmount"].ToString();
                        data.UPIAmount = ds.Tables[0].Rows[i]["UPIAmount"].ToString();
                        model.Add(data);
                    }
                    viewmodel.List = model;
                }
            }


            return viewmodel;
        }

        #region Google Navigation 
        public TripCreateModel GetTripCreateData(string type, int distictId, string blockId, decimal avgVehicleSpeed, decimal workingHours, decimal breakTimePerDeliveryInMinute, decimal lunchTime, int BaggSize)
        {
            DataSet ds = _rsdal.GetTripCreateData(type, distictId, blockId, avgVehicleSpeed, workingHours, breakTimePerDeliveryInMinute, lunchTime, BaggSize);
            //TripCreateViewModel viewmodel = new TripCreateViewModel();


            TripCreateModel model = new TripCreateModel();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (BaggSize == 5)
                {
                    var data = ds.Tables[0].AsEnumerable().Where(x => x.Field<decimal>("Load") >= BaggSize).ToList();
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        var _OrderlistModel = (from p in data
                                               select new TripCreateOrderModel
                                               {
                                                   orderId = p.Field<int>("OrderID"),
                                                   orderRefNo = p.Field<string>("OrderRefNo"),
                                                   orderAmount = p.Field<decimal>("TotalAmount"),
                                                   address = p.Field<string>("Address"),
                                                   dealerId = CommonBal.SplitStringToIntArray(p.Field<string>("DealerId"))

                                               }).ToList();

                        model.orderList = _OrderlistModel;
                    }
                }
                if (BaggSize > 0 && BaggSize < 5)
                {
                    var data = ds.Tables[0].AsEnumerable().Where(x => x.Field<decimal>("Load") <= BaggSize).ToList();
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        var _OrderlistModel = (from p in data
                                               select new TripCreateOrderModel
                                               {
                                                   orderId = p.Field<int>("OrderID"),
                                                   orderRefNo = p.Field<string>("OrderRefNo"),
                                                   orderAmount = p.Field<decimal>("TotalAmount"),
                                                   address = p.Field<string>("Address"),
                                                   dealerId = CommonBal.SplitStringToIntArray(p.Field<string>("DealerId"))

                                               }).ToList();

                        model.orderList = _OrderlistModel;
                    }
                }
                if (BaggSize == 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        var _OrderlistModel = (from p in ds.Tables[0].AsEnumerable()
                                               select new TripCreateOrderModel
                                               {
                                                   orderId = p.Field<int>("OrderID"),
                                                   orderRefNo = p.Field<string>("OrderRefNo"),
                                                   orderAmount = p.Field<decimal>("TotalAmount"),
                                                   address = p.Field<string>("Address"),
                                                   dealerId = CommonBal.SplitStringToIntArray(p.Field<string>("DealerId"))

                                               }).ToList();

                        model.orderList = _OrderlistModel;
                    }
                }

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    var _dealerlistModel = (from p in ds.Tables[1].AsEnumerable()
                                            select new TripCreateDealerModel
                                            {
                                                dealerId = p.Field<int>("DealerId"),
                                                dealerName = p.Field<string>("DealerName"),
                                                address = p.Field<string>("Address"),
                                            }).ToList();

                    model.dealerList = _dealerlistModel;
                }

                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    var _landmarklistModel = (from p in ds.Tables[2].AsEnumerable()
                                              select new TripCreateLandMarkModel
                                              {
                                                  position = p.Field<string>("Position"),
                                                  name = p.Field<string>("Name"),
                                              }).ToList();

                    model.landMarkInDistrict = _landmarklistModel;
                }

                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    model.lunchTime = Convert.ToDouble(ds.Tables[3].Rows[0]["LunchTime"].ToString());
                    model.foCount = Convert.ToInt32(ds.Tables[3].Rows[0]["FOCount"].ToString());
                    model.avgVehicleSpeed = Convert.ToInt32(ds.Tables[3].Rows[0]["AvgVehicleSpeed"].ToString());
                    model.workingHours = Convert.ToDouble(ds.Tables[3].Rows[0]["WorkingHours"].ToString());
                    model.breakTimePerDeliveryInMinute = Convert.ToInt32(ds.Tables[3].Rows[0]["BreakTimePerDeliveryInMinute"].ToString());
                }

                List<TripCreateHistoricalModel> HModel = new List<TripCreateHistoricalModel>();
                model.HistoricalPath = HModel;
            }

            // viewmodel.Data = model;
            return model;
        }

        public GeoTagCategoryViewModel GetGeoCatSubCategoies()
        {
            DataSet ds = _rsdal.GetGeoCatSubCategoies();
            GeoTagCategoryViewModel viewmodel = new GeoTagCategoryViewModel();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new GeoTagCategoryModel
                                      {
                                          CategoryId = p.Field<int>("GeoCategoryID"),
                                          CategoryName = p.Field<string>("GeoCategoryName"),
                                          SubCategory = GetGeoSubcatList(p.Field<int>("GeoCategoryID"), ds.Tables[1])
                                      }).ToList();
                    viewmodel.Category = _listModel;
                }
            }
            return viewmodel;
        }

        private List<GeoTagSubCategoryModel> GetGeoSubcatList(int categoryId, DataTable DT)
        {
            DataTable Tbl = DT.AsEnumerable()
                            .Where(r => r.Field<int>("GeoCategoryID") == categoryId)
                            .CopyToDataTable();

            var list = (from p in Tbl.AsEnumerable()
                        select new GeoTagSubCategoryModel
                        {
                            SubCategoryId = p.Field<int>("GeoSubCategoryID"),
                            SubCategoryName = p.Field<string>("GeoSubCategoryName")
                        }).ToList();

            return list;
        }
        public string GeoTaggingTagInsert(int userid, int subCategoryId, string Name, string lat, string longitute, int villageId, string otherVillage, int blockId)
        {
            string flag = _rsdal.GeoTaggingTagInsert(userid, subCategoryId, Name, lat, longitute, villageId, otherVillage, blockId);

            return flag;
        }

        public GeoTaggingDataViewModel GetGeoTaggingData()
        {
            DataSet ds = _rsdal.GetGeoTaggingData();
            GeoTaggingDataViewModel viewmodel = new GeoTaggingDataViewModel();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new GeoTaggingDataModel
                                      {
                                          CategoryId = p.Field<int>("GeoCategoryID"),
                                          CategoryName = p.Field<string>("GeoCategoryName"),
                                          SubCategoryId = p.Field<int>("GeoSubCategory"),
                                          SubCategoryName = p.Field<string>("GeoSubCategoryName"),
                                          Id = p.Field<int>("GeoLocationID"),
                                          Name = p.Field<string>("LocationName"),
                                          Lat = p.Field<string>("Lat"),
                                          Longitute = p.Field<string>("Longitude"),
                                      }).ToList();

                    viewmodel.GeoDataList = _listModel;
                }
            }
            return viewmodel;
        }

        public GeoTaggingDataWithVillageViewModel GetGeoTaggingDataWithVillage(int UserId)
        {
            DataSet ds = _rsdal.GetGeoTaggingDataWithVillage(UserId);
            GeoTaggingDataWithVillageViewModel viewmodel = new GeoTaggingDataWithVillageViewModel();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new GeoTaggingDataWithVillageModel
                                      {
                                          SubCategoryId = p.Field<int>("Id"),
                                          SubCategoryName = p.Field<string>("Redius"),
                                          VillageId = p.Field<int>("VillageId"),
                                          VillageName = p.Field<string>("VillageName"),
                                      }).ToList();

                    viewmodel.GeoVillageList = _listModel;
                }
            }
            return viewmodel;
        }
        #endregion

        #region Hub Head
        public HubHeadFODistrictWiseViewModel GetHubHeadNFODistrictWiseList(string userid, string type)
        {
            DataSet ds = _rsdal.GetHubHeadNFODistrictWiseList(userid, type);

            HubHeadFODistrictWiseViewModel a = new HubHeadFODistrictWiseViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<HubHeadFODistrictWiseModel> _list = new List<HubHeadFODistrictWiseModel>();

                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new HubHeadFODistrictWiseModel
                                      {
                                          UserId = p.Field<int>("UserID"),
                                          Name = p.Field<string>("Name"),
                                          Mobile = p.Field<string>("MobileNo"),
                                      }).ToList();

                    _list = _listModel;

                    a.UserList = _list;
                }
            }
            return a;
        }

        public int FoCashSubmitToRO(int userid, int hhid, string amount, string desc)
        {
            int flag = _rsdal.FoCashSubmitToRO(userid, hhid, amount, desc);
            return flag;
        }

        public HubHeadApprovalViewModel GetFoAmountApprovalList(int hhid)
        {
            HubHeadApprovalViewModel model = new HubHeadApprovalViewModel();

            DataSet ds = _rsdal.GetFoAmountApprovalList(hhid);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //HubHeadApprovalViewModel _list = new HubHeadApprovalViewModel();

                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new HubHeadApprovalModel
                                      {
                                          FoId = p.Field<int>("FO_ID"),
                                          FoName = p.Field<string>("FoName"),
                                          Desc = p.Field<string>("Desc"),
                                          RecordId = p.Field<int>("RecordID"),
                                          Amount = p.Field<decimal>("Amount"),
                                          Date = p.Field<string>("Date"),
                                          Mobile = p.Field<string>("Mobile")

                                      }).ToList();

                    model.FOList = _listModel;

                }
            }
            return model;
        }

        public int FoCashApprovalDeclineByHubHead(int hhid, int recordid, string status)
        {
            int flag = _rsdal.FoCashApprovalDeclineByHubHead(hhid, recordid, status);

            return flag;
        }

        public HubHeadGetTodayTotalOrderDistrictWiseViewModel GetTodayTotalOrderDistrictWise(int districtid)
        {
            HubHeadGetTodayTotalOrderDistrictWiseViewModel Viewmodel = new HubHeadGetTodayTotalOrderDistrictWiseViewModel();
            HubHeadGetTodayTotalOrderDistrictWiseModel model = new HubHeadGetTodayTotalOrderDistrictWiseModel();
            DataSet ds = _rsdal.GetTodayTotalOrderDistrictWise(districtid);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    model.TotalOrder = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalOrder"]);
                    model.TotalOrderAssigned = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalAssignedOrder"]);
                    model.TotalOrderRemaian = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalRemainOrder"]);

                }

                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        var _listModel = (from p in ds.Tables[1].AsEnumerable()
                                          select new HubHeadGetTodayTotalOrderDistrictWiseFoDetailModel
                                          {
                                              FoId = p.Field<int>("FoId"),
                                              FoName = p.Field<string>("FOName"),
                                              Mobile = p.Field<long>("MobileNo"),
                                              TotalOrderAssignedToFo = p.Field<int>("OrderCount"),
                                              TotalAmountCollectedToday = p.Field<decimal>("AmountCollected"),
                                              TotalAmountToBeCollect = p.Field<decimal>("TotalAmount"),
                                              Cash = p.Field<decimal>("CashAmount"),
                                              PayU = p.Field<decimal>("PGAmount"),
                                              POS = p.Field<decimal>("POSAmount"),
                                              UPI = p.Field<decimal>("UPIAmount"),
                                              TripId = p.Field<int>("TripID"),
                                              TripReleasable = p.Field<int>("TripReleasable"),
                                          }).ToList();

                        model.FODetailList = _listModel;

                    }
                }
            }
            Viewmodel.List = model;
            return Viewmodel;
        }

        public HubHeadGetTodayOrderByFoViewModel GetTodayOrderByFo(int foId, int tripId)
        {
            HubHeadGetTodayOrderByFoViewModel model = new HubHeadGetTodayOrderByFoViewModel();
            DataSet ds = _rsdal.GetTodayOrderByFo(foId, tripId);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new HubHeadGetTodayOrderByFoModel
                                      {
                                          OrderId = p.Field<int>("OrderID"),
                                          OrderRefNo = p.Field<string>("OrderRefNo"),
                                          Status = p.Field<string>("status"),
                                          StatusId = p.Field<string>("OrderStatus"),
                                      }).ToList();

                    model.List = _listModel;

                }

            }

            return model;
        }

        public HubHeadGetTodayOrderStatusByOrderIdViewModel GetTodayOrderStatusByOrderId(int OrderId)
        {
            HubHeadGetTodayOrderStatusByOrderIdViewModel Vmodel = new HubHeadGetTodayOrderStatusByOrderIdViewModel();
            HubHeadGetTodayOrderStatusByOrderIdModel model = new HubHeadGetTodayOrderStatusByOrderIdModel();
            DataSet ds = _rsdal.GetTodayOrderStatusByOrderId(OrderId);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.DeliverTo = Convert.ToString(ds.Tables[0].Rows[0]["FarmerName"]);
                    model.DeliveryTime = Convert.ToString(ds.Tables[0].Rows[0]["DeliveredDate"]);
                    model.Cash = Convert.ToDecimal(ds.Tables[0].Rows[0]["CashAmount"]);
                    model.PayU = Convert.ToDecimal(ds.Tables[0].Rows[0]["PGAmount"]);
                    model.POS = Convert.ToDecimal(ds.Tables[0].Rows[0]["POSAmount"]);
                    model.UPI = Convert.ToDecimal(ds.Tables[0].Rows[0]["UPIAmount"]);
                }
            }
            if (ds != null && ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    var _listModel = (from p in ds.Tables[1].AsEnumerable()
                                      select new HubHeadGetTodayOrderStatusByOrderId
                                      {
                                          DealerName = p.Field<string>("DealerName"),
                                          InvoiceNo = p.Field<string>("Invoice_no"),
                                          PickupTime = p.Field<string>("PickedDate"),
                                          ProductName = p.Field<string>("ProductName"),
                                          OriginalQty = p.Field<int>("Original_Qty"),
                                          Qty = p.Field<int>("Quantity"),
                                          Packaging = p.Field<string>("PackageName"),
                                          Reason = p.Field<string>("Reason"),
                                      }).ToList();

                    model.ProductList = _listModel;

                }

            }
            Vmodel.OrderDetail = model;
            return Vmodel;
        }

        public int ResetOrdersFoWise(int foid, int UserId)
        {
            int flag = _rsdal.ResetOrdersFoWise(foid, UserId);
            return flag;
        }

        public TrackFOByFoViewModel GetTrackFOByFo(int foId)
        {
            TrackFOByFoViewModel Vmodel = new TrackFOByFoViewModel();
            TrackFOByFo model = new TrackFOByFo();
            DataSet ds = _rsdal.GetTrackFOByFo(foId);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new TrackFOByFoModel
                                      {
                                          FoEvent = p.Field<string>("EventName"),
                                          FoAction = p.Field<string>("Action"),
                                          Lat = p.Field<string>("lat"),
                                          Longitude = p.Field<string>("longitude"),
                                          Time = p.Field<string>("Time"),
                                          DealerName = p.Field<string>("DealerName"),
                                          NewDealerName = p.Field<string>("NewDealerName"),
                                          FarmerName = p.Field<string>("FarmerName"),
                                          Orderid = p.Field<string>("OrderIds"),
                                          OrderRefNo = p.Field<string>("OrderRefNo"),
                                          Type = p.Field<string>("Type"),
                                      }).ToList();

                    model.List = _listModel;
                    if (ds.Tables[1].Rows.Count > 0)
                        model.Lastlat = ds.Tables[1].Rows[0]["lat"].ToString();
                    model.Lastlongitude = ds.Tables[1].Rows[0]["longitude"].ToString();
                    Vmodel.TackFOList = model;
                }

            }

            return Vmodel;
        }
        public OrderTripViewModel GetOrderDetailByOrderId(int orderId)
        {
            DataSet ds = _rsdal.GetOrderDetailByOrderId(orderId);

            OrderTripViewModel a = new OrderTripViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<OrderTripModel> p = new List<OrderTripModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderTripModel Reason = new OrderTripModel();
                        Reason.OrderID = ds.Tables[0].Rows[i]["Id"].ToString();
                        Reason.OrderRefNo = ds.Tables[0].Rows[i]["ref_no"].ToString();
                        Reason.Status = ds.Tables[0].Rows[i]["OrderStatus"].ToString();
                        Reason.Village = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.Farmer = ds.Tables[0].Rows[i]["FarmerName"].ToString();
                        Reason.CashCollected = ds.Tables[0].Rows[i]["CashCollected"].ToString();
                        Reason.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                        Reason.OrderReprocessFlag = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderReprocess"].ToString());

                        p.Add(Reason);
                    }
                    a.OrderList = p;
                }
            }
            return a;
        }

        public int OrderDeliverByHubHead(int foid, int userId, int orderId, int status, int IdType, string IdNumber, string otherId)
        {
            int flag = _rsdal.OrderDeliverByHubHead(foid, userId, orderId, status, IdType, IdNumber, otherId);
            return flag;
        }

        public int OrderItemsAssignTOOtherFoByHubHead(int FoId, int NewFoId, int HubheadId, List<DS_ProductPostDetail> ProductList)
        {
            DataTable DT = Helper.Helper.ToDataTable(ProductList);
            int flag = _rsdal.OrderItemsAssignTOOtherFoByHubHead(FoId, NewFoId, HubheadId, DT);

            return flag;
        }

        #endregion

        #region Logistic 
        public LogisticActionTakenTripsViewModel GetActionTakenTrips(DateTime date)
        {
            LogisticActionTakenTripsViewModel model = new LogisticActionTakenTripsViewModel();
            // LogisticActionTakenTrips model = new LogisticActionTakenTrips();
            DataSet ds = _rsdal.GetActionTakenTrips(date);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var _listModel = (from p in ds.Tables[0].AsEnumerable()
                                      select new LogisticActionTakenTripsModel
                                      {
                                          TripId = p.Field<int>("TripID"),
                                          DistrictName = p.Field<string>("DistrictName"),
                                          RequestedBy = p.Field<string>("RequestedBy"),
                                          RequestedTime = p.Field<string>("RequestedTime"),
                                          ActionBy = p.Field<string>("ActionBy"),
                                          ActionTime = p.Field<string>("ActionDate"),
                                          ActionStatus = p.Field<string>("TripStatus"),
                                          Vehicle = p.Field<string>("VehicleName"),
                                          Rule = p.Field<string>("RuleName"),
                                          RuleId = p.Field<int>("RuleID"),
                                          FixRateCharge = p.Field<decimal>("Fixedrateperkm"),
                                          UpCharge = p.Field<decimal>("Upprice"),
                                          DownCharge = p.Field<decimal>("Downprice"),
                                          MinKmRange = p.Field<int>("MinchargeUptoKm"),
                                          MinRatePerKm = p.Field<decimal>("Mincharges"),
                                          RatePerKm = p.Field<decimal>("Priceperkm"),
                                          FixRatePerDay = p.Field<decimal>("Fixedrateperday"),
                                          FuelCharge = p.Field<decimal>("FuelCharges"),
                                      }).ToList();

                    model.List = _listModel;
                }

            }

            return model;
        }

        public GenericViewModel GetActionTakenOrders(int TripId)
        {
            DataSet ds = _rsdal.GetActionTakenOrders(TripId);

            GenericViewModel a = new GenericViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<GenericModel> p = new List<GenericModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GenericModel data = new GenericModel();
                        data.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        data.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        p.Add(data);
                    }
                    a.List = p;
                }
            }
            return a;
        }

        public LogisticGetActionTakenOrderDetailViewModel GetActionTakenOrderDetail(int OrderId)
        {
            DataSet ds = _rsdal.GetActionTakenOrderDetail(OrderId);

            LogisticGetActionTakenOrderDetailViewModel model = new LogisticGetActionTakenOrderDetailViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var list = (from p in ds.Tables[0].AsEnumerable()
                                select new LogisticGetActionTakenOrderDetailModel
                                {
                                    ProductName = p.Field<string>("ProductName"),
                                    Package = p.Field<string>("Package"),
                                    Qty = p.Field<int>("Quantity"),
                                }).ToList();
                    model.List = list;
                }
            }
            return model;
        }
        #endregion

        #region CreatedByArpit for Temp Fo
        public OrderFo GetOrdersByFo(string mobile)
        {
            OrderFo ord = new OrderFo();
            DataSet ds = _rsdal.GetOrdersByFo(mobile);

            OrderViewModel a = new OrderViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<OrderModel> p = new List<OrderModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderModel Reason = new OrderModel();
                        Reason.OrderId = ds.Tables[0].Rows[i]["Id"].ToString();
                        Reason.OrderName = ds.Tables[0].Rows[i]["ref_no"].ToString();
                        Reason.Village = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.Block = ds.Tables[0].Rows[i]["BlockName"].ToString();
                        Reason.Distributor = ds.Tables[0].Rows[i]["DistributorName"].ToString();
                        Reason.IsPrime = ds.Tables[0].Rows[i]["IsPrime"] == DBNull.Value ? 00 : Convert.ToInt32(ds.Tables[0].Rows[i]["IsPrime"]);
                        p.Add(Reason);
                    }
                    a.OrderList = p;
                }
            }
            if (ds != null && ds.Tables[1] != null)
            {
                var userExist = Convert.ToInt16(ds.Tables[1].Rows[0]["userExist"]);
                ord.Status = userExist > 0 ? 1 : 0;
                ord.Msg = userExist > 0 ? "" : "Invalid mobile number";
                ord.Orders = userExist > 0 ? a : null;
            }



            return ord;
        }

        public OrderFo GetOrdersByFo(string mobile,DateTime date)
        {
            OrderFo ord = new OrderFo();
            DataSet ds = _rsdal.GetOrdersByFo(mobile, date);

            OrderViewModel a = new OrderViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<OrderModel> p = new List<OrderModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        OrderModel Reason = new OrderModel();
                        Reason.OrderId = ds.Tables[0].Rows[i]["Id"].ToString();
                        Reason.OrderName = ds.Tables[0].Rows[i]["ref_no"].ToString();
                        Reason.Village = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.Block = ds.Tables[0].Rows[i]["BlockName"].ToString();
                        Reason.Distributor = ds.Tables[0].Rows[i]["DistributorName"].ToString();
                        Reason.IsPrime = ds.Tables[0].Rows[i]["IsPrime"] == DBNull.Value ? 00 : Convert.ToInt32(ds.Tables[0].Rows[i]["IsPrime"]);
                        p.Add(Reason);
                    }
                    a.OrderList = p;
                }
            }
            if (ds != null && ds.Tables[1] != null)
            {
                var userExist = Convert.ToInt16(ds.Tables[1].Rows[0]["userExist"]);
                ord.Status = userExist > 0 ? 1 : 0;
                ord.Msg = userExist > 0 ? "" : "Invalid mobile number";
                ord.Orders = userExist > 0 ? a : null;
            }



            return ord;
        }

        public ResponseObj UpdateOrdersToFo(string orderid, string mobile, string vehicletypeid, string vehiclename, string createdby, string transpoterId, string ruleId)
        {
            var flag = _rsdal.UpdateOrdersToFo(orderid, mobile, vehicletypeid, vehiclename, createdby, transpoterId, ruleId);
            //#region  Trip Sheet Mail send
            //if (ConfigurationManager.AppSettings["TripCreateMailSend"] == "1")
            //{
            //    if (Convert.ToInt32(flag.Value) > 0)
            //    {
            //        /**********Mail Report of Trip*********/
            //        var DS = _rsdal.Get_FoTripOrders(Convert.ToInt32(flag.Value), mobile, "RT");
            //        if (DS != null)
            //        {
            //            if (DS.Tables[0].Rows.Count > 0 && DS.Tables[1].Rows.Count > 0)
            //            {
            //                var sendToList = DS.Tables[1].Rows[0]["ToList"].ToString();
            //                var sendCCList = DS.Tables[1].Rows[0]["CCList"].ToString();

            //                var mailSubject = DS.Tables[1].Rows[0]["Subject"].ToString();
            //                var mailBody = string.Format(DS.Tables[1].Rows[0]["Body"].ToString(), DS.Tables[1].Rows[0]["OrderDate"].ToString());
            //                string  fileName = DS.Tables[1].Rows[0]["FOName"].ToString() + "(" + mobile + ") " + DS.Tables[1].Rows[0]["OrderDate"].ToString();

            //                var _tripOrdersList = (from p in DS.Tables[0].AsEnumerable()
            //                             select new TripSheetModel
            //                             {
            //                                 OrderNo = p.Field<int>("OrderNo"),
            //                                 Product = p.Field<string>("Product(Company)"),
            //                                 Farmer = p.Field<string>("Farmer Name(Father Name)"),
            //                                 Weight = p.Field<string>("Weight"),
            //                                 Qty = p.Field<int>("Total Quantity"),
            //                                 Amount = p.Field<decimal>("Total Collection"),
            //                                 Block = p.Field<string>("Block"),
            //                                 Village = p.Field<string>("Village"),
            //                                 Address = p.Field<string>("Address"),
            //                             }).ToList();

            //                // ReportInXMail.SendMailForFoTripOrders(DS.Tables[0], sendToList, sendCCList, mailSubject, mailBody, fileName, Convert.ToInt32(createdby));
            //                ReportInXMail.SendMailForFoTripOrdersPDF(_tripOrdersList, sendToList, sendCCList, mailSubject, mailBody, fileName, Convert.ToInt32(createdby));
            //            }
            //        }

            //    }
            //}
            //#endregion

            return flag;
        }

        public ResponseObj UpdateOrdersToFo(string orderid, string mobile, string vehicletypeid, string vehiclename, string createdby, string transpoterId, string ruleId,DateTime date)
        {
            var flag = _rsdal.UpdateOrdersToFo(orderid, mobile, vehicletypeid, vehiclename, createdby, transpoterId, ruleId,date);
            
            return flag;
        }

        public User GetUserEmailByUserName(string mobile)
        {

            var ds = _rsdal.GetUserEmailByUserName(mobile);
            var usr = new User();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    usr.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString());
                    usr.FcmId = ds.Tables[0].Rows[0]["FcmId"].ToString();
                    usr.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();


                }
            }
            return usr;
        }

        public OrderDetail GetInvoiceNobyOrderId(string orderId)
        {
            var ordDetail = new OrderDetail();
            DataSet ds = _rsdal.GetInvoiceNobyOrderId(orderId);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ordDetail.CurrentSeriesNumber = ds.Tables[0].Rows[0]["currentSeriesNumber"].ToString();

                }
            }

            DataSet ds1 = _rsdal.GetPodbyOrderId(orderId);

            ordDetail.OrderDetails = GetPODScreen(ds1);



            return ordDetail;
        }

        public List<PodDetail> GetPODScreen(DataSet ds)
        {



            List<PodDetail> PodDetail = new List<PodDetail>();
            var order = new PodDetail();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PodDetail.Add(new PodDetail()
                        {
                            OrderRefNo = ds.Tables[0].Rows[i]["OrderRefNo"].ToString(),
                            OrderID = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"].ToString()),
                            DealerName = ds.Tables[0].Rows[i]["DealerName"].ToString(),
                            FarmerRefNo = ds.Tables[0].Rows[i]["FarmerRefNo"].ToString(),
                            FName = ds.Tables[0].Rows[i]["FName"].ToString(),
                            LName = ds.Tables[0].Rows[i]["LName"].ToString(),
                            NickName = ds.Tables[0].Rows[i]["NickName"].ToString(),
                            FatherName = ds.Tables[0].Rows[i]["FatherName"].ToString(),
                            StateName = ds.Tables[0].Rows[i]["StateName"].ToString(),
                            DistrictName = ds.Tables[0].Rows[i]["DistrictName"].ToString(),
                            ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString(),
                            BlockName = ds.Tables[0].Rows[i]["BlockName"].ToString(),
                            VillageName = ds.Tables[0].Rows[i]["VillageName"].ToString(),
                            NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"].ToString(),
                            Chaupal = ds.Tables[0].Rows[i]["Chaupal"].ToString(),
                            ShippingAddress = ds.Tables[0].Rows[i]["ShippingAddress"].ToString(),
                            MobNo = ds.Tables[0].Rows[i]["MobNo"].ToString(),
                            BrandName = ds.Tables[0].Rows[i]["BrandName"].ToString(),
                            OrganisationName = ds.Tables[0].Rows[i]["OrganisationName"].ToString(),
                            TechnicalName = ds.Tables[0].Rows[i]["TechnicalName"].ToString(),
                            Amount = ds.Tables[0].Rows[i]["Amount"].ToString(),
                            UnitName = ds.Tables[0].Rows[i]["UnitName"].ToString(),
                            OurPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["OurPrice"].ToString()),
                            Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString()),
                            OtherCharges = Convert.ToDecimal(ds.Tables[0].Rows[i]["OtherCharges"].ToString()),
                            DiscType = ds.Tables[0].Rows[i]["DiscType"].ToString(),
                            DiscAmt = Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscAmt"].ToString()),
                            Subtotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["Subtotal"].ToString()),
                            HandleChages = Convert.ToDecimal(ds.Tables[0].Rows[i]["HandleChages"].ToString()),
                            Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total"].ToString()),
                            Total_InWord = ds.Tables[0].Rows[i]["Total_InWord"].ToString(),
                            DeliveryInstruction = ds.Tables[0].Rows[i]["DeliveryInstruction"].ToString(),
                            CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"].ToString(),
                            ProcessedDate = ds.Tables[0].Rows[i]["ProcessedDate"].ToString(),
                            Time = Convert.ToDateTime(ds.Tables[0].Rows[i]["DeliveryInstruction"].ToString()).ToString("hh:MM tt")

                        });
                    }



                }
            }
            return PodDetail;
        }

        #endregion

        #region Invoice & POD
        public void Get_FoTripOrders(string Value, string mobile, string createdby, string TripType)
        {
            // DataSet DS = new DataSet();
            #region  Trip Sheet Mail send
            if (ConfigurationManager.AppSettings["TripCreateMailSend"] == "1")
            {
                if (Convert.ToInt32(Value) > 0)
                {
                    /**********Mail Report of Trip*********/
                    var DS = _rsdal.Get_FoTripOrders(Convert.ToInt32(Value), mobile, TripType);
                    if (DS != null)
                    {
                        if (DS.Tables[0].Rows.Count > 0 && DS.Tables[1].Rows.Count > 0)
                        {
                            var sendToList = DS.Tables[1].Rows[0]["ToList"].ToString();
                            var sendCCList = DS.Tables[1].Rows[0]["CCList"].ToString();

                            string fileName = DS.Tables[1].Rows[0]["FOName"].ToString() + "(" + mobile + ") " + DS.Tables[1].Rows[0]["OrderDate"].ToString();
                            string FOName = DS.Tables[1].Rows[0]["FOName"].ToString();
                            string OrderDate = DS.Tables[1].Rows[0]["OrderDate"].ToString();
                            string DistrictName = DS.Tables[1].Rows[0]["DistrictName"].ToString();


                            var mailSubject = DS.Tables[1].Rows[0]["Subject"].ToString() + " For " + fileName;
                            var mailBody = string.Format(DS.Tables[1].Rows[0]["Body"].ToString(), DS.Tables[1].Rows[0]["OrderDate"].ToString());

                            var _tripOrdersList = (from p in DS.Tables[0].AsEnumerable()
                                                   select new TripSheetModel
                                                   {
                                                       OrderNo = p.Field<int>("OrderNo"),
                                                       Product = p.Field<string>("Product(Company)"),
                                                       Farmer = p.Field<string>("Farmer Name(Father Name)"),
                                                       Weight = p.Field<string>("Weight"),
                                                       Qty = p.Field<int>("Total Quantity"),
                                                       Amount = p.Field<decimal>("Total Collection"),
                                                       Block = p.Field<string>("Block"),
                                                       Village = p.Field<string>("Village"),
                                                       Address = p.Field<string>("Address"),
                                                       InvoicePODStatus = p.Field<string>("InvicePoDStatus"),
                                                   }).ToList();

                            // ReportInXMail.SendMailForFoTripOrders(DS.Tables[0], sendToList, sendCCList, mailSubject, mailBody, fileName, Convert.ToInt32(createdby));
                            ReportInXMail.SendMailForFoTripOrdersPDF(_tripOrdersList, sendToList, sendCCList, mailSubject, mailBody, fileName, FOName, OrderDate, DistrictName, Convert.ToInt32(createdby));
                        }
                    }

                }
            }
            #endregion
            // return DS;
        }
        public void GetPodDetailReport(int tripId, string TripType, int createdby)
        {
            #region  POD and Invoice Mail send
            if (ConfigurationManager.AppSettings["InvoiceCreateMailSend"] == "1")
            {
                string RecordId = "";
                var InvoiceDetailReportList = new List<PodDetailReport>();
                var podDetailReportList = new List<PodDetailReport>();
                DataSet ds = _rsdal.GetPodDetailReport(tripId, TripType);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var sendToList = ds.Tables[1].Rows[0]["ToList"].ToString();
                        var sendCCList = ds.Tables[1].Rows[0]["CCList"].ToString();

                        var mobile = ds.Tables[1].Rows[0]["UserName"].ToString();//

                        string fileName = ds.Tables[1].Rows[0]["FOName"].ToString() + "(" + mobile + ") " + ds.Tables[1].Rows[0]["OrderDate"].ToString();
                        string FOName = ds.Tables[1].Rows[0]["FOName"].ToString();
                        string OrderDate = ds.Tables[1].Rows[0]["OrderDate"].ToString();
                        string DistrictName = ds.Tables[1].Rows[0]["DistrictName"].ToString();

                        var mailSubject = ds.Tables[1].Rows[0]["Subject"].ToString() + " For " + fileName;
                        var mailBody = string.Format(ds.Tables[1].Rows[0]["Body"].ToString(), ds.Tables[1].Rows[0]["OrderDate"].ToString());

                        #region Set Proerty
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            RecordId = RecordId + "," + Convert.ToInt64(ds.Tables[0].Rows[i]["RecordId"]);
                            bool dealerIsPrime = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsPrime"]);
                            if (dealerIsPrime)
                            {
                                #region set Invoice
                                InvoiceDetailReportList.Add(new PodDetailReport
                                {
                                    FarmerID = ds.Tables[0].Rows[i]["FarmerID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["FarmerID"]),
                                    OrderID = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"]),
                                    OrderRefNo = ds.Tables[0].Rows[i]["OrderRefNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrderRefNo"]),
                                    FarmerRefNo = ds.Tables[0].Rows[i]["FarmerRefNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FarmerRefNo"]),
                                    FName = ds.Tables[0].Rows[i]["FName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FName"]),
                                    LName = ds.Tables[0].Rows[i]["LName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LName"]),
                                    Quantity = ds.Tables[0].Rows[i]["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]),
                                    NickName = ds.Tables[0].Rows[i]["NickName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NickName"]),
                                    FatherName = ds.Tables[0].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]),
                                    StateID = ds.Tables[0].Rows[i]["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"]),
                                    StateName = ds.Tables[0].Rows[i]["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateName"]),
                                    DistrictID = ds.Tables[0].Rows[i]["DistrictID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictID"]),
                                    DistrictName = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]),
                                    BLockID = ds.Tables[0].Rows[i]["BLockID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BLockID"]),
                                    BlockName = ds.Tables[0].Rows[i]["BlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockName"]),
                                    VillageID = ds.Tables[0].Rows[i]["VillageID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["VillageID"]),
                                    VillageName = ds.Tables[0].Rows[i]["VillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageName"]),
                                    NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NearByVillage"]),
                                    Chaupal = ds.Tables[0].Rows[i]["Chaupal"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Chaupal"]),
                                    ShippingAddress = ds.Tables[0].Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShippingAddress"]),
                                    MobNo = ds.Tables[0].Rows[i]["MobNo"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["MobNo"]),
                                    ProductID = ds.Tables[0].Rows[i]["ProductID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]),
                                    BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]),
                                    OrganisationName = ds.Tables[0].Rows[i]["OrganisationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrganisationName"]),
                                    TechnicalName = ds.Tables[0].Rows[i]["TechnicalName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TechnicalName"]),
                                    ProductName = ds.Tables[0].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]),
                                    Amount = ds.Tables[0].Rows[i]["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]),
                                    UnitName = ds.Tables[0].Rows[i]["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UnitName"]),
                                    OurPrice = ds.Tables[0].Rows[i]["OurPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OurPrice"]),
                                    OtherCharges = ds.Tables[0].Rows[i]["OtherCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OtherCharges"]),
                                    DiscType = ds.Tables[0].Rows[i]["DiscType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DiscType"]),
                                    DiscAmt = ds.Tables[0].Rows[i]["DiscAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscAmt"]),
                                    Subtotal = ds.Tables[0].Rows[i]["Subtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Subtotal"]),
                                    HandleChages = ds.Tables[0].Rows[i]["HandleChages"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["HandleChages"]),
                                    DiscountedAmount = ds.Tables[0].Rows[i]["DiscountedAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscountedAmount"]),
                                    DealerID = ds.Tables[0].Rows[i]["DealerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DealerID"]),
                                    DealerName = ds.Tables[0].Rows[i]["DealerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerName"]),
                                    DiscText = ds.Tables[0].Rows[i]["DiscText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DiscText"]),
                                    TotalPayableAmount_In_Word = ds.Tables[0].Rows[i]["TotalPayableAmount_In_Word"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TotalPayableAmount_In_Word"]),
                                    TotalPayableAmount = ds.Tables[0].Rows[i]["TotalPayableAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPayableAmount"]),
                                    CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]),
                                    DeliveryInstruction = ds.Tables[0].Rows[i]["DeliveryInstruction"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["DeliveryInstruction"]),
                                    ProcessedDate = ds.Tables[0].Rows[i]["ProcessedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["ProcessedDate"]),
                                    SubCategoryId = ds.Tables[0].Rows[i]["SubCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryId"]),
                                    PanNo = ds.Tables[0].Rows[i]["PanNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PanNo"]),
                                    GstNumber = ds.Tables[0].Rows[i]["GstNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["GstNumber"]),
                                    DealerStateName = ds.Tables[0].Rows[i]["DealerStateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerStateName"]),
                                    DealerDistrictName = ds.Tables[0].Rows[i]["DealerDistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerDistrictName"]),
                                    DealerBlockName = ds.Tables[0].Rows[i]["DealerBlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerBlockName"]),
                                    DealerVillageName = ds.Tables[0].Rows[i]["DealerVillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerVillageName"]),
                                    DealerAddress = ds.Tables[0].Rows[i]["DealerAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerAddress"]),
                                    HsnCode = ds.Tables[0].Rows[i]["HsnCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["HsnCode"]),
                                    Cgst = ds.Tables[0].Rows[i]["Cgst"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Cgst"]),
                                    Igst = ds.Tables[0].Rows[i]["Igst"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Igst"]),
                                    Sgst = ds.Tables[0].Rows[i]["Sgst"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Sgst"]),
                                    Invoice_No = ds.Tables[0].Rows[i]["Invoice_No"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Invoice_No"]),
                                    RecordId = ds.Tables[0].Rows[i]["RecordId"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["RecordId"])
                                });
                                #endregion
                            }
                            else
                            {
                                #region set POD
                                podDetailReportList.Add(new PodDetailReport
                                {
                                    FarmerID = ds.Tables[0].Rows[i]["FarmerID"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["FarmerID"]),
                                    OrderID = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"]),
                                    OrderRefNo = ds.Tables[0].Rows[i]["OrderRefNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrderRefNo"]),
                                    FarmerRefNo = ds.Tables[0].Rows[i]["FarmerRefNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FarmerRefNo"]),
                                    FName = ds.Tables[0].Rows[i]["FName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FName"]),
                                    LName = ds.Tables[0].Rows[i]["LName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LName"]),
                                    Quantity = ds.Tables[0].Rows[i]["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]),
                                    NickName = ds.Tables[0].Rows[i]["NickName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NickName"]),
                                    FatherName = ds.Tables[0].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]),
                                    StateID = ds.Tables[0].Rows[i]["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"]),
                                    StateName = ds.Tables[0].Rows[i]["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateName"]),
                                    DistrictID = ds.Tables[0].Rows[i]["DistrictID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictID"]),
                                    DistrictName = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]),
                                    BLockID = ds.Tables[0].Rows[i]["BLockID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BLockID"]),
                                    BlockName = ds.Tables[0].Rows[i]["BlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockName"]),
                                    VillageID = ds.Tables[0].Rows[i]["VillageID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["VillageID"]),
                                    VillageName = ds.Tables[0].Rows[i]["VillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageName"]),
                                    NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NearByVillage"]),
                                    Chaupal = ds.Tables[0].Rows[i]["Chaupal"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Chaupal"]),
                                    ShippingAddress = ds.Tables[0].Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShippingAddress"]),
                                    MobNo = ds.Tables[0].Rows[i]["MobNo"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["MobNo"]),
                                    ProductID = ds.Tables[0].Rows[i]["ProductID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]),
                                    BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]),
                                    OrganisationName = ds.Tables[0].Rows[i]["OrganisationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrganisationName"]),
                                    TechnicalName = ds.Tables[0].Rows[i]["TechnicalName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TechnicalName"]),
                                    ProductName = ds.Tables[0].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]),
                                    Amount = ds.Tables[0].Rows[i]["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]),
                                    UnitName = ds.Tables[0].Rows[i]["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UnitName"]),
                                    OurPrice = ds.Tables[0].Rows[i]["OurPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OurPrice"]),
                                    OtherCharges = ds.Tables[0].Rows[i]["OtherCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OtherCharges"]),
                                    DiscType = ds.Tables[0].Rows[i]["DiscType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DiscType"]),
                                    DiscAmt = ds.Tables[0].Rows[i]["DiscAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscAmt"]),
                                    Subtotal = ds.Tables[0].Rows[i]["Subtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Subtotal"]),
                                    HandleChages = ds.Tables[0].Rows[i]["HandleChages"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["HandleChages"]),
                                    DiscountedAmount = ds.Tables[0].Rows[i]["DiscountedAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscountedAmount"]),
                                    DealerID = ds.Tables[0].Rows[i]["DealerID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DealerID"]),
                                    DealerName = ds.Tables[0].Rows[i]["DealerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerName"]),
                                    DiscText = ds.Tables[0].Rows[i]["DiscText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DiscText"]),
                                    TotalPayableAmount_In_Word = ds.Tables[0].Rows[i]["TotalPayableAmount_In_Word"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TotalPayableAmount_In_Word"]),
                                    TotalPayableAmount = ds.Tables[0].Rows[i]["TotalPayableAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPayableAmount"]),
                                    CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]),
                                    DeliveryInstruction = ds.Tables[0].Rows[i]["DeliveryInstruction"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["DeliveryInstruction"]),
                                    ProcessedDate = ds.Tables[0].Rows[i]["ProcessedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["ProcessedDate"]),
                                    SubCategoryId = ds.Tables[0].Rows[i]["SubCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryId"]),
                                    PanNo = ds.Tables[0].Rows[i]["PanNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PanNo"]),
                                    GstNumber = ds.Tables[0].Rows[i]["GstNumber"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["GstNumber"]),
                                    DealerStateName = ds.Tables[0].Rows[i]["DealerStateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerStateName"]),
                                    DealerDistrictName = ds.Tables[0].Rows[i]["DealerDistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerDistrictName"]),
                                    DealerBlockName = ds.Tables[0].Rows[i]["DealerBlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerBlockName"]),
                                    DealerVillageName = ds.Tables[0].Rows[i]["DealerVillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerVillageName"]),
                                    DealerAddress = ds.Tables[0].Rows[i]["DealerAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerAddress"]),
                                    HsnCode = ds.Tables[0].Rows[i]["HsnCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["HsnCode"]),
                                    Cgst = ds.Tables[0].Rows[i]["Cgst"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Cgst"]),
                                    Igst = ds.Tables[0].Rows[i]["Igst"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Igst"]),
                                    Sgst = ds.Tables[0].Rows[i]["Sgst"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["Sgst"]),
                                    Invoice_No = ds.Tables[0].Rows[i]["Invoice_No"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Invoice_No"]),
                                    RecordId = ds.Tables[0].Rows[i]["RecordId"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["RecordId"])
                                });
                                #endregion
                            }
                        }
                        #endregion

                        if (InvoiceDetailReportList.Count > 0)
                        {
                            ReportInXMail.SendMailForFoTripInvoice(InvoiceDetailReportList, podDetailReportList, sendToList, sendCCList, mailSubject, mailBody, fileName, createdby, RecordId, "Invoice_");
                        }

                        if (podDetailReportList.Count > 0)
                        {
                            mailSubject = mailSubject.Replace("Invoice", "POD");
                            ReportInXMail.SendMailForFoTripInvoice(InvoiceDetailReportList, podDetailReportList, sendToList, sendCCList, mailSubject, mailBody, fileName, createdby, RecordId, "POD_");
                        }
                    }
                }
                // podDetailReportList;
            }
            #endregion
        }
        #endregion

        #region Change By Ajay
        public int Trip_Transportation_Cost_Insert(int userid, int tripid, int vehicleid, string vehicleNo, string startKm, string EndKm, int ruleId, int subRuleId, string remark)
        {
            int flag = _rsdal.Trip_Transportation_Cost_Insert(userid, tripid, vehicleid, vehicleNo, startKm, EndKm, ruleId, subRuleId, remark);

            return flag;
        }
        public int Trip_Transportation_Expense_Insert(int userid, int tripid, int vehicleid, string Description, double Amount, int ruleId, int subRuleId, int ExpenseID)
        {
            int flag = _rsdal.Trip_Transportation_Expense_Insert(userid, tripid, vehicleid, Description, Amount, ruleId, subRuleId, ExpenseID);

            return flag;
        }
        public int Trip_Transportation_DownKm(int userid, int tripid, int ruleId, int subRuleId)
        {
            int flag = _rsdal.Trip_Transportation_DownKm(userid, tripid, ruleId, subRuleId);

            return flag;
        }
        public List<TransportCostModel> Trip_Transportation_Cost_Select(int userid, int tripid)
        {

            DataSet ds = _rsdal.Trip_Transportation_Cost_Select(userid, tripid);

            TransportCostModel transcost = new TransportCostModel();
            List<TransportCostModel> transcostlist = new List<TransportCostModel>();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        transcost = new TransportCostModel();
                        transcost.Id = ds.Tables[0].Rows[i]["Id"].ToString();
                        transcost.TripId = ds.Tables[0].Rows[i]["TripId"].ToString();
                        transcost.VehicleID = ds.Tables[0].Rows[i]["VehicleID"].ToString();
                        transcost.VehicleName = ds.Tables[0].Rows[i]["VehicleName"].ToString();
                        transcost.TranspoterName = ds.Tables[0].Rows[i]["TranspoterName"].ToString();
                        transcost.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                        transcost.TotalKm = ds.Tables[0].Rows[i]["TotalKm"].ToString();
                        transcost.TotalCost = ds.Tables[0].Rows[i]["TotalCost"].ToString();

                        transcostlist.Add(transcost);
                    }

                }
            }
            return transcostlist;
        }
        public List<Transport_TotalCost> Trip_Transportation_TotalCost_Select(int userid, int tripid)
        {


            var TransCost = _rsdal.Trip_Transportation_TotalCost_Select(userid, tripid);
            DataSet ds = _rsdal.Trip_Transportation_Expense_Select(userid, tripid);


            List<Transport_TotalCost> transcostlist = new List<Transport_TotalCost>();
            if (TransCost != null)
            {
                transcostlist.Add(TransCost);
            }
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var transcost = new Transport_TotalCost();
                        transcost.ExpenseId = Convert.ToInt32(ds.Tables[0].Rows[i]["ExpenseId"].ToString());
                        transcost.ExpenseCost = Convert.ToDouble(ds.Tables[0].Rows[i]["ExpenseCost"].ToString());
                        transcost.ExpenseType = ds.Tables[0].Rows[i]["ExpenseType"].ToString();
                        transcost.Type = ds.Tables[0].Rows[i]["Type"].ToString();
                        transcostlist.Add(transcost);
                    }

                }
            }


            return transcostlist;
        }
        public List<TransportVehicle> Trip_Transportation_Vehicle_Select(int userid)
        {

            DataSet ds = _rsdal.Trip_Transportation_Vehicle_Select(userid);

            TransportVehicle transvehicle = new TransportVehicle();
            List<TransportVehicle> transvehiclelist = new List<TransportVehicle>();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        transvehicle = new TransportVehicle();
                        transvehicle.VehicleId = Convert.ToInt32(ds.Tables[0].Rows[i]["VehicleId"].ToString());
                        transvehicle.VehicleType = ds.Tables[0].Rows[i]["VehicleType"].ToString();

                        transvehiclelist.Add(transvehicle);
                    }

                }
            }
            return transvehiclelist;
        }
        public List<Expense> Trip_Transportation_Expense_Select()
        {

            DataSet ds = _rsdal.Trip_Transportation_Expense_Select();

            Expense transvehicle = new Expense();
            List<Expense> transvehiclelist = new List<Expense>();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        transvehicle = new Expense();
                        transvehicle.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ExpenseId"].ToString());
                        transvehicle.ExpenseType = ds.Tables[0].Rows[i]["ExpenseName"].ToString();

                        transvehiclelist.Add(transvehicle);
                    }

                }
            }
            return transvehiclelist;
        }
        public List<TransportFareRule> Trip_Transportation_FareRule_Select(int userid, int vehicleid)
        {

            DataSet ds = _rsdal.Trip_Transportation_FareRule_Select(userid, vehicleid);

            TransportFareRule transFareRule = new TransportFareRule();
            List<TransportFareRule> transFareRulelist = new List<TransportFareRule>();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        transFareRule = new TransportFareRule();
                        transFareRule.RuleID = Convert.ToInt32(ds.Tables[0].Rows[i]["RuleID"].ToString());
                        transFareRule.RuleName = ds.Tables[0].Rows[i]["RuleName"].ToString();

                        transFareRulelist.Add(transFareRule);
                    }

                }
            }
            return transFareRulelist;
        }
        public int Trip_Transportation_Cost_Delete(int userid, int tripid, int Id)
        {
            int flag = _rsdal.Trip_Transportation_Cost_Delete(userid, tripid, Id);

            return flag;
        }
        public string GetTripId(int Foid)
        {
            string amount = _rsdal.GetTripId(Foid);

            return amount;
        }

        public string GetTripStatus(int Foid)

        {

            string status = _rsdal.GetTripStatus(Foid);



            return status;

        }

        #endregion

        public FarmerOrderListForDeliverViewModel Get_FarmerOrderListForDeliver(int foId, int farmerId)
        {
            FarmerOrderListForDeliverViewModel _vmodel = new FarmerOrderListForDeliverViewModel();
            DataSet ds = _rsdal.Get_FarmerOrderListForDeliver(foId, farmerId);

            if (ds != null && ds.Tables[0] != null && ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    _vmodel.OrderList = (from p in ds.Tables[0].AsEnumerable()
                                         select new FarmerOrderListForDeliverModel
                                         {
                                             orderId = p.Field<int>("OrderID"),
                                             orderRefNo = p.Field<string>("OrderRefNo"),
                                             mobile = p.Field<string>("MobNo"),
                                             deliveryDate = p.Field<string>("DeliveryDate"),
                                             orderAmount = p.Field<decimal>("OrderAmount"),
                                             orderProductList = BindPackageFoOrder(p.Field<int>("OrderID"), ds.Tables[1])
                                         }).ToList();
                }
            }
            return _vmodel;
        }
        private List<FarmerProductListForDeliverableOrders> BindPackageFoOrder(int orderId, DataTable DT)
        {
            var _list = (from p in DT.AsEnumerable()
                         where p.Field<int>("OrderID") == orderId
                         select new FarmerProductListForDeliverableOrders
                         {
                             packageId = p.Field<int>("PackageID"),
                             packageName = p.Field<string>("PackageName"),
                             productName = p.Field<string>("ProductName"),
                             Qty = p.Field<int>("Quantity"),
                             unitPrice = p.Field<decimal>("UnitPrice"),
                         }).ToList();
            return _list;
        }

        #region Dealer Scan Service
        public DS_DeaalerList GetDealerWiseProductInTrip(int userid, int trip)
        {
            DataSet ds = _rsdal.GetDealerWiseProductInTrip(userid, trip);

            DS_DeaalerList _DealerList = new DS_DeaalerList();
            List<DS_DealerDetail> _list = new List<DS_DealerDetail>();

            if (ds != null && ds.Tables[0] != null && ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DS_DealerDetail _dlr = null;
                    List<DS_DealerPackageList> _pcklist = null;
                    int dealerId = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (dealerId == 0)
                        {
                             _dlr = new DS_DealerDetail();
                            _pcklist = new List<DS_DealerPackageList>();
                            _dlr.dealerId = Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString());
                            _dlr.dealerName = ds.Tables[0].Rows[i]["DealerName"].ToString();
                            _dlr.acceptRejectFlag = Convert.ToInt32(ds.Tables[0].Rows[i]["AcceptRejectFlag"].ToString());

                            DS_DealerPackageList _items = new DS_DealerPackageList();
                            _items.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageID"].ToString());
                            _items.PackageName = ds.Tables[0].Rows[i]["PackageName"].ToString();
                            _items.Product = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            _items.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"].ToString());

                            var DR = ds.Tables[1].Select("(DealerId = "+ _dlr.dealerId+ ") AND (PackageID="+ _items.PackageId + ")");
                            _items.Detail = DS_BindProductDetail(DR);

                            _pcklist.Add(_items);
                            dealerId= Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString());
                        }
                        else if ( Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString()) != dealerId)
                        {
                            _dlr.ProdDealerList = _pcklist;
                            _list.Add(_dlr);
                            _dlr = new DS_DealerDetail();
                            _pcklist = new List<DS_DealerPackageList>();
                            _dlr.dealerId = Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString());
                            _dlr.dealerName = ds.Tables[0].Rows[i]["DealerName"].ToString();
                            _dlr.acceptRejectFlag = Convert.ToInt32(ds.Tables[0].Rows[i]["AcceptRejectFlag"].ToString());

                            DS_DealerPackageList _items = new DS_DealerPackageList();
                            _items.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageID"].ToString());
                            _items.PackageName = ds.Tables[0].Rows[i]["PackageName"].ToString();
                            _items.Product = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            _items.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"].ToString());

                            var DR = ds.Tables[1].Select("(DealerId = " + _dlr.dealerId + ") AND (PackageID=" + _items.PackageId + ")");
                            _items.Detail = DS_BindProductDetail(DR);


                            _pcklist.Add(_items);
                            dealerId = Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString());
                        }
                        else if(Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString()) == dealerId)
                        {
                            DS_DealerPackageList _items = new DS_DealerPackageList();
                            _items.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageID"].ToString());
                            _items.PackageName = ds.Tables[0].Rows[i]["PackageName"].ToString();
                            _items.Product = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            _items.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty"].ToString());

                            var DR = ds.Tables[1].Select("(DealerId = " + _dlr.dealerId + ") AND (PackageID=" + _items.PackageId + ")");
                            _items.Detail = DS_BindProductDetail(DR);


                            _pcklist.Add(_items);
                        }
                    }
                    _dlr.ProdDealerList = _pcklist;
                    _list.Add(_dlr);
                    _DealerList.DealerList = _list;
                }
            }
            return _DealerList;
        }
        private List<DS_DealerPackageDetail> DS_BindProductDetail(DataRow [] DRA)
        {
            List<DS_DealerPackageDetail> _list = new List<DS_DealerPackageDetail>();

            foreach(DataRow DR in DRA)
            {
                DS_DealerPackageDetail _obj = new DS_DealerPackageDetail();
               
                _obj.RecordId =Convert.ToInt32( DR[0]);
                _obj.PackageId = Convert.ToInt32(DR[1]);
                _obj.Qty = Convert.ToInt32(DR[2]);
                _obj.DealerId = Convert.ToInt32(DR[3]);

                _list.Add(_obj);
            }

            return _list;
        }
        public int DealerPackagingAcceptRejectByFO(int userid, string actionTaken, int dealerId, List<DS_ProductPostDetail> ProductList)
        {
            DataTable DT = Helper.Helper.ToDataTable(ProductList);
            int flag = _rsdal.DealerPackagingAcceptRejectByFO(userid, actionTaken, dealerId, DT);

            return flag;
        }

        public int CheckTripStatus(int TripId, int FoId)
        {
            int flag = _rsdal.CheckTripStatus(TripId, FoId);


            return flag;
        }
        #endregion

        public SathiCreatedOrderResult CreateOrderBySathi(SathiOrderCreateModel obj)
        {
            //int id = _rsdal.GetFarmerIdByMobile(mobile);
            DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            SathiCreatedOrderResult objCreateData = _rsdal.CreateOrderBySathi(obj.userid, obj.Farmer.FarmerId, obj.Farmer.FarmerName, obj.Farmer.FatherName, obj.Farmer.Mobile,
                obj.Farmer.StateId, obj.Farmer.DistrictId, obj.Farmer.BlockId, obj.Farmer.VillageId, obj.Farmer.OtherVillageName, obj.Farmer.Address,
                obj.DeliveryDate, obj.ModeOfPayment, DT);
            return objCreateData;
        }
    }
}
