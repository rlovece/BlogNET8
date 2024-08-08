using BlogNET8.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BlogNET8.Models;

namespace BlogNET8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
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
		public IActionResult Create(Category newCategory)
		{

			if (newCategory == null)
			{
				// Registrar el error o manejarlo según sea necesario
				return BadRequest("Category cannot be null");
			}

			if (!ModelState.IsValid)
			{
				// Devolver la vista con los errores de validación
				return View(newCategory);
			}

			// Verificar si _unitOfWork o CategoryRepository es null
			if (_unitOfWork == null)
			{
				// Registrar el error o manejarlo según sea necesario
				return StatusCode(500, "Internal server error: UnitOfWork is null");
			}

			if (_unitOfWork.CategoryRepository == null)
			{
				// Registrar el error o manejarlo según sea necesario
				return StatusCode(500, "Internal server error: CategoryRepository is null");
			}

			try
			{
				// Lógica para guardar en la base de datos
				_unitOfWork.CategoryRepository.Add(newCategory);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				// Manejar la excepción (registrar, devolver error, etc.)
				// Registrar el error en un log, por ejemplo:
				// _logger.LogError(ex, "An error occurred while creating a category.");
				var errorMessage = $"Internal server error: {ex.Message}";
				if (ex.InnerException != null)
				{
					errorMessage += $" Inner Exception: {ex.InnerException.Message}";
				}
				return StatusCode(500, errorMessage);
			}
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Category category = new Category();
			category = _unitOfWork.CategoryRepository.Get(id);
			if (category == null)
			{
				return NotFound();
			}
			
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{

			if (category == null)
			{
				// Registrar el error o manejarlo según sea necesario
				return BadRequest("Category cannot be null");
			}

			if (!ModelState.IsValid)
			{
				// Devolver la vista con los errores de validación
				return View(category);
			}

			// Verificar si _unitOfWork o CategoryRepository es null
			if (_unitOfWork == null)
			{
				// Registrar el error o manejarlo según sea necesario
				return StatusCode(500, "Internal server error: UnitOfWork is null");
			}

			if (_unitOfWork.CategoryRepository == null)
			{
				// Registrar el error o manejarlo según sea necesario
				return StatusCode(500, "Internal server error: CategoryRepository is null");
			}

			try
			{
				// Lógica para guardar en la base de datos
				_unitOfWork.CategoryRepository.update(category);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				// Manejar la excepción (registrar, devolver error, etc.)
				// Registrar el error en un log, por ejemplo:
				// _logger.LogError(ex, "An error occurred while creating a category.");
				var errorMessage = $"Internal server error: {ex.Message}";
				if (ex.InnerException != null)
				{
					errorMessage += $" Inner Exception: {ex.InnerException.Message}";
				}
				return StatusCode(500, errorMessage);
			}
		}

		#region LLamadas
		[HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _unitOfWork.CategoryRepository.GetAll()});
        }

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var objFromDb = _unitOfWork.CategoryRepository.Get(id);
			if (objFromDb == null)
			{
				return Json (new {success = false, message = "Error al borrar categoría"});
			}

			_unitOfWork.CategoryRepository.Remove(objFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Categoría borrada con éxito" });
		}

        #endregion
    }
}
