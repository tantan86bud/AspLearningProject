using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models.DataLayer.Repository.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspLearningProject.Controllers
{
    [Authorize(Policy = "Admin")]
	public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Users";
            var users = unitOfWork.AspNetUsersRepository.GetAspNetUsers();

            return View(users);
		}
	}
}
