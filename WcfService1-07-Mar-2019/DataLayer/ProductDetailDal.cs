using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Entity;

namespace DataLayer
{
    public class ProductDetailDal : BaseDal
    {
        public DataSet GetBzProductDetail(string apikey, int ProductId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspBZProductDetail, ProductId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }

        public DataSet GetBzVegetablesContent(string apikey, int ProductId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.GetBZ_VegetablesContent, ProductId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        public DataSet GetFlowersDetail(string apikey, int ProductId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.GetBZFlowersDetail, ProductId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        

        public DataSet InsertReferal(string apikey, string ReferTo, string ReferBy)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.sp_ReferenceByfarmer, ReferTo, ReferBy))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        //InsertForCouponGenaration
        public DataSet InsertForCouponGenaration(string apikey, string ReferTo, string ReferBy, int Amount)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.sp_RefererCouponDetail, ReferTo, ReferBy, Amount))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        //CouponDetails
        public DataSet CouponDetails(string apikey, string ReferBy,int BzProductId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.sp_CouponDetails, ReferBy, BzProductId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        public DataSet GetUserDetail(string MobileNo, string RandomCode, string BzShareUrl, string ReferencedCode, int FarmerId, string DeviceId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.sp_FarmerLogin, Convert.ToDecimal(MobileNo), RandomCode, BzShareUrl, ReferencedCode, FarmerId, DeviceId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }
        public DataSet UpdateFarmerAddress(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, string NearByVillage, string Address)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.Usp_UpdateFarmerAddress, FarmerId, RefSource, Fname, Lname, FatherName, Mobile, StateId, DistrictId, BlockId, VillageId, NearByVillage, Address))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }
        public DataSet UpdateFarmerAddressN(string apiKey, int FarmerId, string RefSource, string Fname, string Lname, string FatherName, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, string NearByVillage, string Address, string Landmark, string Pincode,string Email)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.Usp_UpdateFarmerAddressN, FarmerId, RefSource, Fname, Lname, FatherName, Mobile, StateId, DistrictId, BlockId, VillageId, NearByVillage, Address,Landmark, Pincode,Email))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }

        public DataSet GetBzProducts(int CategoryId, int DistrictId, string DistrictName, string PinCode, int PageIndex, int PageSize)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspBZProducts, CategoryId, DistrictId, DistrictName, PinCode, PageIndex, PageSize))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }

        }
        public DataSet ProductAvailableByDistrict(string apiKey, int DistrictId, int PackageId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.usp_ProductAvailableByDistrict, DistrictId, PackageId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }

        public DataSet ShareCodeexist(string RandomCode)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.sp_ShareCodeExist, RandomCode))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }
        // by akaram ali on 29 sep 2018
        public DataSet BZProductInterestCount(int PackID, string MobNo)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspBZProductInterestCount, PackID, MobNo))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }

        }

        public DataSet FarmerAppInstallationInfo(string apiKey, string DeviceId, string ReferencedBy)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.Usp_FarmerAppInstallationInfo, DeviceId, ReferencedBy))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }
        public DataSet UpdateFCMId(string apiKey, string DeviceId, string FCMId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.Usp_UpdateFCMId, DeviceId, FCMId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;
            }
        }
        public DataSet GetFCMId(string DistrictId, string PackageId, string SDate, string EDate, int PageIndex, int PageSize)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.USP_GetFCMId, DistrictId, PackageId, SDate, EDate, PageIndex, PageSize))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }

        public DataSet GetStateDistrictBlockVilage(int Id, string Type)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.USP_GetBZStateDistrictBlockVillage, Id, Type))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }

        public DataSet GetAllStateDistrictBlockVilage(int Id, string Type)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.USP_GetAllStateDistrictBlockVillage, Id, Type))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }

        public DataSet CreateFarmerLead(string FName, string LName, string MobNo, int StateId, string StateName, int DistrictId, string DistrictName, int BlockId,
                string BlockName, int VillageId, string VillageName, string NearbyVillage, string AdditionalAddress) //, DataTable DT
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.Usp_CreateFarmerLead, FName, LName, MobNo, StateId, StateName, DistrictId, DistrictName, BlockId,
                BlockName, VillageId, VillageName, NearbyVillage, AdditionalAddress))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }

        public DataSet GetBzLiveStockDetails(string apikey, int ProductId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspGetLiveStockDetails, ProductId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }

        public DataSet GetBzMachineryDetails(string apikey, int ProductId)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.usp_GetBzMachineDetails, ProductId))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }

        public DataSet GetBZLeftMenu(string apikey)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.usp_GetBZLeftMenu))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        public DataSet GetBzOfferBanner(string apikey)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspGetBZOfferBanners))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        public DataSet GetUserDetailBZFarmer(string MobileNo, string RandomCode, string BzShareUrl, string ReferencedCode, int FarmerId, string DeviceId, string Name)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.sp_FarmerLoginBZApp, Convert.ToDecimal(MobileNo), RandomCode, BzShareUrl, ReferencedCode, FarmerId, DeviceId, Name))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;
            }
        }

        //For BZ New Website
        public DataSet GetUserDetailBZFarmer(Referal objAppWebLogin)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.usp_BzFarmerAppWebLogin, Convert.ToDecimal(objAppWebLogin.MobileNumber),objAppWebLogin.Email, objAppWebLogin.RandomCode, objAppWebLogin.BzShareUrl, objAppWebLogin.ReferdBy, objAppWebLogin.FarmerID, objAppWebLogin.DeviceId, objAppWebLogin.FirstName))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;
            }
        }
        public DataSet SaveVegetablePrice(string apiKey, string Name, string Mobile, int StateId, int DistrictId, int BlockId, int VillageId, int VegetableId, string VegeVariety, string VegeAmount, string VegPrice)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.usp_SaveVegetablePrice, Name, Mobile, StateId, DistrictId, BlockId, VillageId, VegetableId, VegeVariety, VegeAmount, VegPrice))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);

                return dataset;

            }
        }
        public DataSet GetVegetables(string apikey)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspGetVegetables))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
    }
}
