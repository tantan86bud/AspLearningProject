namespace AspLearningProject.Models.DataLayer.Repository.Implementation
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DataContext.DataContext context;
        private ProductRepository productRepository;
        private CategoryRepository categoryRepository;
        private SupplierRepository supplierRepository;
        private AspNetUsersRepository apsNetUsersRepository;

        public UnitOfWork(DataContext.DataContext context)
        {
            this.context = context;
        }
        public IProductRepository ProductRepository
        {
            get
            {

                if (productRepository == null)
                {
                    productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
            
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {

                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }
        public ISupplierRepository SupplierRepository
        {
            get
            {

                if (supplierRepository == null)
                {
                    supplierRepository = new SupplierRepository(context);
                }
                return supplierRepository;
            }
        }
        public IAspNetUsersRepository AspNetUsersRepository
        {
            get
            {

                if (apsNetUsersRepository == null)
                {
                    apsNetUsersRepository = new AspNetUsersRepository(context);
                }
                return apsNetUsersRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}