using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DataLayer;
using Entity;
using System.Web;

namespace BusinessLayer
{
   public class CommonBal
    {
        public static List<string> GetListValue(string value)
        {
            var listValue = new List<string>();
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains(','))
                {
                    listValue = value.Split(',').ToList();
                }
                else
                {
                    listValue.Add(value);
                }
            }
            return listValue;
        }


        public static AndroidFCMPushNotificationStatus SendNotification(string deviceId, string msg, int userId, string title)
        {
            AndroidFCMPushNotificationStatus result = new AndroidFCMPushNotificationStatus();

            try
            {
                result.Successful = false;
                result.Error = null;
                var serverApiKey = Convert.ToString(ConfigurationSettings.AppSettings["FcmServerApiKeyFO"]);

                var senderId = Convert.ToString(ConfigurationSettings.AppSettings["FcmsenderIdFO"]);
                //deviceId = "fiw592fD138:APA91bHqE4yzlcp3OVX3hHYBz3NE3oJBpz3b6JlFOXu2-wZPTajYLNyRD0BHPsvCG09pZv54zbNMVLW8rdJLArlg0aEHRDmB9z59ATQORy3FpVsnDTSgDh6XnMEjS7iDx_owcbPpSujs";
                // var deviceId = "ae03efc8462cc55a";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //  tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverApiKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));

                // string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

                //Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentType = "application/json";
                //     tRequest.UseDefaultCredentials = true;
                //    tRequest.PreAuthenticate = true;
                //   tRequest.Credentials = CredentialCache.DefaultCredentials;

                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = msg,
                        title = title

                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);


                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                                if (response.success == 1)
                                {
                                    var newTbl_NotifyLog = new NotifyLog();
                                    newTbl_NotifyLog.FcmId = deviceId;
                                    newTbl_NotifyLog.AppKeyId = serverApiKey;
                                    newTbl_NotifyLog.Msg = msg;
                                    newTbl_NotifyLog.Status = "Success";
                                    newTbl_NotifyLog.MsgId = response.results[0].message_id;
                                    newTbl_NotifyLog.CreatedDate = DateTime.Now.ToString();
                                    newTbl_NotifyLog.CreatedBy = userId;
                                    new ReasonStatusDal().SaveNotifyLog(newTbl_NotifyLog);
                                }
                                else if (response.failure == 1)
                                {

                                    var newTbl_NotifyLog = new NotifyLog();
                                    newTbl_NotifyLog.FcmId = deviceId;
                                    newTbl_NotifyLog.AppKeyId = serverApiKey;
                                    newTbl_NotifyLog.Msg = msg;
                                    newTbl_NotifyLog.ErrorMsg = response.results[0].error;
                                    newTbl_NotifyLog.Status = "Failure";
                                    newTbl_NotifyLog.CreatedDate = DateTime.Now.ToString();
                                    newTbl_NotifyLog.CreatedBy = userId;
                                    new ReasonStatusDal().SaveNotifyLog(newTbl_NotifyLog);
                                }

                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var newErrorLog = new ErrorLog();
                newErrorLog.ControllerName = "Notification";
                newErrorLog.ActionName = "SendNotification";
                newErrorLog.Msg = ex.Message;
                newErrorLog.Type = title + " Console";
                LogBal.ErrorLog("CommonBal", "SendNotification", ex.Message, 0);
                //   new DataLayer().SaveErrorLog(newErrorLog);
                //result.Successful = false;
                //result.Response = null;
                //result.Error = ex;
            }

            return result;

        }

        public static List<int> SplitStringToIntArray(string data)
        {
            List<int> list = new List<int>();
            if (data != "")
            {
               string[] arr= data.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    list.Add(Convert.ToInt32(arr[i]));
                }
            }
            return list;
        }

        // By lalit

        public static string GetRandomAlphaNumericCode()
        {
          string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
           // string alphabets = "A";
         string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
         // string small_alphabets = "a";
         string numbers = "1234567890";
          //  string numbers = "1";

            string characters = numbers;

            characters += alphabets + small_alphabets + numbers;
           // int length = 8;
           int length = 5;
            string strCode = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                }
                while (strCode.IndexOf(character) != -1);
                strCode += character;
            }
            string s = DateTime.Now.Ticks.ToString();
            return strCode+s;
        }

        public static string Shorten(string longUrl, string login, string apikey)
        {
            var url = string.Format("http://api.bit.ly/shorten?format=json&version=2.0.1&longUrl={0}&login={1}&apiKey={2}", HttpUtility.UrlEncode(longUrl), login, apikey);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic jsonResponse = js.Deserialize<dynamic>(reader.ReadToEnd());
                    string s = jsonResponse["results"][longUrl]["shortUrl"];
                    return s;
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

    }


}
