using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft;
using DataLayer;

namespace BusinessLayer
{
    
    public class Notification
    {
        public static AndroidFCMPushNotificationStatus SendNotification(string deviceId, string msg, int userId, string title,int productId= 0,int packageId=0,int type = 0,int cateid= 0,int subcat=0 ,int techid = 0)
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
                dynamic datasend = "";
                if (type == 12)
                {
                   var  data = new
                    {
                        to = deviceId,
                        //notification = new
                        //{
                        //    body = msg,
                        //    title = title,

                        //},
                        data = new
                        {
                            body = msg,
                            title = title,
                            categoryId = cateid,
                            subCategoryId = subcat,
                            technicalId = techid,
                            type = type

                        }
                    };
                    datasend = data;
                }

                else
                {
                    var data = new
                    {
                        to = deviceId,
                      //  notification = new
                       // {
                         //   body = msg,
                          //  title = title,

                     //   },
                        data = new
                        {
                              body = msg,
                              title = title,
                            productId = productId,
                            packageId = packageId,
                            type = type

                        }
                    };
                    datasend = data;

                }

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(datasend);
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
                                    NotificationDal objnotify = new NotificationDal();
                                    objnotify.SaveNotifyLog(newTbl_NotifyLog);
                                }
                                else if (response.failure == 1)
                                {

                                    var newTbl_NotifyLog = new NotifyLog();
                                    newTbl_NotifyLog.FcmId = deviceId;
                                    newTbl_NotifyLog.AppKeyId = serverApiKey;
                                    newTbl_NotifyLog.Msg = msg;
                                    newTbl_NotifyLog.ErrorMsg = response.results[0].error;
                                    newTbl_NotifyLog.MsgId = "0";
                                    newTbl_NotifyLog.Status = "Failure";
                                    newTbl_NotifyLog.CreatedDate = DateTime.Now.ToString();
                                    newTbl_NotifyLog.CreatedBy = userId;
                                    NotificationDal objnotify = new NotificationDal();
                                    objnotify.SaveNotifyLog(newTbl_NotifyLog);
                                  //  new DataLayer().SaveNotifyLog(newTbl_NotifyLog);
                                }

                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //var newErrorLog = new ErrorLog();
                //newErrorLog.ControllerName = "Notification";
                //newErrorLog.ActionName = "SendNotification";
                //newErrorLog.Msg = ex.Message;
                //newErrorLog.Type = title + " Console";
                //   new DataLayer().SaveErrorLog(newErrorLog);
                //result.Successful = false;
                //result.Response = null;
                //result.Error = ex;
            }

            return result;


        }

        public static void SendNotificationToProduct(string message,int userid,int productid =0,int Packageid = 0 ,int type=0 )
        {
            NotificationDal objnotify = new NotificationDal();
            var userList = objnotify.GetFcmByRoleId(7);
         //   var item = "f98Xq4PRiho:APA91bFhbXJen4QadYxH5iOgD31KWl4d9gNzOdwRkT3-n-fbB9RV7Ue0mFXKPiKD3YYCcXVhUtgYrDsRbZsSRdM-ems58s79aSq_ZahKmL4_z7PNXv62TJXqTqSCMb4P9fyGslwj5nWh";
            //var msg = "Add new product from Dealer";
              foreach (var item in userList)
              {
                 SendNotification(item, message, userid, "Product",productid, Packageid, type);
              }

        }

        public static void SendNotificationToDealer(string remarks, int userid, int productid = 0, int Packageid = 0, int type = 0,int catid=0,int subcatid=0,int techid = 0,string productName = "")
        {
            NotificationDal objnotify = new NotificationDal();
            var userList = objnotify.GetFcmByRoleId(11);
            //   var item = "f98Xq4PRiho:APA91bFhbXJen4QadYxH5iOgD31KWl4d9gNzOdwRkT3-n-fbB9RV7Ue0mFXKPiKD3YYCcXVhUtgYrDsRbZsSRdM-ems58s79aSq_ZahKmL4_z7PNXv62TJXqTqSCMb4P9fyGslwj5nWh";
            //var msg = "Add new product from Dealer";
            foreach (var item in userList)
            {
                SendNotification(item, remarks, userid, productName, productid, Packageid, type,catid,subcatid,techid);
            }

        }

        public static void SendNotificationbyUser(string message,int userid,int useridTo, int productid = 0, int Packageid = 0, int type = 0)
        {
            NotificationDal objnotify = new NotificationDal();
            var userList = objnotify.GetFcmByUserId(useridTo);
            //  var userList = new DataLayer().GetFcmByUserId(227);
           // var item = "f98Xq4PRiho:APA91bFhbXJen4QadYxH5iOgD31KWl4d9gNzOdwRkT3-n-fbB9RV7Ue0mFXKPiKD3YYCcXVhUtgYrDsRbZsSRdM-ems58s79aSq_ZahKmL4_z7PNXv62TJXqTqSCMb4P9fyGslwj5nWh";
           // var msg = "Your Produtc";
              foreach (var item in userList)
              {
               Notification.SendNotification(item, message, userid, "Product", productid, Packageid, type);
              }

        }
    }
}
