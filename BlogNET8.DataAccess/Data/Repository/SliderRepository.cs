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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SliderRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public void update(Slider slider)
        {
            var objUpdate = _dbContext.Slider.FirstOrDefault(c => c.Id == slider.Id);
            if (objUpdate != null)
            {
                objUpdate.Name = slider.Name;
                objUpdate.UrlImagen = slider.UrlImagen;
                objUpdate.Status = slider.Status;
            }
        }

    }
}
