using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;
using Entity;
using Helper;
using System.Configuration;

namespace BusinessLayer
{
    public class TransporterBal
    {
        TransporterDal _transdal;
        public TransporterBal()
        {
            _transdal = new TransporterDal();

        }

        public List<Transporter> GetTransporterList(int userId)
        {
            var transporterList = new List<Transporter>();
            DataSet ds = _transdal.GetTransporterList(userId);
            transporterList.Add(new Transporter() {
                Name="Self",
                UserId="0",
                Mobile="",
                Address=""
            });


            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var _transporter = new Transporter();
                        _transporter.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        _transporter.UserId = ds.Tables[0].Rows[i]["UserID"].ToString();
                        _transporter.Mobile = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        _transporter.Address = ds.Tables[0].Rows[i]["CenterAddress"].ToString();

                        transporterList.Add(_transporter);
                    }
                }
            }

            transporterList.Add(new Transporter()
            {
                Name = "Other",
                UserId = "-1",
                Mobile = "",
                Address = ""
            });
            return transporterList;
        }

        public Transporter GetTransporterDetailByUserName(string  userName)
        {
            var _transporter = new Transporter();
            DataSet ds = _transdal.GetTransporterDetailByUserName(userName);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _transporter = new Transporter();
                        _transporter.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        _transporter.UserId = ds.Tables[0].Rows[i]["UserID"].ToString();
                        _transporter.Mobile = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        _transporter.Address = ds.Tables[0].Rows[i]["CenterAddress"].ToString();

                       
                    }
                }
            }

            return _transporter;
        }

        public ResponseObj SaveTempTransTrip(TransporterVehicleTrip obj)
        {
            var res=_transdal.SaveTempTransTrip(obj);
            if (Convert.ToInt32(res.Value) > 0)
            {
                var TripRequestMsg = Convert.ToString(ConfigurationSettings.AppSettings["TripRequestMsg"]);
                // var LogisticIds = Convert.ToString(ConfigurationSettings.AppSettings["LogisticIds"]);
                // var idsList = CommonBal.GetListValue(LogisticIds);

                var idsList = new NotificationBal().GetFcmByRoleId(15);
                foreach (var item in idsList)
                {
                    //var fcmList = new NotificationBal().GetFcmByUserId(Convert.ToInt32(item));
                    CommonBal.SendNotification(item, TripRequestMsg, 15, "Trip");
                }
                /**********Mail Report of Trip*********/
                
                #region  Trip Sheet Mail send

                if (ConfigurationManager.AppSettings["TripCreateMailSend"] == "1")
                {
                    if (Convert.ToInt32(res.Value) > 0)
                    {
                        ReasonStatusDal _rsdal = new ReasonStatusDal();
                        var DS = _rsdal.Get_FoTripOrders(Convert.ToInt32(res.Value), obj.Mobile, "TT");
                        if (DS != null)
                        {
                            if (DS.Tables[0].Rows.Count > 0 && DS.Tables[1].Rows.Count > 0)
                            {
                                var sendToList = DS.Tables[1].Rows[0]["ToList"].ToString();
                                var sendCCList = DS.Tables[1].Rows[0]["CCList"].ToString();
                                var mailSubject = DS.Tables[1].Rows[0]["Subject"].ToString();
                                var mailBody = string.Format(DS.Tables[1].Rows[0]["Body"].ToString(), DS.Tables[1].Rows[0]["OrderDate"].ToString());
                                var fileName = DS.Tables[1].Rows[0]["FOName"].ToString() + "(" + obj.Mobile + ") " + DS.Tables[1].Rows[0]["OrderDate"].ToString();

                                ReportInXMail.SendMailForFoTripOrders(DS.Tables[0], sendToList, sendCCList, mailSubject, mailBody, fileName,Convert.ToInt32(obj.CreatedBy));
                            }
                        }

                    }
                }
                #endregion
                
            }
            return res;
        }

        public ResponseObj SaveTempTransTrip(TransporterVehicleTripWithDate obj)
        {
            var res = _transdal.SaveTempTransTrip(obj);
            if (Convert.ToInt32(res.Value) > 0)
            {
                var TripRequestMsg = Convert.ToString(ConfigurationSettings.AppSettings["TripRequestMsg"]);
                // var LogisticIds = Convert.ToString(ConfigurationSettings.AppSettings["LogisticIds"]);
                // var idsList = CommonBal.GetListValue(LogisticIds);

                var idsList = new NotificationBal().GetFcmByRoleId(15);
                foreach (var item in idsList)
                {
                    //var fcmList = new NotificationBal().GetFcmByUserId(Convert.ToInt32(item));
                    CommonBal.SendNotification(item, TripRequestMsg, 15, "Trip");
                }
                /**********Mail Report of Trip*********/

                #region  Trip Sheet Mail send

                if (ConfigurationManager.AppSettings["TripCreateMailSend"] == "1")
                {
                    if (Convert.ToInt32(res.Value) > 0)
                    {
                        ReasonStatusDal _rsdal = new ReasonStatusDal();
                        var DS = _rsdal.Get_FoTripOrders(Convert.ToInt32(res.Value), obj.Mobile, "TT");
                        if (DS != null)
                        {
                            if (DS.Tables[0].Rows.Count > 0 && DS.Tables[1].Rows.Count > 0)
                            {
                                var sendToList = DS.Tables[1].Rows[0]["ToList"].ToString();
                                var sendCCList = DS.Tables[1].Rows[0]["CCList"].ToString();
                                var mailSubject = DS.Tables[1].Rows[0]["Subject"].ToString();
                                var mailBody = string.Format(DS.Tables[1].Rows[0]["Body"].ToString(), DS.Tables[1].Rows[0]["OrderDate"].ToString());
                                var fileName = DS.Tables[1].Rows[0]["FOName"].ToString() + "(" + obj.Mobile + ") " + DS.Tables[1].Rows[0]["OrderDate"].ToString();

                                ReportInXMail.SendMailForFoTripOrders(DS.Tables[0], sendToList, sendCCList, mailSubject, mailBody, fileName, Convert.ToInt32(obj.CreatedBy));
                            }
                        }

                    }
                }
                #endregion

            }
            return res;
        }

        public List<TripForApprove> GetTripForApprove()
        {
            var tripForApproveList = new List<TripForApprove>();
            DataSet ds = _transdal.GetTripForApprove();
            


            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var _tripForApprove = new TripForApprove();
                        _tripForApprove.Name = ds.Tables[0].Rows[i]["TransporterName"].ToString();
                        _tripForApprove.UserId = ds.Tables[0].Rows[i]["TransporterId"].ToString();
                        _tripForApprove.Mobile = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        _tripForApprove.Address = ds.Tables[0].Rows[i]["CenterAddress"].ToString();
                        _tripForApprove.CreatedByName = ds.Tables[0].Rows[i]["CreatedByName"].ToString();
                        _tripForApprove.CreatedToName = ds.Tables[0].Rows[i]["CreatedToName"].ToString();
                        _tripForApprove.Fixedrateperday = ds.Tables[0].Rows[i]["Fixedrateperday"].ToString();
                        _tripForApprove.FuelCharges = ds.Tables[0].Rows[i]["FuelCharges"].ToString();
                        _tripForApprove.Priceperkm = ds.Tables[0].Rows[i]["Priceperkm"].ToString();
                        _tripForApprove.Mincharges = ds.Tables[0].Rows[i]["Mincharges"].ToString();
                        _tripForApprove.MinchargeUptoKm = ds.Tables[0].Rows[i]["MinchargeUptoKm"].ToString();
                        _tripForApprove.UpPrice = ds.Tables[0].Rows[i]["UpPrice"].ToString();
                        _tripForApprove.DownPrice = ds.Tables[0].Rows[i]["DownPrice"].ToString();
                        _tripForApprove.FixedPrice = ds.Tables[0].Rows[i]["Fixedrateperkm"].ToString();
                        _tripForApprove.RuleId = ds.Tables[0].Rows[i]["RuleId"].ToString();
                        _tripForApprove.RuleName = ds.Tables[0].Rows[i]["RuleName"].ToString();
                        _tripForApprove.VehicleId = ds.Tables[0].Rows[i]["VehicleTypeID"].ToString();
                        _tripForApprove.VehicleName = ds.Tables[0].Rows[i]["VehicleType"].ToString();
                        _tripForApprove.TripStatus = ds.Tables[0].Rows[i]["tripStatus"].ToString();
                        _tripForApprove.TripTempId = ds.Tables[0].Rows[i]["tripId"].ToString();
                        _tripForApprove.IsUpDown = ds.Tables[0].Rows[i]["IsUpDown"].ToString();
                        _tripForApprove.ReasonId = ds.Tables[0].Rows[i]["ReasonId"].ToString();
                        _tripForApprove.ReasonDesc = ds.Tables[0].Rows[i]["Reason_Name"].ToString();
                        _tripForApprove.Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString();
                        _tripForApprove.District = ds.Tables[0].Rows[i]["DistrictName"].ToString();
                        _tripForApprove.DistrictId = ds.Tables[0].Rows[i]["DistrictId"].ToString();
                        tripForApproveList.Add(_tripForApprove);
                    }
                }
            }

            
            return tripForApproveList;
        }

        public ResponseObj UpdateTempTripStatus(string tempTripId, string status, int userId)
        {
            string FoFCM = "", FoName = "", RoFCM = "", TransName = "";
            var result=_transdal.UpdateTempTripStatus(tempTripId, status, userId,ref FoFCM,ref FoName,ref RoFCM,ref TransName);
            if (Convert.ToInt32(result.Value) > 0)
            {
                var TripMsg = Convert.ToString(ConfigurationSettings.AppSettings["TripMsg"]);
                var TripRejectMsg = Convert.ToString(ConfigurationSettings.AppSettings["TripRejectMsg"]);
                var TripApproveMsg = Convert.ToString(ConfigurationSettings.AppSettings["TripApproveMsg"]);
                if (status == "1")
                {
                    TripApproveMsg = string.Format(TripApproveMsg, TransName, FoName);
                    CommonBal.SendNotification(FoFCM, TripMsg, userId, "Trip");
                    CommonBal.SendNotification(RoFCM, TripApproveMsg, userId, "Trip");
                }
                else
                {
                    TripRejectMsg = string.Format(TripRejectMsg, TransName, FoName);
                    CommonBal.SendNotification(RoFCM, TripRejectMsg, userId, "Trip");
                }
            }
            return result;
        }

    }
}
