using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Models;
using JournalSystem.DL;

namespace JournalSystem.BL.Repository
{
   public class UserMasterRepository
    {
        public int AddUser(UserMasterModel userMasterModel)
        {
            using (var Db=new AIJDBEntities())
            {
                UserMaster userMaster = new UserMaster();
                userMaster.Email = userMasterModel.Email;
                userMaster.Password = userMasterModel.Password;
                userMaster.PasswordSalt = userMasterModel.PasswordSalt;
                userMaster.FirstName = userMasterModel.FirstName;
                userMaster.LastName = userMasterModel.LastName;
                userMaster.IsActive = userMasterModel.IsActive;
                userMaster.IsSuperAdmin = userMasterModel.IsSuperAdmin;
                userMaster.Role = userMasterModel.Role;
                userMaster.UserIdGuid = Guid.NewGuid();
                userMaster.CreatedOn = DateTime.UtcNow;
                userMaster.UpdatedOn = DateTime.UtcNow;
                userMaster.IsDelete = false;
                Db.UserMasters.Add(userMaster);
                return Db.SaveChanges();

            }
        }

        public bool CheckDuplicateUser(string email)
        {
            using (var Db=new AIJDBEntities())
            {
                var result = Db.UserMasters.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
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

        public List<UserViewModel> GetAllUsers()
        {
            using (var Db=new AIJDBEntities())
            {
                var list = (from a in Db.UserMasters
                            select new UserViewModel
                            {
                                UserId=a.UserId,
                                FirstName=a.FirstName,
                                LastName=a.LastName,
                                Email=a.Email,
                                CreatedOn=a.CreatedOn,
                                IsSuperAdmin=a.IsSuperAdmin

                            }).ToList();
                return list;
            }
        }
        public int DeleteUser(int id)
        {
            using (var Db=new AIJDBEntities())
            {
                var result = Db.UserMasters.FirstOrDefault(x => x.UserId == id);
                if (result!=null)
                {
                    Db.UserMasters.Remove(result);
                }
                return Db.SaveChanges();
            }
        }

        public async Task<List<UserViewModel>> GetUserLoginDetails(string email)
        {
            using (var Db = new AIJDBEntities())
            {
                var list = (from a in Db.UserMasters.Where(x=>x.Email==email)
                            select new UserViewModel
                            {
                                UserId = a.UserId,
                                Password = a.Password,
                                PasswordSalt = a.PasswordSalt,
                                Email=a.Email

                            }).ToList();
                return  list;
            }
        }
    }
}
