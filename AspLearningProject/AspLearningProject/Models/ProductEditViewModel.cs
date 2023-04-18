using AspLearningProject.Models.DataLayer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AspLearningProject.Models
{
    public class ProductEditViewModel
    {
        public ProductEditModel Product { get; set; }
        [ValidateNever]
        public IEnumerable<Category> Categories { get; set; }
        [ValidateNever]
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
