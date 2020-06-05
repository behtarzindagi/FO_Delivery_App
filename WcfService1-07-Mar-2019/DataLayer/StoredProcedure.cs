using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class StoredProcedure
    {
        public static string DISTINCTPRODUCTLIST = "SP_DISTINCTPRODUCTLIST";
        public static string Prod_Get_Master_Data = "SP_Prod_Get_Master_Data";
        public static string Prod_Get_Product_Detail = "SP_Prod_Get_Product_Detail";
        public static string Prod_Add_Master_Data = "SP_Prod_Add_Master_Data";
        public static string Prod_GetStateDistrictBlockDealer = "Sp_Prod_GetStateDistrictBlockDealer";
        public static string Prod_Get_Package_Data = "Sp_Prod_Get_Package_Data";
        public static string Prod_Add_Product_Package_Data = "Sp_Prod_Add_Product_Package_Data";
        public static string Prod_Add_Package_District_Mapping = "Sp_Prod_Add_Package_District_Mapping";
    //    public static string Prod_Get_Package_District_Details = "Sp_Prod_Get_Package_District_Details";
     //   public static string Prod_Add_Package_District_Details = "Sp_Prod_Add_Package_District_Details";

        public static string Prod_Get_Package_District_Details = "Sp_Prod_Get_Package_District_Details_Block";
        public static string Prod_Add_Package_District_Details = "Sp_Prod_Add_Package_District_Details_Block";


        public static string Prod_Get_Package_Price_List = "Sp_Prod_Get_Package_Price_List";
        public static string Prod_Update_Package_Price_List = "Sp_Prod_Update_Package_Price_List";
        public static string Prod_Change_Product_Status = "Sp_Prod_Change_Product_Status";
        public static string Prod_Get_ProductName_CompanyName = "Sp_Prod_Get_ProductName_CompanyName";
        public static string Prod_Get_User = "Sp_Prod_Get_User";
        public static string Add_Product_Package_Data_Temp = "Sp_Prod_Add_Product_Package_Data_Temp";
        public static string Get_dealer_requested_product = "SP_DEALER_REQPRODUCTLIST_Temp";
        public static string Activate_Dealer_Product = "SP_INSERTADDEDPRODUCT1_TEMP";
        public static string Update_Product_Package_Data_Temp = "Sp_Prod_Update_Product_Package_Data_Temp";

        public static string Get_Nutrient = "SP_GET_NUTRIENT";
        public static string Get_Nutrient_Unit = "SP_GET_NUTRIENT_UNIT";
        public static string Add_NutrientValue_Techname = "SP_ADD_NUTRIENTVALUE_TECHNAME";
        public const string SPSaveNotifyLog = "SP_SaveNotifyLog";
        public const string SPGetFcmByRoleId = "SP_GetFcmByRoleId";
        public const string SPGetFcmByUserId = "SP_GetFcmByUserId";
        public const string SPGetRole = "GetRole";
        public const string GetUser_for_dealer = "GetUser_for_dealer";
        public const string GetUserid_for_Product = "sp_prod_GetUserid_for_Product";
        public const string Update_Product_Package_Data_Master = "Sp_Prod_Update_Product_Package_Data_Master";
        public const string ACCEPTED_PRODUCTLIST_Master = "SP_DEALER_ACCEPTED_PRODUCTLIST_Master";
        public const string Get_Product_Detail_temp =    "SP_Prod_Get_Product_Detail_temp";
        public const string Prod_UseForList = "SP_GET_USE_FOR_LIST";
        public const string Add_Update_ProductUseFor =  "Sp_Prod_Add_Update_ProductUseFor";
        public const string Get_ProductId_Package = "Sp_Prod_GetDetail_ProductId";
        public const string Add_Update_DealerProductUseFor = "Sp_Prod_Add_Update_DealerProductUseFor";
        public const string Get_Product_Dealer_Detail = "SP_Prod_Get_Product_Dealer_Detail";
        public const string Add_Package_Dealer_master = "SP_Prod_Add_Package_Dealer";
        public const string ADD_NUTRIENTVALUE_TECHNAME_Dealer = "SP_ADD_NUTRIENTVALUE_TECHNAME_Dealer";
        public const string Add_Demand_Product = "Sp_prod_Insert_Demand_Product"; 
        public const string Get_Demand_Product = "Sp_prod_Get_Demand_Product";
        public const string Sp_Change_Password = "Sp_Prod_Change_Password";

        


    }
}
