using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
   public class LogDal:BaseDal
    {
        public static void SmsLog(string mobiles, string message, int userid = 0)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_Sms_Log";

                command.Parameters.AddWithValue("@SentToMobile", mobiles);
                command.Parameters.AddWithValue("@Message", message);
                command.Parameters.AddWithValue("@userid", userid);

                int data = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }

            connection.Close();
        }

        public static void ErrorLog(string ControllerName, string ActionName, string message, int commonid = 0)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_Error_Log";

                command.Parameters.AddWithValue("@ControllerName", ControllerName);
                command.Parameters.AddWithValue("@ActionName", ActionName);
                command.Parameters.AddWithValue("@Message", message);
                command.Parameters.AddWithValue("@commonid", commonid);

                int data = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }

            connection.Close();
        }

        public static void MethodCallLog(string ControllerName, string ActionName)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_MethodCall_Log";

                command.Parameters.AddWithValue("@ControllerName", ControllerName);
                command.Parameters.AddWithValue("@ActionName", ActionName);

                int data = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }

            connection.Close();
        }

        public static void MailLog(string toList, string ccList, string subject, string body, int userid = 0)
        {

            SqlConnection connection;
            SqlCommand command = new SqlCommand();

            connection = new SqlConnection(connetionString);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_Mail_Log";

                command.Parameters.AddWithValue("@ToList", toList);
                command.Parameters.AddWithValue("@CCList", ccList);
                command.Parameters.AddWithValue("@Subject", subject);
                command.Parameters.AddWithValue("@Body", body);
                command.Parameters.AddWithValue("@userid", userid);

                int data = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }

            connection.Close();
        }
    }
}
