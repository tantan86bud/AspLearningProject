using Microsoft.EntityFrameworkCore;

namespace AspLearningProject.Models.DataLayer.Repository.Implementation
{
    public class CategoryRepository : IDisposable, ICategoryRepository
    {
        private readonly AspLearningProject.Models.DataLayer.DataContext.DataContext context;

        public CategoryRepository(AspLearningProject.Models.DataLayer.DataContext.DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategotyByID(int id)
        {
            var category = context.Categories.Find(id);
            if (category != null)
            {
                category.Picture = category.Picture.Skip(78).ToArray();
            }            
            return category;
        }

        public void InsertCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void DeleteCategory(int categoryID)
        {
            Category category = context.Categories.Find(categoryID);
            context.Categories.Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
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
