namespace AspLearningProject.Models.DataLayer.Repository
{
    public interface IProductRepository
    {
        void DeleteProduct(int productID);
        void Dispose();
        Product GetProductByID(int id);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product product);
        void Save();
        void UpdateProduct(Product product);
        IEnumerable<Product> GetProductsByQuantity(int? amount);
    }
}