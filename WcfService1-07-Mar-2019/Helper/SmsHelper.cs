using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Configuration;
using System.Net;
using System.Web;
using System.IO;

namespace Helper
{
    public class SmsHelper
    {


        public static string MessageHindiSend(int userid, string mobile, string msg)
        {
            string returndata = "0";
            //Your authentication key
            string authKey = ConfigurationManager.AppSettings["authKey"];
            //Multiple mobiles numbers separated by comma
            string mobileNumber = mobile_no_split(mobile);
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderId = ConfigurationManager.AppSettings["senderId"];
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode(msg);

            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&route={0}", "4");
            sbPostData.AppendFormat("&unicode={0}", "1");//unicode=1 
            sbPostData.AppendFormat("&ignoreNdnc={0}", "1");//ignoreNdnc=1 

            try
            {
                //Call Send SMS API
                string sendSMSUri = ConfigurationManager.AppSettings["sendSMSUri"];
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();
                returndata = responseString;
                //Close the response
                reader.Close();
                response.Close();
                SmsLog(mobileNumber, msg, userid);
            }
            catch (SystemException ex)
            {
                // MessageBox.Show(ex.Message.ToString());
            }
            return returndata;
        }
        public static string MessageEnglishSend(int userid, string mobile, string msg)
        {
            string returndata = "0";
            //Your authentication key
            string authKey = ConfigurationManager.AppSettings["authKey"];
            //Multiple mobiles numbers separated by comma
            string mobileNumber = mobile_no_split(mobile);
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderId = ConfigurationManager.AppSettings["senderId"];
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode(msg);

            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&route={0}", "4");
            sbPostData.AppendFormat("&ignoreNdnc={0}", "1");//ignoreNdnc=1 

            try
            {
                //Call Send SMS API
                string sendSMSUri = ConfigurationManager.AppSettings["sendSMSUri"];
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();
                returndata = responseString;
                //Close the response
                reader.Close();
                response.Close();
                SmsLog(mobileNumber, msg, userid);
            }
            catch (SystemException ex)
            {
                // MessageBox.Show(ex.Message.ToString());
            }
            return returndata;
        }


        private static string mobile_no_split(string mobiles)
        {
            string mobile = "";
            string[] mobileArray = mobiles.Split(',');

            for (int i = 0; i < mobileArray.Length; i++)
            {
                if (i == 0)
                {
                    mobile += "91" + mobileArray[i];
                }
                else
                {
                    mobile += ",91" + mobileArray[i];
                }
            }

            return mobile;
        }
        public static void SmsLog(string mobiles, string message, int userid = 0)
        {
            LogDal.SmsLog(mobiles, message, userid);
        }
    }
}
