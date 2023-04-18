
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using RestSharp;

namespace AspLearningProject.API.Tests
{
    [Collection("App run")]
    public class ProductControllerTests
    {
        private ProductApi api;
        ApiClient apiClient;
        private const string URI = "http://localhost:5263/";
        private AppRun appRun;
        
        public ProductControllerTests( AppRun appRun)
        {
           
            this.appRun = appRun;
            api = new ProductApi(URI);
            api.ExceptionFactory = (string methodName, IRestResponse response) => {

                int status = (int)response.StatusCode;
                if (status <= 404)
                {
                    return null;
                }

                return Configuration.DefaultExceptionFactory(methodName, response);
            };

        }
        [Fact]
        public async Task GetAsync_ResponsCollectionProducts()
        {

            var products = await api.ApiProductGetAsync();
            Assert.True(products is List<ProductResource>);
            Assert.True(products.Count() > 0);
           
        }
        [Fact]
        public async Task ProductPOSTAsync_ResultNewProduct()
        {
            ProductEditResource productCreateResource = new ProductEditResource
            {
                CategoryID = 1,
                SupplierID = 2,
                Discontinued = true,
                ProductName = "TestName",
                QuantityPerUnit = "test",
                ReorderLevel = 1,
                UnitPrice = 1,
                UnitsInStock = 1,
                UnitsOnOrder = 1
            };
            Exception exception = null;
            try
            {
                await api.ApiProductPostAsync(productCreateResource);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.Null(exception);
        }
        [Fact]
        public async Task PUTAsync_ResultUpdateProduct()
        {
            ProductEditResource productUpdateResource = new ProductEditResource
            {
                CategoryID =0,
                SupplierID = 0,
                Discontinued = true,
                ProductName = "TestName",
                QuantityPerUnit = "test",
                ReorderLevel = 1,
                UnitPrice = 1,
                UnitsInStock = 1,
                UnitsOnOrder = 1
            };
            Exception exception = null;
            try
            {
                await api.ApiProductIdPutAsync(10, productUpdateResource);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.Null(exception);

        }
        [Fact]
        public async Task PUTAsyncDELETEAsync_ResultDeleteProductOrNot()
        {
            Exception exception = null;
            try
            {
                await api.ApiProductIdDeleteAsync(5);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.Null(exception);

        }

    }
}

