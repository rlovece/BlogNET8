using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogNET8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
	public class MsjContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MsjContactController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
			
			return View(_unitOfWork.MsjContactRepository.GetAll());

			
        }

		[HttpPost]
		//public IActionResult Change(Int id)
		//{

		//	if (message == null)
		//	{
		//		// Registrar el error o manejarlo según sea necesario
		//		return BadRequest("Category cannot be null");
		//	}

		//	message.Answered = !message.Answered;

		//	// Verificar si _unitOfWork o CategoryRepository es null
		//	if (_unitOfWork == null)
		//	{
		//		// Registrar el error o manejarlo según sea necesario
		//		return StatusCode(500, "Internal server error: UnitOfWork is null");
		//	}

		//	if (_unitOfWork.MsjContactRepository == null)
		//	{
		//		// Registrar el error o manejarlo según sea necesario
		//		return StatusCode(500, "Internal server error: MsjContactRepository  is null");
		//	}

		//	try
		//	{
		//		// Lógica para guardar en la base de datos
		//		_unitOfWork.MsjContactRepository.update(message);
		//		_unitOfWork.Save();
		//		return RedirectToAction("Index");
		//	}
		//	catch (Exception ex)
		//	{
		//		// Manejar la excepción (registrar, devolver error, etc.)
		//		// Registrar el error en un log, por ejemplo:
		//		// _logger.LogError(ex, "An error occurred while creating a category.");
		//		var errorMessage = $"Internal server error: {ex.Message}";
		//		if (ex.InnerException != null)
		//		{
		//			errorMessage += $" Inner Exception: {ex.InnerException.Message}";
		//		}
		//		return StatusCode(500, errorMessage);
		//	}
		//}
		[HttpPost]
		public IActionResult Change(int id)
		{
			// Verificar si el id es válido
			if (id == 0)
			{
				return Json(new { success = false, message = "Id inválido." });
			}

			// Recuperar el mensaje de la base de datos
			var message = _unitOfWork.MsjContactRepository.GerFirstOrDefault(x => x.Id == id);
	
			if (message == null)
			{
				return Json(new { success = false, message = "Mensaje no encontrado." });
			}

			// Cambiar el estado de Answered
			message.Answered = !message.Answered;
			

			try
			{
				// Guardar los cambios en la base de datos
				_unitOfWork.MsjContactRepository.update(message);
				_unitOfWork.Save();

				// Devolver una respuesta JSON indicando éxito
				return Json(new { success = true, message = "Estado cambiado correctamente." });
			}
			catch (Exception ex)
			{
				// Manejar la excepción
				return Json(new { success = false, message = $"Error interno: {ex.Message}" });
			}
		}



		[HttpGet]
		public IActionResult GetAll()
		{
			return Json(new { data = _unitOfWork.MsjContactRepository.GetAll() });
		}
	}
}
