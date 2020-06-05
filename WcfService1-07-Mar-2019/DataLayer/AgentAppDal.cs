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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Globalization;

namespace DataLayer
{
    public class AgentAppDal : BaseDal
    {
        public DataSet GetFarmerByFsc(string FscId, int Mode)
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
                command.CommandText = "usp_FarmerDeatils";
                command.Parameters.AddWithValue("@FscID", FscId);
                command.Parameters.AddWithValue("@Mode", Mode);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, int.Parse(FscId));
            }


            connection.Close();
            return ds;
        }

        public DataSet GetFarmerCallHistory(string MobileNo)
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
                command.CommandText = "Usp_FarmerCallHistory";
                command.Parameters.AddWithValue("@FarmerMob", MobileNo);
              
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
        public DataSet GetFarmerDetails(string FarmerKey)
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
                command.CommandText = "Usp_FarmerDetails";
                command.Parameters.AddWithValue("@FarmerKey", FarmerKey);

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
                command.CommandText = "FSC_Change_Password";

                command.Parameters.AddWithValue("@UserId", userid);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@NewPassword", newpassword);

                 flag =Convert.ToInt32(command.ExecuteScalar());
                           }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return flag;
        }

        public int UpdateCallLog(string MobileNo)
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
                command.CommandText = "Usp_Farmer_CallLog_Update";

                command.Parameters.AddWithValue("@MobileNo", MobileNo);
               

                flag = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return flag;
        }
        public DataSet GetCategoryProductDetail(int stateid, int districtId, int CatId, int SubCatId,int PackageID)
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
                command.CommandText = "SP_GetBZProductList";
                //  command.Parameters.AddWithValue("@StateId", stateid);
                command.Parameters.AddWithValue("@DistrictId", districtId);
                command.Parameters.AddWithValue("@PackageID", PackageID);
                if (CatId > 0)
                {
                    command.Parameters.AddWithValue("@CategoryID", Convert.ToString(CatId));
                }
                if (SubCatId > 0)
                {
                    command.Parameters.AddWithValue("@SubCategoryID", Convert.ToString(SubCatId));
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
        private string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
        public DataSet GetOrderDetails(int FSCId, int RoleId, string fromdate, string todate, int status,string Mode)
        {
    
            if(RoleId==0)
            {
                RoleId = 1;
            }
            
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
                command.CommandText = "GetOrderDetails_OrderID_AgentApp";
                command.Parameters.AddWithValue("@OrderID", orderid);
                // command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }

        public DataSet GetSaleOrder(string fromdate, string todate, string Mode, string DistrictId,int UserId, int stateId)
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
                command.CommandText = "SP_G_REPORT_AP";
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

        public DataSet GetDistrictBlockVilage(int id, char type)
        {
            SqlCommand selectCommand = new SqlCommand();
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(BaseDal.connetionString);
            try
            {
                DataTable table = new DataTable();
                connection.Open();
                selectCommand.Connection = connection;
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandText = "GetDistrictBlockVilage";
                selectCommand.Parameters.AddWithValue("@Id", id);
                selectCommand.Parameters.AddWithValue("@Type", type);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            catch (Exception exception)
            {
                LogDal.ErrorLog(base.GetType().Name, MethodBase.GetCurrentMethod().Name, exception.Message, 0);
            }
            connection.Close();
            return dataSet;
        }

        public DataSet GetUserStatus(int userId)
        {
            SqlCommand selectCommand = new SqlCommand();
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(BaseDal.connetionString);
            try
            {
                DataTable table = new DataTable();
                connection.Open();
                selectCommand.Connection = connection;
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandText = "USP_GetUserStatus";
                selectCommand.Parameters.AddWithValue("@UserId", userId);
              
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            catch (Exception exception)
            {
                LogDal.ErrorLog(base.GetType().Name, MethodBase.GetCurrentMethod().Name, exception.Message, 0);
            }
            connection.Close();
            return dataSet;
        }
        public DataSet GetCouponList(string CatId, string SubCatId, string CompnanId, string BrandId, string PCKGId, string Itemval)
        {
            SqlCommand selectCommand = new SqlCommand();
            DataSet dataSet = new DataSet();
            SqlConnection connection = new SqlConnection(BaseDal.connetionString);
            try
            {
                DataTable table = new DataTable();
                connection.Open();
                selectCommand.Connection = connection;
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandText = "SP_GET_APPLICABLE_COUPON";
                selectCommand.Parameters.AddWithValue("@CAT_ids", CatId);
                selectCommand.Parameters.AddWithValue("@SUBCAT_ids", SubCatId);
                selectCommand.Parameters.AddWithValue("@COMP_ids", CompnanId);
                selectCommand.Parameters.AddWithValue("@BRND_ids", BrandId);
                selectCommand.Parameters.AddWithValue("@PCKG_ids", PCKGId);
                selectCommand.Parameters.AddWithValue("@ORD_val", Itemval);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            catch (Exception exception)
            {
                LogDal.ErrorLog(base.GetType().Name, MethodBase.GetCurrentMethod().Name, exception.Message, 0);
            }
            connection.Close();
            return dataSet;
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

        public DataSet GetCategorySubCategory(int id, string type)
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

        public int DemandOrderCreate(string FarmerName, long Mobile, int CropID, int CategoryID,int PackageID, decimal FarmerPrice, string Product, string AadharOrLoanNo,int DistrictId,int UserId,int Qty)
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
                command.CommandText = "Usp_CreateDemandGeneration";

                command.Parameters.AddWithValue("@FarmerName", FarmerName);
                command.Parameters.AddWithValue("@MobNo", Mobile);
                command.Parameters.AddWithValue("@CropID", CropID);
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                command.Parameters.AddWithValue("@PackageID", PackageID);
                command.Parameters.AddWithValue("@FarmerPrice", FarmerPrice);
                command.Parameters.AddWithValue("@Products", Product);
                command.Parameters.AddWithValue("@AadharOrLoanNo", AadharOrLoanNo);
                command.Parameters.AddWithValue("@DistId", DistrictId);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Qty", Qty);
                //SqlParameter Error = new SqlParameter();
                //Error.ParameterName = "@Error";
                //Error.DbType = DbType.Int32;
                //Error.Direction = ParameterDirection.Output;
                //command.Parameters.Add(Error);


                flag = Convert.ToInt32(command.ExecuteScalar());
                //if (flag > 0)
                //{
                //    string GetError = command.Parameters["@Error"].Value.ToString();
                //    flag = int.Parse(GetError);
                //}
                command.Parameters.Clear();
               // LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;

        }

        public DataSet ApplyCoupon(int CatID, int SubCatID, int CompanyID, int BrandID, int ProductID, int PkgID, int Qty, decimal ActualAmt, int CouponID)
        {
            using (var cmd = objDB.GetStoredProcCommand("[dbo].[Usp_ApplyCoupon_AgentApp]"))
            {
                cmd.Parameters.Add(new SqlParameter("@CatID", CatID));
                cmd.Parameters.Add(new SqlParameter("@SubCatID", SubCatID));
                cmd.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
                cmd.Parameters.Add(new SqlParameter("@BrandID", BrandID));
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@PkgID", PkgID));
                cmd.Parameters.Add(new SqlParameter("@Qty", Qty));
                cmd.Parameters.Add(new SqlParameter("@ActualAmt", ActualAmt));
                cmd.Parameters.Add(new SqlParameter("@CouponID", CouponID));
                return objDB.ExecuteDataSet(cmd);
            }
        }

        public int AgentOrderCreate(int userid, int FarmerId, string FarmerName, string FatherName, long Mobile, int StateId, int DistrictId, int BlockId,
    int VillageId, string OtherVillageName, string Address, string DeliveryDate, string ModeOfPayment, DataTable DT)
        {
         
               SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";
            var vDeliverDate = Convert.ToDateTime(Convert.ToDateTime(DeliveryDate).ToString("yyyy-MM-dd 00:00:00.000"));
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_AgentOrderCreate";

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
                command.Parameters.AddWithValue("@DeliveryDate", vDeliverDate);
                command.Parameters.AddWithValue("@ModeOfPayment", ModeOfPayment);
                command.Parameters.AddWithValue("@OrderSource", "Agent App");
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
        public int IssueRegister( decimal Mobile, string Name, int CategoryID, int IssueDetailID, string Product, int CreatedBy,int IssueTypeId)
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
                command.CommandText = "Usp_AddNewQuery";

                command.Parameters.AddWithValue("@MobNo", Mobile);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                command.Parameters.AddWithValue("@IssueDetailID", IssueDetailID);
                command.Parameters.AddWithValue("@Query", Product);
                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                command.Parameters.AddWithValue("@IssueTypeID", IssueTypeId);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;

        }
        public DataSet IssueDetailByFarmer(string Mobileno)
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
                command.CommandText = "Usp_GetIssueDetail_ByFarmer";
                command.Parameters.AddWithValue("@MobNo", Mobileno);
                command.Parameters.AddWithValue("@Mode", "APP");
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
        public string RemoveItemFromCart(int OrderID, int RecordId,  int DeletedBy)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            string flag = "";
         
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_RemoveItem_FromCart";

                command.Parameters.AddWithValue("@OrderID", OrderID);
                command.Parameters.AddWithValue("@RecordId", RecordId);
                command.Parameters.AddWithValue("@DeletedBy", DeletedBy);                        

                SqlParameter Error = new SqlParameter();
                Error.ParameterName = "@Result";
                Error.SqlDbType = SqlDbType.NVarChar;
                Error.Size = 200;
                Error.Direction = ParameterDirection.Output;
                command.Parameters.Add(Error);

                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    string GetError = command.Parameters["@Result"].Value.ToString();
                    flag = GetError;
                }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) {

                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
                flag = "Internal error,please try after sometime.";
            }

            connection.Close();
            return flag;
        }
        public DataSet GetOrderDetailsByFarmerID(int FarmerID, string fromdate, string todate, int status)
        {
            // DateTime from = DateTime.Now;
            DateTime from = Convert.ToDateTime("2016-04-01");
            DateTime to = DateTime.Now;
            //uspOrderList '0' ,0,0,1,0,0,0,0,'2017-09-02' ,'2017-09-02' ,1,0,'',1,5000,'CreatedDate','desc',0
            if (!string.IsNullOrEmpty(fromdate))
            {
                // from = Convert.ToDateTime(fromdate);
                from = Convert.ToDateTime(fromdate);

            }
            if (!string.IsNullOrEmpty(todate))
            {

                to = Convert.ToDateTime(todate);
            }

            using (var objcmd = objDB.GetStoredProcCommand(Constant.GetOrderList))
            {
                objDB.AddInParameter(objcmd, "mode", DbType.String, "0");
                objDB.AddInParameter(objcmd, "TripID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "RID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "UID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "stateID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "DistrictID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "BlockID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "VillageID", DbType.Int32, 0);
                objDB.AddInParameter(objcmd, "from", DbType.DateTime, from);
                objDB.AddInParameter(objcmd, "To", DbType.DateTime, to);
                objDB.AddInParameter(objcmd, "StatusID", DbType.Int32, status);
                objDB.AddInParameter(objcmd, "FarmerID", DbType.Int32, FarmerID);
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
        public DataSet OrderWiseProduct(int orderid)//Change
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
                command.CommandText = "SP_GETORDERITEMS_BYORDERID";
                command.Parameters.AddWithValue("@OrderID", orderid);
                // command.Parameters.AddWithValue("@userid", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }


            connection.Close();
            return ds;
        }
        public int ComplaintRegister(long OrderID, int PackageID, int IssueDetailID, string Query, int CreatedBy)
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
                command.CommandText = "Usp_AddNewComplaint";

                command.Parameters.AddWithValue("@OrderID", OrderID);
                command.Parameters.AddWithValue("@PackageID", PackageID);
                command.Parameters.AddWithValue("@IssueDetailID", IssueDetailID);
                command.Parameters.AddWithValue("@Query", Query);
                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
              

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;

        }
        public DataSet GetFarmerComplaint(int OrderID,int FarmerId)
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
                command.CommandText = "Usp_GetFarmerComplaint";
                command.Parameters.AddWithValue("@pageNo", 0);
                command.Parameters.AddWithValue("@pageSize", 50);
                command.Parameters.AddWithValue("@search", "");
                command.Parameters.AddWithValue("@Mode", "APP");
                command.Parameters.AddWithValue("@OrderID", OrderID);
                command.Parameters.AddWithValue("@FarmerId", FarmerId);
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

        public int AgentOrderConfirmation(int userid, string Remark,int statusID,string DeliveryDate, int OrderID, int CancelReasonID,  long Mobile, int StateId, int DistrictId, int BlockId,
       int VillageId, string OtherVillageName, string Address, string ModeOfPayment, DataTable DT)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();
            int flag = 0;
            //string flag = "";
            var vDeliverDate = Convert.ToDateTime(Convert.ToDateTime(DeliveryDate).ToString("yyyy-MM-dd 00:00:00.000"));
            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Usp_AgentOrderConfirmation";
                command.Parameters.AddWithValue("@mobile", Mobile);
                command.Parameters.AddWithValue("@stateid", StateId);
                command.Parameters.AddWithValue("@districtid", DistrictId);
                command.Parameters.AddWithValue("@blockid", BlockId);
                command.Parameters.AddWithValue("@villageid", VillageId);
                command.Parameters.AddWithValue("@othervillagename", OtherVillageName);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@ModeOfPayment", ModeOfPayment);
                command.Parameters.AddWithValue("@OrderID", OrderID);
                command.Parameters.AddWithValue("@statusId", statusID);
                command.Parameters.AddWithValue("@UserID", userid);
                command.Parameters.AddWithValue("@Remark", Remark);
                command.Parameters.AddWithValue("@CancelReasonID",Convert.ToInt16(CancelReasonID));
                command.Parameters.AddWithValue("@DeliveryDate", vDeliverDate);
                command.Parameters.AddWithValue("@Product", DT);

                SqlParameter Error = new SqlParameter();
                Error.ParameterName = "@Error";
                Error.DbType = DbType.Int32;
                Error.Direction = ParameterDirection.Output;
                command.Parameters.Add(Error);


                flag = command.ExecuteNonQuery();
                //if (flag > 0)
                //{
                    string GetError = command.Parameters["@Error"].Value.ToString();
                    flag = int.Parse(GetError);
               // }
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }

        public int SaveCallLog(int userid, string Remark, int callStatusID, int type, string appointmentDate, string MobileNo)
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
                command.CommandText = "USP_SAVE_CALLLOG";
                command.Parameters.AddWithValue("@callStatusID", callStatusID);
                command.Parameters.AddWithValue("@Remark", Remark);
                command.Parameters.AddWithValue("@mobNo", MobileNo);
                command.Parameters.AddWithValue("@appointmentDate", appointmentDate);
                command.Parameters.AddWithValue("@CreatedBy", userid);
                command.Parameters.AddWithValue("@Addtype", type);
               


                flag = command.ExecuteNonQuery();     
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }

        public int FarmerData(int userid, string Mobile, int StateId, int DistrictId, int BlockId,
        int VillageId, string OtherVillageName, string Address, string RefrenceSource, string FarmerName, string FatherName)
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
                command.CommandText = "Usp_InsertFarmer_Agent";
                command.Parameters.AddWithValue("@Mobile", Mobile);
                command.Parameters.AddWithValue("@UserID", userid);
                command.Parameters.AddWithValue("@Name", FarmerName);
                command.Parameters.AddWithValue("@Fathername", FatherName);
                command.Parameters.AddWithValue("@stateid", StateId);
                command.Parameters.AddWithValue("@districtid", DistrictId);
                command.Parameters.AddWithValue("@blockid", BlockId);
                command.Parameters.AddWithValue("@villageid", VillageId);
                command.Parameters.AddWithValue("@othervillagename", OtherVillageName);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@RefrenceSource", RefrenceSource);

                SqlParameter Error = new SqlParameter();
                Error.ParameterName = "@Error";
                Error.DbType = DbType.Int32;
                Error.Direction = ParameterDirection.Output;
                command.Parameters.Add(Error);


                flag = command.ExecuteNonQuery();
               
                    string GetError = command.Parameters["@Error"].Value.ToString();
                    flag = int.Parse(GetError);
               
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }

            connection.Close();
            return flag;
        }

        public int BlacklistFarmer(string MobileNo, int UserID, string Remark)
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
                command.CommandText = "SP_Insert_BlackListUser";
                command.Parameters.AddWithValue("@MobileNo", MobileNo);
                command.Parameters.AddWithValue("@Remark", Remark);
                command.Parameters.AddWithValue("@CreatedBy", UserID);
             
                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, UserID); }

            connection.Close();
            return flag;
        }
        public int AgreementDetails(string CompanyName, string EnterpenueName, string MobileNo, string AltMobile, string GST, string PAN, string ShopAddress, int StateId, int DistrictId, string Pincode)
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
                command.CommandText = "SP_Insert_AgreementDetails";
                command.Parameters.AddWithValue("@CompanyName", CompanyName);
                command.Parameters.AddWithValue("@EnterpenueName", EnterpenueName);
                command.Parameters.AddWithValue("@MobileNo", MobileNo);
                command.Parameters.AddWithValue("@GST", GST);
                command.Parameters.AddWithValue("@PAN", PAN);
                command.Parameters.AddWithValue("@ShopAddress", ShopAddress);
                command.Parameters.AddWithValue("@StateId", StateId);
                command.Parameters.AddWithValue("@DistrictId", DistrictId);
                command.Parameters.AddWithValue("@Pincode", Pincode);

                //   flag = command.ExecuteNonQuery();
                flag = Convert.ToInt32(command.ExecuteScalar());
                command.Parameters.Clear();

            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;
        }
        public int AvisCallLog(string EventName, string ANI, string DNIS, string Mode, string CallId, string UserLogin, string Campaign, string LeadId, string Skill, string dnisIB, string CallFileName,string CallDisposition)
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
                command.CommandText = "SP_Insert_AvisCallLog";
                command.Parameters.AddWithValue("@EventName", EventName);
                command.Parameters.AddWithValue("@ANI", ANI);
                command.Parameters.AddWithValue("@DNIS", DNIS);
                command.Parameters.AddWithValue("@Mode", Mode);
                command.Parameters.AddWithValue("@CallId", CallId);
                command.Parameters.AddWithValue("@UserLogin", UserLogin);
                command.Parameters.AddWithValue("@Campaign", Campaign);
                command.Parameters.AddWithValue("@LeadId", LeadId);
                command.Parameters.AddWithValue("@Skill", Skill);
                command.Parameters.AddWithValue("@dnisIB", dnisIB);
                command.Parameters.AddWithValue("@CallFileName", CallFileName);
                command.Parameters.AddWithValue("@CallDisposition", CallDisposition);
                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
              
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;
        }
        public int Ozontel_CallLog_Insert(string AgentPhoneNumber, string Disposition, string CallerConfAudioFile, string TransferredTo, string Apikey, string Did
           , string StartTime, string CallDuration, string EndTime, string ConfDuration, string CustomerStatus, string TimeToAnswer
           , string monitorUCID, string AgentID, string AgentStatus, string Location, string FallBackRule, string CampaignStatus
           , string CallerID, string Duration, string Status, string AgentUniqueID, string UserName, string HangupBy
           , string AudioFile, string PhoneName, string TransferType, string DialStatus, string CampaignName, string UUI
           , string AgentName, string Skill, string DialedNumber, string Type, string Comments)
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
                command.CommandText = "Usp_Ozontel_CallLog_Insert";
                command.Parameters.AddWithValue("@AgentPhoneNumber", AgentPhoneNumber);
                command.Parameters.AddWithValue("@Disposition", Disposition);
                command.Parameters.AddWithValue("@CallerConfAudioFile", CallerConfAudioFile);
                command.Parameters.AddWithValue("@TransferredTo", TransferredTo);
                command.Parameters.AddWithValue("@Apikey", Apikey);
                command.Parameters.AddWithValue("@Did", Did);
                command.Parameters.AddWithValue("@StartTime", StartTime);
                command.Parameters.AddWithValue("@CallDuration", CallDuration);
                command.Parameters.AddWithValue("@EndTime", EndTime);
                command.Parameters.AddWithValue("@ConfDuration", ConfDuration);
                command.Parameters.AddWithValue("@CustomerStatus", CustomerStatus);
                command.Parameters.AddWithValue("@TimeToAnswer", TimeToAnswer);
                command.Parameters.AddWithValue("@monitorUCID", monitorUCID);
                command.Parameters.AddWithValue("@AgentID", AgentID);
                command.Parameters.AddWithValue("@AgentStatus", AgentStatus);
                command.Parameters.AddWithValue("@Location", Location);
                command.Parameters.AddWithValue("@FallBackRule", FallBackRule);
                command.Parameters.AddWithValue("@CampaignStatus", CampaignStatus);
                command.Parameters.AddWithValue("@CallerID", CallerID);
                command.Parameters.AddWithValue("@Duration", Duration);
                command.Parameters.AddWithValue("@Status", Status);
                command.Parameters.AddWithValue("@AgentUniqueID", AgentUniqueID);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@HangupBy", HangupBy);
                command.Parameters.AddWithValue("@AudioFile", AudioFile);
                command.Parameters.AddWithValue("@PhoneName", PhoneName);
                command.Parameters.AddWithValue("@TransferType", TransferType);
                command.Parameters.AddWithValue("@DialStatus", DialStatus);
                command.Parameters.AddWithValue("@CampaignName", CampaignName);
                command.Parameters.AddWithValue("@UUI", UUI);
                command.Parameters.AddWithValue("@AgentName", AgentName);
                command.Parameters.AddWithValue("@Skill", Skill);
                command.Parameters.AddWithValue("@DialedNumber", DialedNumber);
                command.Parameters.AddWithValue("@Type", Type);
                command.Parameters.AddWithValue("@Comments", Comments);


                flag = command.ExecuteNonQuery();
                LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0); }

            connection.Close();
            return flag;
        }

        public DataSet Ozontel_CallLog_Select(int userid)
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
                command.CommandText = "Usp_Ozontel_CallLog_Select";
                command.Parameters.AddWithValue("@UserId", userid);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message); }


            connection.Close();
            return ds;
        }
        public int UploadPhoto(int userid, string Lat, string Lang, string path)
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
                command.CommandText = "Usp_InsertAgreementPhoto";

                command.Parameters.AddWithValue("@userId", userid);
                command.Parameters.AddWithValue("@Lat", Lat);
                command.Parameters.AddWithValue("@Lang", Lang);
                command.Parameters.AddWithValue("@path", path);

                flag = command.ExecuteNonQuery();
                command.Parameters.Clear();
              //  LogDal.MethodCallLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex) { LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, userid); }


            connection.Close();
            return flag;
        }

        public DataSet GetPaymentRequestParam()
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
                command.CommandText = "SP_Get_PaymentParam";
              

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
    }
}
