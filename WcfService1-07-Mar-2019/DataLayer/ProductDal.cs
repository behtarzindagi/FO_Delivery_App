using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class ProductDal : BaseDal
    {
        SqlConnection connection;
        public DataSet GetProduct_Master()
        {
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
                command.CommandText = "SP_DISTINCTPRODUCTLIST"; //Reuse Bharat SP
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

        public DataSet Get_Master_Data(int id, string type)
        {
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
                command.CommandText = "SP_Prod_Get_Master_Data";
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Type", type);

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

        public DataSet Get_Product_Detail(int productId)
        {
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
                command.CommandText = "SP_Prod_Get_Product_Detail";
                command.Parameters.AddWithValue("@productid", productId);

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
