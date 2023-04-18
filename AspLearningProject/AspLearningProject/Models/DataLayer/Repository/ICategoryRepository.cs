namespace AspLearningProject.Models.DataLayer.Repository
{
    public interface ICategoryRepository
    {
        void DeleteCategory(int categoryID);
        void Dispose();
        Category GetCategotyByID(int id);
        IEnumerable<Category> GetCategories();
        void InsertCategory(Category category);
        void Save();
        void UpdateCategory(Category category);
    }
}