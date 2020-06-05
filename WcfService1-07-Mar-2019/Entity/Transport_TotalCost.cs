using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Transport_TotalCost
    {
        public string VehicleNo { set; get; }
        public int TotalKm { set; get; }
        public double Cost { set; get; }
        public string Type { set; get; }
        public int ExpenseId { set; get; }
        public string ExpenseType { set; get; }
        public Double ExpenseCost { set; get; }

        //public TotalCostDetails CostDetails { set; get; }
        //public List<ExpenseCostDetails> ExpenseDetails { set; get; }

    }
    public class TotalCostDetails
    {
        public string VehicleNo { set; get; }
        public int TotalKm { set; get; }
        public double Cost { set; get; }
        public string Type { set; get; }

    }
    public class ExpenseCostDetails
    {
        public int ExpenseId { set; get; }
        public string ExpenseType { set; get; }
        public Double ExpenseCost { set; get; }
        public string Type { set; get; }
    }
}
