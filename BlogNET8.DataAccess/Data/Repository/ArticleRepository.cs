using BlogNET8.Data;
using BlogNET8.Data.Migrations;
using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogNET8.DataAccess.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public void update(Article article)
        {
            var objUpdate = _dbContext.Article.FirstOrDefault(c => c.Id == article.Id);
            if (objUpdate != null)
            {
                objUpdate.Title = article.Title;
                objUpdate.Description = article.Description;
                objUpdate.CategoryId = article.CategoryId;
                objUpdate.UrlImagen = article.UrlImagen;
                objUpdate.UpdatedDate = DateTime.Now;

                //_dbContext.SaveChanges();
            }
        }

    }
}
