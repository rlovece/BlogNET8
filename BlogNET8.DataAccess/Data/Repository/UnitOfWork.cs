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
		private ISliderRepository _sliderRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
			_categoryRepository = new CategoryRepository(_dbcontext);
			_articleRepository = new ArticleRepository(_dbcontext);
			_sliderRepository = new SliderRepository(_dbcontext);
			_userRepository = new UserRepository(_dbcontext);	
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

		public ISliderRepository SliderRepository
		{
			get
			{
				return _sliderRepository ??= new SliderRepository(_dbcontext);
			}
			set
			{
				_sliderRepository = value;
			}
		}

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_dbcontext);
            }
            set
            {
                _userRepository = value;
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
