using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Entity;
using System.Collections;

namespace DataLayer
{
   public class DealerDal
    {
        public int Add_Product_Dealer_Data_Temp(Product_Add_Dealer_Temp objProducttemp, string packageData)
        {
            string sql = "";
            DataSet ds = new DataSet();
            int iResult = 0;

            try
            {
               
                if(objProducttemp.CreatedBy > 0)
                objProducttemp.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                if(objProducttemp.ModifiedBy >0)
                objProducttemp.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                sql = StoredProcedure.Add_Product_Package_Data_Temp;
                SqlParameter[] parameters = new SqlParameter[31];
                parameters[0] = new SqlParameter("@OldProductId", objProducttemp.ProductId);
                parameters[1] = new SqlParameter("@ProductType", objProducttemp.ProductType);
                parameters[2] = new SqlParameter("@productName", objProducttemp.ProductName);
                parameters[3] = new SqlParameter("@TechnicalId", objProducttemp.TechnicalID);
                parameters[4] = new SqlParameter("@BrandId", objProducttemp.BrandID);
                parameters[5] = new SqlParameter("@ProductStateId", objProducttemp.StateID);
                parameters[6] = new SqlParameter("@Dosage", objProducttemp.DosageID);
                parameters[7] = new SqlParameter("@ApplyOnCrop", objProducttemp.ApplyOnCrop);
                parameters[8] = new SqlParameter("@Target", objProducttemp.Target);
                parameters[9] = new SqlParameter("@ProductDesc", objProducttemp.ProductDesc);
                parameters[10] = new SqlParameter("@Remark", objProducttemp.Remark);
                parameters[11] = new SqlParameter("@CreateBy", objProducttemp.CreatedBy);
                if (objProducttemp.CreatedBy > 0)
                    parameters[12] = new SqlParameter("@CreatedDate", objProducttemp.CreatedDate);
                parameters[13] = new SqlParameter("@ModifiedBy", objProducttemp.ModifiedBy);
                if (objProducttemp.ModifiedBy > 0)
                    parameters[14] = new SqlParameter("@ModifiedDate", objProducttemp.ModifiedDate);
                parameters[15] = new SqlParameter("@PackageTypeId", objProducttemp.PackageTypeID);
                parameters[16] = new SqlParameter("@QualityId", objProducttemp.QualityID);
                parameters[17] = new SqlParameter("@CropID", objProducttemp.CropID);
                parameters[18] = new SqlParameter("@Disease", objProducttemp.Disease);
                parameters[19] = new SqlParameter("@FeedType", objProducttemp.FeedType);
                parameters[20] = new SqlParameter("@FeedState", objProducttemp.FeedState);
                parameters[21] = new SqlParameter("@status", objProducttemp.status);
                parameters[22] = new SqlParameter("@Technical_Name", objProducttemp.otherTechnicalName);
                if (!string.IsNullOrEmpty(objProducttemp.otherCompanyName.Trim()))
                parameters[23] = new SqlParameter("@Company_Name", objProducttemp.otherCompanyName);
                parameters[24] = new SqlParameter("@Brand_Name", objProducttemp.otherBrandName);
                parameters[25] = new SqlParameter("@Packagedata", packageData);
                parameters[26] = new SqlParameter("@ProductId", 0);
                parameters[26].Direction = ParameterDirection.Output;
                parameters[27] = new SqlParameter("@IsActive", objProducttemp.IsActive);
                parameters[28] = new SqlParameter("@SubcategoryId", objProducttemp.SubCategoryID);
                parameters[29] = new SqlParameter("@CategoryID", objProducttemp.CategoryID);
                if(string.IsNullOrEmpty(objProducttemp.otherCompanyName.Trim()))
                parameters[30] = new SqlParameter("@Company_Name", "ID-" +  objProducttemp.CompanyID);


                iResult = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                iResult = Convert.ToInt32(parameters[26].Value);
                if (iResult > 0)
                {
                    if (objProducttemp.ProductUserFor != null)
                    {
                        //  var objproductuserfor = obj.ProductUserFor.FirstOrDefault();
                        foreach (DealerTbl_ProductUseFor objproductuserfor in objProducttemp.ProductUserFor)
                        {
                            if (objproductuserfor.UseFor > 0)
                            {
                                sql = StoredProcedure.Add_Update_DealerProductUseFor;
                                SqlParameter[] parameter = new SqlParameter[13];
                                parameter[0] = new SqlParameter("@ProductId", objProducttemp.ProductId > 0 ? objProducttemp.ProductId : iResult);
                                parameter[1] = new SqlParameter("@UseFor", objproductuserfor.UseFor);
                                parameter[2] = new SqlParameter("@Category", objproductuserfor.Category);
                                parameter[3] = new SqlParameter("@Srange", objproductuserfor.Srange);
                                parameter[4] = new SqlParameter("@Erange", objproductuserfor.Erange);
                                parameter[5] = new SqlParameter("@RangeUnit", objproductuserfor.RangeUnit);
                                parameter[6] = new SqlParameter("@Remark", objproductuserfor.Remark);
                                parameter[7] = new SqlParameter("@CreateBy", objproductuserfor.CreateBy);
                                parameter[8] = new SqlParameter("@ModifiedBy", objproductuserfor.ModifiedBy);
                                parameter[10] = new SqlParameter("@Id", objproductuserfor.Id);
                                parameter[11] = new SqlParameter("@Isactive", objproductuserfor.IsActive);
                                parameter[12] = new SqlParameter("@status", objproductuserfor.status);
                                int i = BaseDal.ExecuteNonQuerywithSP(sql, parameter);
                            }
                        }
                    }

                    if (objProducttemp.otherCompoundCattleFeed != null)
                    {
                        Add_Nutrient_value(objProducttemp.otherCompoundCattleFeed, objProducttemp.SubCategoryID, false, objProducttemp.ProductId > 0 ? objProducttemp.ProductId : iResult);
                    }
                }
                //if (iResult > 0)
                //  iResult = 1;
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return iResult;
        }

        public int Add_Product_Dealer_Data_Master(Product_Add_Dealer_Temp objProducttemp)
        {
            string sql = "";
            DataSet ds = new DataSet();
            int iResult = 0, ioutput = 0;
            try
            {
                if (objProducttemp.CreatedBy > 0)
                    objProducttemp.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                if (objProducttemp.ModifiedBy > 0)
                    objProducttemp.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

                sql = StoredProcedure.Add_Package_Dealer_master;
               
                if (objProducttemp.ProductId > 0)
                {
                    if (objProducttemp.Packages != null)
                    {
                        //  var objproductuserfor = obj.ProductUserFor.FirstOrDefault();
                        foreach (Prod_Package_Add_dealer objproductuserfor in objProducttemp.Packages)
                        {
                            if (objproductuserfor.dealerID > 0 && objproductuserfor.packageId == 0)
                            {
                                sql = StoredProcedure.Add_Package_Dealer_master;
                                SqlParameter[] parameter = new SqlParameter[14];
                                parameter[0] = new SqlParameter("@productid", objProducttemp.ProductId > 0 ? objProducttemp.ProductId : iResult);
                                parameter[1] = new SqlParameter("@dealerID", objproductuserfor.dealerID);
                                parameter[2] = new SqlParameter("@dealerPrice", objproductuserfor.dealerPrice);
                                parameter[3] = new SqlParameter("@isActive", objproductuserfor.isActive);
                                parameter[4] = new SqlParameter("@mrp", objproductuserfor.mrp);
                                parameter[5] = new SqlParameter("@othercharges", objproductuserfor.othercharges);
                                parameter[6] = new SqlParameter("@packageId", objproductuserfor.packageId);
                                parameter[7] = new SqlParameter("@qty", objproductuserfor.qty);
                                parameter[8] = new SqlParameter("@size", objproductuserfor.size);
                                parameter[10] = new SqlParameter("@unitID", objproductuserfor.unitID);
                                parameter[11] = new SqlParameter("@createdby", objProducttemp.CreatedBy);
                                parameter[12] = new SqlParameter("@ourPrice", objproductuserfor.ourPrice);
                                parameter[13] = new SqlParameter("@IsPackageAdded", 0);
                                parameter[13].Direction = ParameterDirection.Output;
                                int i = BaseDal.ExecuteNonQuerywithSP(sql, parameter);
                                 ioutput = Convert.ToInt32(parameter[13].Value);
                            }
                        }
                    }
                }
                if (ioutput > 0)
                  iResult = 1;
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return iResult;
        }



        public DataSet Get_Dealer_Requested_product(int dealerID,string status, int? stateid =0, int? districtId=0,int? blockID=0, int? category=0, int? subcategory=0, int? techid=0, int? companyid =0, int? brandid = 0)
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.Get_dealer_requested_product;
                SqlParameter[] parameters = new SqlParameter[10];
              //  if(dealerID >0)
                parameters[0] = new SqlParameter("@DealerId", dealerID);
                parameters[1] = new SqlParameter("@status", status);
                parameters[2] = new SqlParameter("@StateID", stateid);
                parameters[3] = new SqlParameter("@DistrictID", districtId);
                parameters[4] = new SqlParameter("@BlockID", blockID);
                parameters[5] = new SqlParameter("@CategoryID", category);
                parameters[6] = new SqlParameter("@SubCategoryID", subcategory);
                parameters[7] = new SqlParameter("@TechnicalNameID", techid);
                parameters[8] = new SqlParameter("@CompanyID", companyid);
                parameters[9] = new SqlParameter("@BrandID", brandid);

                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet Get_Dealer_Accepted_product(int dealerID, string status, int? stateid = 0, int? districtId = 0, int? blockID = 0, int? category = 0, int? subcategory = 0, int? techid = 0, int? companyid = 0, int? brandid = 0)
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.ACCEPTED_PRODUCTLIST_Master;
                SqlParameter[] parameters = new SqlParameter[10];
                if (dealerID > 0)
                    parameters[0] = new SqlParameter("@DealerId", dealerID);
                parameters[1] = new SqlParameter("@status", status);
                parameters[2] = new SqlParameter("@StateID", stateid);
                parameters[3] = new SqlParameter("@DistrictID", districtId);
                parameters[4] = new SqlParameter("@BlockID", blockID);
                parameters[5] = new SqlParameter("@CategoryID", category);
                parameters[6] = new SqlParameter("@SubCategoryID", subcategory);
                parameters[7] = new SqlParameter("@TechnicalNameID", techid);
                parameters[8] = new SqlParameter("@CompanyID", companyid);
                parameters[9] = new SqlParameter("@BrandID", brandid);

                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }


        public DataSet Get_Nutrient(string mode = "")
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.Get_Nutrient;
                SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MODE", mode);
              
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet Get_Nutrient_Unit(string mode="")
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.Get_Nutrient_Unit;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@MODE", mode);

                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet Get_ProductList_useFor(string mode, string CategoryId)
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.Prod_UseForList;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@mode", mode);
                parameters[1] = new SqlParameter("@CategoryID", CategoryId);
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }


        public DataSet GetUser_for_dealer(int DealerID)
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.GetUser_for_dealer;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Dealerid", DealerID);
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet Get_Role (int userid)
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.SPGetRole;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@UserID", userid);
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet GetUserid_for_Product(int productid, bool Ismaster = false)
        {
            string sql = "";
            DataSet ds = new DataSet();
            try
            {
                sql = StoredProcedure.GetUserid_for_Product;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@ProductId", productid);
                parameters[1] = new SqlParameter("@mastertable", Ismaster);

                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public int Add_Demand_Product(int tid, int cid, int subid, string pname, string remark, int uid)
        {
            string sql = "";
            int i = 0;
            try
            {
                sql = StoredProcedure.Add_Demand_Product;
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@TechnicalId", tid);
                parameters[1] = new SqlParameter("@CategoryId", cid);
                parameters[2] = new SqlParameter("@SubcategoryId", subid);
                parameters[3] = new SqlParameter("@ProductName", pname);
                parameters[4] = new SqlParameter("@Remarks", remark);
                parameters[5] = new SqlParameter("@CreatedBy", uid);


                 i = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                if (i > 0)
                    i = 1;
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return i; ;
        }
  

        public int Add_Nutrient_value(List<NutrientValue> nval, int subcategoryid,bool Ismaster = true,int productid= 0)
        {

            var dt = new DataTable();
            dt.Columns.Add("NutrientID", type: typeof(short));
            dt.Columns.Add("Amount", type: typeof(decimal));
            dt.Columns.Add("Unit", type: typeof(byte));

            foreach (var i in nval)
            {
                dt.Rows.Add(i.Nutrient.Id, i.Amount, i.Unit.Id);
            }

            string sql = "";
            DataSet ds = new DataSet();
            int iResult = 0;
            string result = "";

            try
            {
                if (Ismaster)
                {
                    sql = StoredProcedure.Add_NutrientValue_Techname;
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@Tbl_NutrientValue_Type", dt);
                    parameters[1] = new SqlParameter("@SubCategoryID", subcategoryid);
                    parameters[2] = new SqlParameter("@result", "");
                    parameters[2].Direction = ParameterDirection.Output;
                    iResult = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                    result = Convert.ToString(parameters[2].Value);

                }
                else {
                    sql = StoredProcedure.ADD_NUTRIENTVALUE_TECHNAME_Dealer;
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@Tbl_NutrientValue_Type", dt);
                    parameters[1] = new SqlParameter("@SubCategoryID", subcategoryid);
                    parameters[2] = new SqlParameter("@status", 0);
                    parameters[3] = new SqlParameter("@productID", productid);
                    parameters[4] = new SqlParameter("@result", "");
                    parameters[4].Direction = ParameterDirection.Output;
                    iResult = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                    result = Convert.ToString(parameters[4].Value);

                }
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            return iResult;
        }

        public int Activate_Delaer_product_package(int productid, int packageid,string remarks,int createdby)
        {
            string sql = "";
            DataSet ds = new DataSet();
            int iResult = 0;
            int iinsertcount = 0;

            try
            {
                sql = StoredProcedure.Activate_Dealer_Product;
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@ProductID", productid);
                parameters[1] = new SqlParameter("@PackageID", packageid);
                parameters[2] = new SqlParameter("@Remarks", remarks);
                parameters[3] = new SqlParameter("@Createdby", createdby);
                parameters[4] = new SqlParameter("@Record", 0);
                parameters[4].Direction = ParameterDirection.Output;


                iResult = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                iinsertcount = Convert.ToInt32(parameters[4].Value);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return iinsertcount;
        }

        public int Update_Product_Package_Data_Temp(Prod_Dealer_Requested_product objProducttemp)
        {
            string sql = "";
            DataSet ds = new DataSet();
            int iResult = 0;
            int iSuccess = 0;

            try
            {
                //if (objProducttemp.createdBy > 0)
                //    objProducttemp.createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
               

                sql = StoredProcedure.Update_Product_Package_Data_Temp;
                SqlParameter[] parameters = new SqlParameter[36];
                parameters[0] = new SqlParameter("@OldProductId", objProducttemp.productID);
                parameters[1] = new SqlParameter("@ProductType", objProducttemp.productType);
                parameters[2] = new SqlParameter("@productName", objProducttemp.productName);
                parameters[3] = new SqlParameter("@TechnicalId", objProducttemp.technicalId);
                parameters[4] = new SqlParameter("@BrandId", objProducttemp.brandId);
                parameters[5] = new SqlParameter("@ProductStateId", objProducttemp.ProductState);
                parameters[6] = new SqlParameter("@Dosage", objProducttemp.DosageDescription);
                parameters[7] = new SqlParameter("@ApplyOnCrop", objProducttemp.applyOnCrop);
                parameters[8] = new SqlParameter("@Target", objProducttemp.target);
                parameters[9] = new SqlParameter("@ProductDesc", objProducttemp.Description);
                parameters[10] = new SqlParameter("@Remark", objProducttemp.remarks);
                parameters[11] = new SqlParameter("@CreateBy", objProducttemp.createdBy);
                //if (objProducttemp.createdBy > 0)
                //    parameters[12] = new SqlParameter("@CreatedDate", objProducttemp.createdDate);
                //parameters[13] = new SqlParameter("@ModifiedBy", objProducttemp.createdBy);
                //if (objProducttemp.createdBy > 0)
                //    parameters[14] = new SqlParameter("@ModifiedDate", objProducttemp.createdDate);
                parameters[12] = new SqlParameter("@PackageTypeId", objProducttemp.packageTypeId);
                parameters[13] = new SqlParameter("@QualityId", objProducttemp.QualityId);
                parameters[14] = new SqlParameter("@CropID", objProducttemp.cropID);
                parameters[15] = new SqlParameter("@Disease", objProducttemp.Disease);
                parameters[16] = new SqlParameter("@FeedType", objProducttemp.FeedType);
                parameters[17] = new SqlParameter("@FeedState", objProducttemp.FeedState);
                parameters[18] = new SqlParameter("@status", objProducttemp.status);
                parameters[19] = new SqlParameter("@ProductId", 0);
                parameters[19].Direction = ParameterDirection.Output;
                parameters[20] = new SqlParameter("@size", objProducttemp.size);
                parameters[21] = new SqlParameter("@Unit", objProducttemp.unitId);
                parameters[22] = new SqlParameter("@MRP", objProducttemp.mrp);
                parameters[23] = new SqlParameter("@packageId", objProducttemp.packageID);
                parameters[24] = new SqlParameter("@DealerID", objProducttemp.dealerID);
                parameters[25] = new SqlParameter("@DealerPrice", objProducttemp.dealerPrice);
                parameters[26] = new SqlParameter("@othercharges", objProducttemp.otherCharges);
                parameters[27] = new SqlParameter("@ourPrice", objProducttemp.ourPrice);
                parameters[28] = new SqlParameter("@Qty", objProducttemp.qty);
                parameters[29] = new SqlParameter("@DistrictID", objProducttemp.districtId);
                parameters[30] = new SqlParameter("@IsActive", objProducttemp.isActive);
                parameters[31] = new SqlParameter("@SubCategoryId", objProducttemp.subCategoryID);
                parameters[32] = new SqlParameter("@CategoryID", objProducttemp.categoryId);
                parameters[33] = new SqlParameter("@Technical_Name", objProducttemp.otherTechnicalName);
                parameters[34] = new SqlParameter("@Company_Name", objProducttemp.otherCompanyName);
                parameters[35] = new SqlParameter("@Brand_Name", objProducttemp.otherBrandName);

                iResult = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                iSuccess = Convert.ToInt32(parameters[19].Value);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return iSuccess;
        }

        public int Update_Product_Package_Data_Master(Prod_Dealer_Requested_product objProducttemp)
        {
            string sql = "";
            DataSet ds = new DataSet();
            int iResult = 0;
            int iSuccess = 0;

            try
            {
                //if (objProducttemp.createdBy > 0)
                //    objProducttemp.createdDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));


                sql = StoredProcedure.Update_Product_Package_Data_Master;
                SqlParameter[] parameters = new SqlParameter[31];
                parameters[0] = new SqlParameter("@OldProductId", objProducttemp.productID);
                parameters[1] = new SqlParameter("@ProductType", objProducttemp.productType);
                parameters[2] = new SqlParameter("@productName", objProducttemp.productName);
                parameters[3] = new SqlParameter("@TechnicalId", objProducttemp.technicalId);
                parameters[4] = new SqlParameter("@BrandId", objProducttemp.brandId);
                parameters[5] = new SqlParameter("@ProductStateId", objProducttemp.ProductState);
                parameters[6] = new SqlParameter("@Dosage", objProducttemp.DosageDescription);
                parameters[7] = new SqlParameter("@ApplyOnCrop", objProducttemp.applyOnCrop);
                parameters[8] = new SqlParameter("@Target", objProducttemp.target);
                parameters[9] = new SqlParameter("@ProductDesc", objProducttemp.Description);
                parameters[10] = new SqlParameter("@Remark", objProducttemp.remarks);
                parameters[11] = new SqlParameter("@CreateBy", objProducttemp.createdBy);
                //if (objProducttemp.createdBy > 0)
                //    parameters[12] = new SqlParameter("@CreatedDate", objProducttemp.createdDate);
                //parameters[13] = new SqlParameter("@ModifiedBy", objProducttemp.createdBy);
                //if (objProducttemp.createdBy > 0)
                //    parameters[14] = new SqlParameter("@ModifiedDate", objProducttemp.createdDate);
                parameters[12] = new SqlParameter("@PackageTypeId", objProducttemp.packageTypeId);
                parameters[13] = new SqlParameter("@QualityId", objProducttemp.QualityId);
                parameters[14] = new SqlParameter("@CropID", objProducttemp.cropID);
                parameters[15] = new SqlParameter("@Disease", objProducttemp.Disease);
                parameters[16] = new SqlParameter("@FeedType", objProducttemp.FeedType);
                parameters[17] = new SqlParameter("@FeedState", objProducttemp.FeedState);
                parameters[18] = new SqlParameter("@status", objProducttemp.status);
                parameters[19] = new SqlParameter("@ProductId", 0);
                parameters[19].Direction = ParameterDirection.Output;
                parameters[20] = new SqlParameter("@size", objProducttemp.size);
                parameters[21] = new SqlParameter("@Unit", objProducttemp.unitId);
                parameters[22] = new SqlParameter("@MRP", objProducttemp.mrp);
                parameters[23] = new SqlParameter("@packageId", objProducttemp.packageID);
                parameters[24] = new SqlParameter("@DealerID", objProducttemp.dealerID);
                parameters[25] = new SqlParameter("@DealerPrice", objProducttemp.dealerPrice);
                parameters[26] = new SqlParameter("@othercharges", objProducttemp.otherCharges);
                parameters[27] = new SqlParameter("@ourPrice", objProducttemp.ourPrice);
                parameters[28] = new SqlParameter("@Qty", objProducttemp.qty);
                parameters[29] = new SqlParameter("@DistrictID", objProducttemp.districtId);
                parameters[30] = new SqlParameter("@IsActive", objProducttemp.isActive);
                iResult = BaseDal.ExecuteNonQuerywithSP(sql, parameters);
                iSuccess = Convert.ToInt32(parameters[19].Value);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return iSuccess;
        }

        public DataSet Get_Product_Detail_Dealer(int productId)
        {
            string sql = "";
            DataSet ds = new DataSet();

            try
            {
                sql = StoredProcedure.Get_Product_Detail_temp;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@productid", productId);
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet Get_Product_Detail_Dealer_master(int productId, int dealerid)
        {
            string sql = "";
            DataSet ds = new DataSet();

            try
            {
                sql = StoredProcedure.Get_Product_Dealer_Detail;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@productid", productId);
                parameters[1] = new SqlParameter("@dealerID", dealerid);
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }

        public DataSet Get_Product_Package(int productId,int Ismaster = 0)
        {
            string sql = "";
            DataSet ds = new DataSet();

            try
            {
                sql = StoredProcedure.Get_ProductId_Package;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@ProductId", productId);
                parameters[1] = new SqlParameter("@IsMaster", Ismaster);
                ds = BaseDal.ExecuteAdapter(sql, parameters);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }
            return ds;
        }




    }
}
