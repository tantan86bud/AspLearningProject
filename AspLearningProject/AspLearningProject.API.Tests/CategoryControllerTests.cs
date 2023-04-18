
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Model;

namespace AspLearningProject.API.Tests
{
    [Collection("App run")]
    public class CategoryControllerTests
    {
        //private HttpClient client;
        private CategoryApi api;
        private const string URI = "http://localhost:5263/";
        private AppRun appRun;

        public CategoryControllerTests(AppRun appRun)
        {

            this.appRun = appRun;
            api = new CategoryApi(URI);
           
        }
        [Fact]
        public async Task GetAsync_ResponsCollectionCategories()
        {

            var categories = await api.ApiCategoryGetAsync();
            Assert.True(categories is List<Category>);
            Assert.True(categories.Count() > 0);

        }
        [Fact]
        public async Task GetImageAsync_ResultIsBytes()
        {
            byte[] image = await api.ApiCategoryImageIdGetAsync(7);

            Assert.True(image != null);
            Assert.True(image.Length > 0);
        }

    }
}

