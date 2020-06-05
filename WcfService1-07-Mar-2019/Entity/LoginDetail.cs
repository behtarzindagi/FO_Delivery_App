using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Entity
{
   public class LoginDetail
    {

        public LoginDetail()
        {
           
        }
        public LoginDetail(int userid)
        {
            if(userid >0  )
            apiKey = ConfigurationSettings.AppSettings["productkey"];
        }

        public string userName { get; set; }
        public int UserID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string apiKey { get ; set; }
        public int dealerId { get; set; }
        public int roleType { get; set; }


    }

   public class Login
    {
        public string userName { get; set; }
        public string passWord { get; set; }
       // public string apiKey { get; set; }


    }

    public class Headers
    {
        public string test { get; set; }
        public string test1 { get; set; }
        // public string apiKey { get; set; }


    }
}
