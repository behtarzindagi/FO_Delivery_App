using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class HubHeadFODistrictWiseModel
    {
        public string Name { set; get; }
        public string Mobile { set; get; }
        public int UserId { set; get; }
    }

    public class HubHeadFODistrictWiseViewModel
    {
        public List<HubHeadFODistrictWiseModel> UserList { set; get; }
    }

    public class HubHeadFODistrictWise
    {
        public HubHeadFODistrictWiseViewModel Users { set; get; }

    }
}
