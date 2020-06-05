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
using System.Net;
using System.Web;
using System.IO;
using Helper;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Reflection;

namespace ProductServices
{
    public class ProductService : IProductService
    {
        ProductBal _pbal = new ProductBal();
        public Prod_ProductViewModel GetProduct_Master(string apiKey)
        {
            Prod_ProductViewModel model = new Prod_ProductViewModel();

            if (apiKey == ConfigurationManager.AppSettings["productkey"])
            {
                try
                {
                    model = _pbal.GetProduct_Master();
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return model;
        }

        public List<GenericModel> Get_Master_Data(string apiKey, int id, string type)
        {
            List<GenericModel> model = new List<GenericModel>();

            if (apiKey == ConfigurationManager.AppSettings["productkey"])
            {
                try
                {
                    model = _pbal.Get_Master_Data(id, type);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return model;

        }
        public Prod_Product_Detail_Model Get_Product_Detail(string apiKey, int productId)
        {
            Prod_Product_Detail_Model model = new Prod_Product_Detail_Model();

            if (apiKey == ConfigurationManager.AppSettings["productkey"])
            {
                try
                {
                    model = _pbal.Get_Product_Detail(productId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return model;
        }

    }
}
