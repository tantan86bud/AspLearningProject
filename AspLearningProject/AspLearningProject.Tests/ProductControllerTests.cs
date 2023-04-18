using AspLearningProject.Controllers;
using AspLearningProject.Models;
using AspLearningProject.Models.DataLayer;
using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models.Interfacies;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AspLearningProject.Tests
{
    public class ProductControllerTests
	{
		Mock<IUnitOfWork> unitOfWork;
		Mock<IProductSettings> productSettings;
		Mock<IProductRepository> productRepository;
        Mock<ISupplierRepository> supplierRepository;
        Mock<ICategoryRepository> categoryRepository;
        Product product;

        public ProductControllerTests()
		{
           
            product = new Product();
            unitOfWork = new Mock<IUnitOfWork>();
			productRepository = new Mock<IProductRepository>();
            supplierRepository = new Mock<ISupplierRepository>();   
            categoryRepository = new Mock<ICategoryRepository>();

            List<Product> products = new List<Product>();
			products.Add(new Product());
			products.Add(new Product());

            unitOfWork.Setup(p => p.ProductRepository).Returns(productRepository.Object);
            unitOfWork.Setup(p => p.SupplierRepository).Returns(supplierRepository.Object);
            unitOfWork.Setup(p => p.CategoryRepository).Returns(categoryRepository.Object);

            productRepository.Setup(s => s.GetProductsByQuantity(2)).Returns(products);
            supplierRepository.Setup(s => s.GetSuppliers()).Returns(new List<Supplier>());
            categoryRepository.Setup(s => s.GetCategories()).Returns(new List<Category>());

            productRepository.Setup(s => s.InsertProduct(It.IsAny<Product>())); 

            productSettings = new Mock<IProductSettings>();
			productSettings.Setup(s => s.GetProductsAmount()).Returns(2);


        }

		[Fact]
		public void Index_ReturnsViewResult_WithListOfProduct()
		{
			var productController = new  ProductController(productSettings.Object, unitOfWork.Object);
            var result = productController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithProductEditViewModel()
        {
            var productController = new ProductController(productSettings.Object, unitOfWork.Object);
            int? id = null;
            var result = productController.Edit(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductEditViewModel>(
                viewResult.ViewData.Model);
            Assert.IsType<ProductEditViewModel>(model);
        }
        [Fact]
        public void Edit_InsertProductWasCalled()
        {
            var productController = new ProductController(productSettings.Object, unitOfWork.Object);
            ProductEditViewModel productEditViewModel = new ProductEditViewModel() { Product = new ProductEditModel() };
            var result = productController.Edit(productEditViewModel);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            productRepository.Verify(s => s.InsertProduct(It.IsAny<Product>()), Times.Once);
        }
        [Fact]
        public void Edit_UpdateProductNeverCalled()
        {
            var productController = new ProductController(productSettings.Object, unitOfWork.Object);
            ProductEditViewModel productEditViewModel = new ProductEditViewModel() { Product = new ProductEditModel() };
            var result = productController.Edit(productEditViewModel);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            productRepository.Verify(s => s.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}