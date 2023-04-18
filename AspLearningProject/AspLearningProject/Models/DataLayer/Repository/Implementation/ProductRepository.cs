using Microsoft.EntityFrameworkCore;


namespace AspLearningProject.Models.DataLayer.Repository.Implementation
{

    public class ProductRepository : IDisposable, IProductRepository
    {
        private readonly AspLearningProject.Models.DataLayer.DataContext.DataContext context;

        public ProductRepository(AspLearningProject.Models.DataLayer.DataContext.DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products.Include(c => c.Category).Include(s => s.Supplier);
        }     

        public Product GetProductByID(int id)
        {
            
            return context.Products.Include(c => c.Category).Include(s => s.Supplier).Where(w =>w.ProductID == id).FirstOrDefault();
        }
        public IEnumerable<Product> GetProductsByQuantity(int? amount)
        {
            if (amount != null)
            {
                return context.Products.Include(c => c.Category).Include(s => s.Supplier).Take(amount.Value).OrderByDescending(o => o.ProductID); 
            }
            else
            {
                return GetProducts();
            }

            
        }
        public void InsertProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(int productID)
        {
            Product product = context.Products.Find(productID);
            context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
