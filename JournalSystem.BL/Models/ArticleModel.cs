using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.BL.Models
{
  public  class ArticleModel
    {
        public int JournalId { get; set; }
        public int IssueId { get; set; }
        public decimal ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
        public string HtmlText { get; set; }
        public byte[] JournalImage { get; set; }
        public string Authors { get; set; }
        public Nullable<int> FirstPage { get; set; }
        public Nullable<int> LastPage { get; set; }
        public string Extra { get; set; }
        public string PDFPath { get; set; }
        public string XMLPath { get; set; }
        public string EpubPath { get; set; }
        public string DOI { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
