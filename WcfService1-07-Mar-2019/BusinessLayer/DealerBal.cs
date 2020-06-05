using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using DataLayer;

namespace BusinessLayer
{
    public class DealerBal
    {

        DealerDal _dealerDal;
        public DealerBal()
        {
            _dealerDal = new DealerDal();
        }
        public int Add_Product_Dealer_Data_Temp(Product_Add_Dealer_Temp obj)
        {
          //  int createdby = 0;
            DataTable DT = Helper.Helper.ToDataTable(obj.Packages);
            DataSet ds = new DataSet();
            ds.Tables.Add(DT);
            string PackageXml = ds.GetXml();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    createdby = Convert.ToInt32(ds.Tables[0].Rows[0]["dealerID"]);
            //    obj.CreatedBy = createdby;
            //}
            int flag = _dealerDal.Add_Product_Dealer_Data_Temp(obj, PackageXml);
            return flag;
        }

        public int Add_Product_Dealer_Data_MAster(Product_Add_Dealer_Temp obj)
        {
            //  int createdby = 0;
            //DataTable DT = Helper.Helper.ToDataTable(obj.Packages);
            //DataSet ds = new DataSet();
            //ds.Tables.Add(DT);
            //string PackageXml = ds.GetXml();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    createdby = Convert.ToInt32(ds.Tables[0].Rows[0]["dealerID"]);
            //    obj.CreatedBy = createdby;
            //}
            int flag = _dealerDal.Add_Product_Dealer_Data_Master(obj);
            return flag;
        }


        public int GetUser_for_dealer( int DealerID, ref string DealerName)
        {
            int iUserid = 0;
            string rolename = "";
            DataSet ds = _dealerDal.GetUser_for_dealer(DealerID);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    iUserid = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
                DealerName = Convert.ToString(ds.Tables[0].Rows[0]["DealerName"]);
            }

            return iUserid;

        }

        public List<int> GetDealerID(List<Prod_Package_Add_dealer> objdealer)
        {

           // List<int> iDealerid = new List<int>();
            var v = (from o in objdealer
                    select o.dealerID).ToList();
           
            return v;

        }

        public List<Prod_Dealer_Requested_product> Get_Dealer_Requested_product(int dealerid, string status,bool Ismaster = false, int? stateid = 0, int? districtId = 0, int? blockID = 0, int? category = 0, int? subcategory = 0, int? techid = 0, int? companyid = 0, int? brandid = 0)
        {
            DataSet ds = new DataSet ();
            if(Ismaster)
                 ds = _dealerDal.Get_Dealer_Accepted_product(dealerid, status, stateid, districtId, blockID, category, subcategory, techid, companyid, brandid);
            else
                 ds = _dealerDal.Get_Dealer_Requested_product(dealerid, status,stateid,districtId,blockID,category,subcategory,techid,companyid,brandid);


            List<Prod_Dealer_Requested_product> objresult = new List<Prod_Dealer_Requested_product>();

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        var v = (from p in ds.Tables[0].AsEnumerable()
                                 select new Prod_Dealer_Requested_product
                                 {
                                     productID = p.Field<int>("ProductID"),
                                     productName = p.Field<string>("ProductName"),
                                     productType = p.Field<int>("ProductType"),
                                     Description = p.Field<string>("Description"),

                                     DosageDescription = p.Field<string>("DosageDescription"),
                                     packetName = p.Field<string>("PckName"),
                                     pckTypeId = p.Field<int>("PckTypeID"),
                                     ProStateName = p.Field<string>("ProStateName"),
                                     ProductState = p.Field<int>("ProductState"),
                                     brandName = p.Field<string>("BrandName"),
                                     brandId = p.Field<int>("BrandID"),
                                     categoryName = p.Field<string>("CategoryName"),
                                     categoryId = p.Field<int>("CategoryID"),
                                     subCategoryName = p.Field<string>("SubCategoryName"),
                                     subCategoryID = p.Field<int>("SubCategoryID"),
                                     size = p.Field<decimal>("size"),
                                     dealerID = p.Field<int>("DealerID"),
                                     dealerName = p.Field<string>("DealerName"),
                                     dealerPrice = p.Field<decimal>("DealerPrice"),
                                     previousDealerPrice = p.Field<decimal> ("DealerPreviousPrice"),
                                     isActive = p.Field<bool>("IsActive"),
                                     mrp = p.Field<decimal>("MRP"),
                                     qty = p.Field<int>("Qty"),
                                     companyName = p.Field<string>("OrganisationName"),
                                     companyId = p.Field<int>("CompanyID"),
                                     districtId = p.Field<int>("districtid"),
                                     districtName = p.Field<string>("DistrictName"),
                                     packageID = p.Field<int>("PackageID"),
                                     remarks = p.Field<string>("Remarks"),
                                     status = p.Field<int>("Status"),
                                     technicalName = p.Field<string>("TechnicalName"),
                                     technicalId = p.Field<int>("TechNameID"),
                                     unitName = p.Field<string>("UnitName"),
                                     unitId = p.Field<int>("UnitID"),
                                     ourPrice = p.Field<decimal>("OurPrice"),
                                     otherCharges = p.Field<decimal>("OtherCharges"),
                                     applyOnCrop = p.Field<string>("ApplyOnCrop"),
                                     createdBy = p.Field<int>("CreateBy"),
                                     createdDate =Convert.ToString(p.Field<DateTime>("CreatedDate")),
                                     cropID = p.Field<int>("CropID"),
                                     Disease = p.Field<string>("Disease"),
                                     FeedState = p.Field<int>("FeedState"),
                                     FeedType = p.Field<int>("FeedType"),
                                     packageTypeId = p.Field<int>("PckTypeID"),
                                     QualityId = p.Field<int>("QualityID"),
                                     target = p.Field<string>("Target"),
                                     otherBrandName = p.Field<string>("otherBrandName"),
                                     otherCompanyName = p.Field<string>("OtherCompanyName"),
                                     otherTechnicalName = p.Field<string>("otherTechnicalName"),
                                 }
                                 ).ToList();

                        objresult = v;

                    }


                }

            }
            return objresult;

        }
        public int Activate_Delaer_product_package(int productid, int packageid, string remarks, int createdby)
        {

            int flag = _dealerDal.Activate_Delaer_product_package(productid, packageid, remarks, createdby);
            return flag;
        }

        public List<Prod_Product_Package> Get_Product_Package(int productid,int Ismaster)
        {
            DataSet ds = _dealerDal.Get_Product_Package(productid, Ismaster);
           List<Prod_Product_Package> obj = new List<Prod_Product_Package>();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var v = (from p in ds.Tables[0].AsEnumerable()
                             select new Prod_Product_Package
                             {
                                 productID = p.Field<int>("productid"),
                                 packageID = p.Field<int>("PackageID"),
                                 DealerName = p.Field<string>("DealerName"),
                                 Userid = p.Field<int>("Userid")
                             }
                             ).ToList();

                    obj = v;
                }
               
            }

            return obj;

        }

        public int Update_Product_Package_Data_Temp(Prod_Dealer_Requested_product obj)
        {

            int flag = _dealerDal.Update_Product_Package_Data_Temp(obj);
            return flag;
        }
        public int Update_Product_Package_Data_Master(Prod_Dealer_Requested_product obj)
        {

            int flag = _dealerDal.Update_Product_Package_Data_Master(obj);
            return flag;
        }


        public List<GenericModel> Get_Nutrient()
        {

            DataSet ds = _dealerDal.Get_Nutrient();
            List<GenericModel> objresult = new List<GenericModel>();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        GenericModel gm = new GenericModel()
                        {
                            Id = Convert.ToInt32(dr["NutrientID"]),
                            Name = Convert.ToString(dr["NutrientName"])

                        };
                        objresult.Add(gm);
                    }
                }

            }
            return objresult;
        }

        public List<GenericModel> Get_Nutrient_Unit()
        {

            DataSet ds = _dealerDal.Get_Nutrient_Unit();
            List<GenericModel> objresult = new List<GenericModel>();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        GenericModel gm = new GenericModel()
                        {
                            Id = Convert.ToInt32(dr["UnitID"]),
                            Name = Convert.ToString(dr["UnitName"])

                        };
                        objresult.Add(gm);
                    }
                }
            }
            return objresult;
        }

        public List<GenericModel> GET_USE_FOR_LIST(string mode, int categoryId)
        {

            DataSet ds = _dealerDal.Get_ProductList_useFor(mode,Convert.ToString(categoryId));
            List<GenericModel> objresult = new List<GenericModel>();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        GenericModel gm = new GenericModel()
                        {
                            Id = Convert.ToInt32(dr["vl"]),
                            Name = Convert.ToString(dr["nm"])

                        };
                        objresult.Add(gm);
                    }
                }
            }
            return objresult;
        }


        public int Get_Role(int userid, ref string roleName)
        {
            int iRoleID = 0;
            string rolename = "";
            DataSet ds = _dealerDal.Get_Role(userid);

            if (ds != null)
            {
                if(ds.Tables[0].Rows.Count > 0)
                iRoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"]);
                roleName = Convert.ToString(ds.Tables[0].Rows[0]["role_name"]);
            }

            return iRoleID;

        }

        public int GetUserid_for_Product(int productid, ref string productname,bool Ismaster = false)
        {
            int iUserID = 0;
            string rolename = "";
            DataSet ds = _dealerDal.GetUserid_for_Product(productid,Ismaster); 

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    iUserID = Convert.ToInt32(ds.Tables[0].Rows[0]["USerid"]);
                    productname = Convert.ToString(ds.Tables[0].Rows[0]["ProductName"]);
                }
            }

            return iUserID;

        }

       
        public int Add_Nutrient_value(List<NutrientValue> nval, int subcategoryid)
        {
            int iresult = 0;

            iresult = _dealerDal.Add_Nutrient_value(nval, subcategoryid);

            return iresult;
        }

        public int Add_Demand_Product(int tid, int cid, int subid, string pname, string remark, int uid)
        {
            int iresult = 0;

            iresult = _dealerDal.Add_Demand_Product(tid,cid,subid,pname,remark,uid);

            return iresult;
        }

        public Prod_Product_Detail_Dealer Get_Product_Detail_Dealer(int productId,int dealerid = 0,bool Ismaster = false)
        {
            DataSet DS = new DataSet();
            if(Ismaster)
            DS = _dealerDal.Get_Product_Detail_Dealer_master(productId, dealerid);
            else
                DS = _dealerDal.Get_Product_Detail_Dealer(productId);

            Prod_Product_Detail_Dealer _viewModel = new Prod_Product_Detail_Dealer();

            if (DS != null && DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    var _listModel = (from p in DS.Tables[0].AsEnumerable()
                                      select new Prod_Product_Detail_Dealer
                                      {
                                          ProductId = p.Field<int>("ProductID"),
                                          ProductName = p.Field<string>("ProductName"),
                                          Dosage = p.Field<string>("DosageDescription"),
                                          ProductDesc = p.Field<string>("Description"),
                                          Category = new GenericModel { Id = p.Field<int>("CategoryID"), Name = p.Field<string>("CategoryName") },
                                          SubCategory = new GenericModel { Id = p.Field<int>("SubCategoryID"), Name = p.Field<string>("SubCategoryName") },
                                          TechnicalName = new GenericModel { Id = p.Field<int>("TechNameID"), Name = p.Field<string>("TechnicalName") },
                                          Brand = new GenericModel { Id = p.Field<int>("BrandID"), Name = p.Field<string>("BrandName") },
                                          Company = new GenericModel { Id = p.Field<int>("CompanyID"), Name = p.Field<string>("OrganisationName") },
                                          PackageType = new GenericModel { Id = p.Field<int>("PckTypeID"), Name = p.Field<string>("PckName") },
                                          Quality = new GenericModel { Id = p.Field<int>("QualityID"), Name = p.Field<string>("QualityName") },
                                          State = new GenericModel { Id = p.Field<int>("ProStateID"), Name = p.Field<string>("ProStateName") },
                                          Crop = new GenericModel { Id = p.Field<int>("CropID"), Name = p.Field<string>("CropName") },
                                          ProductType = new GenericModel { Id = p.Field<int>("ProductTypeID"), Name = p.Field<string>("ProductType") },
                                          otherBrandName = p.Field<string>("OtherBrandName"),
                                          otherCompanyName = p.Field<string>("OtherCompanyName"),
                                          otherTechnicalName = p.Field<string>("OtherTechnicalName")
                                      }).FirstOrDefault();

                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        var pkglist = (from q in DS.Tables[1].AsEnumerable()
                                       select new Prod_Package_Add_dealer
                                       {
                                           dealerID = q.Field<int>("DealerID"),
                                           dealerPrice = q.Field<decimal>("DealerPrice"),
                                           isActive = q.Field<int>("IsActive"),
                                           mrp  = q.Field<decimal>("mrp"),
                                           ourPrice = q.Field<decimal>("OurPrice"),
                                           othercharges = q.Field<decimal>("Othercharges"),
                                           qty = q.Field<int>("Qty"),
                                           size = q.Field<decimal>("size"),
                                           unitID = q.Field<int>("UnitID"),
                                           packageId = q.Field<int>("PackageID")
                                         
                                       }).ToList();
                        _listModel.Packages = pkglist;

                    }

                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        var Productuserforlist = (from q in DS.Tables[2].AsEnumerable()
                                                  select new DealerTbl_ProductUseFor
                                                  {
                                                      Category = q.Field<int>("Category"),
                                                      CreateBy = q.Field<int>("CreateBy"),
                                                      CreatedDate = q.Field<DateTime?>("CreatedDate").HasValue ? q.Field<DateTime>("CreatedDate").ToString("dd/MM/yyyy hh:mm:ss") : "",
                                                      Erange = q.Field<int>("Erange"),
                                                      Id = q.Field<int>("Id"),
                                                      IsActive = q.Field<int>("IsActive"),
                                                      ModifiedBy = q.Field<int>("ModifiedBy"),
                                                      ModifiedDate = q.Field<DateTime?>("ModifiedDate").HasValue ? q.Field<DateTime>("ModifiedDate").ToString("dd/MM/yyyy hh:mm:ss") : "",
                                                      RangeUnit = q.Field<int>("RangeUnit"),
                                                      Remark = q.Field<string>("Remark"),
                                                      Srange = q.Field<int>("Srange"),
                                                      UseFor = q.Field<int>("UseFor"),
                                                      status = q.Field<int>("Status")
                                                  }).ToList();
                        _listModel.ProductUserFor = Productuserforlist;

                    }

                    _viewModel = _listModel;
                }
            }

            return _viewModel;
        }
    }
}