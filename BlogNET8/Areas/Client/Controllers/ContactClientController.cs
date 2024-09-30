using BlogNET8.DataAccess.Data.Repository.IRepository;
using BlogNET8.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogNET8.Areas.Client.Controllers
{
    [Area("Client")]
    public class ContactClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MsjContact newMessage)
        {

            if (newMessage == null)
            {
                // Registrar el error o manejarlo según sea necesario
                return BadRequest("Category cannot be null");
            }

            if (!ModelState.IsValid)
            {
                // Devolver la vista con los errores de validación
                return View(newMessage);
            }

            // Verificar si _unitOfWork o CategoryRepository es null
            if (_unitOfWork == null)
            {
                // Registrar el error o manejarlo según sea necesario
                return StatusCode(500, "Internal server error: UnitOfWork is null");
            }

            if (_unitOfWork.MsjContactRepository == null)
            {
                // Registrar el error o manejarlo según sea necesario
                return StatusCode(500, "Internal server error: CategoryRepository is null");
            }

            try
            {
                // Lógica para guardar en la base de datos
                _unitOfWork.MsjContactRepository.Add(newMessage);
                _unitOfWork.Save();
                return RedirectToAction("Thank");
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

        public IActionResult Thank()
        {
            return View();
        }

    }
}
