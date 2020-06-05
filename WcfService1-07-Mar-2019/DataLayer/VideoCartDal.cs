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
   public class VideoCartDal:BaseDal
    {
        private string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
        public DataSet UserLogin(string UserId, string Password)
        {
            string encodepassword = Encode(Password);
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
                command.CommandText = "Usp_UserLogin";
                command.Parameters.AddWithValue("@Username", UserId);
                command.Parameters.AddWithValue("@Password", encodepassword);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, Convert.ToInt32(UserId));
            }


            connection.Close();
            return ds;
        }
        public DataSet GetUserDetails(string apikey, string MobNo)
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
                command.CommandText = "SP_FARMEREXIST";
                command.Parameters.AddWithValue("@MobileNO", MobNo);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        public DataSet GetOrderDetails(string apikey, string MobNo)
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
                command.CommandText = "uspOrderList_ByMobNo";
                command.Parameters.AddWithValue("@MobNo", MobNo);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        public DataSet GetSearchProducts(bool IsActive, int CatId, int SubCatId, int ComnyId, int BrandId, int stateID, int DistrictId, int CropId, int blockID, string technicalName, int cropID, string searh, int pageNo, int pageSize, string sortColumn, string sortColumnDir)
        {
            var TotaRecords = 0;
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
                command.CommandText = "uspGetProductLIst";
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@CategoryID", CatId);
                command.Parameters.AddWithValue("@SubCategoryID", SubCatId);
                command.Parameters.AddWithValue("@CompanyID", ComnyId);
                command.Parameters.AddWithValue("@BrandID", BrandId);
                command.Parameters.AddWithValue("@StateID", stateID);
                command.Parameters.AddWithValue("@DistrictID", DistrictId);
                command.Parameters.AddWithValue("@BlockID", blockID);
                command.Parameters.AddWithValue("@TechnicalName", technicalName);
                command.Parameters.AddWithValue("@CropID", cropID);
                command.Parameters.AddWithValue("@search", searh);
                command.Parameters.AddWithValue("@pageNo", pageNo);
                command.Parameters.AddWithValue("@pageSize", pageSize);
                command.Parameters.AddWithValue("@sortColumn", sortColumn);
                command.Parameters.AddWithValue("@sortColumnDir", sortColumnDir);

                
                //SqlParameter Error = new SqlParameter();
                //Error.ParameterName = "@totolRecords";
                //Error.DbType = DbType.Int32;
                //Error.Direction = ParameterDirection.Output;
                //Error.Value=
                //command.Parameters.Add(TotaRecords);
                var p = new SqlParameter("@totolRecords", SqlDbType.Int) { Value = TotaRecords };
                command.Parameters.Add(p);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
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
        public DataSet GetCategorySubCategory(int id, char type)
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
                command.CommandText = "GetCategorySubCategory";
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Type", type);

                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        public int VideoFarmerDataCollect(int userid, string RefSource, string Fname, string Lname, string fathername, string mobile, int stateid, int districtid, int blockid,
            int villageid, string NearByVillage, string Address)
        {
            SqlConnection connection;
            SqlCommand command = new SqlCommand();
             int flag = 0;
            //int flag = "SUCCESS";

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_FarmerDataCollect_Insert";

                command.Parameters.AddWithValue("@CreatedBy", userid);
                command.Parameters.AddWithValue("@RefSource", RefSource);
                command.Parameters.AddWithValue("@Fname", Fname);
                command.Parameters.AddWithValue("@Lname", Lname);
                command.Parameters.AddWithValue("@fathername", fathername);
                command.Parameters.AddWithValue("@mobile", mobile);
                command.Parameters.AddWithValue("@stateid", stateid);
                command.Parameters.AddWithValue("@districtid", districtid);
                command.Parameters.AddWithValue("@blockid", blockid);
                command.Parameters.AddWithValue("@villageid", villageid);
                command.Parameters.AddWithValue("@NearByVillage", NearByVillage);
                command.Parameters.AddWithValue("@Address", Address);

                SqlParameter returnParameter = command.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;


                var data = command.ExecuteNonQuery();

                flag = int.Parse(data.ToString());
                flag = (int)returnParameter.Value;
               
                command.Parameters.Clear();
          

            }
            catch (Exception ex)
            {
                flag =0;
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid);
            }


            connection.Close();
            return flag;
        }
        public DataSet GetCategoryProductDetail(int stateid,int districtId, int CatId, int SubCatId)
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
                command.CommandText = "Usp_GetVideoProductDetail";
                command.Parameters.AddWithValue("@StateId", stateid);
                command.Parameters.AddWithValue("@DistrictId", districtId);
                if(CatId>0)
                {
                    command.Parameters.AddWithValue("@CatId", Convert.ToString(CatId));
                }
                if (SubCatId > 0)
                {
                    command.Parameters.AddWithValue("@SubCatId", Convert.ToString(SubCatId));
                }
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
        public DataSet GetTempOrderDetail(int UseriD, string Mobileno)
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
                command.CommandText = "Usp_TempVideoOrderSelect";
                command.Parameters.AddWithValue("@CreatedBy", UseriD);
                command.Parameters.AddWithValue("@MobileNo", Mobileno);
              
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, UseriD);

            }

            connection.Close();
            return ds;
        }
        public int OrderCreate(int userid, int FarmerId, string FarmerName, string FatherName, long Mobile, int StateId, int DistrictId, int BlockId,
      int VillageId, string OtherVillageName, string Address, string DeliveryDate, string Lat, string Long, string ModeOfPayment)//, DataTable DT
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
                command.CommandText = "Usp_VideoOrderCreate";

                command.Parameters.AddWithValue("@CreatedBy", userid);
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
                command.Parameters.AddWithValue("@Lat", Lat);
                command.Parameters.AddWithValue("@Long", Long);
                command.Parameters.AddWithValue("@OrderSource", "Video App");
              //  command.Parameters.AddWithValue("@Product", DT);

                SqlParameter Error = new SqlParameter();
                Error.ParameterName = "@Error";
                Error.DbType = DbType.Int32;
                Error.Direction = ParameterDirection.Output;
                command.Parameters.Add(Error);


                flag = command.ExecuteNonQuery();
                if (flag > 0)
                {
                    string GetError = command.Parameters["@Error"].Value.ToString();
                    flag = int.Parse(GetError);
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }
        public int TempOrderCreate(int userid, int FarmerId, DataTable DT)
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
                command.CommandText = "Usp_TempVideoOrderCreate";

                command.Parameters.AddWithValue("@CreatedBy", userid);
                command.Parameters.AddWithValue("@farmerid", FarmerId);
                command.Parameters.AddWithValue("@OrderSource", "Video App");
                command.Parameters.AddWithValue("@Product", DT);

                SqlParameter Error = new SqlParameter();
                Error.ParameterName = "@Error";
                Error.DbType = DbType.Int32;
                Error.Direction = ParameterDirection.Output;
                command.Parameters.Add(Error);


                flag = command.ExecuteNonQuery();
                if (flag > 0)
                {
                    string GetError = command.Parameters["@Error"].Value.ToString();
                    flag = int.Parse(GetError);
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;

        }
        public int TempOrderUpdate(string FMobNo, string ProductId, int PackageID, int Qty, int UserId)
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
                command.CommandText = "Usp_TempVideoOrderUpdate";

                command.Parameters.AddWithValue("@CreatedBy", UserId);
                command.Parameters.AddWithValue("@MobileNo", FMobNo);
                command.Parameters.AddWithValue("@Quantity", Qty);
                command.Parameters.AddWithValue("@PackageID", PackageID);
                command.Parameters.AddWithValue("@ProductId", ProductId);


                flag = command.ExecuteNonQuery();

                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, UserId); }

            connection.Close();
            return flag;

        }
        public DataSet GetOrderDetails(int FSCId, int RoleId, string fromdate, string todate, int status, string Mode)
        {
            if (RoleId == 0)
            {
                RoleId = 1;
            }
            //if (Mode.ToUpper() == "APP")
            //{
            //    Mode = "0";
            //}
            // DateTime from = DateTime.Now;
            DateTime from = Convert.ToDateTime("2017-09-02");
            DateTime to = DateTime.Now;
            //uspOrderList '0' ,0,0,1,0,0,0,0,'2017-09-02' ,'2017-09-02' ,1,0,'',1,5000,'CreatedDate','desc',0
            if (!string.IsNullOrEmpty(fromdate))
            {
                from = Convert.ToDateTime(fromdate);

            }
            if (!string.IsNullOrEmpty(todate))
            {

                to = Convert.ToDateTime(todate);
            }

            using (var objcmd = objDB.GetStoredProcCommand(Constant.GetOrderList))
            {

                objDB.AddInParameter(objcmd, "mode", DbType.String, Mode);
                objDB.AddInParameter(objcmd, "TripID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "RID", DbType.Int32, RoleId);
                objDB.AddInParameter(objcmd, "UID", DbType.Int32, FSCId);
                objDB.AddInParameter(objcmd, "stateID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "DistrictID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "BlockID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "VillageID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "from", DbType.DateTime, from);
                objDB.AddInParameter(objcmd, "To", DbType.DateTime, to);
                objDB.AddInParameter(objcmd, "StatusID", DbType.Int32, status);
                objDB.AddInParameter(objcmd, "FarmerID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "search", DbType.String, null);
                objDB.AddInParameter(objcmd, "pageNo", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "pageSize", DbType.Int32, 5000);
                objDB.AddInParameter(objcmd, "sortColumn", DbType.String, "CreatedDate");
                objDB.AddInParameter(objcmd, "sortColumnDir", DbType.String, "desc");
                objDB.AddOutParameter(objcmd, "totalRecords", DbType.Int32, 1);



                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }

        }
        public DataSet GetOrderDetails_OrderID(int orderid)//Change
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
                command.CommandText = "GetOrderDetails_OrderID";
                command.Parameters.AddWithValue("@OrderID", orderid);
                // command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public DataSet GetSaleOrder(string fromdate, string todate, string Mode, string DistrictId, int UserId, int stateId)
        {
            if (DistrictId == "0")
            {
                DistrictId = null;
            }
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
                command.CommandText = "SP_G_REPORT_Video";
                command.Parameters.AddWithValue("@MODE", Mode);
                command.Parameters.AddWithValue("@SDATE", Convert.ToDateTime(fromdate));
                command.Parameters.AddWithValue("@EDATE", Convert.ToDateTime(todate));
                command.Parameters.AddWithValue("@DISTRICTID", DistrictId);
                command.Parameters.AddWithValue("@UserID", UserId);
                command.Parameters.AddWithValue("@StateID", stateId);
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

        public DataSet GetBzProducts(int CategoryId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspBZProducts, CategoryId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }

        }

        public DataSet Get_BZComboProductList()
        {

            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspBZComboProducts))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
            
        }

        public DataSet Get_BZProductBanner()
        {

            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspBZProductBanner))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }

        }
    }
}
