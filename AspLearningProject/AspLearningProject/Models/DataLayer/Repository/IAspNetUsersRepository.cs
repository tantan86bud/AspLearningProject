namespace AspLearningProject.Models.DataLayer.Repository;

public interface IAspNetUsersRepository
{
    IEnumerable<AspNetUser> GetAspNetUsers();
    AspNetUser AspNetUserByID(int id);
    void InsertCategory(AspNetUser aspNetUsers);
    void DeleteCategory(int aspNetUserID);
    void UpdateCategory(AspNetUser aspNetUser);
    void Save();
    void Dispose();
}