using AspLearningProject.Filters;
using Microsoft.AspNetCore.Mvc;


namespace AspLearningProject.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            
            return View();
        }
           
    }
}
