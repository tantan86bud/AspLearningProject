using AspLearningProject.Models.DataLayer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AspLearningProject.Models.DataLayer.Repository.Implementation
{
    public class AspNetUsersRepository : IDisposable, IAspNetUsersRepository
  {
        private readonly DataContext.DataContext context;

        public AspNetUsersRepository(DataContext.DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<AspNetUser> GetAspNetUsers()
        {
            return context.AspNetUsers;
        }

        public AspNetUser AspNetUserByID(int id)
        {
           
            return context.AspNetUsers.Find(id);
        }

        public void InsertCategory(AspNetUser aspNetUsers)
        {
            context.AspNetUsers.Add(aspNetUsers);
        }

        public void DeleteCategory(int aspNetUserID)
        {
            AspNetUser aspNetUser = context.AspNetUsers.Find(aspNetUserID);
            context.AspNetUsers.Remove(aspNetUser);
        }

        public void UpdateCategory(AspNetUser aspNetUser)
        {
            context.Entry(aspNetUser).State = EntityState.Modified;
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