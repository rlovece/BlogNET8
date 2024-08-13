using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using BlogNET8.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogNET8.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;


		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _unitOfWork.SliderRepository.GetAll(),
                Articles = _unitOfWork.ArticleRepository.GetAll()
            };

            ViewBag.IsHome = true;

            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var articleDB = _unitOfWork.ArticleRepository.Get(id);
            return View(articleDB);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
