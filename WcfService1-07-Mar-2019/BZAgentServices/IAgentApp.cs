using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entity;
using System.IO;
using System.ServiceModel.Activation;


namespace BZAgentServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAgentApp
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFarmerByFsc?apiKey={apiKey}&FscId={FscId}&Mode={Mode}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Farmer GetFarmerByFsc(string apiKey, string FscId,int Mode=0);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UpdateCallLog?apiKey={apiKey}&MobileNO={MobileNO}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int UpdateCallLog(string apiKey, string MobileNO);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UserLogin?apiKey={apiKey}&UserId={UserId}&Password={Password}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        UserLoginDetails UserLogin(string apiKey, string UserId, string Password);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ChangePassword?apiKey={apiKey}&userid={userid}&password={password}&newpassword={newpassword}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void ChangePassword(string apiKey, int userid, string password, string newpassword);


        [OperationContract]
        [WebInvoke(Method = "GET",
          UriTemplate = "/GetOrderDetails?apiKey={apiKey}&FSCId={FSCId}&RoleId={RoleId}&status={status}&Mode={Mode}&fromdate={fromdate}&todate={todate}",
          RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FSCOrderDetails GetOrderDetails(string apiKey, int FSCId, string Mode, string fromdate, string todate, int RoleId = 0, int status = 0);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCategoryProductDetail?apiKey={apiKey}&Stateid={Stateid}&Districtid={Districtid}&CatId={CatId}&SubCatId={SubCatId}&PackageID={PackageID}",
    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BZAgentProductViewModel GetCategoryProductDetail(string apiKey, int Stateid, int Districtid, int CatId, int SubCatId, int PackageID = 0);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetSaleOrder?apiKey={apiKey}&fromdate={fromdate}&todate={todate}&Mode={Mode}&DistrictId={DistrictId}&UserID={UserID}&stateId={stateId}",
     RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SaleOrderDetail GetSaleOrder(string apiKey, string fromdate, string todate, string Mode, string DistrictId = "0", int UserID = 0, int stateId = 6);

        [OperationContract]
        [WebInvoke(Method = "GET",
         UriTemplate = "/GetOrderDetails_OrderID?apiKey={apiKey}&orderid={orderid}",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GetPODOrderDetailViewModel GetOrderDetails_OrderID(string apiKey, int orderid);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetDistrictBlockVilage?apiKey={apiKey}&id={id}&type={type}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GenericViewModel GetDistrictBlockVilage(string apiKey, int id, char type);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCouponList?apiKey={apiKey}&CatId={CatId}&SubCatId={SubCatId}&CompanyId={CompanyId}&BrandId={BrandId}&PCKGId={PCKGId}&Itemval={Itemval}",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PromoCouponModel GetCouponList(string apiKey, string CatId = "0", string SubCatId = "0", string CompanyId = "0", string BrandId = "0", string PCKGId = "0", string Itemval = "0");

        [OperationContract]
        [WebInvoke(Method = "GET",
      UriTemplate = "/GetSearchProductDetail?apiKey={apiKey}&SearchKey={SearchKey}&sortColumn={sortColumn}&sortColumnDir={sortColumnDir}&pageNo={pageNo}&pageSize={pageSize}&stateId={stateId}&Districtid={Districtid}&cropID={cropID}&categoryID={categoryID}&SubCatId={SubCatId}&CompanyId={CompanyId}&totalRecords={totalRecords}",
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SearchProduct GetSearchProductDetail(string apiKey, string SearchKey = null, string sortColumn = null, string sortColumnDir = "asc", int pageNo = 1, int pageSize = 100, int stateId = 0, int Districtid = 0, int cropID = 0, int categoryID = 0, int SubCatId = 0, int CompanyId = 0, int totalRecords = 0);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCategorySubCategory?apiKey={apiKey}&id={id}&type={type}",
    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GenericViewModel GetCategorySubCategory(string apiKey, int id, string type);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/DemandOrderCreate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void DemandOrderCreate(DemandCreateModel obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetUserStatus?apiKey={apiKey}&UserId={UserId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        UserValidation GetUserStatus(string apiKey, int UserId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ApplyCoupon?apiKey={apiKey}&CatID={CatID}&SubCatID={SubCatID}&CompanyID={CompanyID}&BrandID={BrandID}&ProductID={ProductID}&PkgID={PkgID}&Qty={Qty}&ActualAmt={ActualAmt}&CouponID={CouponID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ApplyPromoCoupon ApplyCoupon(string apiKey, int CatID, int SubCatID, int CompanyID, int BrandID, int ProductID, int PkgID, int Qty, decimal ActualAmt, int CouponID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AgentOrderCreate",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void AgentOrderCreate(AgentOrderCreateModel obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFarmerDetails?apiKey={apiKey}&FarmerKey={FarmerKey}",
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FarmerDetails GetFarmerDetails(string apiKey, string FarmerKey);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/IssueRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void IssueRegister(Issue obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/IssueDetailByFarmer?apiKey={apiKey}&MobileNo={MobileNo}",
   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        IssueDetailByFarmer IssueDetailByFarmer(string apiKey, string MobileNo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ComplaintRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
       void   ComplaintRegister(Complaint obj);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFarmerComplaint?apiKey={apiKey}&OrderID={OrderID}&FarmerId={FarmerId}",
 RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        CompDetailByFarmer GetFarmerComplaint(string apiKey, int OrderID=0, int FarmerId = 0);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RemoveItemFromCart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void RemoveItemFromCart(RemoveItemFromCart obj);

        [OperationContract]
        [WebInvoke(Method = "GET",
         UriTemplate = "/GetOrderDetailsByFarmerID?apiKey={apiKey}&FarmerID={FarmerID}&status={status}&fromdate={fromdate}&todate={todate}",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FSCOrderDetails GetOrderDetailsByFarmerID(string apiKey, int FarmerID, string fromdate, string todate, int status = 0);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/OrderWiseProduct?apiKey={apiKey}&orderid={orderid}",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        OrderWiseProductViewModel OrderWiseProduct(string apiKey, int orderid);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AgentOrderConfirmation",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void AgentOrderConfirmation(AgentOrderUpdateModel obj);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SaveCallLog",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void SaveCallLog(CallLogged obj);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/FarmerData",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void FarmerData(FarmerData obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/BlacklistFarmer?apiKey={apiKey}&MobileNo={MobileNo}&UserID={UserID}&Remark={Remark}",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    void   BlacklistFarmer(string apiKey, string MobileNo, int UserID, string Remark);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFarmerCallHistory?apiKey={apiKey}&MobileNo={MobileNo}",
  RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FarmecallHistory GetFarmerCallHistory(string apiKey, string MobileNo);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AvisCallLog?EventName={EventName}&ANI={ANI}&DNIS={DNIS}&Mode={Mode}&CallId={CallId}&UserLogin={UserLogin}&Campaign={Campaign}&LeadId={LeadId}&Skill={Skill}&dnisIB={dnisIB}&CallFileName={CallFileName}&CallDisposition={CallDisposition}",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void AvisCallLog(string EventName, string ANI, string DNIS, string Mode, string CallId, string UserLogin, string Campaign, string LeadId, string Skill, string dnisIB, string CallFileName,string CallDisposition);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SaveAgreement",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void SaveAgreement(AgreementDetails obj);

        #region Ashish Ozontel Service
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Ozontel_CallLog_Insert",
          RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int Ozontel_CallLog_Insert(Stream data);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Ozontel_CallLog_Select?apiKey={apiKey}&userid={userid}",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Ozontel_CallLog_Model2> Ozontel_CallLog_Select(string Apikey, int userid);

        #endregion


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UploadPhoto", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void UploadPhoto(UploadPhotoModel obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetPaymentRequestParam?apiKey={apiKey}",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PaymentRequestParam GetPaymentRequestParam(string apiKey);

    }
}
