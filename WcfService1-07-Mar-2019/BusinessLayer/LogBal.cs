using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
   public class LogBal
    {
        public static void ErrorLog(string ControllerName, string ActionName, string message, int commonid)
        {
            LogDal.ErrorLog(ControllerName, ActionName, message, commonid);

        }

        public static void MethodCallLog(string ControllerName, string ActionName)
        {
            LogDal.MethodCallLog(ControllerName, ActionName);
        }
    }
}
