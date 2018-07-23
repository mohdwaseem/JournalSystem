using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Models;
using JournalSystem.DL;

namespace JournalSystem.BL.Repository
{
   public class JournalMasterRepository
    {
        public int AddJournal(JournalMasterModel journalMasterModel)
        {
            using (var Db=new AIJDBEntities())
            {
                JournalMaster journalMaster = new JournalMaster();
                journalMaster.JournalTitle = journalMasterModel.JournalTitle;
                journalMaster.Description = journalMasterModel.Description;
                journalMaster.CoverPage = journalMasterModel.CoverPage;
                journalMaster.CreatedBy = journalMasterModel.CreatedBy;
                journalMaster.CreatedOn = journalMasterModel.CreatedOn;
                Db.JournalMasters.Add(journalMaster);
                return Db.SaveChanges();
            }
        }
        public int EditJournal(JournalMasterModel journalMasterModel)
        {
            using (var Db = new AIJDBEntities())
            {
                var journalMaster = Db.JournalMasters.FirstOrDefault(x => x.Id == journalMasterModel.Id);
                if (journalMaster!=null)
                {
                    journalMaster.JournalTitle = journalMasterModel.JournalTitle;
                    journalMaster.Description = journalMasterModel.Description;
                    journalMaster.CoverPage = journalMasterModel.CoverPage;
                    journalMaster.CreatedBy = journalMasterModel.CreatedBy;
                    journalMaster.CreatedOn = journalMasterModel.CreatedOn;
                }
                return Db.SaveChanges();
            }
        }
        public List<JournalMasterList> GetAllJournals()
        {
            using (var Db=new AIJDBEntities())
            {
                var list = (from a in Db.JournalMasters
                            select new JournalMasterList
                            {
                                Id=a.Id,
                                JournalTitle=a.JournalTitle,
                                Description=a.Description,
                                CreatedBy=a.CreatedBy,
                                CreatedOn=a.CreatedOn
                            }).ToList();
                return list;
            }
        }
    }
}
