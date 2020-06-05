using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;


namespace DataLayer
{
    public class NotificationDal : BaseDal
    {
        public DataSet GetFcmByUserId(int userId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetFcmByUserId";
                command.Parameters.AddWithValue("@UserId", userId);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return ds;
        }

        public DataSet GetFcmByRoleId(int roleId)
        {
            SqlConnection connection;
            SqlDataAdapter adapter;
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connetionString);

            try
            {
                DataTable Dt = new DataTable();
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetFcmByroleId";
                command.Parameters.AddWithValue("@RoleId", roleId);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                LogDal.ErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, 0);
            }

            connection.Close();
            return ds;
        }
    }
}
