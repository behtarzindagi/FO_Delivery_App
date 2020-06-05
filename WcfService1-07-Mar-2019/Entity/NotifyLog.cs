using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class NotifyLog
    {
        public string FcmId { get; set; }
        public string AppKeyId { get; set; }
        public string Msg { get; set; }
        public string Status { get; set; }
        public string MsgId { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class ErrorLog
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Type { get; set; }
        public string Msg { get; set; }
    }
}
