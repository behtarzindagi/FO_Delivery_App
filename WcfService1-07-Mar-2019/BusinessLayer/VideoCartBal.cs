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
   public class VideoCartBal
    {
       VideoCartDal _rsdal=new VideoCartDal();

        public UserLoginDetails UserLogin(string UserId, string Password)
        {
            DataSet ds = _rsdal.UserLogin(UserId, Password);

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

                    }

                }
            }
            return ud;
        }

        public UserDetails GetUserDetails(string apikey, string MobNo)
        {

            DataSet dsUser = _rsdal.GetUserDetails(apikey, MobNo);
            DataSet dsOrder = _rsdal.GetOrderDetails(apikey, MobNo);

            UserDetailsModel ud = null;
            var od = new List<OrderDetailsModel>();
            UserDetails Uvm = new UserDetails();

            if (dsUser.Tables[0].Rows.Count > 0)
            {

                ud = new UserDetailsModel();
                ud.FarmerID = Convert.ToInt64(dsUser.Tables[0].Rows[0]["FarmerID"]);
                ud.FarmerRefNo = Convert.ToString(dsUser.Tables[0].Rows[0]["FarmerRefNo"]);
                ud.FName = Convert.ToString(dsUser.Tables[0].Rows[0]["FName"]);
                ud.FatherName = Convert.ToString(dsUser.Tables[0].Rows[0]["FatherName"]);
                ud.MobNo = Convert.ToDecimal(dsUser.Tables[0].Rows[0]["MobNo"]);
                ud.StateID = Convert.ToInt16(dsUser.Tables[0].Rows[0]["StateID"]);
                ud.StateName = Convert.ToString(dsUser.Tables[0].Rows[0]["StateName"]);
                ud.DistrictID = Convert.ToInt16(dsUser.Tables[0].Rows[0]["DistrictID"]);
                ud.DistrictName = Convert.ToString(dsUser.Tables[0].Rows[0]["DistrictName"]);
                ud.BlockID = Convert.ToInt16(dsUser.Tables[0].Rows[0]["BlockID"]);
                ud.BlockName = Convert.ToString(dsUser.Tables[0].Rows[0]["BlockName"]);
                ud.VillageID = Convert.ToInt16(dsUser.Tables[0].Rows[0]["VillageID"]);
                ud.VillageName = Convert.ToString(dsUser.Tables[0].Rows[0]["VillageName"]);
                ud.NearByVillage = Convert.ToString(dsUser.Tables[0].Rows[0]["NearByVillage"]);
                ud.Address = Convert.ToString(dsUser.Tables[0].Rows[0]["Address"]);


                if (dsOrder.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsOrder.Tables[0].Rows.Count; i++)
                    {
                        od.Add(new OrderDetailsModel
                        {
                            ItemID = dsOrder.Tables[0].Rows[i]["RecordID"] == DBNull.Value ? 0 : Convert.ToInt64(dsOrder.Tables[0].Rows[i]["RecordID"]),
                            OrderId = dsOrder.Tables[0].Rows[i]["OrderId"] == DBNull.Value ? 0 : Convert.ToInt64(dsOrder.Tables[0].Rows[i]["OrderId"]),
                            Quantity = dsOrder.Tables[0].Rows[i]["Quantity"] == DBNull.Value ? 0 : Convert.ToInt16(dsOrder.Tables[0].Rows[i]["Quantity"]),
                            OurPrice = dsOrder.Tables[0].Rows[i]["OurPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["OurPrice"]),
                            OtherCharges = dsOrder.Tables[0].Rows[i]["OtherCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["OtherCharges"]),
                            DiscAmt = dsOrder.Tables[0].Rows[i]["DiscAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["DiscAmt"]),
                            ProductName = dsOrder.Tables[0].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["ProductName"]),
                            Amount = dsOrder.Tables[0].Rows[i]["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(dsOrder.Tables[0].Rows[i]["Amount"]),
                            UnitName = dsOrder.Tables[0].Rows[i]["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["UnitName"]),
                            CategoryName = dsOrder.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["CategoryName"]),
                            SubCategoryName = dsOrder.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["SubCategoryName"]),
                            OrganisationName = dsOrder.Tables[0].Rows[i]["OrganisationName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["OrganisationName"]),
                            BrandName = dsOrder.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["BrandName"]),
                            Status = dsOrder.Tables[0].Rows[i]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["Status"]),
                            DealerID = dsOrder.Tables[0].Rows[i]["DealerID"] == DBNull.Value ? 0 : Convert.ToInt16(dsOrder.Tables[0].Rows[i]["DealerID"]),
                            DealerName = dsOrder.Tables[0].Rows[i]["DealerName"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["DealerName"]),
                            PackageID = dsOrder.Tables[0].Rows[i]["DealerID"] == DBNull.Value ? 0 : Convert.ToInt16(dsOrder.Tables[0].Rows[i]["PackageID"]),
                            TechnicalName_Name = dsOrder.Tables[0].Rows[i]["TechnicalName_Name"] == DBNull.Value ? string.Empty : Convert.ToString(dsOrder.Tables[0].Rows[i]["TechnicalName_Name"])
                        });
                    }
                }
            }
            if (ud != null)
            {
                Uvm.MobileNo = MobNo;
                Uvm.UserStatus = "1";
                Uvm.UserDetail = ud;
                Uvm.OrderDetails = od;
            }
            else
            {

                Uvm.MobileNo = MobNo;
                Uvm.UserStatus = "0";
                Uvm.UserDetail = ud;
                Uvm.OrderDetails = od;
            }
            return Uvm;
        }

        public int FarmerDataCollect(int userid, string RefSource, string Fname, string Lname, string fathername, string mobile, int stateid, int districtid, int blockid,
            int villageid, string NearByVillage, string Address)
        {
            int flag = 0;
             flag = _rsdal.VideoFarmerDataCollect(userid, RefSource, Fname, Lname, fathername, mobile, stateid, districtid, blockid, villageid, NearByVillage, Address);


            return flag;
        }

        public GenericViewModel GetDistrictBlockVilage(int id, char type)
        {
            DataSet ds = _rsdal.GetDistrictBlockVilage(id, type);

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

        public GenericViewModel GetCategorySubCategory(int id, char type)
        {
            DataSet ds = _rsdal.GetCategorySubCategory(id, type);

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

        public CategogyProductViewModel GetCategoryProductDetail(int StateId,int DistrictId, int CatId, int SubCatId)
        {
            DataSet ds = _rsdal.GetCategoryProductDetail(StateId,DistrictId,CatId,SubCatId);

            CategogyProductViewModel _catProList = new CategogyProductViewModel();

            //if (ds != null && ds.Tables[0] != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        List<CategoryDetail> _catList = new List<CategoryDetail>();
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            CategoryDetail _category = new CategoryDetail();
            //            _category.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"].ToString());
            //            _category.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
            //            _catList.Add(_category);
            //        }
            //        _catProList._categoryList = _catList;
            //    }
            //}
            /*
            productId	PackageId	ProductName	TechNameID	TechnicalName	Amount	ourprice	
            unitname	SubCategoryId	SubCategoryName	categoryId	categoryName	CompanyId	OrganisationName	dealerId
            */
            if (ds != null && ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    List<BZProductDescription> _prodList = new List<BZProductDescription>();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        BZProductDescription _product = new BZProductDescription();
                        _product.PackageId = Convert.ToInt32(ds.Tables[1].Rows[i]["PackageId"].ToString());
                        //  _product.PackageName = ds.Tables[1].Rows[i]["ProductName"].ToString() + " - " + ds.Tables[1].Rows[i]["TechnicalName"].ToString()
                        //     + "(" + ds.Tables[1].Rows[i]["Amount"].ToString() + ds.Tables[1].Rows[i]["unitname"].ToString()
                        //    + ds.Tables[1].Rows[i]["ourprice"].ToString() + ")";
                        _product.PackageName =  ds.Tables[1].Rows[i]["Amount"].ToString() +" " + ds.Tables[1].Rows[i]["unitname"].ToString();
                        _product.CategoryId = Convert.ToInt32(ds.Tables[1].Rows[i]["categoryId"].ToString());
                        _product.ProductId = Convert.ToInt32(ds.Tables[1].Rows[i]["productId"].ToString());
                        _product.ProductName = ds.Tables[1].Rows[i]["ProductName"].ToString();
                        _product.Company = ds.Tables[1].Rows[i]["OrganisationName"].ToString();
                        _product.Price = Convert.ToDecimal(ds.Tables[1].Rows[i]["ourprice"].ToString());
                        _product.DealerId = Convert.ToInt32(ds.Tables[1].Rows[i]["dealerId"].ToString());
                        _product.DealerName = ds.Tables[1].Rows[i]["DealerName"].ToString();
                        _product.ImageUrl = ds.Tables[1].Rows[i]["ImageUrl"].ToString();
                        _product.VideoUrl = ds.Tables[1].Rows[i]["VideoUrl"].ToString();

                        _prodList.Add(_product);
                    }
                    _catProList._productList = _prodList;
                }
            }

            if (ds != null && ds.Tables[2] != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    List<DealerDetail> _dealerList = new List<DealerDetail>();
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        DealerDetail _dealer = new DealerDetail();
                        _dealer.DealerId = Convert.ToInt32(ds.Tables[2].Rows[i]["DealerId"].ToString());
                        _dealer.DealerName = ds.Tables[2].Rows[i]["DealerName"].ToString();
                        _dealerList.Add(_dealer);
                    }
                    _catProList._dealerList = _dealerList;
                }
            }


            return _catProList;
        }

        public SearchProduct GetSearchProducts(bool IsActive,int CatId,int SubCatId, int ComnyId, int BrandId, int stateID,int DistrictId,int blockID, string technicalName,int cropID,string searh,int pageNo,int pageSize,string sortColumn,string sortColumnDir)
        {
            SearchProduct SearchPrdctList = new SearchProduct();
            DataSet ds = new VideoCartDal().GetSearchProducts(IsActive, CatId,  SubCatId, ComnyId, BrandId, stateID, DistrictId, cropID, blockID, technicalName, cropID,  searh, pageNo, pageSize, sortColumn, sortColumnDir);
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
                    SearchPrdctList.ProductList  = _SearchPrdctList;
                }
            }
            return SearchPrdctList;
        }

        public TempOrderDetailsModel GetTempOrderDetail(int UserId, string MobileNo)
        {
            DataSet ds = _rsdal.GetTempOrderDetail(UserId,MobileNo);

            TempOrderDetailsModel _TempOrdList = new TempOrderDetailsModel();
            List<TempOrderDetail> _TempList = new List<TempOrderDetail>();
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TempOrderDetail _OrdDetails = new TempOrderDetail();
                        BZProductDescription _TempOrd = new BZProductDescription();
                        _TempOrd.PackageId = Convert.ToInt32(ds.Tables[0].Rows[i]["PackageId"].ToString());
                        _TempOrd.PackageName = ds.Tables[0].Rows[i]["ProductName"].ToString() + " - " + ds.Tables[0].Rows[i]["TechnicalName"].ToString()
                           + "(" + ds.Tables[0].Rows[i]["Amount"].ToString() + ds.Tables[0].Rows[i]["unitname"].ToString()
                           + ds.Tables[0].Rows[i]["ourprice"].ToString() + ")";
                        _TempOrd.CategoryId = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryId"].ToString());
                        _TempOrd.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"].ToString());
                      _TempOrd.ProductName= ds.Tables[0].Rows[i]["ProductName"].ToString();
                        _TempOrd.Company = ds.Tables[0].Rows[i]["OrganisationName"].ToString();
                        _TempOrd.Price = Convert.ToDecimal(ds.Tables[0].Rows[i]["ourprice"].ToString());
                        _TempOrd.DealerId = Convert.ToInt32(ds.Tables[0].Rows[i]["dealerId"].ToString());
                        _TempOrd.DealerName = ds.Tables[0].Rows[i]["DealerName"].ToString();
                        _TempOrd.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"].ToString();
                        _TempOrd.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"].ToString();

                        _OrdDetails.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString());
                        _OrdDetails.product = _TempOrd;
                        _TempList.Add(_OrdDetails);
                    }
                  
                }
            }
            _TempOrdList.List = _TempList;


            return _TempOrdList;
        }
        public int OrderCreate(OrderCreateModel obj)
        {
            //int id = _rsdal.GetFarmerIdByMobile(mobile);
           // DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            int flag = _rsdal.OrderCreate(obj.userid, obj.Farmer.FarmerId, obj.Farmer.FarmerName, obj.Farmer.FatherName, obj.Farmer.Mobile,
                obj.Farmer.StateId, obj.Farmer.DistrictId, obj.Farmer.BlockId, obj.Farmer.VillageId, obj.Farmer.OtherVillageName, obj.Farmer.Address,
                obj.DeliveryDate,obj.Lat,obj.Long, obj.ModeOfPayment);
            return flag;
        }
        public int TempOrderCreate(TempOrderCreateModel obj)
        {
           
            DataTable DT = Helper.Helper.ToDataTable(obj.Product);
            int flag = _rsdal.TempOrderCreate(obj.userid, obj.FarmerId, DT);
            return flag;
        }

        public int TempOrderUpdate(string FMobNo, string ProductId, int PackageID, int Qty, int UserId)
        {

           
            int flag = _rsdal.TempOrderUpdate( FMobNo,  ProductId,  PackageID,  Qty,  UserId);
            return flag;
        }

        public FSCOrderDetails GetOrderDetails(int FSCId, int RoleId, string fromdate, string todate, int status, string Mode)
        {
            FSCOrderDetails FSCOrderList = new FSCOrderDetails();
            DataSet ds = new VideoCartDal().GetOrderDetails(FSCId, RoleId, fromdate, todate, status, Mode);
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
                        _fscOrd.CreateDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        //_fscOrd.Deliverydate = ds.Tables[0].Rows[i]["DeliveryInstruction"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DeliveryInstruction"]);
                        _fscOrd.Deliverydate =
                        (ds.Tables[0].Rows[i]["DeliveryInstruction"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[i]["DeliveryInstruction"])).ToString("dd/MM/yyyy");

                        _fscOrd.TotalPrice = ds.Tables[0].Rows[i]["OrderAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmt"]);
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

            DataSet ds = _rsdal.GetOrderDetails_OrderID(orderid);

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
                    Order.OrderDate = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                    Order.FarmerContact = ds.Tables[0].Rows[0]["MobNo"].ToString();
                    Order.GrandTotal = ds.Tables[0].Rows[0]["GrandTotal"].ToString();
                    Order.DeliveryRemark = ds.Tables[0].Rows[0]["DeliveryRemark"].ToString();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        product = new GetPODOrderDetailModel();

                        product.ProductName = ds.Tables[1].Rows[i]["ProductName"].ToString();
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
                        productlist.Add(product);
                    }
                    Order.ProductList = productlist;


                }
            }
            return Order;
        }

        public SaleOrderDetail GetSaleOrder(string fromdate, string todate, string Mode, string DistrictId, int UserID, int stateId)
        {
            DataSet ds = _rsdal.GetSaleOrder(fromdate, todate, Mode, DistrictId, UserID, stateId);

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

        public BZProduct GetBzProducts(int CategoryId)
        {
            BZProduct BZProductList = new BZProduct();
            DataSet ds = new VideoCartDal().GetBzProducts(CategoryId);
            if (ds != null && ds.Tables.Count > 0)
            {
                {
                    List<BzProduct> _BzPrdctList = new List<BzProduct>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        BzProduct _BzPrdct = new BzProduct();
                        _BzPrdct.PackID = ds.Tables[0].Rows[i]["PackageID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PackageID"].ToString());
                        _BzPrdct.ProductName = ds.Tables[0].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]);
                        _BzPrdct.ProductHindiName = ds.Tables[0].Rows[i]["ProductHindiName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProductHindiName"]);
                        _BzPrdct.OrganisationName = ds.Tables[0].Rows[i]["OrganisationName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OrganisationName"]);
                        _BzPrdct.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                        _BzPrdct.TechnicalName = ds.Tables[0].Rows[i]["TechnicalName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TechnicalName"]);
                        _BzPrdct.CategoryName = ds.Tables[0].Rows[i]["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"]);
                        _BzPrdct.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        _BzPrdct.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        _BzPrdct.OurPrice = ds.Tables[0].Rows[i]["OurPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OurPrice"]);
                        _BzPrdct.OfferPrice = ds.Tables[0].Rows[i]["OfferPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OfferPrice"]);
                        _BzPrdct.UnitName = ds.Tables[0].Rows[i]["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UnitName"]);
                        _BzPrdct.ImagePath = ds.Tables[0].Rows[i]["ImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImagePath"]);
                        _BzPrdct.BzProductId = ds.Tables[0].Rows[i]["RecordID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                        _BzPrdct.IsActive = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                       
                        _BzPrdctList.Add(_BzPrdct);
                    }
                    BZProductList.BzProduct = _BzPrdctList;
                }
            }
            return BZProductList;
        }

        public List<ComboBZProduct> Get_BZComboProductList()
        {
            DataSet DS = new VideoCartDal().Get_BZComboProductList();

            List<ComboBZProduct> _viewModel = new List<ComboBZProduct>();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {

                  
                    foreach (DataRow item in DS.Tables[0].Rows)
                    {
                        var combo = new ComboBZProduct()
                        {
                            ComboId = item["ComboId"] == DBNull.Value ? 0 : Convert.ToInt32(item["ComboId"]),
                            ComboName = item["ComboName"] == DBNull.Value ? string.Empty : Convert.ToString(item["ComboName"]),
                            ComboHindiName = item["CHindName"] == DBNull.Value ? string.Empty : Convert.ToString(item["CHindName"]),
                            ImagePath = item["ImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(item["ImagePath"]),
                            MRP = item["MRP"] == DBNull.Value ? 0 : Convert.ToInt32(item["MRP"]),
                            DiscountAmt = item["ComboDiscount"] == DBNull.Value ? 0 : Convert.ToInt32(item["ComboDiscount"]),
                            OfferPrice = Convert.ToInt32(item["MRP"]) - Convert.ToInt32(item["ComboDiscount"])
                        };
                        _viewModel.Add(combo);
                    }
                }
            }

            return _viewModel;
        }

        public List<BZProductBanner> BZProductBannerList()
        {
            DataSet DS = new VideoCartDal().Get_BZProductBanner();

            List<BZProductBanner> _viewModel = new List<BZProductBanner>();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow item in DS.Tables[0].Rows)
                    {
                        var combo = new BZProductBanner()
                        {
                            BannerId = item["BannerId"] == DBNull.Value ? 0 : Convert.ToInt32(item["BannerId"]),
                            BannerName = item["BannerName"] == DBNull.Value ? string.Empty : Convert.ToString(item["BannerName"]),
                            BannerType = item["BannerType"] == DBNull.Value ? string.Empty : Convert.ToString(item["BannerType"]),
                            ImagePath = item["ImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(item["ImagePath"])
                          
                        };
                        _viewModel.Add(combo);
                    }
                }
            }

            return _viewModel;
        }

    }
}
