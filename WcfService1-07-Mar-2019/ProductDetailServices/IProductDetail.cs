using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entity;

namespace ProductDetailServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    [ServiceContract]
    public interface IProductDetail
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetProductDetail?apiKey={apiKey}&ProductId={ProductId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzProductDtl GetProductDetail(string apiKey, int ProductId);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVegetableDetail?apiKey={apiKey}&ProductId={ProductId}",
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzVegetableDetail GetVegetableDetail(string apiKey, int ProductId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFlowersDetail?apiKey={apiKey}&BzProductId={BzProductId}",
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzFlowersDetail GetFlowersDetail(string apiKey, int BzProductId);

        // service by asim for coupon//
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ReferenceByfarmer?apiKey={apiKey}&ReferTo={ReferTo}&ReferBy={ReferBy}",
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]             // at installation time
        string ReferenceByfarmer(string apiKey, string ReferTo, string ReferBy);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/FarmerAfterpayment?apiKey={apiKey}&ReferTo={ReferTo}&ReferBy={ReferBy}&Amount={Amount}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)] // at ReferedFarmerAfterpayment for coupon generation
        Referal FarmerAfterpayment(string apiKey, string ReferTo, string ReferBy, int Amount);
        // CouponDetails
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/CouponDetails?apiKey={apiKey}&FarmerId={FarmerId}&BzProductId={BzProductId}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)] // at Coupon Details  after coupon generation
        PaymentOptionList CouponDetails(string apiKey, string FarmerId,int BzProductId);

        // Farmer Login
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/FarmerLogin?apiKey={apiKey}&PhoneNo={PhoneNo}&referedBy={referedBy}&FarmerId={FarmerId}&DeviceId={DeviceId}",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        // void FOLogin(string apiKey, string userName, string password, string lat, string longitude, string deviceid, string fcmid);
        List<Referal> FarmerLogin(string apiKey, string PhoneNo, string referedBy, int FarmerId, string DeviceId);


        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UpdateFarmerAddress?apiKey={apiKey}&FarmerId={FarmerId}&RefSource={RefSource}&Fname={Fname}&Lname={Lname}&FatherName={FatherName}&Mobile={Mobile}&StateId={StateId}&DistrictId={DistrictId}&BlockId={BlockId}&VillageId={VillageId}&NearByVillage={NearByVillage}&Address={Address}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateFarmerAddress(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, string NearByVillage, string Address);



        //** CREATE ORDER ** //
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/OrderCreate",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void OrderCreate(OrderCreateModel obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/ProductAvailableByDistrict?apiKey={apiKey}&DistrictId={DistrictId}&PackageId={PackageId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool ProductAvailableByDistrict(string apiKey, int DistrictId, int PackageId);
        // By Lalit// 
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBZappShareLinkUrl?source={source}&campaign={campaign}&ShareCode={ShareCode}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzShareLink GetBZappShareLinkUrl(string source, string campaign, string ShareCode);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBZProduct?apiKey={apiKey}&categoryId={categoryId}&DistrictId={DistrictId}&DistrictName={DistrictName}&PinCode={PinCode}&PageIndex={PageIndex}&PageSize={PageSize}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BZProduct GetBZProduct(string apiKey, int categoryId, int DistrictId, string DistrictName, string PinCode, int PageIndex, int PageSize);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBZProductInterestCount?apiKey={apiKey}&PackID={PackID}&MobNo={MobNo}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BZProductInterestCount GetBZProductInterestCount(string apiKey, int PackID, string MobNo);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/FarmerAppInstallationInfo?apiKey={apiKey}&DeviceId={DeviceId}&ReferencedBy={ReferencedBy}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]             // at installation time
        string FarmerAppInstallationInfo(string apiKey, string DeviceId, string ReferencedBy);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UpdateFCMId?apiKey={apiKey}&DeviceId={DeviceId}&FCMId={FCMId}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateFCMId(string apiKey, string DeviceId, string FCMId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFCMId?apiKey={apiKey}&DistrictId={DistrictId}&PackageId={PackageId}&SDate={SDate}&EDate={EDate}&PageIndex={PageIndex}&PageSize={PageSize}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetFCMId(string apiKey, string DistrictId, string PackageId, string SDate, string EDate, int PageIndex, int PageSize);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetStateDistrictBlockVilage?apiKey={apiKey}&Id={Id}&Type={Type}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        StateDistrictBlockVillageList GetStateDistrictBlockVilage(string apiKey, int Id, string Type);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllStateDistrictBlockVilage?apiKey={apiKey}&Id={Id}&Type={Type}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        StateDistrictBlockVillageList GetAllStateDistrictBlockVilage(string apiKey, int Id, string Type);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/CreateFarmerLead?apiKey={apiKey}&FName={FName}&LName={LName}&MobNo={MobNo}&StateId={StateId}&StateName={StateName}&DistrictId={DistrictId}&DistrictName={DistrictName}&BlockId={BlockId}&BlockName ={BlockName}&VillageId={VillageId}&VillageName={VillageName}&NearbyVillage={NearbyVillage}&AdditionalAddress={AdditionalAddress}",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int CreateFarmerLead(string apiKey, string FName, string LName, string MobNo, int StateId, string StateName, int DistrictId, string DistrictName, int BlockId,
                string BlockName, int VillageId, string VillageName, string NearbyVillage, string AdditionalAddress);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBzLiveStockDetails?apiKey={apiKey}&ProductId={ProductId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzProductDtl GetBzLiveStockDetails(string apiKey, int ProductId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBzMachineryDetails?apiKey={apiKey}&ProductId={ProductId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzProductDtl GetBzMachineryDetails(string apiKey, int ProductId);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetURL", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        string GetURL(LoanDetail url);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/InsertUpdateLoan?apiKey={apiKey}&FarmerName={FarmerName}&MobileNumber={MobileNumber}&StateId={StateId}&DistrictId={DistrictId}&VillageId={VillageId}&LandMark={LandMark}&PanCardNo={PanCardNo}&UIDNo={UIDNo}&VehicleModelNo={VehicleModelNo}&VehiclePurchaseDate={VehiclePurchaseDate}&LoanType={LoanType}&RequiredLoanAmount={RequiredLoanAmount}&IsLastLoan={IsLastLoan}",
           ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool InsertUpdateLoan(string apiKey, string FarmerName, string MobileNumber, int StateId, int DistrictId, int VillageId,
            string LandMark, string PanCardNo, string UIDNo, string VehicleModelNo, DateTime VehiclePurchaseDate, string LoanType,
            decimal RequiredLoanAmount, bool IsLastLoan);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/InsertQuestionAns?Question={Question}&AnsText={AnsText}&AnsValue={AnsValue}&FarmerName={FarmerName}&MobileNo={MobileNo}",
          ResponseFormat = WebMessageFormat.Json)]
        bool InsertQuestionAns(string Question, string AnsText, string AnsValue, string FarmerName, string MobileNo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InsertQuestionAnsTest",
          RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool InsertQuestionAnsTest(QuestionAns obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/InsertUsersQuestionsAns?QuetionId={QuetionId}&AnsValue={AnsValue}&AnsText={AnsText}&MobileNo={MobileNo}&FarmerName={FarmerName}",
            ResponseFormat = WebMessageFormat.Json)]
        bool InsertUsersQuestionsAns(int QuetionId, int AnsValue, string AnsText, string MobileNo, string FarmerName);
        ///// Contest question & Options by asim//
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetContestData?apiKey={apiKey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Contest> GetContestData(string apiKey);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBZLeftMenu?apiKey={apiKey}",
                                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BzLeftMenuList GetBZLeftMenu(string apiKey);

        #region Google Navigation 
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetTripCreateData?apiKey={apiKey}&type={type}&districtid={districtid}&blockId={blockId}&avgVehicleSpeed={avgVehicleSpeed}&workingHours={workingHours}&breakTimePerDeliveryInMinute={breakTimePerDeliveryInMinute}&lunchTime={lunchTime}&BaggSize={BaggSize}",
   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        TripCreateModel GetTripCreateData(string apiKey, string type, int districtid, string blockId, decimal avgVehicleSpeed, decimal workingHours, decimal breakTimePerDeliveryInMinute, decimal lunchTime, int BaggSize);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetGeoCatSubCategoies?apiKey={apiKey}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GeoTagCategoryViewModel GetGeoCatSubCategoies(string apiKey);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GeoTaggingTagInsert?apiKey={apiKey}&userid={userid}&subCategoryId={subCategoryId}&geoName={geoName}&lat={lat}&longitute={longitute}&villageId={villageId}&otherVillage={otherVillage}&blockId={blockId}",
             RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GeoTaggingTagInsert(string apiKey, int userid, int subCategoryId, string geoName, string lat, string longitute, int villageId, string otherVillage, int blockId);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetGeoTaggingData?apiKey={apiKey}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GeoTaggingDataViewModel GetGeoTaggingData(string apiKey);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetGeoTaggingDataWithVillage?apiKey={apiKey}&userId={userId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GeoTaggingDataWithVillageViewModel GetGeoTaggingDataWithVillage(string apiKey, int userId);
        #endregion
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/InsertUsersDetailofContest?MobileNo={MobileNo}&FarmerName={FarmerName}&StateId={StateId}&DistrictId={DistrictId}&CropDetails={CropDetails}&CropSpecies={CropSpecies}&CropArea={CropArea}",
        ResponseFormat = WebMessageFormat.Json)]
        bool InsertUsersDetailofContest(string MobileNo, string FarmerName, int StateId, int DistrictId, string CropDetails, string CropSpecies, string CropArea);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetBZOfferBanner?apiKey={apiKey}",
                                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BZOfferBannerList GetBZOfferBanner(string apikey);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetSathiOrderDetails?apiKey={apiKey}&UserId={UserId}&DateMode={DateMode}&SDATE={SDATE}&EDATE={EDATE}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SathiOrderDetails Get_SathiOrderDetails(string apikey, int UserId, int DateMode, string SDATE, string EDATE);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetSathiPaymentDetails?apiKey={apiKey}&UserId={UserId}&DateMode={DateMode}&SDATE={SDATE}&EDATE={EDATE}",
           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SathiPaymentDetails GetSathiPaymentDetails(string apikey, int UserId, int DateMode, string SDATE, string EDATE);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/InsertFPOData?apiKey={apiKey}&FPOName={FPOName}&Designation={Designation}&ProductRequired={ProductRequired}&Quantity={Quantity}&StateId={StateId}&DistrictId={DistrictId}&BlockId={BlockId}&MobileNo={MobileNo}&CreatedBy={CreatedBy}",
                        RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool InsertFPOData(string apiKey, string FPOName, string Designation, string ProductRequired, int Quantity, int StateId, int DistrictId, int BlockId,
                            string MobileNo, int CreatedBy);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateOrderBySathi",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        SathiCreatedOrderResult CreateOrderBySathi(SathiOrderCreateModel obj);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UpdateOrderStatusBySathi?apiKey={apiKey}&OrderId={OrderId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateOrderStatusBySathi(string apiKey, int OrderId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/InsertRetailerData?apiKey={apiKey}&ShopName={ShopName}&MobileNo={MobileNo}&ProductRequired={ProductRequired}&Quantity={Quantity}&StateId={StateId}&DistrictId={DistrictId}",
                        RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool InsertRetailerData(string apiKey, string ShopName, string MobileNo, string ProductRequired, int Quantity, int StateId, int DistrictId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/BZFarmerAPPLogin?apiKey={apiKey}&PhoneNo={PhoneNo}&referedBy={referedBy}&FarmerId={FarmerId}&DeviceId={DeviceId}&Name={Name}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        // void FOLogin(string apiKey, string userName, string password, string lat, string longitude, string deviceid, string fcmid);
        List<Referal> BZFarmerAPPLogin(string apiKey, string PhoneNo, string referedBy, int FarmerId, string DeviceId, string Name);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/BZFarmerAPP_WebLogin",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        // void FOLogin(string apiKey, string userName, string password, string lat, string longitude, string deviceid, string fcmid);
        List<Referal> BZFarmerAPP_WebLogin(Referal objAppWebLogin);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveWebsiteFarmers?apiKey={apiKey}&StateId={StateId}&PinCode={PinCode}&MobileNumber={MobileNumber}",
                      RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool SaveWebsiteFarmers(string apiKey, int StateId, string PinCode, string MobileNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveKitchenGardenUsers?apiKey={apiKey}&StateId={StateId}&DistrictId={DistrictId}&MobileNumber={MobileNumber}",
                     RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool SaveKitchenGardenUsers(string apiKey, int StateId, int DistrictId, string MobileNumber);

        
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateFarmerData",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ResponseStatus UpdateFarmerDataN(UpdateFarmerDetails obj);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveVegetablePrice?apiKey={apiKey}&Name={Name}&Mobile={Mobile}&StateId={StateId}&DistrictId={DistrictId}&BlockId={BlockId}&VillageId={VillageId}&VegetableId={VegetableId}&VegeVariety={VegeVariety}&VegeAmount={VegeAmount}&VegPrice={VegPrice}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ResponseStatus SaveVegetablePrice(string apiKey, string Name, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, int VegetableId, string VegeVariety, string VegeAmount, string VegPrice);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVegetables?apiKey={apiKey}",
                           RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        VegetableList GetVegetables(string apiKey);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveAstroLiveUsers?apiKey={apiKey}&MobileNumber={MobileNumber}",
                            RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool SaveAstroLiveUsers(string apiKey, string MobileNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/UpdateOrderStatusByBzFarmerApp?apiKey={apiKey}&OrderId={OrderId}&PaymentStatus={PaymentStatus}",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateOrderStatusByBzFarmerApp(string apiKey, int OrderId,string PaymentStatus);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveSBIYonoLeads?apiKey={apiKey}&LeadId={LeadId}&LeadSource={LeadSource}",
                           RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool SaveSBIYonoLeads(string apiKey, string LeadId, string LeadSource);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveBzWebsiteCustomersDetail?apiKey={apiKey}&LeadId={LeadId}&LeadSource={LeadSource}&FName={FName}&LName={LName}&MobNo={MobNo}&StateName={StateName}&DistrictName={DistrictName}&BlockName={BlockName}&VillageName={VillageName}&PinCode={PinCode}&Lang={Lang}&Aff={Aff}",
                           RequestFormat = WebMessageFormat.Json, ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        bool SaveBzWebsiteCustomersDetail(string apiKey, string LeadId, string LeadSource, string FName, string LName, string MobNo, string StateName,
            string DistrictName, string BlockName, string VillageName, string PinCode, string Lang, string Aff);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveTractorCampaignLead?FarmerName={FarmerName}&StateId={StateId}&DistrictId={DistrictId}&MobileNo={MobileNo}&FarmerChoice={FarmerChoice}&ModelNo={ModelNo}&RunningHours={RunningHours}",
       ResponseFormat = WebMessageFormat.Json)]
        bool SaveTractorCampaignLead(string FarmerName, int StateId, int DistrictId, string MobileNo, string FarmerChoice, string ModelNo, string RunningHours);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/SaveOldTractorCampaignLead?FarmerName={FarmerName}&StateId={StateId}&DistrictId={DistrictId}&MobileNo={MobileNo}&BrandModelNo={BrandModelNo}&PurchaseYear={PurchaseYear}&HP={HP}&RunningHours={RunningHours}&SellingPrice={SellingPrice}",
       ResponseFormat = WebMessageFormat.Json)]
        bool SaveOldTractorCampaignLead(string FarmerName, int StateId, int DistrictId, string MobileNo, string BrandModelNo, string PurchaseYear, string HP, string RunningHours, string SellingPrice);
    }
    
}
