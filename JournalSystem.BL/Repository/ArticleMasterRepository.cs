using BAL.Models;
using JournalSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.BL.Repository
{
   public class ArticleMasterRepository
    {
        public int AddArticle(ArticleModel articleModel)
        {
            using (var Db=new AIJDBEntities())
            {
                ArticlesMaster articlesMaster = new ArticlesMaster();
                articlesMaster.JournalId = articleModel.JournalId;
                articlesMaster.IssueId = articleModel.IssueId;
                articlesMaster.ArticleTitle = articleModel.ArticleTitle;
                articlesMaster.Description = articleModel.Description;
                articlesMaster.Abstract = articleModel.Abstract;
                articlesMaster.HtmlText = articleModel.HtmlText;
                articlesMaster.JournalImage = articleModel.JournalImage;
                articlesMaster.Authors = articleModel.Authors;
                articlesMaster.FirstPage = articleModel.FirstPage;
                articlesMaster.LastPage = articleModel.LastPage;
                articlesMaster.Extra = articleModel.Extra;
                articlesMaster.PDFPath = articleModel.PDFPath;
                articlesMaster.XMLPath = articleModel.XMLPath;
                articlesMaster.EpubPath = articleModel.EpubPath;
                articlesMaster.DOI = articleModel.DOI;
                articlesMaster.Category = articleModel.Category;
                articlesMaster.SubCategory = articleModel.SubCategory;
                articlesMaster.CreatedOn = articleModel.CreatedOn;
                articlesMaster.CreatedBy = articleModel.CreatedBy;
                Db.ArticlesMasters.Add(articlesMaster);
                return Db.SaveChanges();

            }
        }
        public int UpdateArticle(ArticleModel articleModel)
        {
            using (var Db = new AIJDBEntities())
            {

                var articlesMaster = Db.ArticlesMasters.FirstOrDefault(x => x.ArticleId == articleModel.ArticleId);
                if (articlesMaster != null)
                {
                    articlesMaster.JournalId = articleModel.JournalId;
                    articlesMaster.IssueId = articleModel.IssueId;
                    articlesMaster.ArticleTitle = articleModel.ArticleTitle;
                    articlesMaster.Description = articleModel.Description;
                    articlesMaster.Abstract = articleModel.Abstract;
                    articlesMaster.HtmlText = articleModel.HtmlText;
                    articlesMaster.JournalImage = articleModel.JournalImage;
                    articlesMaster.Authors = articleModel.Authors;
                    articlesMaster.FirstPage = articleModel.FirstPage;
                    articlesMaster.LastPage = articleModel.LastPage;
                    articlesMaster.Extra = articleModel.Extra;
                    articlesMaster.PDFPath = articleModel.PDFPath;
                    articlesMaster.XMLPath = articleModel.XMLPath;
                    articlesMaster.EpubPath = articleModel.EpubPath;
                    articlesMaster.DOI = articleModel.DOI;
                    articlesMaster.Category = articleModel.Category;
                    articlesMaster.SubCategory = articleModel.SubCategory;
                    articlesMaster.CreatedOn = articleModel.CreatedOn;
                    articlesMaster.CreatedBy = articleModel.CreatedBy;
                   
                }
                
                return Db.SaveChanges();

            }
        }
        public int DeleteArticle(int id)
        {
            using (var Db = new AIJDBEntities())
            {

                var articlesMaster = Db.ArticlesMasters.FirstOrDefault(x => x.ArticleId == id);
                if (articlesMaster != null)
                {
                    Db.ArticlesMasters.Remove(articlesMaster);

                }
                return Db.SaveChanges();

            }
        }
        public List<ArticleVM> GetAllArticles()
        {
            using (var Db=new AIJDBEntities())
            {
                var list = (from a in Db.ArticlesMasters
                            select new ArticleVM
                            {
                                ArticleId=a.ArticleId,
                                JournalId =a.JournalId,
                                IssueId = a.IssueId,
                                ArticleTitle=a.ArticleTitle,
                                Description=a.Description,
                                Abstract = a.Abstract,
                                HtmlText = a.HtmlText,
                                JournalImage=a.JournalImage,
                                Authors = a.Authors,
                                FirstPage =a.FirstPage,
                                LastPage = a.LastPage,
                                Extra = a.Extra,
                                PDFPath = a.PDFPath,
                                XMLPath = a.XMLPath,
                                EpubPath = a.EpubPath,
                                DOI = a.DOI,
                                Category = a.Category,
                                SubCategory=a.SubCategory,
                                CreatedOn =a.CreatedOn,
                                CreatedBy =a.CreatedBy

                            }).ToList();
                return list;
            }
        }
        public List<ArticleVM> GetArticle(int id)
        {
            using (var Db = new AIJDBEntities())
            {
                var list = (from a in Db.ArticlesMasters.Where(x=>x.ArticleId==id)
                            select new ArticleVM
                            {
                                ArticleId = a.ArticleId,
                                JournalId = a.JournalId,
                                IssueId = a.IssueId,
                                ArticleTitle = a.ArticleTitle,
                                Description = a.Description,
                                Abstract = a.Abstract,
                                HtmlText = a.HtmlText,
                                JournalImage = a.JournalImage,
                                Authors = a.Authors,
                                FirstPage = a.FirstPage,
                                LastPage = a.LastPage,
                                Extra = a.Extra,
                                PDFPath = a.PDFPath,
                                XMLPath = a.XMLPath,
                                EpubPath = a.EpubPath,
                                DOI = a.DOI,
                                Category = a.Category,
                                SubCategory = a.SubCategory,
                                CreatedOn = a.CreatedOn,
                                CreatedBy = a.CreatedBy

                            }).ToList();
                return list;
            }
        }
    }
}
