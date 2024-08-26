using BlogNET8.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogNET8.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
			// Obtengo la lista de usuario
			//return View(_unitOfWork.UserRepository.GetAll());

			//Obtengo todos los usuarios menos el actual
			var claimIdentity = (ClaimsIdentity)this.User.Identity;
			var user = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_unitOfWork.UserRepository.GetAll(m => m.Id != user.Value));
        }

        [HttpGet]
        public IActionResult BlockUser(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            _unitOfWork.UserRepository.BlockUser(Id);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult UnblockUser(string Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			_unitOfWork.UserRepository.UnblockUser(Id);
			return RedirectToAction("Index");
		}
	}
}
