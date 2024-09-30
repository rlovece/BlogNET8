using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using BlogNET8.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;

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

        //Sin paginación
		//public IActionResult Index()
  //      {
  //          HomeVM homeVM = new HomeVM
  //          {
  //              Sliders = _unitOfWork.SliderRepository.GetAll(),
  //              Articles = _unitOfWork.ArticleRepository.GetAll()
  //          };

  //          ViewBag.IsHome = true;

  //          return View(homeVM);
  //      }

        //Con Paginación
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var articles = _unitOfWork.ArticleRepository.AsQueryable().OrderByDescending(a => a.Id);

            //paginación
            var pagesEntries = articles.Skip((page - 1) * pageSize).Take(pageSize);

            HomeVM homeVM = new HomeVM
            {
                Sliders = _unitOfWork.SliderRepository.GetAll(),
                Articles = pagesEntries,
                PageIndex = page,
                PageCount = (int)Math.Ceiling(articles.Count() / (double)pageSize)
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

        [HttpGet]
        public IActionResult ArticleSearch(String stringSearch, int page = 1, int pageSize = 6)
        {
            var articles = _unitOfWork.ArticleRepository.AsQueryable();

            //filtrar por string
            if (!string.IsNullOrEmpty(stringSearch)) 
            { 
                articles = articles.Where(a => a.Title.Contains(stringSearch)).OrderByDescending(a => a.Id);
            }

            //paginación
            var pagesEntries = articles.Skip((page-1)*pageSize).Take(pageSize);

            //creamos la pag
            var model = new PaginatedList<Article>(pagesEntries.ToList(), articles.Count(), page, pageSize, stringSearch);
            return View(model);

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
