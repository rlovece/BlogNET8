using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using BlogNET8.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogNET8.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SlidersController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;

		}

        public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(Slider slider)
		{
			if (ModelState.IsValid)
			{
                // Lógica para guardar img articulo y articulo
                string RootMain = _webHostEnvironment.WebRootPath; //ruta principal del root
                var files = HttpContext.Request.Form.Files; //array de archivos

                //Si el articulo tiene id 0, es decir, es decir que es nuevo. Y se ha cargado al menos un archivo.
                if(slider.Id == 0 && files.Count>0)
                {

					// genero un name con serie de nros para que los nombre de archivo no se repitan
					string fileName = Guid.NewGuid().ToString();
                    var upLoad = Path.Combine(RootMain, @"img\sliders"); //carpeta de subida
                    var extension = Path.GetExtension(files[0].FileName); //extensión del 1er archivo del array

                    //guardo el archivo
                    using (var fileStreams = new FileStream(Path.Combine(upLoad, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    slider.UrlImagen = @"\img\sliders\" + fileName + extension;
                    slider.Status = true;

                    //guardamos en la BD
                    _unitOfWork.SliderRepository.Add(slider);
                    _unitOfWork.Save();

					// Redirige a otra acción después de guardar
					return RedirectToAction("Index");

                }
			}

			// Si algo falla, vuelve a cargar la vista con el modelo actual
			return View(slider);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			Slider slider = new Slider();
			
            if (id != null)
            {
                slider = _unitOfWork.SliderRepository.Get(id.GetValueOrDefault());
				return View(slider);
			}
			else
			{
				return NotFound();
			}

			
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Slider slider)
		{
			var sliderBD = _unitOfWork.SliderRepository.Get(slider.Id);

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
					var upLoad = Path.Combine(RootMain, @"img\sliders"); //carpeta de subida
					var extension = Path.GetExtension(files[0].FileName); //extensión del 1er archivo del array

					//Busco la img anterior y si existe la borro
					if (sliderBD.UrlImagen != null)
					{
						var pathImg = Path.Combine(RootMain, sliderBD.UrlImagen.TrimStart('\\'));

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

					slider.UrlImagen = @"\img\sliders\" + fileName + extension;

				}
				else
				{
					//Si no se cargo img
					slider.UrlImagen = sliderBD.UrlImagen;
				}

				//guardamos en la BD
				_unitOfWork.SliderRepository.update(slider);
				_unitOfWork.Save();

				// Redirige a otra acción después de guardar
				return RedirectToAction("Index");
			}

			// Si algo falla, vuelve a cargar la vista con el modelo actual
			return View(slider);
		}


		#region LLamadas
		[HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.SliderRepository.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.SliderRepository.Get(id);

			
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
				_unitOfWork.SliderRepository.Remove(objFromDb);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Slider borrado con éxito" });
			}
			else 
            {
                return Json(new { success = false, message = "Error al borrar Slider" });
            }

            
        }

        #endregion
    }
}
