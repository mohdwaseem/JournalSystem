using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BAL.Models;
using JournalSystem.BL.Repository;
using JournalSystem.Filters;
using NLog;

namespace JournalSystem.Controllers
{
    [CustomHandleError]
    [Authorize]
    public class AdminController : Controller
    {
        private JournalMasterRepository journalMasterRepository = new JournalMasterRepository();
        private IssueMasterRepository issueMasterRepository = new IssueMasterRepository();
        private ArticleMasterRepository articleMasterRepository = new ArticleMasterRepository();
        private UserMasterRepository userMasterRepository = new UserMasterRepository();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.Journals = journalMasterRepository.GetAllJournals().Count;
            ViewBag.Issues = issueMasterRepository.GetAllIssue().Count;
            ViewBag.Articles = articleMasterRepository.GetAllArticles().Count;
            return View();
        }
        public ActionResult JournalMaster()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult JournalMaster(JournalMasterModel journalMasterModel)
        {
            string msg = null;
            string actionType = Request.Form["action"];
            if (actionType == "Submit")
            {
                journalMasterModel.CreatedBy = "Admin";
                journalMasterModel.CreatedOn = DateTime.UtcNow;
                var count = journalMasterRepository.AddJournal(journalMasterModel);
                if (count > 0)
                {
                    msg = "Journal Added Successfully";
                }
                else
                {
                    msg = "Some error occured please try again.";
                }
            }
            else if (actionType == "Update")
            {
                journalMasterModel.Id= Convert.ToInt32(Request.Form["id"]);
                journalMasterModel.CreatedBy = "Admin";
                journalMasterModel.CreatedOn = DateTime.UtcNow;
                var count = journalMasterRepository.EditJournal(journalMasterModel);
                if (count > 0)
                {
                    msg = "Journal Updated Successfully";
                }
                else
                {
                    msg = "Some error occured please try again.";
                }
            }
            else
            {
                msg = "Invalid Action";
            }
                return Json(msg);
        }
        [HttpPost]
        public JsonResult GetAllJournals()
        {
            try
            {

                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // Getting all Customer data  

                var journals = journalMasterRepository.GetAllJournals();
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    journals = journals.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    journals = journals.Where(m => m.JournalTitle.ToLower().Contains(searchValue)
                    || m.Description.ToLower().Contains(searchValue)).ToList();
                }
                //total number of rows count   
                recordsTotal = journals.Count();
                //Paging   
                var data = journals.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetJournal(int id)
        {
            var allJournals = journalMasterRepository.GetAllJournals();
            var result = (from list in allJournals where list.Id == id select list);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region IssueMaster
        public ActionResult IssueMaster()
        {
            #region Journal DropdownList
            var list = journalMasterRepository.GetAllJournals();
            List<SelectListItem> selectLists = new List<SelectListItem>();
            foreach (var item in list)
            {
                selectLists.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.JournalTitle

                });
            }
            selectLists.Insert(0, new SelectListItem { Value = "0", Text = "--Select Journal--" });
            ViewBag.Journals = selectLists;
            #endregion
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult IssueMaster(IssueMasterModel issueMasterModel)
        {
            string msg = null;
            int type= Convert.ToInt32(Request.Form["ddlType"]);
            bool isCurrent = false, isAhead = false;
            if (type==1)
            {
                isCurrent = true;
            }
            if (type==2)
            {
                isAhead = true;
            }
            string actionType = Request.Form["action"];
            if (actionType == "Submit")
            {

                issueMasterModel.JournalId = Convert.ToInt32(Request.Form["JournalId"]);
                issueMasterModel.IsCurrent = isCurrent;
                issueMasterModel.IsAhead = isAhead;
                issueMasterModel.CreatedBy = "Admin";
                issueMasterModel.CreatedOn = DateTime.UtcNow;
                var count = issueMasterRepository.AddIssue(issueMasterModel);
                if (count > 0)
                {
                    msg = "Issue Added Successfully";
                }
                else
                {
                    msg = "Some error occured please try again.";
                }
            }
            else if (actionType == "Update")
            {
                issueMasterModel.Id= Convert.ToInt32(Request.Form["id"]);
                issueMasterModel.JournalId = Convert.ToInt32(Request.Form["JournalId"]);
                issueMasterModel.IsCurrent = isCurrent;
                issueMasterModel.IsAhead = isAhead;
                issueMasterModel.CreatedBy = "Admin";
                issueMasterModel.CreatedOn = DateTime.UtcNow;
                if (issueMasterRepository.EditIssue(issueMasterModel)>0)
                {
                    msg = "Issue Updated Successfully";
                }
                else
                {
                    msg = "Some Error Occured";
                }
            }
            else
            {
                msg = "Invalid Action";
            }
            return Json(msg);
        }
        [HttpPost]
        public JsonResult GetAllIssues()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // Getting all Customer data  

                var issues = issueMasterRepository.GetAllIssue().Where(x=>x.IsCurrent==false);
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    issues = issues.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    issues = issues.Where(m => m.Volume.ToLower().Contains(searchValue)
                    || m.Description.ToLower().Contains(searchValue)).ToList();
                }
                //total number of rows count   
                recordsTotal = issues.Count();
                //Paging   
                var data = issues.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetCurrentIssues()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // Getting all Customer data  

                var issues = issueMasterRepository.GetAllIssue().Where(x=>x.IsCurrent==true&&x.IsAhead==false);
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    issues = issues.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    issues = issues.Where(m => m.Volume.ToLower().Contains(searchValue)
                    || m.Description.ToLower().Contains(searchValue)).ToList();
                }
                //total number of rows count   
                recordsTotal = issues.Count();
                //Paging   
                var data = issues.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetAheadIssues()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // Getting all Customer data  

                var issues = issueMasterRepository.GetAllIssue().Where(x=>x.IsAhead == true);
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    issues = issues.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    issues = issues.Where(m => m.Volume.ToLower().Contains(searchValue)
                    || m.Description.ToLower().Contains(searchValue)).ToList();
                }
                //total number of rows count   
                recordsTotal = issues.Count();
                //Paging   
                var data = issues.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetIssuesList(int id)
        {

            var issueList = issueMasterRepository.GetAllIssue().Where(x => x.JournalId == id);
            List<SelectListItem> selectLists = new List<SelectListItem>();
            foreach (var item in issueList)
            {
                selectLists.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = "Volume: " + item.Volume + "- Issue: " + item.Issue + "- Year: " + item.Year + "-" + "(" + item.Description + ")"

                });
            }
            return Json(selectLists, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteIssue(int id)
        {
            try
            {
                if (issueMasterRepository.DeleteIssue(id) > 0)
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("No", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            
            
        }
        public JsonResult GetIssue(int id)
        {
            var list = issueMasterRepository.GetIssue(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region ArticlesMaster
        [HttpGet]
        public ActionResult ArticlesMaster()
        {
            #region Journal DropdownList
            var list = journalMasterRepository.GetAllJournals();
            List<SelectListItem> selectLists = new List<SelectListItem>();
            foreach (var item in list)
            {
                selectLists.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.JournalTitle

                });
            }
            selectLists.Insert(0, new SelectListItem { Value = "0", Text = "--Select Journal--" });
            ViewBag.Journals = selectLists;
            #endregion

            return View();
        }
        [HttpPost]
        public JsonResult ArticlesMaster(ArticleModel articleModel, HttpPostedFileBase PDFPath)
        {
            string msg = null;
            try
            {
                string actionType = Request.Form["actionTy"];
                if (actionType == "Submit")
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            string folderName = "../Uploads/" + articleModel.JournalId + articleModel.IssueId + "/";
                            if (!Directory.Exists(Server.MapPath(folderName)))
                            {
                                Directory.CreateDirectory(Server.MapPath(folderName));
                            }
                            HttpPostedFileBase file = files[i];
                            string fname;
                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            string path = Path.Combine(Server.MapPath(folderName), Path.GetFileName(file.FileName));
                            file.SaveAs(path);
                            int journalId = (int.Parse(Request.Form["JournalId"]));
                            int issueId = (int.Parse(Request.Form["IssueId"]));
                            articleModel.PDFPath = folderName + fname;
                            articleModel.JournalId = journalId;
                            articleModel.IssueId = issueId;
                            articleModel.CreatedBy = "Admin";
                            articleModel.CreatedOn = DateTime.UtcNow;
                            var count = articleMasterRepository.AddArticle(articleModel);
                            if (count > 0)
                            {
                                msg = "Article Added Successfully";
                            }
                            else
                            {
                                msg = "Some error occured please try again.";
                            }
                        }
                    }
                    else
                    {
                        return Json("No files selected.", JsonRequestBehavior.AllowGet);
                    }
                    return Json(msg);
                }
                else if (actionType == "Update")
                {
                    if (PDFPath != null)
                    {
                        HttpFileCollectionBase files = Request.Files;

                        string folderName = "../Uploads/" + articleModel.JournalId + articleModel.IssueId + "/";
                        if (!Directory.Exists(Server.MapPath(folderName)))
                        {
                            Directory.CreateDirectory(Server.MapPath(folderName));
                        }
                        HttpPostedFileBase file = files[0];
                        string fname;
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string path = Path.Combine(Server.MapPath(folderName), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        int journalId = (int.Parse(Request.Form["JournalId"]));
                        int issueId = (int.Parse(Request.Form["IssueId"]));
                        articleModel.ArticleId = (int.Parse(Request.Form["ids"]));
                        articleModel.PDFPath = folderName + fname;
                        articleModel.JournalId = journalId;
                        articleModel.IssueId = issueId;
                        articleModel.CreatedBy = "Admin";
                        articleModel.CreatedOn = DateTime.UtcNow;
                        var count = articleMasterRepository.UpdateArticle(articleModel);
                        if (count > 0)
                        {
                            msg = "Article Updated Successfully";
                        }
                        else
                        {
                            msg = "Some error occured please try again.";
                        }

                    }

                    else
                    {


                        int journalId = (int.Parse(Request.Form["JournalId"]));
                        int issueId = (int.Parse(Request.Form["IssueId"]));
                        articleModel.PDFPath = Request.Form["pdfs"];
                        articleModel.ArticleId = (int.Parse(Request.Form["ids"]));
                        articleModel.JournalId = journalId;
                        articleModel.IssueId = issueId;
                        articleModel.CreatedBy = "Admin";
                        articleModel.CreatedOn = DateTime.UtcNow;
                        var count = articleMasterRepository.UpdateArticle(articleModel);
                        if (count > 0)
                        {
                            msg = "Article Updated Successfully";
                        }
                        else
                        {
                            msg = "Some error occured please try again.";
                        }
                    }
                    return Json(msg);
                }
                else
                {
                    return Json("Invalid Action");
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetAllArticles()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // Getting all Customer data  

                var articles = articleMasterRepository.GetAllArticles();
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    articles = articles.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    articles = articles.Where(m => m.ArticleTitle.ToLower().Contains(searchValue)
                    || m.Authors.ToLower().Contains(searchValue)).ToList();
                }
                //total number of rows count   
                recordsTotal = articles.Count();
                //Paging   
                var data = articles.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetArticle(int id)
        {
            var result = articleMasterRepository.GetArticle(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult DeleteArticle(int id)
        {
            if (articleMasterRepository.DeleteArticle(id) > 0)
            {
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region User Management
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUser(UserMasterModel userMasterModel)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var encrypPass = crypto.Compute(userMasterModel.Password);
            userMasterModel.Password = encrypPass;
            userMasterModel.PasswordSalt = crypto.Salt;
            if (userMasterModel.IsSuperAdmin)
            {
                userMasterModel.Role = "Super Admin";
            }
            else
            {
                userMasterModel.Role = "User";
            }
            userMasterModel.IsActive = true;

            if (userMasterRepository.AddUser(userMasterModel) == 1)
            {
                return Json("User Created Successfully", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("Some Error Occured", JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Method to Check Duplicate User Entry
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckDuplicateUser(string email)
        {
            if (userMasterRepository.CheckDuplicateUser(email))
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetAllUsers()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                // Getting all Users data  

                var users = userMasterRepository.GetAllUsers();
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    users = users.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    users = users.Where(m => m.FirstName.ToLower().Contains(searchValue)
                    || m.LastName.ToLower().Contains(searchValue) || m.Email.ToLower().Contains(searchValue)).ToList();
                }
                //total number of rows count   
                recordsTotal = users.Count();
                //Paging   
                var data = users.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            if (userMasterRepository.DeleteUser(id) > 0)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewData["Msg"] = "Please enter email and password";
            }
            else
            {
                var result = await userMasterRepository.GetUserLoginDetails(email);
                if (result != null)
                {
                    var crpto = new SimpleCrypto.PBKDF2();
                    if (result.ElementAt(0).Password == crpto.Compute(password, result.ElementAt(0).PasswordSalt))
                    {
                        FormsAuthentication.SetAuthCookie(email, false);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ViewData["Msg"] = "Invalid Username/Password";
                    }

                }
                else
                {
                    ViewData["Msg"] = "Invalid Username/Password";
                }
            }

            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }
        #endregion
    }
}