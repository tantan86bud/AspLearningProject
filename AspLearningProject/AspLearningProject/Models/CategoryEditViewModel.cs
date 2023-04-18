using AspLearningProject.Models.DataLayer;
using System.Web;


namespace AspLearningProject.Models
{
    public class CategoryEditViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public IFormFile FilePath {   get; set;} 
    }
}
