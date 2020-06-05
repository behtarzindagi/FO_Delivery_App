using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entity;
using BusinessLayer;
using System.Configuration;
using System.Net;
using System.Web;
using System.IO;
using Helper;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Reflection;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace BZAgentServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AgentApp : IAgentApp
    {
        AgentAppBal _agentbal = new AgentAppBal();
        public Farmer GetFarmerByFsc(string apiKey, string FscId, int Mode =0)
        {
            Farmer fr = new Farmer();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    FarmerViewModel a = _agentbal.GetFarmerByFsc(FscId,Mode);
                    var f = new List<FarmerModel>();

                    fr.Farmers = a;
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(FscId));
                }
            }
            return fr;
        }
        public BZAgentProductViewModel GetCategoryProductDetail(string apiKey, int Stateid, int Districtid, int CatId, int SubCatId, int PackageID = 0)
        {
            BZAgentProductViewModel _catProList = new BZAgentProductViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _catProList = _agentbal.GetCategoryProductDetail(Stateid, Districtid, CatId, SubCatId, PackageID);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _catProList;
        }
        public UserLoginDetails UserLogin(string apiKey, string UserId, string Password)
        {
            UserLoginDetails _userLD = new UserLoginDetails();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _userLD = _agentbal.UserLogin(UserId, Password);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _userLD;
        }

        public void  ChangePassword(string apiKey, int userid, string password, string newpassword)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("status", "Some problem occurred ,Please try again later.");
            returndata.Add("UserID", Convert.ToString(userid));
            returndata.Add("statusId", Convert.ToString(0));
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.ChangePassword(userid, password, newpassword);
                
                 if (flag == 1)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Add("statusId", Convert.ToString(flag));
                    returndata.Add("status", "Password Updated Successfully.");
                }
                else if (flag == 2)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Add("statusId", Convert.ToString(flag));
                    returndata.Add("status", "You have entered Wrong Old Passwod.");

                }
                else if (flag == 3)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Add("statusId", Convert.ToString(flag));
                    returndata.Add("status", "User does not exist.");

                }
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }
        public FSCOrderDetails GetOrderDetails(string apiKey, int FSCId,string Mode, string fromdate, string todate, int RoleId = 0, int status = 0)
        {
            FSCOrderDetails FSCOrderList = new FSCOrderDetails();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {

                try
                {
                    FSCOrderList = _agentbal.GetOrderDetails(FSCId, RoleId, fromdate, todate, status,Mode);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            else
            { }
            return FSCOrderList;

        }
        public GetPODOrderDetailViewModel GetOrderDetails_OrderID(string apiKey, int orderid)
        {
            GetPODOrderDetailViewModel ord = null;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    ord = _agentbal.GetOrderDetails_OrderID(orderid);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return ord;

        }
        public SearchProduct GetSearchProductDetail(string apiKey, string SearchKey = null, string sortColumn = null, string sortColumnDir = "asc", int pageNo = 0, int pageSize = 100, int stateId = 0, int Districtid = 0, int cropID = 0, int categoryID = 0, int SubCatId = 0, int CompanyId = 0, int totalRecords = 0)
        {
            SearchProduct _catProList = new SearchProduct();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _catProList = _agentbal.GetSearchProducts(true, categoryID, SubCatId, CompanyId, 0, stateId, Districtid, 0, "0", cropID, SearchKey, pageNo, pageSize, sortColumn, sortColumnDir);

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _catProList;
        }
        public SaleOrderDetail GetSaleOrder(string apiKey, string fromdate, string todate, string Mode, string DistrictId = "0", int UserID=0, int stateId=6)
        {
            SaleOrderDetail ord = null;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    ord = _agentbal.GetSaleOrder(fromdate, todate, Mode, DistrictId, UserID,stateId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return ord;

        }
        public GenericViewModel GetDistrictBlockVilage(string apiKey, int id, char type)
        {
            GenericViewModel list = new GenericViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    GenericViewModel a = _agentbal.GetDistrictBlockVilage(id, type);

                    list = a;

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, id);
                }
            }
            return list;

        }
        public PromoCouponModel GetCouponList(string apiKey, string CatId = "0", string SubCatId = "0", string CompnanId = "0", string BrandId = "0", string PCKGId = "0", string Itemval = "0")
        {
            PromoCouponModel list = new PromoCouponModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    PromoCouponModel a = _agentbal.GetCouponList(CatId, SubCatId, CompnanId, BrandId, PCKGId, Itemval);

                    list = a;

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return list;

        }
        public GenericViewModel GetCategorySubCategory(string apiKey, int id, string type)
        {
            GenericViewModel list = new GenericViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    GenericViewModel a = _agentbal.GetCategorySubCategory(id, type);

                    list = a;

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, id);
                }
            }
            return list;

        }
        public void DemandOrderCreate(DemandCreateModel obj)
        {
            int flag = 0;
            Dictionary<string, int> returndata = new Dictionary<string, int>();
            returndata.Add("status", 0);

            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.DemandOrderCreate(obj);

                    //if (flag > 0)
                    //{
                    //    flag = 1;
                    //}
                    //else
                    //{ flag = 0; }

                    returndata.Remove("status");
                    returndata.Add("status", flag);

                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add(ex.Message, 0);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }
        public ApplyPromoCoupon ApplyCoupon(string apiKey, int CatID, int SubCatID, int CompanyID, int BrandID, int ProductID, int PkgID, int Qty, decimal ActualAmt, int CouponID)
        {
            ApplyPromoCoupon _applyPromoCoupon = new ApplyPromoCoupon();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    return new AgentAppBal().ApplyCoupon(CatID, SubCatID, CompanyID, BrandID, ProductID, PkgID, Qty, ActualAmt, CouponID);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message,0);
                }
            }
            return _applyPromoCoupon;
        }
        public void AgentOrderCreate(AgentOrderCreateModel obj)
        {
            int flag = 0;
            Dictionary<string, int> returndata = new Dictionary<string, int>();
            returndata.Add("status", 0);
            returndata.Add("OrderId", 0);

            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.AgentOrderCreate(obj);

                    if (flag > 0)
                    {

                        returndata.Remove("status");
                        returndata.Remove("OrderId");
                        returndata.Add("status", 1);
                        returndata.Add("OrderId", flag);
                    }
                    else
                    {
                        returndata.Remove("status");
                        returndata.Remove("OrderId");
                        returndata.Add("status", 0);
                        returndata.Add("OrderId", 0);
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add(ex.Message, 0);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.userid);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }
        public FarmerDetails GetFarmerDetails(string apiKey, string FarmerKey)
        {
            FarmerDetails fr = new FarmerDetails();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                  fr = _agentbal.GetFarmerDetails(FarmerKey);
                   
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return fr;
        }

        public UserValidation GetUserStatus(string apiKey,int UserId)
        {
            UserValidation uVal = new UserValidation();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    uVal = _agentbal.GetUserStatus(UserId);

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return uVal;
        }

        public void IssueRegister(Issue obj)
        {
            int flag = 0;
            Dictionary<string, int> returndata = new Dictionary<string, int>();
            returndata.Add("status", 0);

            if (obj.apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.IssueRegister(obj);

                    if (flag > 0)
                    {
                        flag = 1;
                    }
                    else
                    { flag = 0; }

                    returndata.Remove("status");
                    returndata.Add("status", flag);

                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add(ex.Message, 0);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public IssueDetailByFarmer IssueDetailByFarmer(string apiKey, string MobileNo)
        {
            IssueDetailByFarmer ord = null;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    ord = _agentbal.IssueDetailByFarmer(MobileNo);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return ord;

        }

        public void ComplaintRegister(Complaint obj)
        {
            int flag = 0;
            Dictionary<string, int> returndata = new Dictionary<string, int>();
            returndata.Add("status", 0);

            if (obj.apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.ComplaintRegister(obj);

                    if (flag > 0)
                    {
                        flag = 1;
                    }
                    else
                    { flag = 0; }

                    returndata.Remove("status");
                    returndata.Add("status", flag);

                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add(ex.Message, 0);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public CompDetailByFarmer GetFarmerComplaint(string apiKey, int OrderID=0,int FarmerId= 0)
        {
            CompDetailByFarmer ord = null;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    ord = _agentbal.GetFarmerComplaint(OrderID,FarmerId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return ord;

        }
        public void RemoveItemFromCart(RemoveItemFromCart obj)
        {
            string Msg = "";
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("statusMsg", "");
          

            if (obj.apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    Msg = _agentbal.RemoveItemFromCart(obj);

                    if (Msg !="")
                    {

                        returndata.Remove("statusMsg");
                        returndata.Add("statusMsg", Msg);
                       
                    }
                    else
                    {
                        returndata.Remove("statusMsg");
                        returndata.Add("statusMsg", "You can not delete this item.");
                    
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("statusMsg");
                    returndata.Add("statusMsg", "Internal error,please try after sometime.");
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public FSCOrderDetails GetOrderDetailsByFarmerID(string apiKey, int FarmerID, string fromdate, string todate, int status = 0)
        {
            FSCOrderDetails FSCOrderList = new FSCOrderDetails();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {

                try
                {
                    FSCOrderList = _agentbal.GetOrderDetailsByFarmerID(FarmerID, fromdate, todate, status);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            else
            { }
            return FSCOrderList;

        }
        public OrderWiseProductViewModel OrderWiseProduct(string apiKey, int orderid)
        {
            OrderWiseProductViewModel FSCOrderList = new OrderWiseProductViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {

                try
                {
                    FSCOrderList = _agentbal.OrderWiseProduct(orderid);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            else
            { }
            return FSCOrderList;

        }

        public void AgentOrderConfirmation(AgentOrderUpdateModel obj)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("status", "Some problem occurred ,Please try again later.");
            returndata.Add("OrderId",Convert.ToString(obj.OrderID));
            returndata.Add("statusId", Convert.ToString(obj.statusId));

            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.AgentOrderConfirmation(obj);

                    if (flag == 1)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId",Convert.ToString(obj.statusId));
                        returndata.Add("status", "Order Confirmed Successfully.");       
                    }
                  else  if (flag == 2)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", Convert.ToString(obj.statusId));
                        returndata.Add("status", "Order Cancelled Successfully.");

                    }
                    else if (flag == 3)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", Convert.ToString(obj.statusId));
                        returndata.Add("status", "Order does not exist.");

                    }
                    else if (flag == 4)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", Convert.ToString(obj.statusId));
                        returndata.Add("status", "Order Delivery Date Changed.");

                    }
                    else
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId","0");
                        returndata.Add("status", "Some problem occurred ,Please try again later.");  
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Add("statusId", "0");
                    returndata.Add("Some problem occurred ,Please try again later.",ex.Message);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.userid);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public void SaveCallLog(CallLogged obj)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("statusId", "0");
            returndata.Add("status", "Some problem occurred ,Please try again later.");
         
         //   returndata.Add("OrderId", Convert.ToString(obj.OrderID));

            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.SaveCallLog(obj);

                    if (flag > 0)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", "1");
                        returndata.Add("status", "Call Logged Saved Successfully.");
                    }
                   
                    else
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", "0");
                        returndata.Add("status", "Some problem occurred ,Please try again later.");
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Add("statusId", "0");
                    returndata.Add("Some problem occurred ,Please try again later.", ex.Message);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.userid);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public int UpdateCallLog(string apiKey,string MobileNO)
        {
            int flag = 0;
          

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.UpdateCallLog(MobileNO);

                }
                catch (Exception ex)
                {
                  
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return flag;
        }
        public void FarmerData(FarmerData obj)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("statusId", "0");
            returndata.Add("FarmerID", "0");
            returndata.Add("status", "Some problem occurred ,Please try again later.");
            returndata.Add("Mobile", obj.MobileNo);


            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.FarmerData(obj);

                    if (obj.statusId == 1)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Remove("FarmerID");
                        returndata.Add("statusId", "1");
                        returndata.Add("FarmerID", Convert.ToString(flag));
                        returndata.Add("status", "Farmer Registered Successfully.");
                    }
                    else if (obj.statusId == 2)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Remove("FarmerID");
                        returndata.Add("statusId", "2");
                        returndata.Add("FarmerID", Convert.ToString(flag));
                        returndata.Add("status", "Farmer updated Successfully.");

                    }
                    else if (flag == 3)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Remove("FarmerID");
                        returndata.Add("statusId", "3");
                        returndata.Add("FarmerID", "0");
                        returndata.Add("status", "Mobileno is not valid,Please Check.");

                    }
                    else
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Remove("FarmerID");
                        returndata.Add("statusId", "0");
                        returndata.Add("FarmerID", "0");
                        returndata.Add("status", "Some problem occurred ,Please try again later.");
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Remove("FarmerID");
                    returndata.Add("statusId", "0");
                    returndata.Add("FarmerID", "0");
                    returndata.Add("Some problem occurred ,Please try again later.", ex.Message);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.UserID);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public void BlacklistFarmer(string apiKey, string MobileNo,int UserID,string Remark)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("status", "Some problem occurred ,Please try again later.");
            returndata.Add("Mobile", MobileNo);


            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.BlacklistFarmer(MobileNo,UserID,Remark);

                    if (flag > 0)
                    {
                        returndata.Remove("status");
                        returndata.Add("status", "Farmer Mobile Number BlackListed Successfully.");
                    }
                  
                    else
                    {
                        returndata.Remove("status");
                        returndata.Add("status", "Already BlackListed.");
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add("Some problem occurred ,Please try again later.", ex.Message);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, UserID);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public FarmecallHistory GetFarmerCallHistory(string apiKey, string MobileNo)
        {
            FarmecallHistory fr = new FarmecallHistory();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    fr = _agentbal.GetFarmerCallHistory(MobileNo);

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return fr;
        }
        
        public int Ozontel_CallLog_Insert(Stream data)
        {
            int flag = 0;
            try
            {
                StreamReader reader = new StreamReader(data);
                string text = reader.ReadToEnd();            
                text = text.Replace("data=", "");
                text = text.Replace("%7B", "{");
                text = text.Replace("%22", "\"");
                text = text.Replace("%3A", ":");
                text = text.Replace("+", " ");
                text = text.Replace("%2C", ",");
                text = text.Replace("%7D", "}");
                text = text.Replace("%2F", "/");
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Ozontel_CallLog_Model>(text);            
                if (obj.Apikey == ConfigurationManager.AppSettings["ozonetelauthkey"])
                {                   
                    try
                    {
                        flag = _agentbal.Ozontel_CallLog_Insert(obj);
                    }
                    catch (Exception ex)
                    {
                        LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return flag;
        }

        public List<Ozontel_CallLog_Model2> Ozontel_CallLog_Select(string Apikey, int userid)
        {
            List<Ozontel_CallLog_Model2> _list = new List<Ozontel_CallLog_Model2>();

            if (Apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _list = _agentbal.Ozontel_CallLog_Select(userid);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _list;
        }

        public void AvisCallLog(string EventName, string ANI, string DNIS, string Mode, string CallId, string UserLogin, string Campaign, string LeadId, string Skill, string dnisIB,string CallFileName,string CallDisposition)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("status", "Some problem occurred ,Please try again later.");
            returndata.Add("Mobile", ANI);


            if (!string.IsNullOrEmpty(ANI))
            {
                try
                {
                    flag = _agentbal.AvisCallLog(EventName, ANI, DNIS, Mode, CallId, UserLogin, Campaign, LeadId, Skill, dnisIB, CallFileName, CallDisposition);

                    if (flag > 0)
                    {
                        returndata.Remove("status");
                        returndata.Add("status", "Success");
                    }

                    else
                    {
                        returndata.Remove("status");
                        returndata.Add("status", "Already BlackListed.");
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add("Some problem occurred ,Please try again later.", ex.Message);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public void SaveAgreement(AgreementDetails obj)
        {
            int flag = 0;
            Dictionary<string, string> returndata = new Dictionary<string, string>();
            returndata.Add("statusId", "0");
            returndata.Add("status", "Some problem occurred ,Please try again later.");
            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _agentbal.SaveAgreement(obj);

                    if (flag==1)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", "1");
                        returndata.Add("status", "Agreement Details Saved Successfully.");
                    }
                  else  if (flag == 2)
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", "2");
                        returndata.Add("status", "Agreement Details Already Exists.");
                    }
                    else
                    {
                        returndata.Remove("status");
                        returndata.Remove("statusId");
                        returndata.Add("statusId", "0");
                        returndata.Add("status", "Some problem occurred ,Please try again later.");
                    }


                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Remove("statusId");
                    returndata.Add("statusId", "0");
                    returndata.Add("Some problem occurred ,Please try again later.", ex.Message);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public void UploadPhoto(UploadPhotoModel obj)
        {
            int flag = 0;
            Dictionary<string, int> returndata = new Dictionary<string, int>();
            returndata.Add("status", 0);

            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {

                try
                {
                    #region To Convert string into image
                    //  string fullpath = "sjdhsj";

                    byte[] bytes = Convert.FromBase64String(obj.imageBase64String);
                    string path = ConfigurationManager.AppSettings["agreementimagepath"];
                    string fullpath = path + "user" + obj.userid.ToString() + ".jpg";
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bytes)))
                    {


                        if (File.Exists(fullpath))
                        {
                            File.Delete(fullpath);
                        }
                        // image.Save(@"D:\Temp\output.jpg", ImageFormat.Jpeg);  // Or Png
                        image.Save(fullpath, ImageFormat.Jpeg);
                    }
                    #endregion

                    flag = _agentbal.UploadPhoto(obj.userid, obj.Lat,obj.Lang, fullpath);

                    returndata.Remove("status");
                    returndata.Add("status", flag);
                    // flag = 1;
                }
                catch (Exception ex)
                {
                    returndata.Remove("status");
                    returndata.Add(ex.Message, 0);
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.userid);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }

        public PaymentRequestParam GetPaymentRequestParam(string apiKey)
        {
            PaymentRequestParam fr = new PaymentRequestParam();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    fr = _agentbal.GetPaymentRequestParam();

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return fr;
        }
    }
}
