
using AspLearningProject.Controllers;
using AspLearningProject.Models.DataLayer;
using AspLearningProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspLearningProject.Tests
{
    public class HomeControllerTests
	{
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var homeController = new HomeController();
            var result = homeController.Index();
            Assert.IsType<ViewResult>(result);            
        }
    }
}
