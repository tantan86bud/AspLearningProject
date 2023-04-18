namespace AspLearningProject.Models.DataLayer.Repository
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryRepository { get; }
		IProductRepository ProductRepository { get;}
		ISupplierRepository SupplierRepository { get; }
        IAspNetUsersRepository AspNetUsersRepository { get; }

        void Dispose();
		void Save();
	}
}