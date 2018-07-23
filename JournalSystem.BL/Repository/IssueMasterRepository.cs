using BAL.Models;
using JournalSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.BL.Repository
{
    public class IssueMasterRepository
    {
        public int AddIssue(IssueMasterModel issueMasterModel)
        {
            using (var Db=new AIJDBEntities())
            {
                if (issueMasterModel.IsCurrent)
                {
                    ResetCurrentIssue(issueMasterModel.JournalId);
                }
                if (issueMasterModel.IsAhead)
                {
                    ResetIsAheadIssue(issueMasterModel.JournalId);
                }
                IssueMaster issueMaster = new IssueMaster();
                issueMaster.JournalId = issueMasterModel.JournalId;
                issueMaster.Volume = issueMasterModel.Volume;
                issueMaster.Issue = issueMasterModel.Issue;
                issueMaster.Year = issueMasterModel.Year;
                issueMaster.Description = issueMasterModel.Description;
                issueMaster.IsCurrent = issueMasterModel.IsCurrent;
                issueMaster.IsActive = issueMasterModel.IsActive;
                issueMaster.IsAhead = issueMasterModel.IsAhead;
                issueMaster.CreatedOn = issueMasterModel.CreatedOn;
                issueMaster.CreatedBy = issueMasterModel.CreatedBy;
                if (CheckDuplicateIssue(issueMasterModel.JournalId, issueMasterModel.Volume, issueMasterModel.Issue, issueMasterModel.Year))
                {
                   
                    Db.IssueMasters.Add(issueMaster);
                    return Db.SaveChanges();
                }
                else
                {
                    return 0;
                }
               
            }
             
        }

        public bool CheckDuplicateIssue(int journalId,string volume,string issue,string year)
        {
            using (var db=new AIJDBEntities())
            {
                var result = db.IssueMasters.FirstOrDefault(x =>x.JournalId==journalId&& x.Volume.ToLower().Trim() == volume.ToLower().Trim() && x.Issue.ToLower().Trim() == issue.ToLower().Trim() && x.Year.ToLower().Trim() == year.ToLower().Trim());
                if (result==null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ResetCurrentIssue(int journalId)
        {
            using (var db=new AIJDBEntities())
            {
                var result = db.IssueMasters.Where(x => x.JournalId == journalId);
                foreach (var item in result)
                {
                    item.IsCurrent = false;
                    
                }
                db.SaveChanges();
            }
        }
       
        public void ResetIsAheadIssue(int journalId)
        {
            using (var db = new AIJDBEntities())
            {
                var result = db.IssueMasters.Where(x => x.JournalId == journalId);
                foreach (var item in result)
                {
                    item.IsAhead = false;
                }
                db.SaveChanges();
            }
        }
        public int EditIssue(IssueMasterModel issueMasterModel)
        {
            using (var Db = new AIJDBEntities())
            {
                var issueMaster = Db.IssueMasters.FirstOrDefault(x => x.Id == issueMasterModel.Id);
                if (issueMaster!=null)
                {
                    if (issueMasterModel.IsCurrent)
                    {
                        ResetCurrentIssue(issueMasterModel.JournalId);
                    }
                    if (issueMasterModel.IsAhead)
                    {
                        ResetIsAheadIssue(issueMasterModel.JournalId);
                    }
                    issueMaster.JournalId = issueMasterModel.JournalId;
                    issueMaster.Volume = issueMasterModel.Volume;
                    issueMaster.Issue = issueMasterModel.Issue;
                    issueMaster.Year = issueMasterModel.Year;
                    issueMaster.Description = issueMasterModel.Description;
                    issueMaster.IsCurrent = issueMasterModel.IsCurrent;
                    issueMaster.IsActive = issueMasterModel.IsActive;
                    issueMaster.IsAhead = issueMasterModel.IsAhead;
                    issueMaster.CreatedOn = issueMasterModel.CreatedOn;
                    issueMaster.CreatedBy = issueMasterModel.CreatedBy;
                }
               
                return Db.SaveChanges();

            }
        }
        public int DeleteIssue(int id)
        {
            using (var Db = new AIJDBEntities())
            {
                var issueMaster = Db.IssueMasters.FirstOrDefault(x => x.Id == id);
                if (issueMaster != null)
                {
                    Db.IssueMasters.Remove(issueMaster);
                }
                return Db.SaveChanges();

            }
        }

        public List<IssueViewModel> GetAllIssue()
        {
            using (var Db=new AIJDBEntities())
            {
                var list = (from a in Db.IssueMasters
                            select new IssueViewModel
                            {
                                Id=a.Id,
                                JournalId=a.JournalId,
                                Volume = a.Volume,
                                Issue = a.Issue,
                                Year = a.Year,
                                Description=a.Description,
                                IsCurrent=a.IsCurrent,
                                IsActive =a.IsActive,
                                IsAhead =a.IsAhead,
                                CreatedOn=a.CreatedOn,
                                CreatedBy=a.CreatedBy
                            }).ToList();
                return list;
            }
        }
        public List<IssueViewModel> GetIssue(int id)
        {
            using (var Db = new AIJDBEntities())
            {
                var list = (from a in Db.IssueMasters.Where(x=>x.Id==id)
                            select new IssueViewModel
                            {
                                Id = a.Id,
                                JournalId = a.JournalId,
                                Volume = a.Volume,
                                Issue = a.Issue,
                                Year = a.Year,
                                Description = a.Description,
                                IsCurrent = a.IsCurrent,
                                IsActive = a.IsActive,
                                IsAhead = a.IsAhead,
                                CreatedOn = a.CreatedOn,
                                CreatedBy = a.CreatedBy
                            }).ToList();
                return list;
            }
        }
    }
}
