using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entity;

namespace ProductServices
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [WebInvoke(Method ="POST",UriTemplate = "/GetProduct_Master?apiKey={apiKey}", RequestFormat =WebMessageFormat.Json,ResponseFormat =WebMessageFormat.Json)]
        Prod_ProductViewModel GetProduct_Master(string apiKey);
       
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Get_Master_Data?apiKey={apiKey}&id={id}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<GenericModel> Get_Master_Data(string apiKey, int id, string type);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Get_Product_Detail?apiKey={apiKey}&productId={productId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Prod_Product_Detail_Model Get_Product_Detail(string apiKey, int productId);
    }
}
