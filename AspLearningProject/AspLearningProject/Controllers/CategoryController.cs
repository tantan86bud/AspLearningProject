using AspLearningProject.Filters;
using AspLearningProject.Models;
using AspLearningProject.Models.DataLayer;
using AspLearningProject.Models.DataLayer.DataContext;
using AspLearningProject.Models.DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspLearningProject.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CategoryController : Controller
    {
        private readonly ConfigurationFeature configurationFeature;
        private IUnitOfWork unitOfWork;
        
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            
        }
        
        [Breadcrumb("Category", "Index",true)]
        [Breadcrumb("Home", "Index", Title: "Home")]
        public IActionResult Index()
        {
            var categoryes = unitOfWork.CategoryRepository.GetCategories().ToList();
            ViewBag.Title = "Categories";

            return View(categoryes);
        }
        
        [Route("Category/ViewImage/{image_id}")]
        [Route("/image/{image_id}")]
       
        public IActionResult ViewImage(int image_id)
        {
            byte[] imageData = unitOfWork.CategoryRepository.GetCategotyByID(image_id).Picture;

            return File(imageData, "image/jpeg");          
        }
        
        [HttpGet("EditImage")]
        [Breadcrumb("Category", "EditImage", true)]
        [Breadcrumb("Category", "Index", Title: "Categories")]
        [Breadcrumb("Home", "Index", Title: "Home")]
        public IActionResult EditImage(int id)
        {         
            Category category = unitOfWork.CategoryRepository.GetCategotyByID(id);
            CategoryEditViewModel categoryEditViewModel = new CategoryEditViewModel();
            categoryEditViewModel.CategoryID = id;
            categoryEditViewModel.Picture = category.Picture;


            byte[] imageData = category.Picture;
            var base64 = Convert.ToBase64String(imageData);
            ViewBag.Image= String.Format("data:image/jpg;base64,{0}", base64);
            ViewBag.File = File(imageData, "image/jpeg");
            ViewBag.Title = "Edit image";
            return View(categoryEditViewModel);
        }
        [HttpPost("EditImage")]
        public IActionResult EditImage(CategoryEditViewModel categoryEditViewModel)
        {
            using (var ms = new MemoryStream())
            {
                categoryEditViewModel.FilePath.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var category = unitOfWork.CategoryRepository.GetCategotyByID(categoryEditViewModel.CategoryID);
                byte[] imageData = category.Picture.ToArray();

                byte[] firstBytes = imageData.Take(78).ToArray();

                List<byte> list = new List<byte>();
                list.AddRange(firstBytes);
                list.AddRange(fileBytes);
                byte[] result = list.ToArray();
                category.Picture = result;
                unitOfWork.CategoryRepository.UpdateCategory(category);
                unitOfWork.Save();

            }
            return RedirectToAction("Index","Category");
        }
    }
}
