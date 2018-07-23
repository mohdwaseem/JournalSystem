using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
   public class IssueMasterModel
    {
        public int Id { get; set; }
        public int JournalId { get; set; }
        public string Volume { get; set; }
        public string Issue { get; set; }
        public string Year { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsActive { get; set; }
        public bool IsAhead { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public List<IssueViewModel> IssueList { get; set; }
    }

    public class IssueViewModel
    {
        public int Id { get; set; }
        public int JournalId { get; set; }
        public string Volume { get; set; }
        public string Issue { get; set; }
        public string Year { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsActive { get; set; }
        public bool IsAhead { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
    }
}
