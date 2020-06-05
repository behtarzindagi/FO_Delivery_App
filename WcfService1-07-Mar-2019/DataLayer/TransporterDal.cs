using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using Entity;

namespace DataLayer
{
    public class TransporterDal : BaseDal
    {
        public DataSet GetTransporterList(int userId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_GetTransporterList";
                command.Parameters.AddWithValue("@userid", userId);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return ds;
        }

        public DataSet GetTransporterDetailByUserName(string userName)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_GetTransporterDetailByUserName";
                command.Parameters.AddWithValue("@username", userName);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return ds;
        }


        public ResponseObj SaveTempTransTrip(TransporterVehicleTrip obj)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            var result = new ResponseObj();
            connection = new SqlConnection(connetionString);
            var fName = obj.Transporter.Name;
            var lName = string.Empty;
            if (obj.Transporter.Name.Contains(" "))
            {
                fName = obj.Transporter.Name.Split(' ')[0];
                lName = obj.Transporter.Name.Split(' ')[1];

            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SAVETempTRIP_DETAIL";//SP_SAVETRIP_DETAIL

                command.Parameters.AddWithValue("@ORDER_IDS", obj.Orderid);

                SqlParameter TripId = new SqlParameter();
                TripId.ParameterName = "@TripID";
                TripId.DbType = DbType.Int32;
                TripId.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripId);

                command.Parameters.AddWithValue("@PickupAddress", "NA");
                command.Parameters.AddWithValue("@VehicleTypeID", obj.VehicleId);
                command.Parameters.AddWithValue("@VehicleName", obj.VehicleName);
                command.Parameters.AddWithValue("@mobile", obj.Mobile);
                command.Parameters.AddWithValue("@vehicleNo", "NA");
                command.Parameters.AddWithValue("@StartKmReading", 0);
                command.Parameters.AddWithValue("@EndKmReading", 0);
                command.Parameters.AddWithValue("@ChargesPerKm", 0);
                command.Parameters.AddWithValue("@OtherCharges", 0);
                command.Parameters.AddWithValue("@LabourCharges", 0);
                command.Parameters.AddWithValue("@Remark", "NA");
                command.Parameters.AddWithValue("@CreateBy", Convert.ToInt32(obj.CreatedBy));
                command.Parameters.AddWithValue("@ModifiedBy", 0);
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@TransporterId", obj.Transporter.UserId);
                command.Parameters.AddWithValue("@TransporterFName", fName);
                command.Parameters.AddWithValue("@TransporterLName", lName);
                command.Parameters.AddWithValue("@TransMobile", obj.Transporter.Mobile);
                command.Parameters.AddWithValue("@TransAddress", obj.Transporter.Address);
                command.Parameters.AddWithValue("@RuleId", obj.RuleId);
                command.Parameters.AddWithValue("@Fixedrateperday", obj.Fixedrateperday);
                command.Parameters.AddWithValue("@FuelCharges", obj.FuelCharges);
                command.Parameters.AddWithValue("@Priceperkm", obj.Priceperkm);
                command.Parameters.AddWithValue("@Mincharges", obj.Mincharges);
                command.Parameters.AddWithValue("@MinchargeUptoKm", obj.MinchargeUptoKm);
                command.Parameters.AddWithValue("@UpPrice", obj.UpPrice);
                command.Parameters.AddWithValue("@DownPrice", obj.DownPrice);
                command.Parameters.AddWithValue("@FixedPrice", obj.FixedPrice);
                command.Parameters.AddWithValue("@IsUpDown", Convert.ToBoolean(obj.IsUpDown));
                command.Parameters.AddWithValue("@ReasonId",obj.ReasonId );
                command.Parameters.AddWithValue("@Remarks",obj.Remarks );
                SqlParameter TripMSG = new SqlParameter();
                TripMSG.ParameterName = "@TripMsg";
                TripMSG.DbType = DbType.String;
                TripMSG.Size = 299;
                TripMSG.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripMSG);
                flag = command.ExecuteNonQuery();
                //if (flag > 0)
                //{
                    result.Status = "Sucess";
                   
                    string GetTripID = command.Parameters["@TripID"].Value.ToString();
                   // flag = int.Parse(GetTripID);
                    result.Value = GetTripID;
                    result.Msg= command.Parameters["@TripMsg"].Value.ToString();
                //}
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.Status = "Failed";
                result.Value = "-1";
               LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse("0"));
            }


            connection.Close();
            return result;
        }

        public ResponseObj SaveTempTransTrip(TransporterVehicleTripWithDate obj)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            var result = new ResponseObj();
            connection = new SqlConnection(connetionString);
            var fName = obj.Transporter.Name;
            var lName = string.Empty;
            if (obj.Transporter.Name.Contains(" "))
            {
                fName = obj.Transporter.Name.Split(' ')[0];
                lName = obj.Transporter.Name.Split(' ')[1];

            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SAVETempTRIP_DETAILWithDate";//SP_SAVETRIP_DETAIL

                command.Parameters.AddWithValue("@ORDER_IDS", obj.Orderid);

                SqlParameter TripId = new SqlParameter();
                TripId.ParameterName = "@TripID";
                TripId.DbType = DbType.Int32;
                TripId.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripId);

                command.Parameters.AddWithValue("@PickupAddress", "NA");
                command.Parameters.AddWithValue("@VehicleTypeID", obj.VehicleId);
                command.Parameters.AddWithValue("@VehicleName", obj.VehicleName);
                command.Parameters.AddWithValue("@mobile", obj.Mobile);
                command.Parameters.AddWithValue("@vehicleNo", "NA");
                command.Parameters.AddWithValue("@StartKmReading", 0);
                command.Parameters.AddWithValue("@EndKmReading", 0);
                command.Parameters.AddWithValue("@ChargesPerKm", 0);
                command.Parameters.AddWithValue("@OtherCharges", 0);
                command.Parameters.AddWithValue("@LabourCharges", 0);
                command.Parameters.AddWithValue("@Remark", "NA");
                command.Parameters.AddWithValue("@CreateBy", Convert.ToInt32(obj.CreatedBy));
                command.Parameters.AddWithValue("@ModifiedBy", 0);
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@TransporterId", obj.Transporter.UserId);
                command.Parameters.AddWithValue("@TransporterFName", fName);
                command.Parameters.AddWithValue("@TransporterLName", lName);
                command.Parameters.AddWithValue("@TransMobile", obj.Transporter.Mobile);
                command.Parameters.AddWithValue("@TransAddress", obj.Transporter.Address);
                command.Parameters.AddWithValue("@RuleId", obj.RuleId);
                command.Parameters.AddWithValue("@Fixedrateperday", obj.Fixedrateperday);
                command.Parameters.AddWithValue("@FuelCharges", obj.FuelCharges);
                command.Parameters.AddWithValue("@Priceperkm", obj.Priceperkm);
                command.Parameters.AddWithValue("@Mincharges", obj.Mincharges);
                command.Parameters.AddWithValue("@MinchargeUptoKm", obj.MinchargeUptoKm);
                command.Parameters.AddWithValue("@UpPrice", obj.UpPrice);
                command.Parameters.AddWithValue("@DownPrice", obj.DownPrice);
                command.Parameters.AddWithValue("@FixedPrice", obj.FixedPrice);
                command.Parameters.AddWithValue("@IsUpDown", Convert.ToBoolean(obj.IsUpDown));
                command.Parameters.AddWithValue("@ReasonId", obj.ReasonId);
                command.Parameters.AddWithValue("@Remarks", obj.Remarks);
                command.Parameters.AddWithValue("@date", obj.Date);
                SqlParameter TripMSG = new SqlParameter();
                TripMSG.ParameterName = "@TripMsg";
                TripMSG.DbType = DbType.String;
                TripMSG.Size = 299;
                TripMSG.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripMSG);
                flag = command.ExecuteNonQuery();
                //if (flag > 0)
                //{
                result.Status = "Sucess";

                string GetTripID = command.Parameters["@TripID"].Value.ToString();
                // flag = int.Parse(GetTripID);
                result.Value = GetTripID;
                result.Msg = command.Parameters["@TripMsg"].Value.ToString();
                //}
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.Status = "Failed";
                result.Value = "-1";
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse("0"));
            }


            connection.Close();
            return result;
        }

        public DataSet GetTripForApprove()
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_Get_TripForApprove";
             
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return ds;
        }

        public ResponseObj UpdateTempTripStatus(string tempTripId, string status,int userId, ref string FoFCM , ref string FoName, ref string RoFCM, ref string TransName)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            string res = "0";
            connection = new SqlConnection(connetionString);
            var result = new ResponseObj();
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_UpdateTempTripStatus";//SP_SAVETRIP_DETAIL

              
                command.Parameters.AddWithValue("@tempTripId", tempTripId);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@userId", userId);
                SqlParameter FCMFo = new SqlParameter();
                FCMFo.ParameterName = "@FoFCM";
                FCMFo.DbType = DbType.String;
                FCMFo.Size = 999;
                FCMFo.Direction = ParameterDirection.Output;
                command.Parameters.Add(FCMFo);
                SqlParameter FCMName = new SqlParameter();
                FCMName.ParameterName = "@FoName";
                FCMName.DbType = DbType.String;
                FCMName.Size = 299;
                FCMName.Direction = ParameterDirection.Output;
                command.Parameters.Add(FCMName);
                SqlParameter FCMRo = new SqlParameter();
                FCMRo.ParameterName = "@RoFCM";
                FCMRo.DbType = DbType.String;
                FCMRo.Size = 999;
                FCMRo.Direction = ParameterDirection.Output;
                command.Parameters.Add(FCMRo);
                SqlParameter TransporterName = new SqlParameter();
                TransporterName.ParameterName = "@TransporterName";
                TransporterName.DbType = DbType.String;
                TransporterName.Size = 299;
                TransporterName.Direction = ParameterDirection.Output;
                command.Parameters.Add(TransporterName);

                flag = command.ExecuteNonQuery();
                if (flag > 0)
                {
                    result.Status = "Sucess";
                    result.Value = flag.ToString();
                    result.Msg = "Status updated successfully!";
                    FoFCM= command.Parameters["@FoFCM"].Value.ToString();
                    FoName=command.Parameters["@FoName"].Value.ToString();
                    RoFCM= command.Parameters["@RoFCM"].Value.ToString();
                    TransName=command.Parameters["@TransporterName"].Value.ToString();
                }
                else
                {
                    result.Status = "Sucess";
                    result.Value = flag.ToString();
                    result.Msg = "Some problem in request contact with admin";
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.Status = "Failed";
                result.Value = "-1";
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(tempTripId));
            }


            connection.Close();
            return result;
        }

    }
}
