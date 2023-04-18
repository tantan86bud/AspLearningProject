namespace AspLearningProject.Models.DataLayer.Repository
{
    public interface ISupplierRepository
    {
        void DeleteSupplier(int supplierID);
        void Dispose();
        Supplier GetSupplierByID(int id);
        IEnumerable<Supplier> GetSuppliers();
        void InsertSupplier(Supplier supplier);
        void Save();
        void UpdateSupplier(Supplier supplier);
    }
}