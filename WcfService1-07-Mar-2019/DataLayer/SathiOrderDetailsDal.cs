using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Entity;
using System;

namespace DataLayer
{
    public class SathiOrderDetailsDal : BaseDal
    {
        public DataSet GetSathiSalesReport(string apikey,int UserId,int DateMode, DateTime? SDATE, DateTime? EDATE)
        {
 
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspGetSathiSalesReport, UserId, DateMode, SDATE, EDATE))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
        public DataSet GetSathiPaymentReport(string apikey, int UserId, int DateMode, DateTime? SDATE, DateTime? EDATE)
        {
            using (var objcmd = objDB.GetStoredProcCommand(Constant.uspGetSathiPaymentReport, UserId, DateMode, null, null))
            {
                var dataset = objDB.ExecuteDataSet(objcmd);
                return dataset;
            }
        }
    }
}