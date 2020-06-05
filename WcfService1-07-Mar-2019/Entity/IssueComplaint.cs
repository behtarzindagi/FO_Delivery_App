using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class IssueComplaint
    {

    }
    public class Issue
   {
        public string apikey { get; set; }
        public decimal MobileNo { get; set; }
    public string Name { get; set; }
    public int CategoryID { get; set; }
        public int IssueTypeId { get; set; }
        public int IssueDetailID { get; set; }
    public string Query { get; set; }
    public int CreatedBy { get; set; }
}
    public class Complaint
    {
        public string apikey { get; set; }
        public long OrderID { get; set; }
        public int PackageID { get; set; }
        public int IssueDetailID { get; set; }
        public string Query { get; set; }     
        public int CreatedBy { get; set; }
    }

    public class IssueDetailByFarmer
    {
        public List<IssueDetail> IssueList { set; get; }
    }
    public class IssueDetail
    {
        public int QueryID { get; set; }
        public string MobileNo { get; set; }
        public string FarmerName { get; set; }
        public string CategoryName { get; set; }
        public string IssueType { get; set; }
        public string IssueDetails { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }

        public string Query { get; set; }
    }

    public class CompDetailByFarmer
    {
        public List<ComplainModel> CompList { set; get; }
    }
    public class ComplainModel
    {
        public Int64 ComplaintID { get; set; }
        public string Query { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string IssueType { get; set; }
        public string IssueDetail { get; set; }
        public string CreatedBy { get; set; }
        public string ProductName { get; set; }
        public string OrderRefNo { get; set; }
        public int TotalRecord { get; set; }
    }

}
