using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Constant
    {
        public const string UserMobileValidMsg = "Mobile number should be 10 digit.";
        public const string TransporterMobileValidMsg = "Transpoter Mobile number should be 10 digit.";
        public const string MobileNoValid = "Please enter valid mobile number.";
        public const string GetOrderList = "uspOrderList_APP";
        // public const string uspBZProducts = "SP_BZ_PRODUCTLIST"; // commented by Lalit
        public const string uspBZProducts = "SP_BZ_PRODUCTLIST_New";
        public const string uspBZComboProducts = "Usp_Get_BZComboProductList";
        public const string uspBZProductBanner = "Usp_GetBZProductBanner";
        public const string uspBZProductDetail = "Usp_GetBzProductDetail";
        public const string GetBZ_VegetablesContent = "GetBZ_SeedDetail";
        public const string GetBZFlowersDetail = "usp_GetBZFlowersDetail";
        //by asim for coupon sp_CouponDetails
        public const string sp_ReferenceByfarmer = "sp_ReferenceByfarmer";
        public const string sp_RefererCouponDetail = "sp_RefererCouponDetail";
        public const string sp_CouponDetails = "sp_CouponDetails"; //Usp_UpdateFarmerAddress sp_ShareCodeExist
        public const string Usp_UpdateFarmerAddress = "Usp_UpdateFarmerAddress";
        public const string Usp_UpdateFarmerAddressN = "Usp_UpdateFarmerAddressN";
        public const string usp_SaveVegetablePrice = "usp_SaveVegetablePrice";
        public const string usp_ProductAvailableByDistrict = "usp_ProductAvailableByDistrict";

        public const string sp_ShareCodeExist = "sp_ShareCodeExist";
        // sp_FarmerLogin

        public const string sp_FarmerLogin = "sp_FarmerLogin";
        public const string sp_FarmerLoginBZApp = "sp_FarmerLogin_BZApp";

        public const string uspBZProductInterestCount = "USP_UpdateandGetProductInterestCount"; // by akaram ali on 29 sep 2018
        public const string Usp_FarmerAppInstallationInfo = "Sp_FarmerAppInstallationInfo";
        public const string Usp_UpdateFCMId = "USP_UpdateFCMId";
        public const string USP_GetFCMId = "USP_GetFCMId";
        public const string USP_GetBZStateDistrictBlockVillage = "usp_GetBZStateDistrictBlockVillage";
        public const string USP_GetAllStateDistrictBlockVillage = "usp_GetAllStateDistrictBlockVillage";

        public const string Usp_CreateFarmerLead = "usp_CreateFarmerLead";
        public const string uspGetLiveStockDetails = "usp_GetLiveStockDetails";
        public const string usp_GetBzMachineDetails = "usp_GetBzMachineDetails";

        public const string usp_GetBZLeftMenu = "usp_GetBZLeftMenu";
        public const string uspGetBZOfferBanners = "usp_GetBZ_OfferBanners";
        public const string uspGetSathiSalesReport = "usp_Get_SathiSalesReport";
        public const string uspGetSathiPaymentReport = "usp_GetBZSathiPaymentDetails";
        public const string uspGetVegetables = "usp_GetVegetables";

        public const string usp_BzFarmerAppWebLogin = "usp_BzFarmerAppWeb_Login";
    }
}
