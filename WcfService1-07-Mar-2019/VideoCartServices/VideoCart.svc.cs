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
using System.Reflection;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;

namespace VideoCartServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class VideoCart : IVideoCart
    {
        VideoCartBal _rsbal = new VideoCartBal();

        public UserDetails CheckUserExist(string apiKey,string MobileNo)
        {
            UserDetails ud = null;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    ud = _rsbal.GetUserDetails(apiKey,MobileNo);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return ud;

        }

        public int FarmerDataCollect(string apiKey, int userid, string RefSource, string Fname, string Lname, string fathername, string mobile, int stateid, int districtid, int blockid,
            int villageid, string NearByVillage, string Address)
        {

          
            int status = 0;
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    status = _rsbal.FarmerDataCollect(userid, RefSource, Fname, Lname, fathername, mobile, stateid, districtid, blockid, villageid, NearByVillage,Address);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
                }
            }
            return status;
        }

        public GenericViewModel GetDistrictBlockVilage(string apiKey, int id, char type)
        {
            GenericViewModel list = new GenericViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    GenericViewModel a = _rsbal.GetDistrictBlockVilage(id, type);

                    list = a;

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, id);
                }
            }
            return list;

        }

        public GenericViewModel GetCategorySubCategory(string apiKey, int id, char type)
        {
            GenericViewModel list = new GenericViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    GenericViewModel a = _rsbal.GetCategorySubCategory(id, type);

                    list = a;

                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, id);
                }
            }
            return list;
        }

        public CategogyProductViewModel GetCategoryProductDetail(string apiKey, int Stateid, int Districtid, int CatId, int SubCatId)
        {
            CategogyProductViewModel _catProList = new CategogyProductViewModel();
            if (apiKey == ConfigurationManager.AppSettings["reasonkey"])
            {
                try
                {
                    _catProList = _rsbal.GetCategoryProductDetail(Stateid, Districtid,CatId,SubCatId);
                }
                catch (Exception ex)
                {
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                }
            }
            return _catProList;
        }

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
                    LogBal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, obj.userid);
                }
            }
            string json = JsonConvert.SerializeObject(returndata);
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(json);
        }


    }
}
