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
    public class MsjContactRepository : Repository<MsjContact>, IMsjContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MsjContactRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public void update(MsjContact message)
        {
			var objUpdate = _dbContext.MsjContact.FirstOrDefault(c => c.Id == message.Id);
			if (objUpdate != null)
			{
                objUpdate.Answered = message.Answered;
                objUpdate.Message = message.Message;
                objUpdate.Name = message.Name;
                objUpdate.Email = message.Email;
				Console.WriteLine("Estado actualizado a: " + objUpdate.Answered); // Depurar
			}
		}
    }
}
