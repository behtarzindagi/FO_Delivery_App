using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TaskListModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
    public class TaskListViewModel
    {
        public List<TaskListModel> TaskList { get; set; }
    }

    public class CallLogged
    {
        public string apiKey { get; set; }
        public int userid { get; set; }
        public int callStatusID { get; set; }
        public string appointmentDate { get; set; }       
        public string Remark { get; set; }
        public string MobileNo { get; set; }
        public int type { get; set; }
    
    }

}
