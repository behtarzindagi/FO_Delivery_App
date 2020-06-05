using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VegetablePrice
    {
        public string apiKey { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int VillageId { get; set; }
        public int VegetableId { get; set; }
        public string VegeVariety { get; set; }
        public string VegeAmount { get; set; }
        public string VegPrice { get; set; }
    }
    public class VegeResponseStatus
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
    }

    public class Vegetables
    {
        public int VegId { get; set; }
        public string VegeName { get; set; }
    }
    public class VegetableList
    {
        public List<Vegetables> VegeList { get; set; }
    }
}
