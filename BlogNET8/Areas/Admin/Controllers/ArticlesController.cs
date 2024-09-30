using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using BlogNET8.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogNET8.Areas.Admin.Controllers
{
	
	[Area("Admin")]
	public class ArticlesController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticlesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;

		}

        [Authorize(Roles = "Admin, Autor")]
        public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        [Authorize(Roles = "Admin, Autor")]
        public IActionResult Create()
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = new Models.Article(),
                CategoriesList = _unitOfWork.CategoryRepository.GetList()
            };
            return View(articleVM);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Autor")]
        public IActionResult Create(ArticleVM newArticle)
		{
			if (ModelState.IsValid)
			{
                // Lógica para guardar img articulo y articulo
                string RootMain = _webHostEnvironment.WebRootPath; //ruta principal del root
                var files = HttpContext.Request.Form.Files; //array de archivos

                //Si el articulo tiene id 0, es decir, es decir que es nuevo. Y se ha cargado al menos un archivo.
                if(newArticle.Article.Id == 0 && files.Count>0)
                {

					// genero un name con serie de nros para que los nombre de archivo no se repitan
					string fileName = Guid.NewGuid().ToString();
                    var upLoad = Path.Combine(RootMain, @"img\articles"); //carpeta de subida
                    var extension = Path.GetExtension(files[0].FileName); //extensión del 1er archivo del array

                    //guardo el archivo
                    using (var fileStreams = new FileStream(Path.Combine(upLoad, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    newArticle.Article.UrlImagen = @"\img\articles\" + fileName + extension;
                    newArticle.Article.CreatedDate = DateTime.Now;
                    newArticle.Article.UpdatedDate = DateTime.Now;

                    //guardamos en la BD
                    _unitOfWork.ArticleRepository.Add(newArticle.Article);
                    _unitOfWork.Save();

					// Redirige a otra acción después de guardar
					return RedirectToAction("Index");

                }
              			
			}

			// Si algo falla, vuelve a cargar la vista con el modelo actual
			newArticle.CategoriesList = _unitOfWork.CategoryRepository.GetList();
			return View(newArticle);
		}

        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult Edit(int? id)
		{
			ArticleVM articleVM = new ArticleVM()
			{
				Article = new Models.Article(),
				CategoriesList = _unitOfWork.CategoryRepository.GetList()
			};

            if (id != null)
            {
                articleVM.Article = _unitOfWork.ArticleRepository.Get(id.GetValueOrDefault());
            }

			return View(articleVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ArticleVM newArticle)
		{
			var articleBD = _unitOfWork.ArticleRepository.Get(newArticle.Article.Id);

			if (ModelState.IsValid)
			{
				// Lógica para guardar img articulo y articulo
				string RootMain = _webHostEnvironment.WebRootPath; //ruta principal del root
				var files = HttpContext.Request.Form.Files; //array de archivos

				//Si se agrego una img
				if (files.Count > 0)
				{

					// genero un name con serie de nros para que los nombre de archivo no se repitan
					string fileName = Guid.NewGuid().ToString();
					var upLoad = Path.Combine(RootMain, @"img\articles"); //carpeta de subida
					var extension = Path.GetExtension(files[0].FileName); //extensión del 1er archivo del array

					//Busco la img anterior y si existe la borro
					if (articleBD.UrlImagen != null)
					{
						var pathImg = Path.Combine(RootMain, articleBD.UrlImagen.TrimStart('\\'));

						if (System.IO.File.Exists(pathImg))
						{
							System.IO.File.Delete(pathImg);
						}
					}

					//guardo el nuevo archivo
					using (var fileStreams = new FileStream(Path.Combine(upLoad, fileName + extension), FileMode.Create))
					{
						files[0].CopyTo(fileStreams);
					}

					newArticle.Article.UrlImagen = @"\img\articles\" + fileName + extension;
					newArticle.Article.CreatedDate = DateTime.Now;
					newArticle.Article.UpdatedDate = DateTime.Now;

				}
				else
				{
					//Si no se cargo img
					newArticle.Article.UrlImagen = articleBD.UrlImagen;

				}

				//guardamos en la BD
				_unitOfWork.ArticleRepository.update(newArticle.Article);
				_unitOfWork.Save();

				// Redirige a otra acción después de guardar
				return RedirectToAction("Index");
			}

			// Si algo falla, vuelve a cargar la vista con el modelo actual
			newArticle.CategoriesList = _unitOfWork.CategoryRepository.GetList();
			return View(newArticle);
		}


		#region LLamadas
		[HttpGet]
        [Authorize(Roles = "Admin, Autor")]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.ArticleRepository.GetAll(includeProperties: "Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ArticleRepository.Get(id);

			
			if (objFromDb != null)
			{
				//Borramos la imagen si existe
				if (objFromDb.UrlImagen != null)
				{
					string RootMain = _webHostEnvironment.WebRootPath; //ruta principal del root
					var pathImg = Path.Combine(RootMain, objFromDb.UrlImagen.TrimStart('\\'));

					if (System.IO.File.Exists(pathImg))
					{
						System.IO.File.Delete(pathImg);
					}

				}
				_unitOfWork.ArticleRepository.Remove(objFromDb);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Artículo con éxito" });
			}
			else 
            {
                return Json(new { success = false, message = "Error al borrar artículo" });
            }

            
        }

        #endregion
    }
}
