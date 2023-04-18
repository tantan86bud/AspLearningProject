using AspLearningProject.Controllers;
using AspLearningProject.Models.DataLayer;
using AspLearningProject.Models;
using AspLearningProject.Models.DataLayer.Repository;
using AspLearningProject.Models.DataLayer.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspLearningProject.Tests
{
    public class CategoryControllerTests
	{
        Mock<IUnitOfWork> unitOfWork;
        Mock<ICategoryRepository> categoryRepository;
        public  CategoryControllerTests()        
        {     
            unitOfWork = new Mock<IUnitOfWork>();           
            categoryRepository = new Mock<ICategoryRepository>();          
            unitOfWork.Setup(p => p.CategoryRepository).Returns(categoryRepository.Object);
            categoryRepository.Setup(s => s.GetCategories()).Returns(new List<Category>());
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfCategory()
        {
            var categoryController = new CategoryController(unitOfWork.Object);
            var result = categoryController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Category>>(
                viewResult.ViewData.Model);            
        }

    }
}
