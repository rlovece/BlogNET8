using BlogNET8.Data;
using BlogNET8.DataAccess.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.DataAccess.Data.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbcontext;
        private ICategoryRepository _categoryRepository;
		private IArticleRepository _articleRepository;

		public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
		}

        //Se agregarán los distintos repos.
        public ICategoryRepository CategoryRepository {
			get
			{
				return _categoryRepository ??= new CategoryRepository(_dbcontext);
			}
			set
			{
				_categoryRepository = value;
			}
		}

		public IArticleRepository ArticleRepository
		{
			get
			{
				return _articleRepository ??= new ArticleRepository(_dbcontext);
			}
			set
			{
				_articleRepository = value;
			}
		}

		//Método para liberar recursos.
		public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public void Save()
        {
             _dbcontext.SaveChanges();
        }
    }
}
