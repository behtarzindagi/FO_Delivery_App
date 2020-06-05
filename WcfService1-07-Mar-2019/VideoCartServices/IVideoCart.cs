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

namespace VideoCartServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IVideoCart
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/CheckUserExist?apiKey={apiKey}&MobileNo={MobileNo}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
       UserDetails CheckUserExist(string apiKey, string MobileNo);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/FarmerDataCollect?apiKey={apiKey}&userid={userid}&RefSource={RefSource}&Fname={Fname}&Lname={Lname}&fathername={fathername}&mobile={mobile}&stateid={stateid}&districtid={districtid}&blockid={blockid}&villageid={villageid}&NearByVillage={NearByVillage}&Address={Address}",
      RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int FarmerDataCollect(string apiKey, int userid, string RefSource, string Fname, string Lname, string fathername, string mobile, int stateid, int districtid, int blockid, 
            int villageid, string NearByVillage,string Address);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetDistrictBlockVilage?apiKey={apiKey}&id={id}&type={type}",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GenericViewModel GetDistrictBlockVilage(string apiKey, int id, char type);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCategorySubCategory?apiKey={apiKey}&id={id}&type={type}",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GenericViewModel GetCategorySubCategory(string apiKey, int id, char type);

            [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCategoryProductDetail?apiKey={apiKey}&Stateid={Stateid}&Districtid={Districtid}&CatId={CatId}&SubCatId={SubCatId}",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        CategogyProductViewModel GetCategoryProductDetail(string apiKey, int Stateid,int Districtid,int CatId,int SubCatId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/OrderCreate",
    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void OrderCreate(OrderCreateModel obj);

    }
}
