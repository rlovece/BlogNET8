using BlogNET8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogNET8.DataAccess.Data.Repository.IRepository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        void update(Slider slider);

    }
}
