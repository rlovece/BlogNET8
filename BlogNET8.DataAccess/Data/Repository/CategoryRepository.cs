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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogNET8.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

	
		public void update(Category category)
        {
            var objUpdate = _dbContext.Category.FirstOrDefault(c => c.Id == category.Id);
            if (objUpdate != null)
            {
                objUpdate.Name = category.Name;
                objUpdate.Order = category.Order;

               //_dbContext.SaveChanges();
            }
        }

		public SelectList GetList()
		{
            return new SelectList(_dbContext.Category, "Id", "Name");
		}

	}
}
