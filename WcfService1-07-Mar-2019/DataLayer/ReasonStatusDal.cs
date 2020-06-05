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
    public class ReasonStatusDal : BaseDal
    {
        public ReasonStatusDal()
        {


        }

        #region Login, Tracking, Leave 
        /*FO Login Check */
        public DataSet FOLogin(string userName, string encodepassword)
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
                command.CommandText = "FoLogin";

                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", encodepassword);

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
        /*FO Change Password*/
        public int ChangePassword(int userid, string password, string newpassword)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FO_Change_Password";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@NewPassword", newpassword);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return flag;
        }

        /*Insert FO Location and Logout also*/
        public int InsertLatLong(int userid, string lat, string longitude, int tstatus)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FoLocation_Insert";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);
                command.Parameters.AddWithValue("@status", tstatus);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }


            connection.Close();
            return flag;
        }
        /* FO Leave Mark After Login*/
        public int FO_LeaveMark(int userid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_FO_LeaveMark";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.Add(new SqlParameter { ParameterName = "@result", DbType = DbType.Int32, Direction = ParameterDirection.Output });

                flag = command.ExecuteNonQuery();
                if (flag == 0)
                {
                    string result = command.Parameters["@result"].Value.ToString();
                    flag = int.Parse(result);
                }
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }

        /* FO Leave Mark if cash pending*/
        public DataSet FO_CashPendingLeaveMark()
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_FO_CashPendingLeaveMark";
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

        /*Message Send for list of FO whose not updating in App in last 1 hour*/
        public DataSet FoNotUpdateApp( )
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FoNotUpdateApp";

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
        /*Event Tracking*/
        #region Event Tracking
        public int EventTracking_SelectOrder(int userid, string orderIds, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_SelectOrder";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@OrderIds", orderIds);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_PickReviseOrder(int userid, int dealerId, int orderId, string type, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_PickReviseOrder";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@DealerId", dealerId);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);
                command.Parameters.AddWithValue("@Type", type);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_StartDelivery(int userid, int tripId, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_StartDelivery";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@TripId", tripId);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_ActionAtOrder(int userid, int orderId, string action, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_ActionAtOrder";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@Action", action);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);


                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_ProductReturn(int userid, int orderId, string recordIds, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_ProductReturn";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@RecordIds", recordIds);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);


                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_DataCollMarket(int userid, string type, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {

                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_DataColl_Market";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);


                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_ReprocessOrder(int userid, int orderId, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_ReprocessOrder";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_BookNewOrder(int userid, int farmerId, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_BookingOrder";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@FarmerId", farmerId);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_PackageDealerChange(int userid, int farmerId, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_PackageDealerChange";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@FarmerId", farmerId);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);

                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public int EventTracking_PackageDealerChange(int userid, int tripid, int recordid, int olddealerid, int newdealerid, string lat, string longitude)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_EventTracking_PackageDealerChange";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@RecordIds", recordid);
                command.Parameters.AddWithValue("@DealerId", olddealerid);
                command.Parameters.AddWithValue("@NewDealerId", newdealerid);
                command.Parameters.AddWithValue("@lat", lat);
                command.Parameters.AddWithValue("@longitude", longitude);


                flag = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }

        #endregion

        #endregion

        /*get all reason list for pending ,cancel, modify at distributor & farmer end*/
        public DataSet GetReasons()
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
                command.CommandText = "Get_CancelReason";
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
        /*select order screen after login */
        public DataSet GetOrders(string userid)
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
                command.CommandText = "Usp_Get_Orders";
                command.Parameters.AddWithValue("@userid", userid);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return ds;
        }

        /*update selected orders in pickup orders*/
        public int UpdateOrders(string orderid, string userid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SAVETRIP_DETAIL_For_Android";//SP_SAVETRIP_DETAIL

                command.Parameters.AddWithValue("@MODE", "SAVETRIP");
                command.Parameters.AddWithValue("@ORDER_IDS", orderid);

                SqlParameter TripId = new SqlParameter();
                TripId.ParameterName = "@TripID";
                TripId.DbType = DbType.Int32;
                TripId.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripId);

                command.Parameters.AddWithValue("@PickupAddress", "NA");
                command.Parameters.AddWithValue("@VehicleTypeID", "1");
                command.Parameters.AddWithValue("@VehicleName", "Two Wheeler");
                command.Parameters.AddWithValue("@FO_ID", userid);
                command.Parameters.AddWithValue("@vehicleNo", "NA");
                command.Parameters.AddWithValue("@StartKmReading", 0);
                command.Parameters.AddWithValue("@EndKmReading", 0);
                command.Parameters.AddWithValue("@ChargesPerKm", 0);
                command.Parameters.AddWithValue("@OtherCharges", 0);
                command.Parameters.AddWithValue("@LabourCharges", 0);
                command.Parameters.AddWithValue("@Remark", "NA");
                command.Parameters.AddWithValue("@CreateBy", 0);
                command.Parameters.AddWithValue("@ModifiedBy", 0);
                command.Parameters.AddWithValue("@IsActive", 1);

                flag = command.ExecuteNonQuery();
                if (flag > 0)
                {
                    string GetTripID = command.Parameters["@TripID"].Value.ToString();
                    flag = int.Parse(GetTripID);
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return flag;
        }
        /*Farmer Screen/Delivery Screen*/
        public DataSet GetOrdersByTrip(string tripid)
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
                command.CommandText = "Usp_Get_Orders_By_Trip";
                command.Parameters.AddWithValue("@TripID", tripid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(tripid));
            }


            connection.Close();
            return ds;
        }
        public DataSet GetPackagesByProductId(string productid)
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
                command.CommandText = "PackageDetailsProductId";
                command.Parameters.AddWithValue("@ProductID", productid);
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
        /*Pick orders  at distributor end*/
        public DataSet GetDistributorOrdersByTrip(string tripid)
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
                command.CommandText = "GetDistributorOrdersByTrip";
                command.Parameters.AddWithValue("@TripID", tripid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(tripid));
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        public DataSet GetProductsByOrderId(string orderid, string distributorid)
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
                command.CommandText = "ProductDetailsByOrderId";
                command.Parameters.AddWithValue("@orderid", orderid);
                command.Parameters.AddWithValue("@distributorid", distributorid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(distributorid));
            }


            connection.Close();
            return ds;
        }

        /*Revise orders at Dealer End & FO send notification to FSC*/
        public int NotificationForReviseOrder(string userid, string recordids, string productids, string reasonid, string remark)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_NotificationForReviseOrder";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@RecordIDs", recordids);
                command.Parameters.AddWithValue("@productids", productids);
                command.Parameters.AddWithValue("@reasonid", reasonid);
                command.Parameters.AddWithValue("@Remark", remark);


                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return flag;
        }

        public int NotificationForCancelPendingOrderByFarmer(string userid, string orderid, int statusid, string reasonid, string remark)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_NotificationForCancelPendingOrderByFarmer";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@orderid", orderid);
                command.Parameters.AddWithValue("@statusid", statusid);
                command.Parameters.AddWithValue("@reasonid", reasonid);
                command.Parameters.AddWithValue("@remark", remark);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return flag;
        }

        public DataSet GetDealerMobile(string userid, string orderid, int statusid, string reasonid)
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
                command.CommandText = "Insert_Order_OTP_Get_Dealer_Mobile";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@orderid", orderid);
                command.Parameters.AddWithValue("@statusid", statusid);
                command.Parameters.AddWithValue("@reasonid", reasonid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return ds;
        }

        public int UpdateInvoiceOrder(string userid, string orderid, int dealerid, string invoiceno)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_Update_Invoice_Order";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@OrderID", orderid);
                command.Parameters.AddWithValue("@DealerID", dealerid);
                command.Parameters.AddWithValue("@Invoice_no", invoiceno);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return flag;
        }

        #region Return TO Dealer Items
        /*Return product list which items return to distributor*/
        public DataSet GetDistributorReturnOrdersByTrip(string tripid, int userid)
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
                command.CommandText = "GetDistributorReturnOrdersByTrip";
                command.Parameters.AddWithValue("@TripID", tripid);
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(tripid)); }


            connection.Close();
            return ds;
        }
        /*Returning Product to Distributor*/
        public int UpdateReturnOrder(string userid, int dealerid, string tripid, int orderid, string recordids)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_Update_Return_Order";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@DealerID", dealerid);
                command.Parameters.AddWithValue("@RecordIds", recordids);
                command.Parameters.AddWithValue("@orderId", orderid);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid)); }


            connection.Close();
            return flag;
        }

        /*Return product list which items return to distributor Fo Wise in Last 3 Days trips*/
        public DataSet GetDistributorReturnOrdersByFOid(int userid)
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
                command.CommandText = "Usp_GetDistributorReturnOrdersByUserId";
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return ds;
        }
        #endregion

        public DataSet GetProductsByOrderId(string orderid)
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
                command.CommandText = "ProductDetailsByOrder";
                command.Parameters.AddWithValue("@orderid", orderid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(orderid)); }


            connection.Close();
            return ds;
        }

        public int NotificationForReviseOrderByFarmer(string userid, string recordid, int quantity, string productid, string reasonid, string remark)//Change
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_NotificationForReviseOrderByFarmer";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@RecordID", recordid);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@productid", productid);
                command.Parameters.AddWithValue("@reasonid", reasonid);
                command.Parameters.AddWithValue("@Remark", remark);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid)); }


            connection.Close();
            return flag;
        }

        /*End Trip: Check all orders attend by fo before end trip*/
        public int CheckTodayAllOrdersAttended(string tripid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CheckTodayAllOrdersAttended";

                command.Parameters.AddWithValue("@TripId", tripid);

                var data = command.ExecuteScalar();
                flag = int.Parse(data.ToString());
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(tripid)); }


            connection.Close();
            return flag;
        }

        public DataSet GetFarmerDetail(string userid, string orderid, string type)
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
                command.CommandText = "GetFarmerDetailByOrderId";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@orderid", orderid);
                command.Parameters.AddWithValue("@type", type);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid)); }


            connection.Close();
            return ds;
        }

        public double GetAmountCollectedAtDelivery(int orderid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            double amount = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAmountAtOrderDeliver";

                command.Parameters.AddWithValue("@orderid", orderid);

                var data = command.ExecuteScalar();
                amount = double.Parse(data.ToString());
                command.Parameters.Clear();
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, orderid); }

            connection.Close();
            return amount;
        }

        /*Display POD at farmer End before delivery*/
        public DataSet GetPODScreen(int userid, int orderid)//Change
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
                command.CommandText = "GetPODScreen";
                command.Parameters.AddWithValue("@OrderID", orderid);
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return ds;
        }

        /*Order Deliver  to Farmer */
        public int OrderDelivery(int userid, int orderid, string statusid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_InsertUpdtOrderDeliveryWithSign";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@OrderID", orderid);
                // command.Parameters.AddWithValue("@IdType", IdType);
                // command.Parameters.AddWithValue("@IdNumber", IdNo);
                //command.Parameters.AddWithValue("@OtherId", remark);
                command.Parameters.AddWithValue("@orderStatus", statusid);


                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*Upload POD with Sign*/
        public int OfflinePODUpload(int userid, int orderid, string imageBase64String)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);


            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_OfflinePODUpload";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@OrderID", orderid);
                command.Parameters.AddWithValue("@imageBase64String", imageBase64String);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*Enter Transportation Cost of Trip*/
        public int Trip_Transportation_Cost_Insert(int userid, int tripid, int vehicleid, string name, string mobile, string distance, decimal cost, string remark)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UspTrip_Transportation_Cost_Insert";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@VehicleID", vehicleid);
                command.Parameters.AddWithValue("@TranspoterName", name);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@TotalKm", distance);
                command.Parameters.AddWithValue("@TotalCost", cost);
                command.Parameters.AddWithValue("@OtherVehicle", remark);

                var data = command.ExecuteNonQuery();
                flag = int.Parse(data.ToString());
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }
        /*Enter Transportation Costs of Trip*/
        //public DataSet Trip_Transportation_Cost_Select(int userid, int tripid)
        //{
        //    SqlConnection connection;
        //    SqlDataAdapter adapter;
        //    SqlCommand command = new SqlCommand();
        //    DataSet ds = new DataSet();

        //    connection = new SqlConnection(connetionString);
        //    try
        //    {
        //        DataTable Dt = new DataTable();
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "Get_Trip_Transportation_Cost";
        //        command.Parameters.AddWithValue("@TripId", tripid);
        //        command.Parameters.AddWithValue("@userid", userid);
        //        adapter = new SqlDataAdapter(command);
        //        adapter.Fill(ds);
        //    }
        //    catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


        //    connection.Close();
        //    return ds;
        //}

        ///*Delete Transportation Cost of Trip*/
        //public int Trip_Transportation_Cost_Delete(int userid, int id, int tripid)
        //{
        //    SqlConnection connection;
        //    SqlCommand command = new SqlCommand();
        //    int flag = 0;

        //    connection = new SqlConnection(connetionString);

        //    try
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "UspTrip_Transportation_Cost_Delete";

        //        command.Parameters.AddWithValue("@userId", userid);
        //        command.Parameters.AddWithValue("@Id", id);
        //        command.Parameters.AddWithValue("@TripId", tripid);



        //        var data = command.ExecuteNonQuery();
        //        flag = int.Parse(data.ToString());
        //        command.Parameters.Clear();
        //        LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

        //    connection.Close();
        //    return flag;
        //}

        public DataSet GetFoCashCollect(string userid)
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
                command.CommandText = "GetFoCashCollect";
                command.Parameters.AddWithValue("@FoID", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid)); }


            connection.Close();
            return ds;
        }

        public int UpdateFoCashDebit(string userid, string amount, string transid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_UpdateFoCashDebit";

                command.Parameters.AddWithValue("@FoID", userid);
                command.Parameters.AddWithValue("@Debit", amount);
                command.Parameters.AddWithValue("@Comment", transid);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid)); }


            connection.Close();
            return flag;
        }
        /*Hold Last screen of Fo after login*/
        public string Get_FO_Stage(int userid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            string serviceurl = "0";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_FO_Stage";

                command.Parameters.AddWithValue("@FoId", userid);

                var data = command.ExecuteScalar();
                serviceurl = data.ToString();
                command.Parameters.Clear();
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return serviceurl;
        }
        /*Reset orders for fresh trip from old trip*/
        public int ResetOrdersByTrip(int userid, string tripid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_ResetOrdersByTrip";

                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@Userid", userid);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*Notification send FSC to FO*/
        public DataSet FSCToFONotification(int userid)
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
                command.CommandText = "FSCToFONotification";
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return ds;
        }
        /*Send Read flag fter Fo Received notification*/
        public int NotificationReadFlag(int userid, int notificationreceiverid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FONotificationReadFlag";

                command.Parameters.AddWithValue("@NotificationReceiversId", notificationreceiverid);
                command.Parameters.AddWithValue("@userid", userid);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*deviceid & fcmid insert with FOlogin API after login*/
        public int FCMDeviceIdInsert(int userid, string deviceid, string fcmid, string modelName, string imei)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FCMDeviceIdInsert";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@DeviceId", deviceid);
                command.Parameters.AddWithValue("@FCMId", fcmid);

                command.Parameters.AddWithValue("@DeviceModel", modelName);
                command.Parameters.AddWithValue("@IMEI", imei);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*deviceid & fcmid insert with getReason API without login*/
        public int FCMDeviceIdWithoutLoginInsert(string deviceid, string fcmid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FCMDeviceIdWithoutLoginInsert";

                //command.Parameters.AddWithValue("@AppType", "AIzaSyA8o9f1gNyERdiVeh3e4n6HoYC3CpBiL3U");
                command.Parameters.AddWithValue("@DeviceId", deviceid);
                command.Parameters.AddWithValue("@FCMId", fcmid);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);

            }


            connection.Close();
            return flag;
        }
        /*Payment Done By farmer*/
        public int OrderPaymentResponse(int userid, int orderid, decimal? cash, decimal? PaymentGateway, decimal? UPI, decimal? POS, string POS_MachineNo, string POS_BatchNo)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_OrderPaymentResponse";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@OrderID", orderid);
                command.Parameters.AddWithValue("@cash", cash);
                command.Parameters.AddWithValue("@PaymentGateway", PaymentGateway);
                command.Parameters.AddWithValue("@UPI", UPI);
                command.Parameters.AddWithValue("@POS", POS);
                command.Parameters.AddWithValue("@POS_MachineNo", POS_MachineNo);
                command.Parameters.AddWithValue("@POS_BatchNo", POS_BatchNo);


                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }

        /*Remove Under Revision Orders from Trip at Dealer End*/
        public int RemoveUnderRevisionOrderFromTrip(int userid, int tripid)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            connection = new SqlConnection(connetionString);

            try
            {

                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_RemoveUnderRevisionOrderFromTrip";
                command.Parameters.AddWithValue("@TripID", tripid);
                command.Parameters.AddWithValue("@userid", userid);
                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*Farmer Data collected by FO */
        public string FarmerDataCollect(int userid, string name, string fathername, Int64 mobile, int stateid, int districtid, int blockid, int villageid, string othervillagename)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            // int flag = 0;
            string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FarmerDataCollect";

                command.Parameters.AddWithValue("@FoId", userid);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@fathername", fathername);
                command.Parameters.AddWithValue("@mobile", mobile);
                command.Parameters.AddWithValue("@stateid", stateid);
                command.Parameters.AddWithValue("@districtid", districtid);
                command.Parameters.AddWithValue("@blockid", blockid);
                command.Parameters.AddWithValue("@villageid", villageid);
                command.Parameters.AddWithValue("@othervillagename", othervillagename);

                var data = command.ExecuteScalar();
                flag = data.ToString();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*Get Address Masters || Get Farmer Raw Data Collect by FO || Get District of HubHead*/
        public DataSet GetDistrictBlockVilage(int id, char type)
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
                command.CommandText = "GetDistrictBlockVilage";
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Type", type);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        /*Upload Farmer Photo*/
        public int GetFarmerPhoto(int userid, int orderid, string path)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);


            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_GetFarmerPhoto";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@OrderID", orderid);
                command.Parameters.AddWithValue("@path", path);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }
        /*Get dynamic Urls */
        public DataSet GetWebUrl(string type)
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
                command.CommandText = "Get_Urls";
                command.Parameters.AddWithValue("@Type", type);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        /*Get Meanig of Words like Reprocess and Revise*/
        public DataSet GetWordMeaning(string Type)
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
                command.CommandText = "UspGetWordMeaning";
                command.Parameters.AddWithValue("@Type", Type);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public int TripEnd(int userid, int tripid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_TripEnd";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@TripID", tripid);

                flag = command.ExecuteNonQuery();
                //flag = int.Parse(data.ToString());
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }

        #region Order Create by FO Services
        /*Get categories and get product detail district wise*/
        public DataSet GetCategoryProductDetail(int userid)
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
                command.CommandText = "Usp_GetCategoryProductDetail";
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);

            }

            connection.Close();
            return ds;
        }

        /*Get farmerId by Mobile else get zero*/
        public int GetFarmerIdByMobile(long mobile)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int id = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_GetFarmerIdByMobile";
                command.Parameters.AddWithValue("@mobile", mobile);
                var data = command.ExecuteScalar();
                if (data != null)
                {
                    id = Convert.ToInt32(data);
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);

            }

            connection.Close();
            return id;
        }

        /*Get Farmer Detail by farmerid*/
        public DataSet GetFarmerDetailByFarmerId(int farmerid)
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
                command.CommandText = "Usp_GetFarmerDetailByFarmerId";
                command.Parameters.AddWithValue("@FarmerId", farmerid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, farmerid);
            }


            connection.Close();
            return ds;
        }

        /*Create order by FO*/
        public int OrderCreate(int OfferDiscount,int Pckgid, int Qty,decimal Amount,int FarmerId,long Mobile, int StateId, int DistrictId, int BlockId,
            int VillageId, string OtherVillageName, string Address, string DeliveryDate, string ModeOfPayment,int DiscountPrice,string DiscountCode,string yonoTransactionID) //, DataTable DT
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_OrderCreateByFarmerApp";
                command.Parameters.AddWithValue("@OfferDiscount", OfferDiscount);
                command.Parameters.AddWithValue("@PackageId",Pckgid);
                command.Parameters.AddWithValue("@Quantity",Qty);
                command.Parameters.AddWithValue("@Amount",Amount);
                command.Parameters.AddWithValue("@farmerid", FarmerId);
                command.Parameters.AddWithValue("@DiscText", DiscountCode);
                command.Parameters.AddWithValue("@mobile", Mobile);
                command.Parameters.AddWithValue("@stateid", StateId);
                command.Parameters.AddWithValue("@districtid", DistrictId);
                command.Parameters.AddWithValue("@blockid", BlockId);
                command.Parameters.AddWithValue("@villageid", VillageId);
                command.Parameters.AddWithValue("@othervillagename", OtherVillageName);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
                command.Parameters.AddWithValue("@ModeOfPayment", ModeOfPayment);
                command.Parameters.AddWithValue("@yonoTransactionID", yonoTransactionID);
              //  command.Parameters.AddWithValue("@Product", DT);

                SqlParameter ErrorId = new SqlParameter();
                ErrorId.ParameterName = "@Error";
                ErrorId.DbType = DbType.Int32;
                ErrorId.Direction = ParameterDirection.Output;
                command.Parameters.Add(ErrorId);

                flag = command.ExecuteNonQuery();
                if (flag >= 1)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                if (flag == 0)
                {
                    string Error = command.Parameters["@Error"].Value.ToString();
                    flag = int.Parse(Error);// flag =2 set by error means fo try to create new order to other district so flag =2 
                }
                command.Parameters.Clear();

                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, FarmerId); }

            connection.Close();
            return flag;
        }
        #endregion

        #region Item Reprocess
        public DataSet GetItemsForReprocess(int userid, int trip)
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
                command.CommandText = "GetItemsForReprocess";//GetItemsForReprocess2
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@TripId", trip);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return ds;
        }

        public int ReprocessOrderCreate(int userid, int FarmerId, int TripId, string FarmerName, string FatherName, long Mobile, int StateId, int DistrictId, int BlockId,
            int VillageId, string OtherVillageName, string Address, string DeliveryDate, string ModeOfPayment, DataTable DT)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_ReprocessOrderCreateByFO";

                command.Parameters.AddWithValue("@FoId", userid);
                command.Parameters.AddWithValue("@TripId", TripId);
                command.Parameters.AddWithValue("@farmerid", FarmerId);
                command.Parameters.AddWithValue("@name", FarmerName);
                command.Parameters.AddWithValue("@fathername", FatherName);
                command.Parameters.AddWithValue("@mobile", Mobile);
                command.Parameters.AddWithValue("@stateid", StateId);
                command.Parameters.AddWithValue("@districtid", DistrictId);
                command.Parameters.AddWithValue("@blockid", BlockId);
                command.Parameters.AddWithValue("@villageid", VillageId);
                command.Parameters.AddWithValue("@othervillagename", OtherVillageName);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
                command.Parameters.AddWithValue("@ModeOfPayment", ModeOfPayment);
                command.Parameters.AddWithValue("@Product", DT);

                var data = command.ExecuteScalar();
                if (data != null)
                {
                    flag = Convert.ToInt32(data);
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }

        public DataSet GetOrderReprocessDetail(int userid, int orderid)
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
                command.CommandText = "GetOrderReprocessDetail";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@orderid", orderid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return ds;
        }
        #endregion
        public int InventoryItemInsert(int userId, int dealerId, DataTable DT)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FO_Inventory_Insert";

                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@DealerId", dealerId);
                command.Parameters.AddWithValue("@Items", DT);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userId); }


            connection.Close();
            return flag;
        }

        #region Dealer Change
        /*Get Dealer List by Packageid*/
        public DataSet GetDealerList(int userid, int packageid)
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
                command.CommandText = "Usp_GetDealerList";
                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@PackageId", packageid);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return ds;
        }
        /*Change Dealer in order Item wise*/
        public int DealerChange(int userId, int dealerId, int recordid, int packageid, int priceid, int reasonid, string remark)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_DealerChange";

                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@DealerId", dealerId);
                command.Parameters.AddWithValue("@PackageId", packageid);
                command.Parameters.AddWithValue("@Priceid", priceid);
                command.Parameters.AddWithValue("@RecordID", recordid);
                command.Parameters.AddWithValue("@Reasonid", reasonid);
                command.Parameters.AddWithValue("@Remark", remark);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userId); }


            connection.Close();
            return flag;
        }
        #endregion

        /*Get All Order for Delivery*/
        public DataSet GetTodayOrderDeliver(int userid)
        {
            //string connetionStringtemp = ConfigurationManager.ConnectionStrings["conprod"].ConnectionString;
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
                command.CommandText = "Usp_Get_Today_Order_Delivery";

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        #region Google Navigation 
        /*Get Data for Trip Create */
        public DataSet GetTripCreateData(string type, int distictId, string blockId, decimal avgVehicleSpeed, decimal workingHours, decimal breakTimePerDeliveryInMinute, decimal lunchTime,int BaggSize)
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
                command.CommandText = "Usp_GetTripCreateData_Praveesh";
                command.Parameters.AddWithValue("@DistictId", distictId);
                command.Parameters.AddWithValue("@BlockId", blockId);
                command.Parameters.AddWithValue("@avgVehicleSpeed", avgVehicleSpeed);
                command.Parameters.AddWithValue("@workingHours", workingHours);
                command.Parameters.AddWithValue("@breakTimePerDeliveryInMinute", breakTimePerDeliveryInMinute);
                command.Parameters.AddWithValue("@lunchTime", lunchTime);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@BaggSize", BaggSize);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public DataSet GetGeoCatSubCategoies()
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
                command.CommandText = "Usp_GetGeoCatSubCategoies";
                // command.Parameters.AddWithValue("@DistictId", distictId);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public string GeoTaggingTagInsert(int userid, int subCategoryId, string Name, string lat, string longitute, int villageId, string otherVillage, int blockId)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            string Errormsg = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_GeoTaggingTagInsert";

                command.Parameters.AddWithValue("@Userid", userid);
                command.Parameters.AddWithValue("@GeoSubCategory", subCategoryId);
                command.Parameters.AddWithValue("@LocationName", Name);
                command.Parameters.AddWithValue("@Lat", lat);
                command.Parameters.AddWithValue("@Longitude", longitute);
                command.Parameters.AddWithValue("@villageId", villageId);
                command.Parameters.AddWithValue("@otherVillageName", otherVillage);
                command.Parameters.AddWithValue("@blockId", blockId);

                SqlParameter Error = new SqlParameter();
                Error.ParameterName = "@Error";
                Error.DbType = DbType.String;
                Error.Size = 299;
                Error.Direction = ParameterDirection.Output;
                command.Parameters.Add(Error);

                flag = command.ExecuteNonQuery();
                Errormsg = command.Parameters["@Error"].Value.ToString();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                if (Errormsg == "")
                {
                    Errormsg = flag.ToString();
                }
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return Errormsg;
        }

        public DataSet GetGeoTaggingData()
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
                command.CommandText = "Usp_GetGeoTaggingData";
                // command.Parameters.AddWithValue("@DistictId", distictId);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public DataSet GetGeoTaggingDataWithVillage(int UserId)
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
                command.CommandText = "Usp_GetGeoTaggingDataWithVillage";
                command.Parameters.AddWithValue("@FoId", UserId);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        #endregion

        #region Hub Head
        /// <summary>
        /// This Service is used to get all FO & HubHead List with userid as Hubhead districtwise with Type=FHH
        /// and this service also used to get all Hubhead List with userid as FO with districtwise with Type=HH 
        /// </summary>
        /// <param name="userid">Hubheadid or FOID</param>
        /// <param name="type">FHH Or HH</param>
        /// <returns></returns>
        public DataSet GetHubHeadNFODistrictWiseList(string userid, string type)
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
                command.CommandText = "Usp_GetHubHeadNFODistrictWiseList";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@type", type);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(userid));
            }


            connection.Close();
            return ds;
        }

        /* This Service is used to submit FO's Cash to HubHead */
        public int FoCashSubmitToRO(int userid, int hhid, string amount, string desc)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FoCashSubmitToRO";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@RoId", hhid);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Desc", desc);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }

        /* Hubhead wise Fo Amount Approval List*/
        public DataSet GetFoAmountApprovalList(int hhid)
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
                command.CommandText = "Usp_GetFoAmountApprovalList";
                command.Parameters.AddWithValue("@RoId", hhid);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, hhid);
            }

            connection.Close();
            return ds;
        }
        /* Approval/ Decline FO's Amount by HubHead*/
        public int FoCashApprovalDeclineByHubHead(int hhid, int recordid, string status)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FoCashApprovalDeclineByHubHead";

                command.Parameters.AddWithValue("@RoId", hhid);
                command.Parameters.AddWithValue("@RecordId", recordid);
                command.Parameters.AddWithValue("@Status", status);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, hhid); }


            connection.Close();
            return flag;
        }

        /*HubHead Fetch All orders with Amount of his District*/
        public DataSet GetTodayTotalOrderDistrictWise(int districtid)
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
                command.CommandText = "Usp_GetTodayTotalOrderDistrictWise";
                command.Parameters.AddWithValue("@DistictId", districtid);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, districtid);
            }

            connection.Close();
            return ds;
        }

        /*HubHead Get All Order Status By Fo */
        public DataSet GetTodayOrderByFo(int foId, int tripId)
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
                command.CommandText = "Usp_GetTodayOrderByFo";
                command.Parameters.AddWithValue("@FoId", foId);
                command.Parameters.AddWithValue("@TripId", tripId);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, foId);
            }

            connection.Close();
            return ds;
        }

        public DataSet GetTodayOrderStatusByOrderId(int OrderId)
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
                command.CommandText = "Usp_GetTodayOrderStatusByOrderId";
                command.Parameters.AddWithValue("@OrderId", OrderId);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, OrderId);
            }

            connection.Close();
            return ds;
        }
        /*HubHead Release Fo Trip,until he pick order*/
        public int ResetOrdersFoWise(int foid, int UserId)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_ResetOrdersFoWise";

                command.Parameters.AddWithValue("@FoId", foid);
                command.Parameters.AddWithValue("@Userid", UserId);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, foid); }


            connection.Close();
            return flag;
        }
        /*********Get FO Tracking Detail by Fo Wise*******************************/
        public DataSet GetTrackFOByFo(int foId)
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
                command.CommandText = "Usp_GetTrackFOByFo";
                command.Parameters.AddWithValue("@FoId", foId);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, foId);
            }

            connection.Close();
            return ds;
        }

        /**********Get Order Detail for Delete by HubHead*****************/
        public DataSet GetOrderDetailByOrderId(int OrderId)
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
                command.CommandText = "Usp_GetOrderDetailBy_OrderId";
                command.Parameters.AddWithValue("@OrderId", OrderId);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, OrderId);
            }


            connection.Close();
            return ds;
        }

        /*Order Deliver by HubHead*/
        public int OrderDeliverByHubHead(int foid, int userId, int orderId, int status, int IdType, string IdNumber, string otherId)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_OrderDeliverByHubHead";

                command.Parameters.AddWithValue("@FoId", foid);
                command.Parameters.AddWithValue("@Userid", userId);
                command.Parameters.AddWithValue("@OrderID", orderId);
                command.Parameters.AddWithValue("@IdType", IdType);
                command.Parameters.AddWithValue("@IdNumber", IdNumber);
                command.Parameters.AddWithValue("@otherId", otherId);
                command.Parameters.AddWithValue("@orderStatus", status);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, foid); }


            connection.Close();
            return flag;
        }

        /*Order Items  Assign to Other fo for same Order*/
        public int OrderItemsAssignTOOtherFoByHubHead(int FoId, int NewFoId, int HubheadId, DataTable DT)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_OrderItemsAssignTOOtherFoByHubHead";

                command.Parameters.AddWithValue("@FoId", FoId);
                command.Parameters.AddWithValue("@NewFoId", NewFoId);
                command.Parameters.AddWithValue("@HubheadId", HubheadId);
                command.Parameters.AddWithValue("@Items", DT);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, FoId); }


            connection.Close();
            return flag;
        }

        #endregion

        #region Logistic 
        public DataSet GetActionTakenTrips(DateTime date)
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
                command.CommandText = "Usp_GetActionTakenTrips";
                command.Parameters.AddWithValue("@date", date);

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

        public DataSet GetActionTakenOrders(int TripId)
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
                command.CommandText = "Usp_GetActionTakenOrders";
                command.Parameters.AddWithValue("@TripId", TripId);


                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public DataSet GetActionTakenOrderDetail(int OrderId)
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
                command.CommandText = "Usp_GetActionTakenOrderDetail";
                command.Parameters.AddWithValue("@OrderId", OrderId);


                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        #endregion

        #region CreatedByArpit for Temp Fo
        /*select order screen after login */
        public DataSet GetOrdersByFo(string mobile)
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
                command.CommandText = "Usp_Get_OrdersByFo";
                command.Parameters.AddWithValue("@mobile", mobile);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(mobile));
            }


            connection.Close();
            return ds;
        }
        public DataSet GetOrdersByFo(string mobile,DateTime date)
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
                command.CommandText = "Usp_Get_OrdersByFoWithDate";
                command.Parameters.AddWithValue("@mobile", mobile);
                command.Parameters.AddWithValue("@date", date);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(mobile));
            }


            connection.Close();
            return ds;
        }

        /*update selected orders in pickup orders*/
        public ResponseObj UpdateOrdersToFo(string orderid, string mobile, string vehicletypeid, string vehiclename, string createdby, string transpoterId, string ruleId)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            var result = new ResponseObj();
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SAVETRIP_DETAIL_For_AndroidToFo";//SP_SAVETRIP_DETAIL

                command.Parameters.AddWithValue("@MODE", "SAVETRIP");
                command.Parameters.AddWithValue("@ORDER_IDS", orderid);

                SqlParameter TripId = new SqlParameter();
                TripId.ParameterName = "@TripID";
                TripId.DbType = DbType.Int32;
                TripId.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripId);

                command.Parameters.AddWithValue("@PickupAddress", "NA");
                command.Parameters.AddWithValue("@VehicleTypeID", vehicletypeid);
                command.Parameters.AddWithValue("@VehicleName", vehiclename);
                command.Parameters.AddWithValue("@mobile", mobile);
                command.Parameters.AddWithValue("@vehicleNo", "NA");
                command.Parameters.AddWithValue("@StartKmReading", 0);
                command.Parameters.AddWithValue("@EndKmReading", 0);
                command.Parameters.AddWithValue("@ChargesPerKm", 0);
                command.Parameters.AddWithValue("@OtherCharges", 0);
                command.Parameters.AddWithValue("@LabourCharges", 0);
                command.Parameters.AddWithValue("@Remark", "NA");
                command.Parameters.AddWithValue("@CreateBy", Convert.ToInt32(createdby));
                command.Parameters.AddWithValue("@ModifiedBy", 0);
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@TransporterId", transpoterId);
                command.Parameters.AddWithValue("@RuleId", ruleId);
                SqlParameter TripMSG = new SqlParameter();
                TripMSG.ParameterName = "@TripMsg";
                TripMSG.DbType = DbType.String;
                TripMSG.Size = 299;
                TripMSG.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripMSG);

                flag = command.ExecuteNonQuery();

                //   if (flag > 0)
                // {
                result.Status = "Sucess";

                string GetTripID = command.Parameters["@TripID"].Value.ToString();
                // flag = int.Parse(GetTripID);
                result.Value = GetTripID;
                result.Msg = command.Parameters["@TripMsg"].Value.ToString();
                //  }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name + "DAL");
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

        public ResponseObj UpdateOrdersToFo(string orderid, string mobile, string vehicletypeid, string vehiclename, string createdby, string transpoterId, string ruleId,DateTime date)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            var result = new ResponseObj();
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SAVETRIP_DETAIL_For_AndroidToFoWithDate";//SP_SAVETRIP_DETAIL

                command.Parameters.AddWithValue("@MODE", "SAVETRIP");
                command.Parameters.AddWithValue("@ORDER_IDS", orderid);

                SqlParameter TripId = new SqlParameter();
                TripId.ParameterName = "@TripID";
                TripId.DbType = DbType.Int32;
                TripId.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripId);

                command.Parameters.AddWithValue("@PickupAddress", "NA");
                command.Parameters.AddWithValue("@VehicleTypeID", vehicletypeid);
                command.Parameters.AddWithValue("@VehicleName", vehiclename);
                command.Parameters.AddWithValue("@mobile", mobile);
                command.Parameters.AddWithValue("@vehicleNo", "NA");
                command.Parameters.AddWithValue("@StartKmReading", 0);
                command.Parameters.AddWithValue("@EndKmReading", 0);
                command.Parameters.AddWithValue("@ChargesPerKm", 0);
                command.Parameters.AddWithValue("@OtherCharges", 0);
                command.Parameters.AddWithValue("@LabourCharges", 0);
                command.Parameters.AddWithValue("@Remark", "NA");
                command.Parameters.AddWithValue("@CreateBy", Convert.ToInt32(createdby));
                command.Parameters.AddWithValue("@ModifiedBy", 0);
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@TransporterId", transpoterId);
                command.Parameters.AddWithValue("@RuleId", ruleId);
                command.Parameters.AddWithValue("@date", date);
                SqlParameter TripMSG = new SqlParameter();
                TripMSG.ParameterName = "@TripMsg";
                TripMSG.DbType = DbType.String;
                TripMSG.Size = 299;
                TripMSG.Direction = ParameterDirection.Output;
                command.Parameters.Add(TripMSG);

                flag = command.ExecuteNonQuery();

                //   if (flag > 0)
                // {
                result.Status = "Sucess";

                string GetTripID = command.Parameters["@TripID"].Value.ToString();
                // flag = int.Parse(GetTripID);
                result.Value = GetTripID;
                result.Msg = command.Parameters["@TripMsg"].Value.ToString();
                //  }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name + "DAL");
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

        public DataSet GetUserEmailByUserName(string mobile)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            //var emailId = "";
            connection = new SqlConnection(connetionString);

            try
            {
                // DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetUserEmailByUserName";
                command.Parameters.AddWithValue("@userName", mobile);

                //emailId = command.ExecuteScalar().ToString();
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(mobile));
            }


            connection.Close();
            return ds;
        }

        public DataSet GetInvoiceNobyOrderId(string orderId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                // DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetInvoiceNobyOrderId";
                command.Parameters.AddWithValue("@orderId", orderId);


                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(orderId));
            }


            connection.Close();
            return ds;
        }

        public DataSet GetPodbyOrderId(string orderId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                // DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_PODDETAILBYOrderId";
                command.Parameters.AddWithValue("@orderId", orderId);


                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(orderId));
            }


            connection.Close();
            return ds;
        }

        public void SaveNotifyLog(NotifyLog notifyLog)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();


            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SaveNotifyLog";

                command.Parameters.AddWithValue("@FcmId", notifyLog.FcmId);
                command.Parameters.AddWithValue("@AppKeyId", notifyLog.AppKeyId);
                command.Parameters.AddWithValue("@Msg", notifyLog.Msg);

                command.Parameters.AddWithValue("@Status", notifyLog.Status);
                command.Parameters.AddWithValue("@MsgId", notifyLog.MsgId);
                command.Parameters.AddWithValue("@ErrorMsg", notifyLog.ErrorMsg);
                command.Parameters.AddWithValue("@CreatedDate", notifyLog.CreatedDate);
                command.Parameters.AddWithValue("@CreatedBy", notifyLog.CreatedBy);

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();







        }

        #endregion

        #region  Change By Ajay
        public int Trip_Transportation_Cost_Insert(int userid, int tripid, int vehicleid, string vehicleNo, string startKm, string EndKm, int ruleId, int subRuleId, string remark)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UspTrip_Transportation_Cost_Insert";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@VehicleID", vehicleid);
                command.Parameters.AddWithValue("@vehicleNo", vehicleNo);
                command.Parameters.AddWithValue("@startKm", startKm);
                command.Parameters.AddWithValue("@EndKm", EndKm);
                command.Parameters.AddWithValue("@ruleId", ruleId);
                command.Parameters.AddWithValue("@subruleId", subRuleId);
                command.Parameters.AddWithValue("@remark", remark);

                SqlParameter returnParameter = command.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;


                var data = command.ExecuteNonQuery();

                flag = int.Parse(data.ToString());
                if (flag != 1)
                {
                    flag = (int)returnParameter.Value;
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }
        /*Enter Transportation Expense of Trip*/
        public int Trip_Transportation_Expense_Insert(int userid, int tripid, int vehicleid, string Description, double Amount, int ruleId, int subRuleId, int ExpenseID)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UspTrip_Transportation_Expense_Insert";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@VehicleID", vehicleid);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@Amount", Amount);
                command.Parameters.AddWithValue("@ruleId", ruleId);
                command.Parameters.AddWithValue("@subruleId", subRuleId);
                command.Parameters.AddWithValue("@ExpenseID", ExpenseID);

                var data = command.ExecuteNonQuery();

                flag = int.Parse(data.ToString());

                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }

        /*Enter Transportation DownKm of Trip*/
        public int Trip_Transportation_DownKm(int userid, int tripid, int ruleId, int subRuleId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            int DownKM = 0;
            connection = new SqlConnection(connetionString);
            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_Trip_Transportation_Cost";
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@ruleId", ruleId);
                command.Parameters.AddWithValue("@subRuleId", subRuleId);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                if (ds != null)
                {

                    DownKM = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return DownKM;
        }

        /*Enter Transportation Costs of Trip*/
        public DataSet Trip_Transportation_Cost_Select(int userid, int tripid)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);
            try
            {
              //  DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_Trip_Transportation_Cost";
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return ds;
        }
        public Transport_TotalCost Trip_Transportation_TotalCost_Select(int userid, int tripid)
        {
            Transport_TotalCost objtcost = new Transport_TotalCost();
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
                command.CommandText = "Get_Trip_Transportation_TotalCost";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@TripId", tripid);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                if (ds.Tables.Count>0 && ds!=null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            objtcost = new Transport_TotalCost();

                            objtcost.TotalKm = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalKM"].ToString());
                            objtcost.Cost = Convert.ToDouble(ds.Tables[0].Rows[i]["Cost"].ToString());
                            objtcost.VehicleNo = ds.Tables[0].Rows[i]["Vehicleno"].ToString();
                            objtcost.Type = ds.Tables[0].Rows[i]["Type"].ToString();



                        }

                    }
                    else
                    {
                        objtcost = null;
                    }
                }
                else
                {
                    objtcost = null;
                }
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
                objtcost = null;
            }


            connection.Close();
            return objtcost;
        }
        public DataSet Trip_Transportation_Expense_Select(int userid, int tripid)
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
                command.CommandText = "Get_Trip_Transportation_ExpenseCost";
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@userid", userid);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return ds;
        }
        /*Select Transportation Vehicle Type*/
        public DataSet Trip_Transportation_Vehicle_Select(int userid)
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
                command.CommandText = "Get_Trip_Transportation_VehicleType";
                command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        /*Select Transportation Expense Type*/
        public DataSet Trip_Transportation_Expense_Select()
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
                command.CommandText = "Get_Trip_Transportation_Expense";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        /*Select Transportation Vehicle Rule*/
        public DataSet Trip_Transportation_FareRule_Select(int userid, int vehicleid)
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
                command.CommandText = "Get_Trip_Transportation_Vehicle_FareRule";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@VehicleID", vehicleid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        /*Delete Transportation Cost of Trip*/
        public int Trip_Transportation_Cost_Delete(int userid, int tripid, int Id)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UspTrip_Transportation_Cost_Delete";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@TripId", tripid);
                command.Parameters.AddWithValue("@Id", Id);


                var data = command.ExecuteNonQuery();
                flag = int.Parse(data.ToString());
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }
        /*Select Trip Id of Trip*/
        public string GetTripId(int Foid)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            string amount = "0";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Get_TripId";

                command.Parameters.AddWithValue("@FoId", Foid);

                var data = command.ExecuteScalar();
                amount = data.ToString();
                command.Parameters.Clear();
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, Foid); }

            connection.Close();
            return amount;
        }

        public string GetTripStatus(int Foid)

        {

            SqlConnection connection;

            SqlCommand command = new SqlCommand();

            string status = "0";



            connection = new SqlConnection(connetionString);



            try

            {

                connection.Open();

                command.Connection = connection;

                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = "SP_Get_TripStatus";



                command.Parameters.AddWithValue("@FoId", Foid);



                var data = command.ExecuteScalar();

                status = data.ToString();

                command.Parameters.Clear();

            }

            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, Foid); }



            connection.Close();

            return status;

        }

        #endregion


        #region Invoice and POD 
        /*TO Get Trip Order Detail for send mail*/
        public DataSet Get_FoTripOrders(int tripId, string userName, string TripType)
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
                command.CommandText = "Get_FoTripOrdersDetailForMail";
                command.Parameters.AddWithValue("@TripId", tripId);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Type", TripType);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, tripId); }


            connection.Close();
            return ds;
        }
        /*TO Get Trip Order Invoice Detail for send mail*/
        public DataSet GetPodDetailReport(int tripId, string TripType)
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
                command.CommandText = "USP_GetPodDetailReport";
                command.Parameters.AddWithValue("@TRIPID", tripId);
                command.Parameters.AddWithValue("@Type", TripType);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, tripId); }


            connection.Close();
            return ds;
        }

        /*To Set Flag which RecordId invoice and POD generated*/
        public int InvoicePDFSendFlagUpdate(string RecordId)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_InvoicePDFSendFlagUpdate";

                command.Parameters.AddWithValue("@RecordId", RecordId);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;
        }
        #endregion

        public DataSet Get_FarmerOrderListForDeliver(int foId, int farmerId)
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
                command.CommandText = "Usp_Get_FarmerOrderListForDeliver";
                command.Parameters.AddWithValue("@FO_Id", foId);
                command.Parameters.AddWithValue("@FarmerId", farmerId);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, farmerId); }


            connection.Close();
            return ds;
        }

        #region Dealer Scan Service
        public DataSet GetDealerWiseProductInTrip(int userid, int trip)
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
                command.CommandText = "Usp_GetDealerWiseProductInTrip";
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@TripId", trip);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return ds;
        }

        public int DealerPackagingAcceptRejectByFO(int userid, string  actionTaken, int  dealerId,DataTable DT)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_DealerPackagingAcceptRejectByFO";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@ActionTaken", actionTaken);
                command.Parameters.AddWithValue("@DealerId", dealerId);
                command.Parameters.AddWithValue("@Items", DT);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }

        public int CheckTripStatus(int TripId, int FoId)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_CheckTripStatus";
                command.Parameters.AddWithValue("@TripId", TripId);
                command.Parameters.AddWithValue("@FOID", FoId);

                SqlParameter returnParameter = command.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                var data = command.ExecuteNonQuery();
                flag = (int)returnParameter.Value;
              
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;
        }
        #endregion

        public SathiCreatedOrderResult CreateOrderBySathi(int userid, int FarmerId, string FarmerName, string FatherName, long Mobile, int StateId, int DistrictId, int BlockId,
            int VillageId, string OtherVillageName, string Address, string DeliveryDate, string ModeOfPayment, DataTable DT)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";

            connection = new SqlConnection(connetionString);
            SathiCreatedOrderResult objDataObject = new SathiCreatedOrderResult();
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_OrderCreateBySathi";

                command.Parameters.AddWithValue("@SathiId", userid);
                command.Parameters.AddWithValue("@farmerid", FarmerId);
                command.Parameters.AddWithValue("@name", FarmerName);
                command.Parameters.AddWithValue("@fathername", FatherName);
                command.Parameters.AddWithValue("@mobile", Mobile);
                command.Parameters.AddWithValue("@stateid", StateId);
                command.Parameters.AddWithValue("@districtid", DistrictId);
                command.Parameters.AddWithValue("@blockid", BlockId);
                command.Parameters.AddWithValue("@villageid", VillageId);
                command.Parameters.AddWithValue("@othervillagename", OtherVillageName);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
                command.Parameters.AddWithValue("@ModeOfPayment", ModeOfPayment);
                command.Parameters.AddWithValue("@Product", DT);

                SqlParameter ErrorId = new SqlParameter();
                ErrorId.ParameterName = "@Error";
                ErrorId.DbType = DbType.Int32;
                ErrorId.Direction = ParameterDirection.Output;
                command.Parameters.Add(ErrorId);                 
                flag = command.ExecuteNonQuery();
                
                if (flag >= 1)
                {
                    flag = 1;
                    string Error = command.Parameters["@Error"].Value.ToString();
                    SathiCreatedOrderResult objData = GetCreateOrderData(Convert.ToInt32(Error));
                    //objDataObject.OrderAmount = 1;
                    //objDataObject.OrderId = 329676;
                    //objDataObject.Status = 1;
                    objDataObject.OrderAmount = objData.OrderAmount;
                    objDataObject.OrderId = Convert.ToInt32(Error);
                    objDataObject.Status = 1;
                }
                else
                {
                    //flag = 0;
                    string Error = command.Parameters["@Error"].Value.ToString();
                    objDataObject.OrderAmount = 0;
                    objDataObject.OrderId = 0;
                    objDataObject.Status = Convert.ToInt32(Error); 
                }
                if (flag == 0)
                {
                    //string Error = command.Parameters["@Error"].Value.ToString();
                    //flag = int.Parse(Error);// flag =2 set by error means fo try to create new order to other district so flag =2 
                    
                    objDataObject.OrderAmount = 0;
                    objDataObject.OrderId = 0;
                    objDataObject.Status = 2;
                }
                command.Parameters.Clear();

                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return objDataObject;
        }

        public SathiCreatedOrderResult GetCreateOrderData(int OrderId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);
            SathiCreatedOrderResult objData = new SathiCreatedOrderResult();
            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetSathiOrderAmount";
                command.Parameters.AddWithValue("@OrderId", OrderId); 
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objData.OrderAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OrderAmount"].ToString());
                    objData.OrderId = OrderId;
                    objData.Status = 1;
                }
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, OrderId); }
            connection.Close();
            return objData;
        }
    }
}
