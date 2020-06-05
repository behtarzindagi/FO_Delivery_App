using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
namespace DataLayer
{
   public class BaseDal
    {
     public readonly static string connetionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
     public  Database objDB=new SqlDatabase(connetionString);
    }
}
