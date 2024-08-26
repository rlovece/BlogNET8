using BlogNET8.Data;
using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.DataAccess.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }


        public void BlockUser(string Id)
        {
            var userDb = _dbContext.ApplicationUser.FirstOrDefault(x => x.Id == Id);
            userDb.LockoutEnd = DateTime.Now.AddYears(1000); //Agregamos 1000 años
            _dbContext.SaveChanges();
        }

        public void UnblockUser(string Id)
        {
            var userDb = _dbContext.ApplicationUser.FirstOrDefault(x => x.Id == Id);
            userDb.LockoutEnd = DateTime.Now; //Hasta ahora
            _dbContext.SaveChanges();
        }
    }
}
