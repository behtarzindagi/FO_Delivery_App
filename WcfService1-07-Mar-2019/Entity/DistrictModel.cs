using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class DistrictModel
    {
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
    public class DistrictViewModel
    {
        public List<DistrictModel> List { get; set; }

    }
}
