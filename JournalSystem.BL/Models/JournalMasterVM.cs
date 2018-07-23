using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
   public class JournalMasterModel
    {
        public int Id { get; set; }
        public string JournalTitle { get; set; }
        public string Description { get; set; }
        public string CoverPage { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<JournalMasterList> JournalList { get; set; }
    }

    public class JournalMasterList
    {
        public int Id { get; set; }
        public string JournalTitle { get; set; }
        public string Description { get; set; }
        public string CoverPage { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    
}
