using AspLearningProject.API.Models;
using AspLearningProject.Models.DataLayer;
using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models.Interfacies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AspLearningProject.API.Controllers.Models;
using AutoMapper;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspLearningProject.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductSettings productSettings;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;


        public ProductController(IProductSettings productSettings, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.productSettings = productSettings;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET: api/<ProductController>

        [HttpGet]
        public IEnumerable<ProductResource> GetAsync()
        {
            var result = unitOfWork.ProductRepository.GetProductsByQuantity(productSettings.GetProductsAmount());
            return mapper.Map<IEnumerable<ProductResource>>(result);
        }

        [HttpPost]
        public IActionResult Create(ProductEditResource productCreateResource)
        {
            Product product = mapper.Map<Product>(productCreateResource);

            if (unitOfWork.CategoryRepository.GetCategotyByID(product.CategoryID) is null
                || unitOfWork.SupplierRepository.GetSupplierByID(product.SupplierID) is null)
            {
                return BadRequest();
            }

            unitOfWork.ProductRepository.InsertProduct(product);

            unitOfWork.Save();
            ProductResource productResource = mapper.Map < ProductResource>( unitOfWork.ProductRepository.GetProductByID(product.ProductID));

            return CreatedAtAction(nameof(Create), new { id = product.ProductID }, productResource);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductEditResource productUpdateResource)
        {
            Product product = unitOfWork.ProductRepository.GetProductByID(id);
            if (product is null 
                || unitOfWork.CategoryRepository.GetCategotyByID(productUpdateResource.CategoryID) is null
                || unitOfWork.SupplierRepository.GetSupplierByID(productUpdateResource.SupplierID) is null)
                return NotFound();
           
            mapper.Map(productUpdateResource, product);
            unitOfWork.ProductRepository.UpdateProduct(product);
            unitOfWork.Save();
            ProductResource productResource = mapper.Map<ProductResource>(unitOfWork.ProductRepository.GetProductByID(product.ProductID));
            return CreatedAtAction(nameof(Update), new { id = product.ProductID }, productResource);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = unitOfWork.ProductRepository.GetProductByID(id);

            if (product is null)
                return NotFound();

            unitOfWork.ProductRepository.DeleteProduct(id);
            try
            {
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
