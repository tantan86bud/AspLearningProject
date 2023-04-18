using Microsoft.EntityFrameworkCore;

namespace AspLearningProject.Models.DataLayer.Repository.Implementation
{
    public class SupplierRepository : IDisposable, ISupplierRepository
    {
        private readonly AspLearningProject.Models.DataLayer.DataContext.DataContext context;

        public SupplierRepository(AspLearningProject.Models.DataLayer.DataContext.DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return context.Suppliers.ToList();
        }

        public Supplier GetSupplierByID(int id)
        {
            return context.Suppliers.Find(id);
        }

        public void InsertSupplier(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
        }

        public void DeleteSupplier(int supplierID)
        {
            Supplier supplier = context.Suppliers.Find(supplierID);
            context.Suppliers.Remove(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            context.Entry(supplier).State = EntityState.Modified;
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
