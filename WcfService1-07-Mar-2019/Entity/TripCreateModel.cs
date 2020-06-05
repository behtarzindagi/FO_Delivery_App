using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TripCreateViewModel
    {
        public TripCreateModel Data { set; get; }
    }
    public class TripCreateModel
    {
        public List<TripCreateOrderModel> orderList { set; get; }
        public List<TripCreateDealerModel> dealerList { set; get; }
        public List<TripCreateLandMarkModel> landMarkInDistrict { set; get; }
        public List<TripCreateHistoricalModel> HistoricalPath { set; get; }

        public int foCount { get; set; }
        public double lunchTime { get; set; }
        public double avgVehicleSpeed { get; set; }
        public double workingHours { get; set; }
        public int breakTimePerDeliveryInMinute { get; set; }
    }
    public class TripCreateOrderModel
    {
        public int orderId { get; set; }
        public string orderRefNo { get; set; }
        public decimal orderAmount { get; set; }
        public string address { get; set; }
        public List<int> dealerId { get; set; }
    }

    public class TripCreateDealerModel
    {
        public int dealerId { get; set; }
        public string dealerName { get; set; }
        public string address { get; set; }
    }
    public class TripCreateLandMarkModel
    {
        public string position { get; set; }
        public string name { get; set; }
    }

    public class TripCreateHistoricalModel
    {

    }
}