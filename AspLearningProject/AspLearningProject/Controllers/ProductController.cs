using AspLearningProject.Models;
using AspLearningProject.Models.DataLayer.DataContext;
using AspLearningProject.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models.DataLayer.Repository.Implementation;
using AspLearningProject.Models.Interfacies;
using AspLearningProject.Filters;
using Azure;
using System.Net;

namespace AspLearningProject.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]    
    public class ProductController : Controller
    {
        private readonly IProductSettings productSettings;
        private IUnitOfWork unitOfWork;


        public ProductController(IProductSettings productSettings, IUnitOfWork unitOfWork)
        {
            this.productSettings = productSettings;
            this.unitOfWork = unitOfWork;
        }
        [Breadcrumb("Product", "Index", true)]
        [Breadcrumb("Home", "Index", Title: "Home")]
        public IActionResult Index()
        {
            ViewBag.Title = "Products";
            int? productsAmount = productSettings.GetProductsAmount() > 0 ? productSettings.GetProductsAmount() : null;
            var products = unitOfWork.ProductRepository.GetProductsByQuantity(productsAmount);
            var r = products.Count();
            return View(products);
        }
        [HttpGet("edit")]
        [TypeFilter(typeof(LogAttribute), Arguments = new object[] { true })]
        [Breadcrumb("Product", "Edit", true)]
        [Breadcrumb("Product", "Index", Title: "Products")]
        [Breadcrumb("Home", "Index", Title: "Home")]
        public IActionResult Edit(int? id)
        {
            ProductEditViewModel productEditViewModel = new ProductEditViewModel();
            productEditViewModel.Suppliers = unitOfWork.SupplierRepository.GetSuppliers();
            productEditViewModel.Categories = unitOfWork.CategoryRepository.GetCategories();
            if (id != null)
            {
                ViewBag.Title = "Edit product";
                Product ptoduct = unitOfWork.ProductRepository.GetProductByID(id.Value);
                productEditViewModel.Product = new ProductEditModel();
                productEditViewModel.Product.SetPropFromProduct(ptoduct);
                return View(productEditViewModel);
            }
            else
            {
                ViewBag.Title = "Create product";
                return View(productEditViewModel);
            }
        }
        [HttpPost("edit")]
        public IActionResult Edit(ProductEditViewModel productEditModel)
        {
            if (!ModelState.IsValid)
            {
                productEditModel.Suppliers = unitOfWork.SupplierRepository.GetSuppliers();
                productEditModel.Categories = unitOfWork.CategoryRepository.GetCategories();
                return View("Edit", productEditModel);
            }
            if (productEditModel.Product.ProductID == null)
            {
                unitOfWork.ProductRepository.InsertProduct(productEditModel.Product.GetProduct());
            }
            else
            {
                unitOfWork.ProductRepository.UpdateProduct(productEditModel.Product.GetProduct());

            }
            unitOfWork.Save();

            return RedirectToAction("Index", "Product");

        }
      
    }
}
