using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TrackFOByFoModel
    {
        public string FoEvent { get; set; }
        public string FoAction { get; set; }
        public string Type { get; set; }
        public string Lat { get; set; }
        public string Longitude { get; set; }
        public string Time { get; set; }
        public string DealerName { get; set; }
        public string NewDealerName { get; set; }
        public string Orderid { get; set; }
        public string OrderRefNo { get; set; }
        public string FarmerName { get; set; }
    }
    public class TrackFOByFo
    {
        public List<TrackFOByFoModel> List { set; get; }
        public string Lastlat { get; set; }
        public string Lastlongitude { get; set; }
    }

    public class TrackFOByFoViewModel
    {
        public TrackFOByFo TackFOList { get; set; }
    }
}
