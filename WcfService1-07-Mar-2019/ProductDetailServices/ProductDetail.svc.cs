using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entity;
using BusinessLayer;
using System.Configuration;
using System.Web;
using System.Reflection;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ProductDetailServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ProductDetail : IProductDetail
    {
        ProductDetailBal _productbal = new ProductDetailBal();
        ReasonStatusBal _rsbal = new ReasonStatusBal();
        TransporterBal _tranBal = new TransporterBal();
        BZBannerBal _offerBanner = new BZBannerBal();

        public BzProductDtl GetProductDetail(string apikey, int ProductId)
        {
            BzProductDtl pd = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    pd = _productbal.GetBzProductDetail(apikey, ProductId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return pd;
        }

        public BzVegetableDetail GetVegetableDetail(string apikey, int ProductId)
        {
            BzVegetableDetail pd = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    pd = _productbal.GetBzVegetablesContent(apikey, ProductId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return pd;
        }

        public BzFlowersDetail GetFlowersDetail(string apikey, int BzProductId)
        {
            BzFlowersDetail pd = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    pd = _productbal.GetFlowersDetail(apikey, BzProductId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return pd;
        }

        public string ReferenceByfarmer(string apiKey, string ReferTo, string ReferBy)
        {
            string status = string.Empty;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.InsertReferal(apiKey, ReferTo, ReferBy);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return status;
        }
        // ReferedFarmerAfterpayment

        public Referal FarmerAfterpayment(string apiKey, string ReferTo, string ReferBy, int Amount)
        {
            Referal obj = new Referal();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    obj = _productbal.InsertForCouponGenaration(apiKey, ReferTo, ReferBy, Amount);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return obj;
        }
        //CouponDetails
        public PaymentOptionList CouponDetails(string apiKey, string ReferBy,int BzProductId)
        {
            PaymentOptionList CouponList = new PaymentOptionList();
            //var CouponList = new List<PaymentOptionList>();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    CouponList = _productbal.CouponDetails(apiKey, ReferBy,BzProductId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return CouponList;
        }

        public List<Referal> FarmerLogin(string apiKey, string MobNo, string ShareCode, int FarmerId, string DeviceId)
        {
            var userList = new List<Referal>();
            // obj.DiscountInfoValue = new List<Referal>();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    if (!string.IsNullOrEmpty(MobNo) || FarmerId != 0)
                    {
                        userList = _productbal.FarmerLogin(MobNo, ShareCode, FarmerId, DeviceId);
                    }
                    else
                    {
                        userList = null;
                    }


                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return userList;
        }

        public bool UpdateFarmerAddress(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId,
                int BlockId, int VillageId, string NearByVillage, string Address)
        {
            bool status = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.UpdateFarmerAddress(apiKey, FarmerId, RefSource, Fname, Lname, FatherName, Mobile, StateId, DistrictId, BlockId, VillageId, NearByVillage, Address);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return status;
        }

        //public bool UpdateFarmerAddressN(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId,
        //        int BlockId, int VillageId, string NearByVillage, string Address, string Landmark, string Pincode)
        //{
        //    bool status = false;
        //    if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
        //    {
        //        try
        //        {
        //            status = _productbal.UpdateFarmerAddressN(apiKey, FarmerId, RefSource, Fname, Lname, FatherName, Mobile, StateId, DistrictId, BlockId, VillageId, NearByVillage, Address, Landmark, Pincode);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
        //        }
        //    }
        //    return status;
        //}
        // By Lalit

        public BzShareLink GetBZappShareLinkUrl(string source, string campaign, string ShareCode)
        {
            string ShareLink = String.Empty;
            BzShareLink url = new BzShareLink();
            //if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            //{
            try
            {
                url = _productbal.GetBzAppShareLink(source, campaign, ShareCode);
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Redirect;
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Location", url.BzShareUrl);
            }
            catch (Exception ex)
            {
                LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            //}
            return url;
        }

        public BZProduct GetBZProduct(string apiKey, int CatId, int DistrictId, string DistrictName, string PinCode, int PageIndex, int PageSize)
        {
            BZProduct _ProList = new BZProduct();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _ProList = _productbal.GetBzProducts(CatId, DistrictId, DistrictName, PinCode, PageIndex, PageSize);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _ProList;
        }
        public bool ProductAvailableByDistrict(string apiKey, int DistrictId, int PackageId)
        {

            bool status = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.ProductAvailableByDistrict(apiKey, DistrictId, PackageId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return status;
        }
        // ORDER CREATION //
        public void OrderCreate(OrderCreateModel obj)
        {
            int flag = 0;
            Dictionary<string, int> returndata = new Dictionary<string, int>();
            returndata.Add("status", 0);

            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _rsbal.OrderCreate(obj);
                    returndata.Remove("status");
                    returndata.Add("status", flag);

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
        // by akaram ali on 29 sep 2018
        public BZProductInterestCount GetBZProductInterestCount(string apiKey, int PackID, string MobNo)
        {
            BZProductInterestCount _ProInterestList = new BZProductInterestCount();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _ProInterestList = _productbal.GetBZProductInterestCount(PackID, MobNo);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _ProInterestList;
        }

        public string FarmerAppInstallationInfo(string apiKey, string DeviceId, string ReferencedBy)
        {
            string status = "";

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.FarmerAppInstallationInfo(apiKey, DeviceId, ReferencedBy);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return status;
        }
        public bool UpdateFCMId(string apiKey, string DeviceId, string FCMId)
        {
            bool status = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.UpdateFCMId(apiKey, DeviceId, FCMId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return status;
        }

        public string GetFCMId(string apiKey, string DistrictId, string PackageId, string SDate, string EDate, int PageIndex, int PageSize)
        {
            string _FCMIdList = string.Empty;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _FCMIdList = _productbal.GetFCMId(DistrictId, PackageId, SDate, EDate, PageIndex, PageSize);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _FCMIdList;
        }

        public StateDistrictBlockVillageList GetStateDistrictBlockVilage(string apiKey, int Id, string Type)
        {
            StateDistrictBlockVillageList _StateDistList = new StateDistrictBlockVillageList();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _StateDistList = _productbal.GetStateDistrictBlockVilage(Id, Type);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _StateDistList;
        }

        public StateDistrictBlockVillageList GetAllStateDistrictBlockVilage(string apiKey, int Id, string Type)
        {
            StateDistrictBlockVillageList _StateDistList = new StateDistrictBlockVillageList();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _StateDistList = _productbal.GetAllStateDistrictBlockVilage(Id, Type);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _StateDistList;
        }

        public int CreateFarmerLead(string apiKey, string FName, string LName, string MobNo, int StateId, string StateName, int DistrictId, string DistrictName, int BlockId,
                string BlockName, int VillageId, string VillageName, string NearbyVillage, string AdditionalAddress)
        {
            int flag = 0;

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _productbal.CreateFarmerLead(FName, LName, MobNo, StateId, StateName, DistrictId, DistrictName, BlockId,
                BlockName, VillageId, VillageName, NearbyVillage, AdditionalAddress);

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return flag;
        }

        public BzProductDtl GetBzLiveStockDetails(string apikey, int ProductId)
        {
            BzProductDtl pd = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    pd = _productbal.GetBzLiveStockDetails(apikey, ProductId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return pd;
        }
        public BzProductDtl GetBzMachineryDetails(string apikey, int ProductId)
        {
            BzProductDtl pd = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    pd = _productbal.GetBzMachineryDetails(apikey, ProductId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return pd;
        }

        public string GetURL(LoanDetail obj)
        {
            string msg = string.Empty;
            // url = "http://example.com/file?a=1&b=2&c=string%20param";
            string decodeUrl = HttpUtility.UrlDecode(obj.ParameterValue);
            string querystring = (decodeUrl.Substring(decodeUrl.IndexOf('?'))).Replace("?", "");
            string JSONString = "";

            using (SqlConnection conn = new SqlConnection())
            {
                var dict = HttpUtility.ParseQueryString(querystring);
                JSONString = JsonConvert.SerializeObject(dict.AllKeys.ToDictionary(k => k, k => dict[k]));
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_InsertLoanParametersValues", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParameterValue", JSONString);
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        msg = "Success";
                    }
                    else { msg = "Fail"; }
                }
            }
            return JSONString;
        }
        public bool InsertUpdateLoan(string apiKey, string FarmerName, string MobileNumber, int StateId, int DistrictId, int VillageId,
       string LandMark, string PanCardNo, string UIDNo, string VehicleModelNo, DateTime VehiclePurchaseDate, string LoanType,
       decimal RequiredLoanAmount, bool IsLastLoan)
        {
            bool isSuccess = false;
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_InsertUpdateLoanDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoanId", 0);
                    cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                    cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                    cmd.Parameters.AddWithValue("@StateId", StateId);
                    cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                    cmd.Parameters.AddWithValue("@VillageId", VillageId);
                    cmd.Parameters.AddWithValue("@LandMark", LandMark);
                    cmd.Parameters.AddWithValue("@PanCardNo", PanCardNo);
                    cmd.Parameters.AddWithValue("@UIDNo", UIDNo);
                    cmd.Parameters.AddWithValue("@VehicleModelNo", VehicleModelNo);
                    cmd.Parameters.AddWithValue("@VehiclePurchaseDate", VehiclePurchaseDate);
                    cmd.Parameters.AddWithValue("@LoanType", LoanType);
                    cmd.Parameters.AddWithValue("@RequiredLoanAmount", RequiredLoanAmount);
                    cmd.Parameters.AddWithValue("@IsLastLoan", IsLastLoan);
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i >= 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }

        public bool InsertQuestionAns(string Question, string AnsText, string AnsValue, string FarmerName, string MobileNo)
        {
            bool isSuccess = false;
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_Tbl_QuestionsAns", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@Question", Question);
                    cmd.Parameters.AddWithValue("@AnsText", AnsText);
                    cmd.Parameters.AddWithValue("@AnsValue", AnsValue);
                    cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }

        public bool InsertQuestionAnsTest(QuestionAns obj)
        {
            bool isSuccess = false;
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_Tbl_QuestionsAns", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@Question", obj.Question);
                    cmd.Parameters.AddWithValue("@AnsText", obj.AnsText);
                    cmd.Parameters.AddWithValue("@AnsValue", obj.AnsValue);
                    cmd.Parameters.AddWithValue("@FarmerName", obj.FarmerName);
                    cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }

        public List<Contest> GetContestData(string apiKey)
        {
            string JSONString = string.Empty;
            List<Contest> list = new List<Contest>();
            using (SqlConnection conn = new SqlConnection())
            {
                DataSet ds = new DataSet();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_Get_Tbl_QuestionsDetails", conn))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    Contest objContest = new Contest();


                    List<Option> IList = new List<Option>();
                    foreach (DataRow dr1 in ds.Tables[1].Rows)
                    {
                        IList.Add(new Option
                        {
                            OptionId = Convert.ToInt32(dr1["QuesOptionsId"].ToString()),
                            QuetionId = Convert.ToInt32(dr1["QuetionId"].ToString()),
                            OptionValueId = Convert.ToInt32(dr1["OptionValue"].ToString()),
                            OptionText = Convert.ToString(dr1["OptionText"].ToString())
                        });
                    }
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int id = Convert.ToInt32(dr["QuetionId"].ToString());
                        list.Add(new Contest
                        {
                            QuestionId = Convert.ToInt32(dr["QuetionId"].ToString()),
                            QuestionText = Convert.ToString(dr["QuestionText"]),
                            RightAnsID = Convert.ToInt32(dr["QuestionRightAns"]),
                            Option = new List<Option>()
                        });
                    }
                    foreach (var d in list)
                    {
                        foreach (Option QOp in IList)
                        {
                            if (QOp.QuetionId == d.QuestionId)
                            {
                                d.Option.Add(new Option
                                {
                                    OptionId = QOp.OptionId,
                                    QuetionId = QOp.QuetionId,
                                    OptionText = QOp.OptionText,
                                    OptionValueId = QOp.OptionValueId
                                });
                            }
                        }
                    }

                    JSONString = JsonConvert.SerializeObject(list);
                }
                //HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                //HttpContext.Current.Response.Write(JSONString);
                return list;

                //return (new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(JSONString));
            }
        }

        public bool InsertUsersQuestionsAns(int QuetionId, int AnsValue, string AnsText, string MobileNo, string FarmerName)
        {
            bool isSuccess = false;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_Tbl_InsertUsersAns", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuetionId", QuetionId);
                    cmd.Parameters.AddWithValue("@AnsValue", AnsValue);
                    cmd.Parameters.AddWithValue("@AnsText", AnsText);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@FarmerName", @FarmerName);
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }

        public BzLeftMenuList GetBZLeftMenu(string apikey)
        {
            BzLeftMenuList ml = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    ml = _productbal.GetBZLeftMenu(apikey);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return ml;
        }

        #region Google Navigation 
        public TripCreateModel GetTripCreateData(string apiKey, string type, int districtid, string blockId, decimal avgVehicleSpeed, decimal workingHours, decimal breakTimePerDeliveryInMinute, decimal lunchTime, int BaggSize)
        {
            TripCreateModel model = new TripCreateModel();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    model = _rsbal.GetTripCreateData(type, districtid, blockId, avgVehicleSpeed, workingHours, breakTimePerDeliveryInMinute, lunchTime, BaggSize);


                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }

            return model;
        }

        public GeoTagCategoryViewModel GetGeoCatSubCategoies(string apiKey)
        {
            GeoTagCategoryViewModel model = new GeoTagCategoryViewModel();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    model = _rsbal.GetGeoCatSubCategoies();


                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }

            return model;
        }

        public string GeoTaggingTagInsert(string apiKey, int userid, int subCategoryId, string geoName, string lat, string longitute, int villageId, string otherVillage, int blockId)
        {
            string flag = "0";
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    flag = _rsbal.GeoTaggingTagInsert(userid, subCategoryId, geoName, lat, longitute, villageId, otherVillage, blockId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
                }
            }
            return flag;
        }
        public GeoTaggingDataViewModel GetGeoTaggingData(string apiKey)
        {
            GeoTaggingDataViewModel model = new GeoTaggingDataViewModel();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    model = _rsbal.GetGeoTaggingData();


                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }

            return model;
        }
        public GeoTaggingDataWithVillageViewModel GetGeoTaggingDataWithVillage(string apiKey, int userId)
        {
            GeoTaggingDataWithVillageViewModel model = new GeoTaggingDataWithVillageViewModel();

            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    model = _rsbal.GetGeoTaggingDataWithVillage(userId);


                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }

            return model;
        }
        #endregion

        public bool InsertUsersDetailofContest(string MobileNo, string FarmerName, int StateId, int DistrictId, string CropDetails, string CropSpecies, string CropArea)
        {
            bool isSuccess = false;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_InsertUsersDetailofContest", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                    cmd.Parameters.AddWithValue("@StateId", StateId);
                    cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                    cmd.Parameters.AddWithValue("@CropDetails", CropDetails);
                    cmd.Parameters.AddWithValue("@CropSpecies", CropSpecies);
                    cmd.Parameters.AddWithValue("@CropArea", CropArea);

                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }
        public BZOfferBannerList GetBZOfferBanner(string apikey)
        {
            BZOfferBannerList offerBanner = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    offerBanner = _productbal.GetBzOfferBanner(apikey);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return offerBanner;
        }

        public SathiOrderDetails Get_SathiOrderDetails(string apikey, int UserId, int DateMode, string  SDATE, string EDATE)
        {
            SathiOrderDetails _sathiOrderDetails = new SathiOrderDetails();
            SathiOrderDetailsBal _sathiOrder = new SathiOrderDetailsBal();
            DateTime StartDATE= (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue; ;
            DateTime EndDATE = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
            //if (!string.IsNullOrEmpty(SDATE))
            //{
            //    StartDATE = Convert.ToDateTime(SDATE);
            //    EndDATE = Convert.ToDateTime(EDATE);
            //}
            if (!string.IsNullOrEmpty(SDATE.ToString()))
            {
                StartDATE = Convert.ToDateTime(SDATE);
                EndDATE = Convert.ToDateTime(EDATE);                
            }
            //else
            //{
            //    StartDATE = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            //    EndDATE = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
            //}
            
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _sathiOrderDetails = _sathiOrder.GetSathiSalesReport(apikey, UserId, DateMode, StartDATE, EndDATE);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _sathiOrderDetails;
        }

        public SathiPaymentDetails GetSathiPaymentDetails(string apikey, int UserId, int DateMode, string SDATE, string EDATE)
        {
            SathiPaymentDetails _sathiPaymentDetails = new SathiPaymentDetails();
            SathiOrderDetailsBal _sathiPayment = new SathiOrderDetailsBal();
            DateTime StartDATE = new DateTime();
            DateTime EndDATE = new DateTime();
            if (!string.IsNullOrEmpty(SDATE))
            {
                StartDATE = Convert.ToDateTime(SDATE);
                EndDATE = Convert.ToDateTime(EDATE);
            }

            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _sathiPaymentDetails = _sathiPayment.GetSathiPaymentReport(apikey, UserId, DateMode, StartDATE, EndDATE);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _sathiPaymentDetails;
        }

        public bool InsertFPOData(string apiKey, string FPOName, string Designation, string ProductRequired, int Quantity, int StateId, int DistrictId, int BlockId,
            string MobileNo, int CreatedBy)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("Usp_FPO_Data", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FPOName", FPOName);
                            cmd.Parameters.AddWithValue("@Designation", Designation);
                            cmd.Parameters.AddWithValue("@ProductReqired", ProductRequired);
                            cmd.Parameters.AddWithValue("@Quantity", Quantity);
                            cmd.Parameters.AddWithValue("@StateId", StateId);
                            cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                            cmd.Parameters.AddWithValue("@BlockId", BlockId);
                            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                            cmd.Parameters.AddWithValue("@Counter", 1);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }

                    }
                }
            }
            return isSuccess;
        }

        public SathiCreatedOrderResult CreateOrderBySathi(SathiOrderCreateModel obj)
        {
            SathiCreatedOrderResult objCreateOrderData = new SathiCreatedOrderResult();
            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    objCreateOrderData = _rsbal.CreateOrderBySathi(obj);

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.userid);
                }
            }
            return objCreateOrderData;
        }

        public bool UpdateOrderStatusBySathi(string apiKey, int OrderId)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_UpdateOrderStatusbySathi", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@OrderId", OrderId);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i > 0)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }

                    }
                }
            }
            return isSuccess;
        }

        public bool InsertRetailerData(string apiKey, string ShopName, string MobileNo, string ProductRequired, int Quantity, int StateId, int DistrictId)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("Usp_InsertRetailer_Data", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ShopName", ShopName);
                            cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                            cmd.Parameters.AddWithValue("@ProductRequired", ProductRequired);
                            cmd.Parameters.AddWithValue("@Quantity", Quantity);
                            cmd.Parameters.AddWithValue("@StateId", StateId);
                            cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }

                    }
                }
            }
            return isSuccess;
        }

        public List<Referal> BZFarmerAPPLogin(string apiKey, string MobNo, string ShareCode, int FarmerId, string DeviceId, string Name)
        {
            var userList = new List<Referal>();
            // obj.DiscountInfoValue = new List<Referal>();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    if (!string.IsNullOrEmpty(MobNo) || FarmerId != 0)
                    {
                        userList = _productbal.BZFarmerAPPLogin(MobNo, ShareCode, FarmerId, DeviceId, Name);
                    }
                    else
                    {
                        userList = null;
                    }

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return userList;
        }

        public List<Referal> BZFarmerAPP_WebLogin(Referal objAppWebLogin)
        {
            var userList = new List<Referal>();
            if (objAppWebLogin.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    if (!string.IsNullOrEmpty(objAppWebLogin.MobileNumber) || objAppWebLogin.FarmerID != 0||!string.IsNullOrEmpty(objAppWebLogin.Email))
                    {
                        userList = _productbal.BZFarmerAPP_WebLogin(objAppWebLogin);
                    }
                    else
                    {
                        userList = null;
                    }

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return userList;
        }

        public bool SaveWebsiteFarmers(string apiKey, int StateId, string PinCode, string MobileNumber)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_SaveBzWebsiteFarmers", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StateId", StateId);
                            cmd.Parameters.AddWithValue("@PinCode", PinCode);
                            cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }

                    }
                }
            }
            return isSuccess;
        }

        public bool SaveKitchenGardenUsers(string apiKey, int StateId, int DistrictID, string MobileNumber)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_KitchenGardenUsers", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StateId", StateId);
                            cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
                            cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }
                    }
                }
            }
            return isSuccess;
        }


        //public ResponseStatus UpdateFarmerData(UpdateFarmerDetails obj)
        //{
        //    ResponseStatus objStatus = new ResponseStatus();
        //    bool status = false;
        //    if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
        //    {
        //        try
        //        {
        //            status = _productbal.UpdateFarmerAddress(obj.apiKey, obj.FarmerId, obj.RefSource, obj.Fname, obj.Lname, obj.FatherName, obj.Mobile, obj.StateId, obj.DistrictId, obj.BlockId, obj.VillageId, obj.NearByVillage, obj.Address);
        //            if (status == true)
        //            {
        //                objStatus.Success = status;
        //                objStatus.Msg = "आपका विवरण सफलतापूर्वक अपडेट कर दिया गया है।";

        //            }
        //            else
        //            {
        //                objStatus.Success = status;
        //                objStatus.Msg = "आपका विवरण अपडेट नहीं किया गया है। कृपया पुन: प्रयास करें।";

        //            }
        //            return objStatus;
        //        }
        //        catch (Exception ex)
        //        {
        //            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
        //        }
        //    }
        //    objStatus.Msg = "Failed! Please try again.";
        //    return objStatus;
        //}


        public ResponseStatus UpdateFarmerDataN(UpdateFarmerDetails obj)
        {
            ResponseStatus objStatus = new ResponseStatus();
            bool status = false;
            if (obj.apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.UpdateFarmerAddressN(obj.apiKey, obj.FarmerId, obj.RefSource, obj.Fname, obj.Lname, obj.FatherName, obj.Mobile, obj.StateId, obj.DistrictId, obj.BlockId, obj.VillageId, obj.NearByVillage, obj.Address, obj.Landmark, obj.PinCode, obj.Email);
                    if (status == true)
                    {
                        objStatus.Success = status;
                        objStatus.Msg = "आपका विवरण सफलतापूर्वक अपडेट कर दिया गया है।";

                    }
                    else
                    {
                        objStatus.Success = status;
                        objStatus.Msg = "आपका विवरण अपडेट नहीं किया गया है। कृपया पुन: प्रयास करें।";

                    }
                    return objStatus;
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            objStatus.Msg = "Failed! Please try again.";
            return objStatus;
        }

        public ResponseStatus SaveVegetablePrice(string apiKey, string Name, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, int VegetableId, string VegeVariety, string VegeAmount, string VegPrice)
        {
            //LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "msg", 0);

            ResponseStatus objStatus = new ResponseStatus();
            bool status = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _productbal.SaveVegetablePrice(apiKey, Name, Mobile, StateId, DistrictId, BlockId, VillageId, VegetableId, VegeVariety, VegeAmount, VegPrice);
                    if (status == true)
                    {
                        objStatus.Success = status;
                        objStatus.Msg = "Your details has been successfully updated.";
                    }
                    else
                    {
                        objStatus.Success = status;
                        objStatus.Msg = "Your details has not been update. Please try again.";

                    }
                    return objStatus;
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            objStatus.Msg = "Failed! Please try again.";
            return objStatus;
        }

        public VegetableList GetVegetables(string apikey)
        {
            VegetableList vege = null;
            if (apikey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    vege = _productbal.GetVegetables(apikey);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return vege;
        }


        //Temp method for astro live
        public bool SaveAstroLiveUsers(string apiKey, string MobileNumber)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_SaveAstroLiveUsers", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MobileNumber", MobileNumber);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }
                    }
                }
            }
            return isSuccess;
        }


        public bool UpdateOrderStatusByBzFarmerApp(string apiKey, int OrderId,string PaymentStatus)
        {

            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_UpdateOrderStatusbyBzFarmerApp", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@OrderId", OrderId);
                            cmd.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i > 0)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }

                    }
                }
            }
            return isSuccess;
        }

        public bool SaveSBIYonoLeads(string apiKey, string LeadId,string LeadSource)
        {
            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_SaveSBIYONOLead", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@LeadId", LeadId);
                            cmd.Parameters.AddWithValue("@LeadSource", LeadSource);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }
                    }
                }
            }
            return isSuccess;
        }

        public bool SaveBzWebsiteCustomersDetail(string apiKey, string LeadId, string LeadSource,string FName,string LName,string MobNo,string StateName,
            string DistrictName,string BlockName,string VillageName,string PinCode,string Lang,string Aff)
        {
            bool isSuccess = false;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dt = new DataTable();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand("usp_SaveBzWebsiteCustomersDetail", conn))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@LeadId", LeadId);
                            cmd.Parameters.AddWithValue("@LeadSource", LeadSource);
                            cmd.Parameters.AddWithValue("@FName", FName);
                            cmd.Parameters.AddWithValue("@LName", LName);
                            cmd.Parameters.AddWithValue("@MobNo", MobNo);
                            cmd.Parameters.AddWithValue("@StateName", StateName);
                            cmd.Parameters.AddWithValue("@DistrictName", DistrictName);
                            cmd.Parameters.AddWithValue("@BlockName", BlockName);
                            cmd.Parameters.AddWithValue("@VillageName", VillageName);
                            cmd.Parameters.AddWithValue("@PinCode", PinCode);
                            cmd.Parameters.AddWithValue("@Lang", Lang);
                            cmd.Parameters.AddWithValue("@Aff", Aff);
                            cmd.Connection = conn;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            if (i == 1)
                            {
                                isSuccess = true;
                            }
                            else { isSuccess = false; }
                        }
                        catch (Exception ex)
                        {
                            LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                        }
                    }
                }
            }
            return isSuccess;
        }

        public bool SaveTractorCampaignLead(string FarmerName, int StateId, int DistrictId, string MobileNo, string FarmerChoice, string ModelNo, string RunningHours)
        {
            bool isSuccess = false;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_SaveUpdateTractorUsersDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                    cmd.Parameters.AddWithValue("@StateId", StateId);
                    cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@FarmerChoice", FarmerChoice);
                    cmd.Parameters.AddWithValue("@ModelNo", ModelNo);
                    cmd.Parameters.AddWithValue("@RunningHours", RunningHours);
                    cmd.Parameters.AddWithValue("@RequestSource", "Campaign");

                        
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }

        public bool SaveOldTractorCampaignLead(string FarmerName, int StateId, int DistrictId, string MobileNo,  string BrandModelNo,string PurchaseYear,string HP, string RunningHours,string SellingPrice)
        {
            bool isSuccess = false;
            using (SqlConnection conn = new SqlConnection())
            {
                DataTable dt = new DataTable();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("usp_SaveUpdateOldTractorUsersDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FarmerName", FarmerName);
                    cmd.Parameters.AddWithValue("@StateId", StateId);
                    cmd.Parameters.AddWithValue("@DistrictId", DistrictId);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                    cmd.Parameters.AddWithValue("@BrandModelNo", BrandModelNo);
                    cmd.Parameters.AddWithValue("@PurchaseYear", PurchaseYear);
                    cmd.Parameters.AddWithValue("@HP", HP);
                    cmd.Parameters.AddWithValue("@RunningHours", RunningHours);
                    cmd.Parameters.AddWithValue("@SellingPrice", SellingPrice);

                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    int i = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    if (i == 1)
                    {
                        isSuccess = true;
                    }
                    else { isSuccess = false; }
                }
            }
            return isSuccess;
        }
    }
}