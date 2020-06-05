using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;
using Entity;
using Newtonsoft.Json;

namespace BusinessLayer
{
    public class ProductDetailBal
    {
        ProductDetailDal _pddal = new ProductDetailDal();

        public object CryptorEngine { get; private set; }

        public BzProductDtl GetBzProductDetail(string apikey, int ProductId)
        {
            BzProductDtl BzProductDetail = new BzProductDtl();
            DataSet ds = new ProductDetailDal().GetBzProductDetail(apikey, ProductId);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BzProductDetails> _BzProductDetail = new List<BzProductDetails>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)


                {
                    BzProductDetails _BzProductDtl = new BzProductDetails();

                    _BzProductDtl.CropName = ds.Tables[0].Rows[i]["CropName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CropName"]);
                    _BzProductDtl.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                    _BzProductDtl.TechnicalName = ds.Tables[0].Rows[i]["TechnicalName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TechnicalName"]);
                    _BzProductDtl.Days = ds.Tables[0].Rows[i]["Days"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Days"]);
                    _BzProductDtl.Dose = ds.Tables[0].Rows[i]["Dose"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Dose"]);
                    _BzProductDtl.Method = ds.Tables[0].Rows[i]["Method"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Method"]);
                    _BzProductDtl.Control = ds.Tables[0].Rows[i]["Control"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Control"]);
                    _BzProductDtl.Symptoms = ds.Tables[0].Rows[i]["Symptoms"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Symptoms"]);
                    _BzProductDtl.TimeofUse = ds.Tables[0].Rows[i]["TimeofUse"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TimeofUse"]);
                    _BzProductDtl.AdditionalInfo = ds.Tables[0].Rows[i]["AdditionalInfo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AdditionalInfo"]);
                    _BzProductDtl.Manufacturer = ds.Tables[0].Rows[i]["Manufacturer"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Manufacturer"]);
                    _BzProductDtl.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    _BzProductDtl.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VideoUrl"]);

                    _BzProductDetail.Add(_BzProductDtl);
                }
                BzProductDetail.BzPrdctDtl = _BzProductDetail;
            }
            return BzProductDetail;
        }

        public BzVegetableDetail GetBzVegetablesContent(string apikey, int ProductId)
        {
            BzVegetableDetail BzProductDetail = new BzVegetableDetail();
            DataSet ds = new ProductDetailDal().GetBzVegetablesContent(apikey, ProductId);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BzVegetableContent> _BzProductDetail = new List<BzVegetableContent>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BzVegetableContent _BzProductDtl = new BzVegetableContent();
                    _BzProductDtl.ProductId = ds.Tables[0].Rows[i]["ProductId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"].ToString());
                    _BzProductDtl.CropName = ds.Tables[0].Rows[i]["CropName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CropName"]);
                    _BzProductDtl.Species = ds.Tables[0].Rows[i]["Species"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Species"]);
                    _BzProductDtl.Manufacturer = ds.Tables[0].Rows[i]["Manufacturer"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Manufacturer"]);
                    _BzProductDtl.SeedingTime = ds.Tables[0].Rows[i]["SeedingTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SeedingTime"]);
                    _BzProductDtl.TransplantingTime = ds.Tables[0].Rows[i]["TransplantingTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TransplantingTime"]);
                    _BzProductDtl.SeedQty = ds.Tables[0].Rows[i]["SeedQty"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SeedQty"]);
                    _BzProductDtl.CropTime = ds.Tables[0].Rows[i]["CropTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CropTime"]);
                    _BzProductDtl.Distance = ds.Tables[0].Rows[i]["Distance"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Distance"]);
                    _BzProductDtl.YieldingPerAcre = ds.Tables[0].Rows[i]["YieldingPerAcre"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["YieldingPerAcre"]);
                    _BzProductDtl.FruitWeight = ds.Tables[0].Rows[i]["FruitWeight"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FruitWeight"]);
                    _BzProductDtl.CropSpeciality = ds.Tables[0].Rows[i]["CropSpeciality"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CropSpeciality"]);
                    _BzProductDtl.FruitSize = ds.Tables[0].Rows[i]["FruitSize"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FruitSize"]);
                    _BzProductDtl.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    _BzProductDtl.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VideoUrl"]);

                    _BzProductDetail.Add(_BzProductDtl);
                }
                BzProductDetail.BzVegetablesDtl = _BzProductDetail;
            }
            return BzProductDetail;
        }

        public BzFlowersDetail GetFlowersDetail(string apikey, int BzProductId)
        {
            BzFlowersDetail BzFlowerDetail = new BzFlowersDetail();
            DataSet ds = new ProductDetailDal().GetFlowersDetail(apikey, BzProductId);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BzFlowersContent> _BzFlowerDetail = new List<BzFlowersContent>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BzFlowersContent _BzFlowerDtl = new BzFlowersContent();
                    _BzFlowerDtl.BzProductId = ds.Tables[0].Rows[i]["BzProductId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BzProductId"].ToString());
                    _BzFlowerDtl.FlowerName = ds.Tables[0].Rows[i]["FlowerName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FlowerName"]);
                    _BzFlowerDtl.NoOfSeeds = ds.Tables[0].Rows[i]["NoOfSeeds"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NoOfSeeds"]);
                    _BzFlowerDtl.SowingTemperature = ds.Tables[0].Rows[i]["SowingTemperature"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SowingTemperature"]);
                    _BzFlowerDtl.SowingTime = ds.Tables[0].Rows[i]["SowingTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SowingTime"]);
                    _BzFlowerDtl.PlantType = ds.Tables[0].Rows[i]["PlantType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["PlantType"]);
                    _BzFlowerDtl.Spacing = ds.Tables[0].Rows[i]["Spacing"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Spacing"]);
                    _BzFlowerDtl.GerminationRequirement = ds.Tables[0].Rows[i]["GerminationRequirement"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["GerminationRequirement"]);
                    _BzFlowerDtl.SowingMethod = ds.Tables[0].Rows[i]["SowingMethod"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SowingMethod"]);
                    _BzFlowerDtl.BestFor = ds.Tables[0].Rows[i]["BestFor"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BestFor"]);
                    _BzFlowerDtl.GerminationTime = ds.Tables[0].Rows[i]["GerminationTime"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["GerminationTime"]);
                    _BzFlowerDtl.FertilizerRecommended = ds.Tables[0].Rows[i]["FertilizerRecommended"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FertilizerRecommended"]);
                    _BzFlowerDtl.SpecialFeatures = ds.Tables[0].Rows[i]["SpecialFeatures"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SpecialFeatures"]);
                    _BzFlowerDtl.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    _BzFlowerDtl.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VideoUrl"]);

                    _BzFlowerDetail.Add(_BzFlowerDtl);
                }
                BzFlowerDetail.BzVegetablesDtl = _BzFlowerDetail;
            }
            return BzFlowerDetail;
        }

        public string InsertReferal(string apikey, string ReferTo, string ReferBy)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().InsertReferal(apikey, ReferTo, ReferBy);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    refral.FarmerExist = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReturnValue"]);

                }
            }
            return refral.FarmerExist;
        }
        //InsertForCouponGenaration
        public Referal InsertForCouponGenaration(string apikey, string ReferTo, string ReferBy, int Amount)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().InsertForCouponGenaration(apikey, ReferTo, ReferBy, Amount);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    refral.Status = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? default(bool) : Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"]);

                }
            }
            return refral;
        }
        //CouponDetails

        //public List<Referal> CouponDetails(string apikey, string ReferBy,int BzProductId)
        //{
        //    Referal refral = new Referal();
        //    List<Referal> list = new List<Referal>();
        //    DataSet ds = new ProductDetailDal().CouponDetails(apikey, ReferBy, BzProductId);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {

        //        if (ds.Tables.Count == 2)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {

        //                refral.TotalCoupons = ds.Tables[0].Rows[i]["RemainingCoupons"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["RemainingCoupons"]);
        //                //list.Add(refral);
        //            }
        //            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
        //            {

        //                refral.PaymentMode = ds.Tables[2].Rows[i]["PaymentType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[i]["PaymentType"]);
        //                refral.DeliveryCharges = ds.Tables[0].Rows[i]["DeliveryCharges"] == DBNull.Value ? default(decimal) : Convert.ToInt32(ds.Tables[0].Rows[i]["DeliveryCharges"]);               
        //            }
        //            list.Add(refral);
        //        }
        //        if (ds.Tables.Count == 3)
        //        {

        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {

        //                refral.MinTransactionValue = ds.Tables[0].Rows[i]["MinTransValue"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["MinTransValue"]);
        //                refral.DiscAmount = ds.Tables[0].Rows[i]["DiscAmount"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["DiscAmount"]);
        //                refral.CouponCode = ds.Tables[0].Rows[i]["CouponCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CouponCode"]);
        //                refral.CouponDesc = ds.Tables[0].Rows[i]["CouponDesc"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CouponDesc"]);
        //                refral.ReferdBy = ds.Tables[0].Rows[i]["ReferBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReferBy"]);
        //                refral.ReferdTo = ds.Tables[0].Rows[i]["ReferTo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReferTo"]);
        //                refral.OfferType= ds.Tables[0].Rows[i]["OfferType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OfferType"]);
        //                // refral.TotalCoupons = ds.Tables[0].Rows[i]["TotalCoupons"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["TotalCoupons"]);
        //                // _BzProductDtl.YieldingPerAcre = ds.Tables[0].Rows[i]["YieldingPerAcre"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["YieldingPerAcre"]);
        //                // _BzProductDtl.FruitWeight = ds.Tables[0].Rows[i]["FruitWeight"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FruitWeight"]);
        //                //_BzProductDtl.CropSpeciality = ds.Tables[0].Rows[i]["CropSpeciality"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CropSpeciality"]);
        //                // refral.Status = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReturnValue"]);

        //            }
        //            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        //            {
        //                refral.TotalCoupons = ds.Tables[1].Rows[i]["RemainingCoupons"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[1].Rows[i]["RemainingCoupons"]);

        //            }
        //            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
        //            {

        //                refral.PaymentMode = ds.Tables[2].Rows[i]["PaymentType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[i]["PaymentType"]);
        //                refral.DeliveryCharges = ds.Tables[0].Rows[i]["DeliveryCharges"] == DBNull.Value ? default(decimal) : Convert.ToInt32(ds.Tables[0].Rows[i]["DeliveryCharges"]);

        //               // list.Add(refral);
        //            }
        //            list.Add(refral);
        //        }

        //    }
        //    return list;
        //}
        public PaymentOptionList CouponDetails(string apikey, string ReferBy, int BzProductId)
        {
            PaymentOptionList PaymentOptions = new PaymentOptionList();
            List<CouponDetail> _BzCouponDetails = new List<CouponDetail>();
            DataSet ds = new ProductDetailDal().CouponDetails(apikey, ReferBy, BzProductId);
            if (ds != null && ds.Tables.Count > 0)
            {               
                List<PaymentOptions> _BzPaymentOptions = new List<PaymentOptions>();
                
                if (ds.Tables.Count == 3)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {                    
                        CouponDetail _CouponDetail = new CouponDetail();

                        _CouponDetail.MinTransactionValue = ds.Tables[0].Rows[i]["MinTransValue"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["MinTransValue"]);
                        _CouponDetail.DiscAmount = ds.Tables[0].Rows[i]["DiscAmount"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["DiscAmount"]);
                        _CouponDetail.CouponCode = ds.Tables[0].Rows[i]["CouponCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CouponCode"]);
                        _CouponDetail.CouponDesc = ds.Tables[0].Rows[i]["CouponDesc"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CouponDesc"]);
                        _CouponDetail.ReferdBy = ds.Tables[0].Rows[i]["ReferBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReferBy"]);
                        _CouponDetail.ReferdTo = ds.Tables[0].Rows[i]["ReferTo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ReferTo"]);
                        _CouponDetail.OfferType = ds.Tables[0].Rows[i]["OfferType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OfferType"]);

                        _CouponDetail.TotalCoupons= ds.Tables[1].Rows[i]["RemainingCoupons"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[1].Rows[i]["RemainingCoupons"]);
                     
                        _BzCouponDetails.Add(_CouponDetail);                       
                    }

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        PaymentOptions _PaymentOption = new PaymentOptions();
                        _PaymentOption.PaymentMode = ds.Tables[2].Rows[i]["PaymentType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[i]["PaymentType"]);
                        _PaymentOption.DeliveryCharges = ds.Tables[2].Rows[i]["DeliveryCharges"] == DBNull.Value ? default(decimal) : Convert.ToInt32(ds.Tables[2].Rows[i]["DeliveryCharges"]);
                        _PaymentOption.PaymentModeText = ds.Tables[2].Rows[i]["PaymentModeText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[i]["PaymentModeText"]);
                        _PaymentOption.DeliveryChargeText = ds.Tables[2].Rows[i]["DeliveryChargeText"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[2].Rows[i]["DeliveryChargeText"]);

                        _BzPaymentOptions.Add(_PaymentOption);
                    }
                    PaymentOptions.CouponDetails = _BzCouponDetails;
                    PaymentOptions.PaymentOptions = _BzPaymentOptions;
                }
            }
            return PaymentOptions;
        }

        public BzLeftMenuList GetBZLeftMenu(string apikey)
        {
            BzLeftMenuList BzLeftMenu = new BzLeftMenuList();
            DataSet ds = new ProductDetailDal().GetBZLeftMenu(apikey);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BzAppLeftMenu> _BzLeftMenu = new List<BzAppLeftMenu>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BzAppLeftMenu _BzAppMenu = new BzAppLeftMenu();

                    _BzAppMenu.MenuId = ds.Tables[0].Rows[i]["MenuId"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["MenuId"]);
                    _BzAppMenu.MenuName = ds.Tables[0].Rows[i]["MenuName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MenuName"]);
                    _BzAppMenu.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    _BzAppMenu.MenuHindiName = ds.Tables[0].Rows[i]["MenuHindiName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MenuHindiName"]);

                    _BzLeftMenu.Add(_BzAppMenu);
                }
                BzLeftMenu.BzLeftMenu = _BzLeftMenu;
            }
            return BzLeftMenu;
        }
        // FarmerLogin
        public List<Referal> FarmerLogin(string MobileNo, string ShareCode, int FarmerId, string DeviceId)
        {
            //BzShareLink url = new BzShareLink();

            string ReferencedCode = ShareCode;
            string BzLongUrl = string.Empty;
            string BzShareUrl = String.Empty;
            string login = "o_6oj17fogqh";
            string apikey = "R_7f5f33fcb3344a7b80e285c67d3a0570";
            // CryptorEngine.Encrypt(MobileNo, true);
            string RandomCode = CommonBal.GetRandomAlphaNumericCode();
            //  string RandomCode = ValidShareCode();


            // BzLongUrl = "https://www.behtarzindagi.in/bz_productdetails_test/ProductDetail.svc/api/GetBZappShareLinkUrl?source=bzsource&campaign=bzcampaign&ShareCode="+ RandomCode;

            BzShareUrl = "https://www.behtarzindagi.in/bz_productdetails_test/ProductDetail.svc/api/GetBZappShareLinkUrl?source=bzsource&campaign=bzcampaign&ShareCode=" + RandomCode;
            //CommonBal.Shorten(BzLongUrl, login, apikey);
            // BzShareUrl = "https://play.google.com/store/apps/details?id=com.behtarzindagi.consumer&referrer=utm_source%3Dbz%26utm_content%3D" + RandomCode + "%26utm_campaign%3DBZCampaign";
            var userList = new List<Referal>();
            DataSet ds = new ProductDetailDal().GetUserDetail(MobileNo, RandomCode, BzShareUrl, ReferencedCode, FarmerId, DeviceId);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //FarmerID
                        userList.Add(new Referal
                        {
                            StateId = ds.Tables[0].Rows[i]["StateID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateID"]),
                            FirstName = ds.Tables[0].Rows[i]["FName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FName"]),
                            LastName = ds.Tables[0].Rows[i]["LName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LName"]),
                            Address = ds.Tables[0].Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShippingAddress"]),
                            DistrictId = ds.Tables[0].Rows[i]["DistrictID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictID"]),
                            BlockId = ds.Tables[0].Rows[i]["BlockID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockID"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobNo"]),
                            VillageId = ds.Tables[0].Rows[i]["VillageID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageID"]),
                            // PinCode = ds.Tables[0].Rows[i]["Pin"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Pin"]),
                            ShippingAddressId = i,
                            StateName = ds.Tables[0].Rows[i]["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateName"]),
                            DistrictName = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]),
                            BlockName = ds.Tables[0].Rows[i]["BlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockName"]),
                            VillageName = ds.Tables[0].Rows[i]["VillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageName"]),
                            FatherName = ds.Tables[0].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]),
                            Landmark = ds.Tables[0].Rows[i]["LandMark"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LandMark"]),
                            NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NearByVillage"]),
                            Status = true,
                            BzShareUrl = ds.Tables[0].Rows[i]["ShareUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShareUrl"]),
                            RandomCode = ds.Tables[0].Rows[i]["ShareCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShareCode"]),
                            FarmerID = ds.Tables[0].Rows[i]["FarmerID"] == DBNull.Value ? default(Int64) : Convert.ToInt64(ds.Tables[0].Rows[i]["FarmerID"]),
                            ReferencedCode = ds.Tables[0].Rows[i]["RefrencedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RefrencedBy"])

                        });
                    }
                }
            }
            else
            {
                userList.Add(new Referal()
                {
                    Status = false

                });
            }
            return userList;

        }

        public bool UpdateFarmerAddress(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, string NearByVillage, string Address)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().UpdateFarmerAddress(apiKey, FarmerId, RefSource, Fname, Lname, FatherName, Mobile, StateId, DistrictId, BlockId, VillageId, NearByVillage, Address);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    refral.Status = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? default(bool) : Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"]);

                }
            }
            return refral.Status;
        }

        public bool UpdateFarmerAddressN(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, string NearByVillage, string Address, string Landmark, string Pincode, string Email)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().UpdateFarmerAddressN(apiKey, FarmerId, RefSource, Fname, Lname, FatherName, Mobile, StateId, DistrictId, BlockId, VillageId, NearByVillage, Address, Landmark, Pincode, Email);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    refral.Status = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? default(bool) : Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"]);

                }
            }
            return refral.Status;
        }

        // By lalit
        public BzShareLink GetBzAppShareLink(string source, string campaign, string ShareCode)
        {
            BzShareLink url = new BzShareLink();
            string strShareLink = String.Empty;
            //string RandomCode = CommonBal.GetRandomAlphaNumericCode();
            string login = "o_6oj17fogqh";
            string apikey = "R_7f5f33fcb3344a7b80e285c67d3a0570";

            // string longurl= "https://play.google.com/store/apps/details?id=com.behtarzindagi.consumer&referrer=utm_source%3D"+source+"%26utm_content%3D"+ShareCode+ "%26utm_campaign%3D"+campaign;
            url.BzShareUrl = "https://play.google.com/store/apps/details?id=com.behtarzindagi.consumer&referrer=utm_source%3D" + source + "%26utm_content%3D" + ShareCode + "%26utm_campaign%3D" + campaign;
            //CommonBal.Shorten(longurl, login, apikey);

            return url;
        }
        public BZProduct GetBzProducts(int CategoryId, int DistrictId, string DistrictName, string PinCode, int PageIndex, int PageSize)
        {
            // BzProduct _BzPrdct2 = new BzProduct();
            BZProduct BZProductList = new BZProduct();
            DataSet ds = new ProductDetailDal().GetBzProducts(CategoryId, DistrictId, DistrictName, PinCode, PageIndex, PageSize);
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
                        _BzPrdct.CategoryHindi = ds.Tables[0].Rows[i]["CategoryHindi"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CategoryHindi"]);
                        _BzPrdct.CategoryId = ds.Tables[0].Rows[i]["CategoryId"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryId"]);
                        _BzPrdct.SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["SubCategoryName"]);
                        _BzPrdct.CreatedDate = ds.Tables[0].Rows[i]["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]);
                        _BzPrdct.OurPrice = ds.Tables[0].Rows[i]["OurPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OurPrice"]);
                        // _BzPrdct.OfferPrice = ds.Tables[0].Rows[i]["OfferPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OfferPrice"]);
                        _BzPrdct.UnitName = ds.Tables[0].Rows[i]["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UnitName"]);
                        _BzPrdct.ImagePath = ds.Tables[0].Rows[i]["ImagePath"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImagePath"]);
                        _BzPrdct.BzProductId = ds.Tables[0].Rows[i]["RecordID"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                        _BzPrdct.IsActive = ds.Tables[0].Rows[i]["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        _BzPrdct.OfferPrice_Qty = ds.Tables[0].Rows[i]["OfferPrice_Qty"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["OfferPrice_Qty"]);
                        _BzPrdct.OnlinePrice = ds.Tables[0].Rows[i]["OnlinePrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OnlinePrice"]);
                        _BzPrdct.COD = ds.Tables[0].Rows[i]["COD"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["COD"]);
                        _BzPrdct.MRP = ds.Tables[0].Rows[i]["MRP"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["MRP"]);
                        _BzPrdct.OfferDiscount = ds.Tables[0].Rows[i]["OfferDiscount"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OfferDiscount"]);
                        _BzPrdct.IsDetails = ds.Tables[0].Rows[i]["IsDetails"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[i]["IsDetails"]);

                        _BzPrdctList.Add(_BzPrdct);
                    }
                    BZProductList.TotalCount = ds.Tables[1].Rows[0]["TotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);

                    BZProductList.BzProduct = _BzPrdctList; //public int TotalCount { get; set; } 
                    //BZProductList.TotalCount = _BzPrdct2.TotalCount;
                }
            }
            return BZProductList;
        }
        public bool ProductAvailableByDistrict(string apiKey, int DistrictId, int PackageId)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().ProductAvailableByDistrict(apiKey, DistrictId, PackageId);

            if (DistrictId == 0)
            {
                refral.Status = true;
            }
            else
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        refral.Status = true;
                    }
                    else
                    {
                        refral.Status = false;
                    }
                }
            }

            return refral.Status;
        }

        public BZProductInterestCount GetBZProductInterestCount(int PackID, string MobNo)
        {
            BZProductInterestCount BZProductInterestList = new BZProductInterestCount();
            DataSet ds = new ProductDetailDal().BZProductInterestCount(PackID, MobNo);
            if (ds != null && ds.Tables.Count > 0)
            {
                BZProductInterestList.TotalCount = ds.Tables[0].Rows[0]["TotalCount"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
            }
            return BZProductInterestList;
        }
        public string FarmerAppInstallationInfo(string apiKey, string DeviceId, string ReferencedBy)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().FarmerAppInstallationInfo(apiKey, DeviceId, ReferencedBy);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    refral.Status = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? default(bool) : Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"]);
                }
            }
            return ReferencedBy;
        }

        public bool UpdateFCMId(string apiKey, string DeviceId, string FCMId)
        {
            Referal refral = new Referal();
            DataSet ds = new ProductDetailDal().UpdateFCMId(apiKey, DeviceId, FCMId);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    refral.Status = ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? default(bool) : Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"]);

                }
            }
            return refral.Status;
        }

        public string GetFCMId(string DistrictId, string PackageId, string SDate, string EDate, int PageIndex, int PageSize)
        {
            BZFCMIdList BZFcmId = new BZFCMIdList();
            DataSet ds = new ProductDetailDal().GetFCMId(DistrictId, PackageId, SDate, EDate, PageIndex, PageSize);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<string> FCMList = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //BZFCMId _BzFcm = new BZFCMId();
                    //_BzFcm.FCMId
                    FCMList.Add(ds.Tables[0].Rows[i]["FCMId"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FCMId"]));

                    //FCMList.Add(_BzFcm);
                }
                BZFcmId.FCMId = string.Join(",", FCMList.ToArray());
            }
            return BZFcmId.FCMId;
        }

        public StateDistrictBlockVillageList GetStateDistrictBlockVilage(int Id, string Type)
        {
            StateDistrictBlockVillageList BZStateDist = new StateDistrictBlockVillageList();
            DataSet ds = new ProductDetailDal().GetStateDistrictBlockVilage(Id, Type);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<StateDistrictBlockVillage> stateDistList = new List<StateDistrictBlockVillage>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StateDistrictBlockVillage _StateDist = new StateDistrictBlockVillage();
                    _StateDist.Id = ds.Tables[0].Rows[i]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    _StateDist.Name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    stateDistList.Add(_StateDist);
                }
                BZStateDist.List = stateDistList;
            }
            return BZStateDist;
        }

        public StateDistrictBlockVillageList GetAllStateDistrictBlockVilage(int Id, string Type)
        {
            StateDistrictBlockVillageList BZStateDist = new StateDistrictBlockVillageList();
            DataSet ds = new ProductDetailDal().GetAllStateDistrictBlockVilage(Id, Type);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<StateDistrictBlockVillage> stateDistList = new List<StateDistrictBlockVillage>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StateDistrictBlockVillage _StateDist = new StateDistrictBlockVillage();
                    _StateDist.Id = ds.Tables[0].Rows[i]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    _StateDist.Name = ds.Tables[0].Rows[i]["Name"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    stateDistList.Add(_StateDist);
                }
                BZStateDist.List = stateDistList;
            }
            return BZStateDist;
        }

        public int CreateFarmerLead(string FName, string LName, string MobNo, int StateId, string StateName, int DistrictId, string DistrictName, int BlockId,
                string BlockName, int VillageId, string VillageName, string NearbyVillage, string AdditionalAddress)
        {
            LeadCreateModel objLead = new LeadCreateModel();
            DataSet ds = new ProductDetailDal().CreateFarmerLead(FName, LName, MobNo, StateId, StateName, DistrictId, DistrictName, BlockId,
                BlockName, VillageId, VillageName, NearbyVillage, AdditionalAddress);

            if (ds != null && ds.Tables.Count > 0)
            {
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                objLead.UserId = ds.Tables[0].Rows[0]["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"].ToString());
                // }
            }
            return objLead.UserId;
        }

        public BzProductDtl GetBzLiveStockDetails(string apikey, int ProductId)
        {
            BzProductDtl BzProductDetail = new BzProductDtl();
            DataSet ds = new ProductDetailDal().GetBzLiveStockDetails(apikey, ProductId);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BzProductDetails> _BzProductDetail = new List<BzProductDetails>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)


                {
                    BzProductDetails _BzProductDtl = new BzProductDetails();

                    _BzProductDtl.Product = ds.Tables[0].Rows[i]["Product"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Product"]);
                    _BzProductDtl.BrandName = ds.Tables[0].Rows[i]["Brand"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Brand"]);
                    _BzProductDtl.Creator = ds.Tables[0].Rows[i]["Creator"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Creator"]);
                    _BzProductDtl.Purpose = ds.Tables[0].Rows[i]["Purpose"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Purpose"]);
                    _BzProductDtl.Dose = ds.Tables[0].Rows[i]["Dose"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Dose"]);
                    _BzProductDtl.Features = ds.Tables[0].Rows[i]["Features"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Features"]);
                    _BzProductDtl.Weight = ds.Tables[0].Rows[i]["Weight"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Weight"]);
                    _BzProductDtl.RecordId = ds.Tables[0].Rows[i]["RecordId"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["RecordId"]);
                    //_BzProductDtl.TimeofUse = ds.Tables[0].Rows[i]["TimeofUse"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TimeofUse"]);
                    //_BzProductDtl.AdditionalInfo = ds.Tables[0].Rows[i]["AdditionalInfo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["AdditionalInfo"]);
                    //_BzProductDtl.Manufacturer = ds.Tables[0].Rows[i]["Manufacturer"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Manufacturer"]);
                    _BzProductDtl.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    _BzProductDtl.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VideoUrl"]);

                    _BzProductDetail.Add(_BzProductDtl);
                }
                BzProductDetail.BzPrdctDtl = _BzProductDetail;
            }
            return BzProductDetail;
        }
        public BzProductDtl GetBzMachineryDetails(string apikey, int ProductId)
        {
            BzProductDtl BzProductDetail = new BzProductDtl();
            DataSet ds = new ProductDetailDal().GetBzMachineryDetails(apikey, ProductId);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BzProductDetails> _BzProductDetail = new List<BzProductDetails>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)


                {
                    BzProductDetails _BzProductDtl = new BzProductDetails();

                    _BzProductDtl.RecordId = ds.Tables[0].Rows[i]["RecordId"] == DBNull.Value ? default(int) : Convert.ToInt32(ds.Tables[0].Rows[i]["RecordId"]);
                    _BzProductDtl.Manufacturer = ds.Tables[0].Rows[i]["Manufacturer"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Manufacturer"]);
                    _BzProductDtl.BrandName = ds.Tables[0].Rows[i]["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BrandName"]);
                    _BzProductDtl.ProductName = ds.Tables[0].Rows[i]["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]);
                    _BzProductDtl.Capacity = ds.Tables[0].Rows[i]["Capacity"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Capacity"]);
                    _BzProductDtl.ModelNo = ds.Tables[0].Rows[i]["ModelNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModelNo"]);
                    _BzProductDtl.UseMethod = ds.Tables[0].Rows[i]["UseMethod"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["UseMethod"]);
                    _BzProductDtl.Description = ds.Tables[0].Rows[i]["Description"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Description"]);
                    //_BzProductDtl.TractorPTO = ds.Tables[0].Rows[i]["TractorPTO"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["TractorPTO"]);
                    //_BzProductDtl.Transmission = ds.Tables[0].Rows[i]["Transmission"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Transmission"]);
                    //_BzProductDtl.GearBox = ds.Tables[0].Rows[i]["GearBox"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["GearBox"]);
                    _BzProductDtl.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    _BzProductDtl.VideoUrl = ds.Tables[0].Rows[i]["VideoUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VideoUrl"]);

                    _BzProductDetail.Add(_BzProductDtl);
                }
                BzProductDetail.BzPrdctDtl = _BzProductDetail;
            }
            return BzProductDetail;
        }



        public BZOfferBannerList GetBzOfferBanner(string apikey)
        {
            BZOfferBannerList ObjOfferBanner = new BZOfferBannerList();
            DataSet ds = new ProductDetailDal().GetBzOfferBanner(apikey);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<BZOfferBanner> _BzOfferBannerDetail = new List<BZOfferBanner>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BZOfferBanner _BzOfferBannerDtl = new BZOfferBanner();

                    _BzOfferBannerDtl.BannerId = ds.Tables[0].Rows[i]["BannerId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["BannerId"]);
                    _BzOfferBannerDtl.PackageId = ds.Tables[0].Rows[i]["PackageId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["PackageId"]);
                    _BzOfferBannerDtl.OfferName = ds.Tables[0].Rows[i]["OfferName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["OfferName"]);
                    _BzOfferBannerDtl.Weight = ds.Tables[0].Rows[i]["Weight"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Weight"]);
                    _BzOfferBannerDtl.MRP = ds.Tables[0].Rows[i]["MRP"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["MRP"]);
                    _BzOfferBannerDtl.COD = ds.Tables[0].Rows[i]["COD"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["COD"]);
                    _BzOfferBannerDtl.OnlinePrice = ds.Tables[0].Rows[i]["OnlinePrice"] == DBNull.Value ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["OnlinePrice"]);
                    _BzOfferBannerDtl.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ImageUrl"]);
                    //_BzOfferBannerDtl.CreatedOn = ds.Tables[0].Rows[i]["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["CreatedOn"]);
                    //_BzOfferBannerDtl.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]);
                    //_BzOfferBannerDtl.ModifiedOn = ds.Tables[0].Rows[i]["ModifiedOn"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ModifiedOn"]);
                    // _BzOfferBannerDtl.ModifiedBy = ds.Tables[0].Rows[i]["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ModifiedBy"]);
                    _BzOfferBannerDtl.DistrictId = ds.Tables[0].Rows[i]["DistrictId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["DistrictId"]);
                    _BzOfferBannerDtl.WebImageUrl = ds.Tables[0].Rows[i]["WebImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["WebImageUrl"]);
                    _BzOfferBannerDtl.MobImageUrl = ds.Tables[0].Rows[i]["MobImageUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobImageUrl"]);

                    _BzOfferBannerDetail.Add(_BzOfferBannerDtl);
                }
                ObjOfferBanner.BzOfferBannerList = _BzOfferBannerDetail;
            }
            return ObjOfferBanner;
        }
        public List<Referal> BZFarmerAPPLogin(string MobileNo, string ShareCode, int FarmerId, string DeviceId, string Name)
        {
            //BzShareLink url = new BzShareLink();

            string ReferencedCode = ShareCode;
            string BzLongUrl = string.Empty;
            string BzShareUrl = String.Empty;
            string login = "o_6oj17fogqh";
            string apikey = "R_7f5f33fcb3344a7b80e285c67d3a0570";
            // CryptorEngine.Encrypt(MobileNo, true);
            string RandomCode = CommonBal.GetRandomAlphaNumericCode();
            //  string RandomCode = ValidShareCode();


            // BzLongUrl = "https://www.behtarzindagi.in/bz_productdetails_test/ProductDetail.svc/api/GetBZappShareLinkUrl?source=bzsource&campaign=bzcampaign&ShareCode="+ RandomCode;

            BzShareUrl = "https://www.behtarzindagi.in/bz_productdetails_test/ProductDetail.svc/api/GetBZappShareLinkUrl?source=bzsource&campaign=bzcampaign&ShareCode=" + RandomCode;
            //CommonBal.Shorten(BzLongUrl, login, apikey);
            // BzShareUrl = "https://play.google.com/store/apps/details?id=com.behtarzindagi.consumer&referrer=utm_source%3Dbz%26utm_content%3D" + RandomCode + "%26utm_campaign%3DBZCampaign";
            var userList = new List<Referal>();
            DataSet ds = new ProductDetailDal().GetUserDetailBZFarmer(MobileNo, RandomCode, BzShareUrl, ReferencedCode, FarmerId, DeviceId, Name);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //FarmerID
                        userList.Add(new Referal
                        {
                            StateId = ds.Tables[0].Rows[i]["StateID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateID"]),
                            FirstName = ds.Tables[0].Rows[i]["FName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FName"]),
                            LastName = ds.Tables[0].Rows[i]["LName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LName"]),
                            Address = ds.Tables[0].Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShippingAddress"]),
                            DistrictId = ds.Tables[0].Rows[i]["DistrictID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictID"]),
                            BlockId = ds.Tables[0].Rows[i]["BlockID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockID"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobNo"]),
                            VillageId = ds.Tables[0].Rows[i]["VillageID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageID"]),
                            // PinCode = ds.Tables[0].Rows[i]["Pin"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Pin"]),
                            ShippingAddressId = i,
                            StateName = ds.Tables[0].Rows[i]["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateName"]),
                            DistrictName = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]),
                            BlockName = ds.Tables[0].Rows[i]["BlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockName"]),
                            VillageName = ds.Tables[0].Rows[i]["VillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageName"]),
                            FatherName = ds.Tables[0].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]),
                            Landmark = ds.Tables[0].Rows[i]["LandMark"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LandMark"]),
                            NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NearByVillage"]),
                            Status = true,
                            BzShareUrl = ds.Tables[0].Rows[i]["ShareUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShareUrl"]),
                            RandomCode = ds.Tables[0].Rows[i]["ShareCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShareCode"]),
                            FarmerID = ds.Tables[0].Rows[i]["FarmerID"] == DBNull.Value ? default(Int64) : Convert.ToInt64(ds.Tables[0].Rows[i]["FarmerID"]),
                            ReferencedCode = ds.Tables[0].Rows[i]["RefrencedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RefrencedBy"]),
                            LoginType= ds.Tables[0].Rows[i]["LoginType"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LoginType"])

                        });
                    }
                }
            }
            else
            {
                userList.Add(new Referal()
                {
                    Status = false

                });
            }
            return userList;

        }
        public List<Referal> BZFarmerAPP_WebLogin(Referal objAppWebLogin)
        {
            var userList = new List<Referal>();
            DataSet ds = new ProductDetailDal().GetUserDetailBZFarmer(objAppWebLogin);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //FarmerID
                        userList.Add(new Referal
                        {
                            StateId = ds.Tables[0].Rows[i]["StateID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateID"]),
                            FirstName = ds.Tables[0].Rows[i]["FName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FName"]),
                            LastName = ds.Tables[0].Rows[i]["LName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LName"]),
                            Email= ds.Tables[0].Rows[i]["Email"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Email"]),
                            Address = ds.Tables[0].Rows[i]["ShippingAddress"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShippingAddress"]),
                            DistrictId = ds.Tables[0].Rows[i]["DistrictID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictID"]),
                            BlockId = ds.Tables[0].Rows[i]["BlockID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockID"]),
                            MobileNumber = ds.Tables[0].Rows[i]["MobNo"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MobNo"]),
                            VillageId = ds.Tables[0].Rows[i]["VillageID"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageID"]),
                            // PinCode = ds.Tables[0].Rows[i]["Pin"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["Pin"]),
                            ShippingAddressId = i,
                            StateName = ds.Tables[0].Rows[i]["StateName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["StateName"]),
                            DistrictName = ds.Tables[0].Rows[i]["DistrictName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["DistrictName"]),
                            BlockName = ds.Tables[0].Rows[i]["BlockName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BlockName"]),
                            VillageName = ds.Tables[0].Rows[i]["VillageName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VillageName"]),
                            FatherName = ds.Tables[0].Rows[i]["FatherName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]),
                            Landmark = ds.Tables[0].Rows[i]["LandMark"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["LandMark"]),
                            NearByVillage = ds.Tables[0].Rows[i]["NearByVillage"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["NearByVillage"]),
                            Status = true,
                            BzShareUrl = ds.Tables[0].Rows[i]["ShareUrl"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShareUrl"]),
                            RandomCode = ds.Tables[0].Rows[i]["ShareCode"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ShareCode"]),
                            FarmerID = ds.Tables[0].Rows[i]["FarmerID"] == DBNull.Value ? default(Int64) : Convert.ToInt64(ds.Tables[0].Rows[i]["FarmerID"]),
                            ReferencedCode = ds.Tables[0].Rows[i]["RefrencedBy"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["RefrencedBy"])

                        });
                    }
                }
            }
            else
            {
                userList.Add(new Referal()
                {
                    Status = false

                });
            }
            return userList;

        }

        public bool SaveVegetablePrice(string apiKey, string Name, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, int VegetableId, string VegeVariety, string VegeAmount, string VegPrice)
        {
            VegeResponseStatus status = new VegeResponseStatus();
            DataSet ds = new ProductDetailDal().SaveVegetablePrice(Constant.usp_SaveVegetablePrice, Name, Mobile, StateId, DistrictId, BlockId, VillageId, VegetableId, VegeVariety, VegeAmount, VegPrice);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    status.Success = Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"] == DBNull.Value ? default(bool) : Convert.ToBoolean(ds.Tables[0].Rows[i]["ReturnValue"]));

                }
            }
            return status.Success;
        }
        public VegetableList GetVegetables(string apikey)
        {
            VegetableList objVege = new VegetableList();
            DataSet ds = new ProductDetailDal().GetVegetables(apikey);
            if (ds != null && ds.Tables.Count > 0)
            {
                List<Vegetables> _BzVegetables = new List<Vegetables>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Vegetables _Vege = new Vegetables();

                    _Vege.VegId = ds.Tables[0].Rows[i]["VegId"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["VegId"]);
                    _Vege.VegeName = ds.Tables[0].Rows[i]["VegName"] == DBNull.Value ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["VegName"]);


                    _BzVegetables.Add(_Vege);
                }
                objVege.VegeList = _BzVegetables;
            }
            return objVege;
        }
    }
}
