using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;
using Entity;

namespace BusinessLayer
{
    public class NotificationBal
    {
        NotificationDal _notifidal;
        public NotificationBal()
        {
            _notifidal = new NotificationDal();

        }

        public List<string> GetFcmByUserId(int userId)
        {
            var fcmList = new List<string>();
            DataSet ds = _notifidal.GetFcmByUserId(userId);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        
                        fcmList.Add(ds.Tables[0].Rows[i]["FcmId"].ToString());
                    }
                }
            }


            return fcmList;
        }

        public List<string> GetFcmByRoleId(int roleId)
        {
            var fcmList = new List<string>();
            DataSet ds = _notifidal.GetFcmByRoleId(roleId);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        fcmList.Add(ds.Tables[0].Rows[i]["FcmId"].ToString());
                    }
                }
            }


            return fcmList;
        }
    }
}
