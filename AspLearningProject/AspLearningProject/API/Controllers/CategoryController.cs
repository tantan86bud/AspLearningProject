using System.ComponentModel;
using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using AspLearningProject.Models.DataLayer;
using AspLearningProject.Models.Interfacies;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspLearningProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ConfigurationFeature configurationFeature;
        private IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<Category> GetAsync()
        {
            return unitOfWork.CategoryRepository.GetCategories();
        }
        [HttpGet("Image/{id}"),]
        public ActionResult<byte[]> Get(int id)
        {
            var category = unitOfWork.CategoryRepository.GetCategotyByID(id);
            if (category == null)
                return NotFound();
            byte[] imageData = category.Picture;
            return imageData;
        }
        [HttpPut("Image/{id}")]
        public IActionResult Update(int id, IFormFile uploadedFile)
        {
            var category = unitOfWork.CategoryRepository.GetCategotyByID(id);
            if (category is null)
                return NotFound();
            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                var fileBytes = ms.ToArray();


                byte[] imageData = category.Picture.ToArray();
                byte[] firstBytes = imageData.Take(78).ToArray();

                List<byte> list = new List<byte>();
                list.AddRange(firstBytes);
                list.AddRange(fileBytes);

                byte[] result = list.ToArray();
                category.Picture = result;
            }
            unitOfWork.CategoryRepository.UpdateCategory(category);
            unitOfWork.Save();

            return Ok();
        }
    }
}
