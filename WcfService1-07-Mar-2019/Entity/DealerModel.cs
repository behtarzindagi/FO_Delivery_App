using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DealerModel
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public int PriceId { get; set; }
        public int PackageId { get; set; }
        public decimal Price { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
    }

    public class DealerViewModel
    {
        public List<DealerModel> Dealer { get; set; }
    }
}
