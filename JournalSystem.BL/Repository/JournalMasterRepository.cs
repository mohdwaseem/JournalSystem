using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JournalSystem.BL.Models;
using JournalSystem.DL;

namespace JournalSystem.BL.Repository
{
   public class JournalMasterRepository
    {
        public int AddJournal(JournalMasterModel journalMasterModel)
        {
            using (var Db=new AIJDBEntities())
            {
                return 0;
            }
        }
    }
}
