using AspLearningProject.Models.Interfacies;
using ILogger = Serilog.ILogger;
namespace AspLearningProject.Models
{
    public class ProductSettings : IProductSettings
    {
        private readonly ConfigurationFeature configurationFeature;
        private readonly ILogger logger;
        public ProductSettings(ConfigurationFeature configurationFeature, ILogger logger)
        {
            this.configurationFeature = configurationFeature;
            this.logger = logger;
        }
        public int GetProductsAmount()
        {
            logger.Information("Configuration of ProductsAmount is reading");
            return configurationFeature.ProductsAmount;
        }
    }
}
