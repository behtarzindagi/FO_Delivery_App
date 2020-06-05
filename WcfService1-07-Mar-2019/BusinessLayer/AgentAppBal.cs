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
using System.Globalization;

namespace BusinessLayer
{
    public class AgentAppBal
    {
        AgentAppDal _Apdal = new AgentAppDal();
        public FarmerViewModel GetFarmerByFsc(string FscId, int Mode = 0)
        {
            DataSet ds = _Apdal.GetFarmerByFsc(FscId, Mode);

            FarmerViewModel a = new FarmerViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<FarmerModel> p = new List<FarmerModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FarmerModel Reason = new FarmerModel();
                        Reason.FarmerId = ds.Tables[0].Rows[i]["FarmerId"].ToString();
                        Reason.FarmerName = ds.Tables[0].Rows[i]["FarmerName"].ToString();
                        Reason.MobileNo = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        Reason.District = ds.Tables[0].Rows[i]["DistrictName"].ToString();
                        Reason.DistrictId = ds.Tables[0].Rows[i]["DistrictID"].ToString();
                        Reason.StateId = ds.Tables[0].Rows[i]["StateID"].ToString();
                        Reason.StatusID = Convert.ToInt32(ds.Tables[0].Rows[i]["BlackListStatus"].ToString());
                        Reason.CallStatus = ds.Tables[0].Rows[i]["AgentStatus"].ToString();
                        p.Add(Reason);
                    }
                    a.FarmerList = p;
                }
            }
            return a;
        }
        public FarmecallHistory GetFarmerCallHistory(string MobileNo)
        {
            DataSet ds = _Apdal.GetFarmerCallHistory(MobileNo);

            FarmecallHistory a = new FarmecallHistory();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<FarmerCall> p = new List<FarmerCall>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FarmerCall Reason = new FarmerCall();
                        Reason.FarmerId = ds.Tables[0].Rows[i]["FarmerId"].ToString();
                        Reason.FarmerName = ds.Tables[0].Rows[i]["FarmerName"].ToString();
                        Reason.FatherName = ds.Tables[0].Rows[i]["FatherName"].ToString();
                        Reason.MobileNo = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        Reason.StateId = Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"].ToString());
                        Reason.StateName = ds.Tables[0].Rows[i]["StateName"].ToString();
                        Reason.DistrictId = Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictID"].ToString());
                        Reason.DistrictName = ds.Tables[0].Rows[i]["DistrictName"].ToString();
                        Reason.BlockID = Convert.ToInt32(ds.Tables[0].Rows[i]["BlockID"].ToString());
                        Reason.BlockName = ds.Tables[0].Rows[i]["BlockName"].ToString();
                        Reason.VillageID = Convert.ToInt32(ds.Tables[0].Rows[i]["VillageID"].ToString());
                        Reason.VillageName = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        Reason.NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"].ToString();
                        Reason.CallDate = ds.Tables[0].Rows[i]["CallDate"].ToString();
                        Reason.CallDuration = ds.Tables[0].Rows[i]["CallDuration"].ToString();
                        Reason.CallStatus = ds.Tables[0].Rows[i]["CallStatus"].ToString();
                        Reason.RescheduledDate = ds.Tables[0].Rows[i]["RescheduledDate"].ToString();
                        Reason.Message = ds.Tables[0].Rows[i]["Message"].ToString();
                        p.Add(Reason);
                    }
                    a.Farmerc = p;
                }
            }
            return a;
        }
        public FarmerDetails GetFarmerDetails(string FarmerKey)
        {
            DataSet ds = _Apdal.GetFarmerDetails(FarmerKey);

            FarmerDetails Reason = new FarmerDetails();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        Reason.FarmerId = ds.Tables[0].Rows[i]["FarmerId"].ToString();
                        Reason.FarmerName = ds.Tables[0].Rows[i]["FarmerName"].ToString();
                        Reason.RefernceSource = ds.Tables[0].Rows[i]["RefernceSource"].ToString();
                        Reason.FatherName = ds.Tables[0].Rows[i]["FatherName"].ToString();
                        Reason.MobileNo = ds.Tables[0].Rows[i]["MobNo"].ToString();
                        Reason.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        Reason.StateId = Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"].ToString());
                        Reason.StateName = ds.Tables[0].Rows[i]["StateName"].ToString();
                        Reason.DistrictId = Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictID"].ToString());
                        Reason.DistrictName = ds.Tables[0].Rows[i]["DistrictName"].ToString();
                        Reason.BlockID = Convert.ToInt32(ds.Tables[0].Rows[i]["BlockID"].ToString());
                        Reason.BlockName = ds.Tables[0].Rows[i]["BlockName"].ToString();
                        Reason.VillageID = Convert.ToInt32(ds.Tables[0].Rows[i]["VillageID"].ToString());
                        Reason.VillageName = ds.Tables[0].Rows[i]["VillageName"].ToString();
                        Reason.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        Reason.NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"].ToString();
                        Reason.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());

                    }

                }
            }
            return Reason;
        }

        public UserLoginDetails UserLogin(string UserId, string Password)
        {
            DataSet ds = _Apdal.UserLogin(UserId, Password);

            UserLoginDetails ud = new UserLoginDetails();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        ud.Name = ds.Tables[0].Rows[i]["FName"].ToString() + " " + ds.Tables[0].Rows[i]["LName"].ToString();
                        ud.MobileNO = ds.Tables[0].Rows[i]["MobileNo"].ToString();
                        ud.RoleId = Convert.ToInt32(ds.Tables[0].Rows[i]["RoleId"].ToString());
                        ud.UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"].ToString());
                        ud.UserName= ds.Tables[0].Rows[i]["UserName"].ToString();
                        ud.OzontelapiKey = ds.Tables[0].Rows[i]["OzontelapiKey"].ToString();
                        ud.OzonteluserName = ds.Tables[0].Rows[i]["OzonteluserName"].ToString();
                        ud.Ozonteldid = ds.Tables[0].Rows[i]["Ozonteldid"].ToString();
                        ud.AppVersionCode= ds.Tables[0].Rows[i]["appVersionCode"].ToString();
                    }

                }
            }
            return ud;
        }

        public int ChangePassword(int userid, string password, string newpassword)
        {
            int flag = 0;

            string encodepassword = Encode(password);
            string encodenewpassword = Encode(newpassword);
            flag = _Apdal.ChangePassword(userid, encodepassword, encodenewpassword);

            return flag;
        }
        public BZAgentProductViewModel GetCategoryProductDetail(int StateId, int DistrictId, int CatId, int SubCatId, int PackageID=0)
        {
            DataSet ds = _Apdal.GetCategoryProductDetail(StateId, DistrictId, CatId, SubCatId,PackageID);

            BZAgentProductViewModel _catProList = new BZAgentProductViewModel();

            //if (ds != null && ds.Tables[0] != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        List<ProductPopularDetail> _popularList = new List<ProductPopularDetail>();
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            ProductPopularDetail _popular = new ProductPopularDetail();
            //            _popular.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"].ToString());
            //            _popular.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
            //            _popularList.Add(_popular);
            //        }
            //        _catProList._PopularProductList = _popularList;
            //    }
            //}
            /*
            productId	PackageId	ProductName	TechNameID	TechnicalName	Amount	ourprice	
            unitname	SubCategoryId	SubCategoryName	categoryId	categoryName	CompanyId	OrganisationName	dealerId
            */
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<BZProductDescription> _prodList = new List<BZProductDescription>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        BZProductDescription _product = new BZProductDescription();
                        _product.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageId"].ToString());
                        _product.PackageName = ds.Tables[0].Rows[i]["Amount"].ToString() + " " + ds.Tables[0].Rows[i]["unitname"].ToString();

                        _product.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryId"].ToString());
                        _product.SubCategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubCategoryId"].ToString());
                        _product.BrandID = Convert.ToInt32(ds.Tables[0].Rows[i]["BrandID"].ToString());
                        _product.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["productId"].ToString());
                        _product.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        _product.DistrictId = Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictID"].ToString());
                        _product.DistrictName = ds.Tables[0].Rows[i]["DistrictName"].ToString();
                        _product.Company = ds.Tables[0].Rows[i]["OrganisationName"].ToString();
                        _product.TechnicalName = ds.Tables[0].Rows[i]["TechnicalName"].ToString();
                        _product.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyID"].ToString());
                        _product.Price = Convert.ToDecimal(ds.Tables[0].Rows[i]["ourprice"].ToString());
                        _product.DealerId = Convert.ToInt32(ds.Tables[0].Rows[i]["dealerId"].ToString());
                        _product.DealerName = ds.Tables[0].Rows[i]["DealerName"].ToString();
                        _product.IsTrending = Convert.ToInt32(ds.Tables[0].Rows[i]["IsTrendingProduct"].ToString());
                        _prodList.Add(_product);
                    }
                    _catProList._productList = _prodList;
                }
            }




            return _catProList;
        }
        public FSCOrderDetails GetOrderDetails(int FSCId, int RoleId, string fromdate, string todate, int status, string Mode)
        {
            FSCOrderDetails FSCOrderList = new FSCOrderDetails();
            DataSet ds = new AgentAppDal().GetOrderDetails(FSCId, RoleId, fromdate, todate, status, Mode);
            if (ds != null && ds.Tables.Count > 0)
            {
                {
                    List<FSCOrderList> _FscOList = new List<FSCOrderList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FSCOrderList _fscOrd = new FSCOrderList();
                        _fscOrd.OrderId = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"].ToString());
                        _fscOrd.OrderRefNo = ds.Tables[0].Rows[i]["OrderRefNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrderRefNo"]);
                        _fscOrd.FarmerId = ds.Tables[0].Rows[i]["FarmerId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["FarmerId"].ToString());
                        _fscOrd.FarmerName = ds.Tables[0].Rows[i]["FarmerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FarmerName"]);
                        _fscOrd.FatherName = ds.Tables[0].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                        _fscOrd.DistrictId = ds.Tables[0].Rows[i]["DistrictId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictId"].ToString());
                        _fscOrd.District = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]);
                        _fscOrd.StateId = ds.Tables[0].Rows[i]["StateId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["StateId"].ToString());
                        _fscOrd.MobileNo = ds.Tables[0].Rows[i]["MobNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobNo"]);
                       
                        _fscOrd.CreateDate = (ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"])).ToString("dd/MM/yyyy").Replace('-','/');
                        _fscOrd.Deliverydate =
                          (ds.Tables[0].Rows[i]["DeliveryInstruction"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["DeliveryInstruction"])).ToString("dd/MM/yyyy").Replace('-', '/');

                        _fscOrd.TotalPrice = ds.Tables[0].Rows[i]["OrderAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmt"]);
                      _fscOrd.DiscAmount = ds.Tables[0].Rows[i]["DiscountedAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["DiscountedAmt"]);
                        _fscOrd.OrderStatus = ds.Tables[0].Rows[i]["OrderStatus"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderStatus"].ToString());
                        _fscOrd.Status = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Status"]);

                        _FscOList.Add(_fscOrd);
                    }
                    FSCOrderList.OrderList = _FscOList;
                }
            }
            return FSCOrderList;
        }

        public GetPODOrderDetailViewModel GetOrderDetails_OrderID(int orderid)
        {

            DataSet ds = _Apdal.GetOrderDetails_OrderID(orderid);

            GetPODOrderDetailViewModel Order = new GetPODOrderDetailViewModel();
            List<GetPODOrderDetailModel> productlist = new List<GetPODOrderDetailModel>();
            GetPODOrderDetailModel product = null;
            if (ds != null && ds.Tables[0] != null && ds.Tables[1] != null)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {/*
                    RecordID	Quantity	PricePerUnit	Total	ProductID	ProductName	PackageID	PackageName*/
                    Order.OrderRefNo = ds.Tables[0].Rows[0]["OrderRefNo"].ToString();
                    Order.DealerName = ds.Tables[0].Rows[0]["Dealer"].ToString();
                    Order.FarmerId = ds.Tables[0].Rows[0]["FarmerID"].ToString();
                    Order.FarmerName = ds.Tables[0].Rows[0]["FarmerName"].ToString();
                    Order.FarmerAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                    Order.StateName = ds.Tables[0].Rows[0]["StateName"].ToString();
                    Order.StateId =Convert.ToInt32(ds.Tables[0].Rows[0]["StateID"].ToString());
                    Order.DistrictName = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                    Order.DistrictId = Convert.ToInt32(ds.Tables[0].Rows[0]["DistrictID"].ToString());
                    Order.PaymentMode = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentMode"].ToString());
                    Order.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    Order.VillageName = ds.Tables[0].Rows[0]["VillageName"].ToString();
                    Order.NearByVillageName = ds.Tables[0].Rows[0]["NearByVillage"].ToString();
                    Order.OrderDate = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                    Order.DeliveryDate =ds.Tables[0].Rows[0]["DeliveryDate"].ToString();        
                    Order.FarmerContact = ds.Tables[0].Rows[0]["MobNo"].ToString();
                    Order.GrandTotal = ds.Tables[0].Rows[0]["GrandTotal"].ToString();
                    Order.DeliveryRemark = ds.Tables[0].Rows[0]["DeliveryRemark"].ToString();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        product = new GetPODOrderDetailModel();

                        product.ProductName = ds.Tables[1].Rows[i]["ProductName"].ToString();
                        product.ProductID = Convert.ToInt32(ds.Tables[1].Rows[i]["ProductID"].ToString());
                        product.couponStatus = Convert.ToInt32(ds.Tables[1].Rows[i]["couponStatus"].ToString());
                        product.Company = ds.Tables[1].Rows[i]["OrganisationName"].ToString();
                        product.CompanyId = Convert.ToInt32(ds.Tables[1].Rows[i]["CompanyID"].ToString());
                        product.Brand = ds.Tables[1].Rows[i]["BrandName"].ToString();
                        product.BrandId = Convert.ToInt32(ds.Tables[1].Rows[i]["BrandID"].ToString());
                        product.CategoryId = Convert.ToInt32(ds.Tables[1].Rows[i]["CategoryID"].ToString());
                        product.SubCategoryId = Convert.ToInt32(ds.Tables[1].Rows[i]["SubCategoryID"].ToString());
                        product.Package = ds.Tables[1].Rows[i]["PackageName"].ToString();
                        product.Qty = ds.Tables[1].Rows[i]["Quantity"].ToString();
                        product.PricePerUnit = ds.Tables[1].Rows[i]["PricePerUnit"].ToString();
                        product.TotalPrice = ds.Tables[1].Rows[i]["Total"].ToString();
                        product.RecordId = ds.Tables[1].Rows[i]["RecordID"].ToString();
                        product.DiscAmount = ds.Tables[1].Rows[i]["DiscAmt"].ToString();
                        product.AmountAfterDiscount = ds.Tables[1].Rows[i]["NetAmount"].ToString();
                        product.OtherCharges = ds.Tables[1].Rows[i]["OtherCharges"].ToString();
                        //	NetDiscount	NetPrice					
                        product.HSNCode = ds.Tables[1].Rows[i]["HSNcode"].ToString();
                        product.CGST = ds.Tables[1].Rows[i]["CGST"].ToString();
                        product.SGST = ds.Tables[1].Rows[i]["SGST"].ToString();
                        product.TaxValue = ds.Tables[1].Rows[i]["GSTAmout"].ToString();
                        product.UnitPrice = ds.Tables[1].Rows[i]["UnitPrice"].ToString();
                        product.PackageId = ds.Tables[1].Rows[i]["PackageID"].ToString();
                        product.DistrictID = Convert.ToInt32(ds.Tables[1].Rows[i]["DistrictId"].ToString());
                        product.CouponID = Convert.ToInt32(ds.Tables[1].Rows[0]["CouponID"].ToString());
                        product.CouponCode = ds.Tables[1].Rows[0]["CouponCode"].ToString();
                        product.DiscType = Convert.ToInt32(ds.Tables[1].Rows[0]["DiscTypeID"].ToString());
                        product.DiscAmount = ds.Tables[1].Rows[0]["DiscAmt"].ToString();
                        productlist.Add(product);
                    }
                    Order.ProductList = productlist;


                }
            }
            return Order;
        }

        public SaleOrderDetail GetSaleOrder(string fromdate, string todate, string Mode, string DistrictId, int UserID, int stateId)
        {
            DataSet ds = _Apdal.GetSaleOrder(fromdate, todate, Mode, DistrictId, UserID,stateId);

            SaleOrderDetail SaleOD = new SaleOrderDetail();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<SaleOrderList> _Sale = new List<SaleOrderList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SALECOUNT"].ToString()))
                        {
                            SaleOrderList SaleO = new SaleOrderList();
                            if (Mode.ToUpper().Trim() == "USERWISE")
                            {
                                SaleO.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"].ToString());
                            }

                            SaleO.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                            SaleO.SaleCount = ds.Tables[0].Rows[i]["SALECOUNT"].ToString();
                            SaleO.SaleValue = Convert.ToDecimal(ds.Tables[0].Rows[i]["SALEVALUE"].ToString());
                            _Sale.Add(SaleO);
                        }
                    }
                    SaleOD.SaleList = _Sale;

                }
            }
            return SaleOD;
        }

        public GenericViewModel GetDistrictBlockVilage(int id, char type)
        {
            DataSet districtBlockVilage = _Apdal.GetDistrictBlockVilage(id, type);
            GenericViewModel model = new GenericViewModel();
            if ((districtBlockVilage != null))
            {
                if (districtBlockVilage.Tables[0].Rows.Count <= 0)
                {
                    return model;
                }
                List<GenericModel> list = new List<GenericModel>();
                for (int i = 0; i < districtBlockVilage.Tables[0].Rows.Count; i++)
                {
                    GenericModel item = new GenericModel
                    {
                        Id = Convert.ToInt32(districtBlockVilage.Tables[0].Rows[i]["Id"].ToString()),
                        Name = districtBlockVilage.Tables[0].Rows[i]["Name"].ToString()
                    };
                    list.Add(item);
                }
                model.List = list;
            }
            return model;
        }

        public UserValidation GetUserStatus(int userId)
        {
            DataSet dstPromobanner = _Apdal.GetUserStatus(userId);
            UserValidation model = new UserValidation();
            if ((dstPromobanner != null))
            {
                model.Userid = userId;
                model.UserStatus = Convert.ToInt32(dstPromobanner.Tables[0].Rows[0]["UserStatus"]);
                  
                List<PromoBanner> lstBanner = new List<PromoBanner>();
                for (int i = 0; i < dstPromobanner.Tables[1].Rows.Count; i++)
                {
                    PromoBanner item2 = new PromoBanner
                    {
                        Imagepath = Convert.ToString(dstPromobanner.Tables[1].Rows[i]["Image"])
                    };
                    lstBanner.Add(item2);
                }
             
                model.lstBanner = lstBanner;
            }
            return model;
        }

        public PromoCouponModel GetCouponList(string CatId, string SubCatId, string CompanyId, string BrandId, string PCKGId, string Itemval)
        {
            DataSet districtBlockVilage = _Apdal.GetCouponList(CatId, SubCatId, CompanyId, BrandId, PCKGId, Itemval);
            PromoCouponModel model = new PromoCouponModel();
            if ((districtBlockVilage != null))
            {
                if (districtBlockVilage.Tables[0].Rows.Count <= 0)
                {
                    return model;
                }
                List<PromoCoupon> list = new List<PromoCoupon>();
                for (int i = 0; i < districtBlockVilage.Tables[0].Rows.Count; i++)
                {
                    PromoCoupon item = new PromoCoupon
                    {
                        CouponID = Convert.ToInt32(districtBlockVilage.Tables[0].Rows[i]["CouponID"].ToString()),
                        CouponCode = districtBlockVilage.Tables[0].Rows[i]["CouponCode"].ToString(),
                        CouponName = districtBlockVilage.Tables[0].Rows[i]["CouponName"].ToString(),
                        CouponDesc = districtBlockVilage.Tables[0].Rows[i]["CouponDesc"].ToString()

                    };
                    list.Add(item);
                }
                model.List = list;
            }
            return model;
        }

        public SearchProduct GetSearchProducts(bool IsActive, int CatId, int SubCatId, int ComnyId, int BrandId, int stateID, int DistrictId, int blockID, string technicalName, int cropID, string searh, int pageNo, int pageSize, string sortColumn, string sortColumnDir)
        {
            SearchProduct SearchPrdctList = new SearchProduct();
            DataSet ds = new VideoCartDal().GetSearchProducts(IsActive, CatId, SubCatId, ComnyId, BrandId, stateID, DistrictId, cropID, blockID, technicalName, cropID, searh, pageNo, pageSize, sortColumn, sortColumnDir);
            if (ds != null && ds.Tables.Count > 0)
            {
                {
                    List<SearchProductList> _SearchPrdctList = new List<SearchProductList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SearchProductList _Prdouct = new SearchProductList();
                        //  _Prdouct.ROWNUM = ds.Tables[0].Rows[i]["ROWNUM"] == DBNull.Value ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ROWNUM"].ToString());
                        _Prdouct.CategoryId = ds.Tables[0].Rows[i]["CategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryId"].ToString());
                        _Prdouct.Company = ds.Tables[0].Rows[i]["OrganisationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrganisationName"]);
                        _Prdouct.DealerId = ds.Tables[0].Rows[i]["DealerId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DealerId"].ToString());
                        _Prdouct.DealerName = ds.Tables[0].Rows[i]["DealerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DealerName"]);
                        _Prdouct.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                        _Prdouct.PackageId = ds.Tables[0].Rows[i]["packageId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["packageId"].ToString());
                        _Prdouct.PackageName = ds.Tables[0].Rows[i]["Amount"].ToString() + " " + ds.Tables[0].Rows[i]["unitname"].ToString();
                        _Prdouct.Price = ds.Tables[0].Rows[i]["OurPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OurPrice"]);
                        _Prdouct.ProductId = ds.Tables[0].Rows[i]["ProductID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"].ToString());
                        _Prdouct.ProductName = ds.Tables[0].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]);
                        _Prdouct.DistrictId = ds.Tables[0].Rows[i]["DistrictId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictId"].ToString());
                        _Prdouct.DistrictName = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]);
                        _Prdouct.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VideoUrl"]);
                        _SearchPrdctList.Add(_Prdouct);
                    }
                    SearchPrdctList.ProductList = _SearchPrdctList;
                }
            }
            return SearchPrdctList;
        }

        public GenericViewModel GetCategorySubCategory(int id, string type)
        {
            DataSet ds = _Apdal.GetCategorySubCategory(id, type);

            GenericViewModel a = new GenericViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<GenericModel> p = new List<GenericModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GenericModel data = new GenericModel();
                        data.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                        data.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        p.Add(data);
                    }
                    a.List = p;
                }
            }
            return a;
        }

        public int DemandOrderCreate(DemandCreateModel obj)
        {


            int flag = _Apdal.DemandOrderCreate(obj.FarmerName, obj.Mobile, obj.CropID, obj.CategoryID,obj.PackageID,obj.FarmerPrice, obj.Product, obj.AadharOrLoanNo,obj.DistrictId,obj.UserId,obj.Qty);
            return flag;
        }
        public ApplyPromoCoupon ApplyCoupon(int CatID, int SubCatID, int CompanyID, int BrandID, int ProductID, int PkgID, int Qty, decimal ActualAmt, int CouponID)
        {
            DataSet ds = new AgentAppDal().ApplyCoupon(CatID, SubCatID, CompanyID, BrandID, ProductID, PkgID, Qty, ActualAmt, CouponID);
            ApplyPromoCoupon _ApplyPromoCoupon = new ApplyPromoCoupon();
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    _ApplyPromoCoupon = ds.Tables[0].AsEnumerable().Select(a => new ApplyPromoCoupon
                    {
                        ActualAmt = a.Field<decimal>("ActualAmt"),
                        BrandID = a.Field<int>("BrandID"),
                        CatID = a.Field<int>("CatID"),
                        CompanyID = a.Field<int>("CompanyID"),
                        CouponCode = a.Field<string>("CouponCode"),
                        CouponID = a.Field<int>("CouponID"),
                        DiscAmount = a.Field<decimal>("DiscAmount"),
                        DiscType = a.Field<Int16>("DiscType"),
                        FinalAmount = a.Field<decimal>("SubTotal"),
                        PkgID = a.Field<int>("PkgID"),
                        ProductID = a.Field<int>("ProductID"),
                        Qty = a.Field<int>("Qty"),
                        SubCatID = a.Field<int>("SubCatID"),
                        CouponMsg = a.Field<string>("MSG"),
                        CouponStatus = a.Field<int>("CouponStatus")
                    }).FirstOrDefault();
                }
            }
            return _ApplyPromoCoupon;
        }
        public int AgentOrderCreate(AgentOrderCreateModel obj)
        {
            //int id = _rsdal.GetFarmerIdByMobile(mobile);
            DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            int flag = _Apdal.AgentOrderCreate(obj.userid, obj.Farmer.FarmerId, obj.Farmer.FarmerName, obj.Farmer.FatherName, obj.Farmer.Mobile,
                obj.Farmer.StateId, obj.Farmer.DistrictId, obj.Farmer.BlockId, obj.Farmer.VillageId, obj.Farmer.OtherVillageName, obj.Farmer.Address,
                obj.DeliveryDate, obj.ModeOfPayment, DT);
            return flag;
        }
        public int UpdateCallLog(string MobileNo)
        {
          
            int flag = _Apdal.UpdateCallLog(MobileNo);
            return flag;
        }
        public string RemoveItemFromCart(RemoveItemFromCart obj)
        {

            string flag = _Apdal.RemoveItemFromCart(obj.OrderID, obj.RecordId, obj.DeletedBy);
            return flag;
        }

        public int IssueRegister(Issue obj)
        {

            int flag = _Apdal.IssueRegister(obj.MobileNo, obj.Name, obj.CategoryID, obj.IssueDetailID, obj.Query, obj.CreatedBy, obj.IssueTypeId);
            return flag;
        }

        public IssueDetailByFarmer IssueDetailByFarmer(string MobileNo)
        {
            DataSet ds = _Apdal.IssueDetailByFarmer(MobileNo);

            IssueDetailByFarmer Isd = new IssueDetailByFarmer();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<IssueDetail> _IL = new List<IssueDetail>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        IssueDetail ILF = new IssueDetail();
                        ILF.QueryID = Convert.ToInt32(ds.Tables[0].Rows[i]["QueryID"].ToString());
                        ILF.MobileNo = ds.Tables[0].Rows[i]["MobNo"].ToString();
                        ILF.FarmerName = ds.Tables[0].Rows[i]["FarmerName"].ToString();
                        ILF.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                        ILF.IssueType = ds.Tables[0].Rows[i]["IssueType"].ToString();
                        ILF.IssueDetails = ds.Tables[0].Rows[i]["IssueDetail"].ToString();
                        ILF.CreatorName = ds.Tables[0].Rows[i]["CreatorName"].ToString();
                        ILF.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"].ToString();
                        ILF.Query = ds.Tables[0].Rows[i]["Query"].ToString();
                        _IL.Add(ILF);

                    }
                    Isd.IssueList = _IL;

                }
            }
            return Isd;
        }

        public FSCOrderDetails GetOrderDetailsByFarmerID(int FarmerID, string fromdate, string todate, int status)
        {
            FSCOrderDetails FSCOrderList = new FSCOrderDetails();
            DataSet ds = new AgentAppDal().GetOrderDetailsByFarmerID(FarmerID, fromdate, todate, status);
            if (ds != null && ds.Tables.Count > 0)
            {
                {
                    List<FSCOrderList> _FscOList = new List<FSCOrderList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FSCOrderList _fscOrd = new FSCOrderList();
                        _fscOrd.OrderId = ds.Tables[0].Rows[i]["OrderID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderID"].ToString());
                        _fscOrd.OrderRefNo = ds.Tables[0].Rows[i]["OrderRefNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrderRefNo"]);
                        _fscOrd.FarmerName = ds.Tables[0].Rows[i]["FarmerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FarmerName"]);
                        _fscOrd.CreateDate = (ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"])).ToString("dd/MM/yyyy").Replace('-', '/');
                        _fscOrd.Deliverydate =
                          (ds.Tables[0].Rows[i]["DeliveryInstruction"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["DeliveryInstruction"])).ToString("dd/MM/yyyy").Replace('-', '/');
                        _fscOrd.TotalPrice = ds.Tables[0].Rows[i]["OrderAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmt"]);
                        _fscOrd.OrderStatus = ds.Tables[0].Rows[i]["OrderStatus"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["OrderStatus"].ToString());
                        _FscOList.Add(_fscOrd);
                    }
                    FSCOrderList.OrderList = _FscOList;
                }
            }
            return FSCOrderList;
        }
        public OrderWiseProductViewModel OrderWiseProduct(int orderid)
        {
            DataSet ds = _Apdal.OrderWiseProduct(orderid);

            OrderWiseProductViewModel Isd = new OrderWiseProductViewModel();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<OrderWiseProduct> _IL = new List<OrderWiseProduct>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        OrderWiseProduct ILF = new OrderWiseProduct();
                        ILF.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageId"].ToString());
                        ILF.PackageName = ds.Tables[0].Rows[i]["ProductName"].ToString() + "(" + ds.Tables[0].Rows[i]["CategoryName"].ToString() + "," + ds.Tables[0].Rows[i]["BrandName"].ToString() + ")";
                        _IL.Add(ILF);

                    }
                    Isd._OrderWiseProductList = _IL;

                }
            }
            return Isd;
        }
        public int ComplaintRegister(Complaint obj)
        {

            int flag = _Apdal.ComplaintRegister(obj.OrderID, obj.PackageID, obj.IssueDetailID, obj.Query, obj.CreatedBy);
            return flag;
        }

        public CompDetailByFarmer GetFarmerComplaint(int OrderID, int FarmerId)
        {
            DataSet ds = _Apdal.GetFarmerComplaint(OrderID, FarmerId);

            CompDetailByFarmer Isd = new CompDetailByFarmer();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<ComplainModel> _IL = new List<ComplainModel>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        ComplainModel ILF = new ComplainModel();
                        ILF.ComplaintID = Convert.ToInt32(ds.Tables[0].Rows[i]["ComplaintID"].ToString());
                        ILF.Query = ds.Tables[0].Rows[i]["Query"].ToString();
                        ILF.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"].ToString();
                        ILF.IssueType = ds.Tables[0].Rows[i]["IssueType"].ToString();
                        ILF.IssueDetail = ds.Tables[0].Rows[i]["IssueDetail"].ToString();
                        ILF.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        ILF.CreatedBy = ds.Tables[0].Rows[i]["CreatorName"].ToString();


                        _IL.Add(ILF);

                    }
                    Isd.CompList = _IL;

                }
            }
            return Isd;
        }
        public int AgentOrderConfirmation(AgentOrderUpdateModel obj)
        {
            DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            int flag = _Apdal.AgentOrderConfirmation(obj.userid, obj.Remark, obj.statusId, obj.DeliveryDate, obj.OrderID, obj.CancelReasonID = 0, obj.Farmer.Mobile,
                obj.Farmer.StateId, obj.Farmer.DistrictId, obj.Farmer.BlockId, obj.Farmer.VillageId, obj.Farmer.OtherVillageName, obj.Farmer.Address, obj.ModeOfPayment, DT);
            return flag;
        }
        public int SaveCallLog(CallLogged obj)
        {

            int flag = _Apdal.SaveCallLog(obj.userid, obj.Remark, obj.callStatusID, obj.type, obj.appointmentDate, obj.MobileNo);
            return flag;
        }
        public int SaveAgreement(AgreementDetails obj)
        {

            int flag = _Apdal.AgreementDetails(obj.CompanyName, obj.EnterpenueName, obj.MobileNo, obj.AltMobile, obj.GST, obj.PAN,obj.ShopAddress,obj.StateId,obj.DistrictId,obj.Pincode);
            return flag;
        }
        public int FarmerData(FarmerData obj)
        {

            int flag = _Apdal.FarmerData(obj.UserID, obj.MobileNo, obj.StateId, obj.DistrictId, obj.BlockID, obj.VillageID, obj.NearByVillage, obj.Address, obj.RefrenceSource, obj.FarmerName, obj.FatherName);
            return flag;
        }
        public int BlacklistFarmer(string MobileNo, int UserID, string Remark)
        {

            int flag = _Apdal.BlacklistFarmer(MobileNo, UserID, Remark);
            return flag;
        }
        public int AvisCallLog(string EventName, string ANI, string DNIS, string Mode, string CallId, string UserLogin, string Campaign, string LeadId, string Skill, string dnisIB, string CallFileName,string CallDisposition)
        {

            int flag = _Apdal.AvisCallLog(EventName, ANI, DNIS, Mode, CallId, UserLogin, Campaign, LeadId, Skill, dnisIB, CallFileName, CallDisposition);
            return flag;
        }
        private string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
        public int UploadPhoto(int userid, string Lat,string Lang, string path)
        {
            int flag = _Apdal.UploadPhoto(userid, Lat,Lang, path);
            return flag;
        }
        #region Ashish Ozontel Service
        public int Ozontel_CallLog_Insert(Ozontel_CallLog_Model obj)
        {
            int flag = 0;
            flag = _Apdal.Ozontel_CallLog_Insert(obj.AgentPhoneNumber, obj.Disposition, obj.CallerConfAudioFile, obj.TransferredTo, obj.Apikey, obj.Did
            , obj.StartTime, obj.CallDuration, obj.EndTime, obj.ConfDuration, obj.CustomerStatus, obj.TimeToAnswer
            , obj.monitorUCID, obj.AgentID, obj.AgentStatus, obj.Location, obj.FallBackRule, obj.CampaignStatus
            , obj.CallerID, obj.Duration, obj.Status, obj.AgentUniqueID, obj.UserName, obj.HangupBy
            , obj.AudioFile, obj.PhoneName, obj.TransferType, obj.DialStatus, obj.CampaignName, obj.UUI
            , obj.AgentName, obj.Skill, obj.DialedNumber, obj.Type, obj.Comments);

            return flag;

            /*
            AgentPhoneNumber		,Disposition				,CallerConfAudioFile		,TransferredTo			,Apikey					,Did						
            ,StartTime				,CallDuration			,EndTime					,ConfDuration			,CustomerStatus			,TimeToAnswer			
            ,monitorUCID				,AgentID					,AgentStatus				,Location				,FallBackRule			,CampaignStatus			
            ,CallerID				,Duration				,[Status]				,AgentUniqueID			,UserName				,HangupBy				
            ,AudioFile				,PhoneName				,TransferType			,DialStatus				,CampaignName			,UUI						
            ,AgentName				,Skill					,DialedNumber			,[Type]					,Comments				
            */
        }

        public List<Ozontel_CallLog_Model2> Ozontel_CallLog_Select(int userid)
        {
            DataSet DS = _Apdal.Ozontel_CallLog_Select(userid);

            List<Ozontel_CallLog_Model2> _list = new List<Ozontel_CallLog_Model2>();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS != null && DS.Tables[0] != null)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {

                        _list = (from x in DS.Tables[0].AsEnumerable()
                                 select new Ozontel_CallLog_Model2
                                 {
                                     Id = x.Field<long>("Id"),
                                     AgentPhoneNumber = x.Field<string>("AgentPhoneNumber"),
                                     Disposition = x.Field<string>("Disposition"),
                                     CallerConfAudioFile = x.Field<string>("CallerConfAudioFile"),
                                     TransferredTo = x.Field<string>("TransferredTo"),
                                     Apikey = x.Field<string>("Apikey"),
                                     Did = x.Field<string>("Did"),
                                     StartTime = x.Field<string>("StartTime"),
                                     CallDuration = x.Field<string>("CallDuration"),
                                     EndTime = x.Field<string>("EndTime"),
                                     ConfDuration = x.Field<string>("ConfDuration"),
                                     CustomerStatus = x.Field<string>("CustomerStatus"),
                                     TimeToAnswer = x.Field<string>("TimeToAnswer"),
                                     monitorUCID = x.Field<string>("monitorUCID"),
                                     AgentID = x.Field<string>("AgentID"),
                                     AgentStatus = x.Field<string>("AgentStatus"),
                                     Location = x.Field<string>("Location"),
                                     FallBackRule = x.Field<string>("FallBackRule"),
                                     CampaignStatus = x.Field<string>("CampaignStatus"),
                                     CallerID = x.Field<string>("CallerID"),
                                     Duration = x.Field<string>("Duration"),
                                     Status = x.Field<string>("Status"),
                                     AgentUniqueID = x.Field<string>("AgentUniqueID"),
                                     UserName = x.Field<string>("UserName"),
                                     HangupBy = x.Field<string>("HangupBy"),
                                     AudioFile = x.Field<string>("AudioFile"),
                                     PhoneName = x.Field<string>("PhoneName"),
                                     TransferType = x.Field<string>("TransferType"),
                                     DialStatus = x.Field<string>("DialStatus"),
                                     CampaignName = x.Field<string>("CampaignName"),
                                     UUI = x.Field<string>("UUI"),
                                     AgentName = x.Field<string>("AgentName"),
                                     Skill = x.Field<string>("Skill"),
                                     DialedNumber = x.Field<string>("DialedNumber"),
                                     Type = x.Field<string>("Type"),
                                     Comments = x.Field<string>("Comments"),
                                     CreateDate = x.Field<string>("CreateDate")
                                 }).ToList();
                    }
                }

            }
            return _list;
        }



        #endregion

        public PaymentRequestParam GetPaymentRequestParam()
        {
            DataSet ds = _Apdal.GetPaymentRequestParam();

            PaymentRequestParam a = new PaymentRequestParam();

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<PaymentRequestParamList> p = new List<PaymentRequestParamList>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PaymentRequestParamList Reason = new PaymentRequestParamList();
                        Reason.EncKey = ds.Tables[0].Rows[i]["EncKey"].ToString();
                        Reason.PGMerchant_ID = ds.Tables[0].Rows[i]["PGMerchant_ID"].ToString();
                        Reason.User_Name = ds.Tables[0].Rows[i]["User_Name"].ToString();
                        Reason.OAuth_Password = ds.Tables[0].Rows[i]["OAuth_Password"].ToString();
                        Reason.PaymentType = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                        Reason.Name= ds.Tables[0].Rows[i]["Name"].ToString();   
                        Reason.VPA = ds.Tables[0].Rows[i]["VPA"].ToString();
                        Reason.merchantCategoryCode = ds.Tables[0].Rows[i]["merchantCategoryCode"].ToString();
                        Reason.PaymentType = ds.Tables[0].Rows[i]["PaymentType"].ToString();
                        Reason.TxnType = ds.Tables[0].Rows[i]["TxnType"].ToString();
                       
                        p.Add(Reason);
                    }
                    a.Farmerc = p;
                }
            }
            return a;
        }
    }
}
