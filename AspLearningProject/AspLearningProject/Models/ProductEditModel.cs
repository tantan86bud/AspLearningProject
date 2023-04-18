using AspLearningProject.Models.DataLayer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AspLearningProject.Models
{
    public class ProductEditModel
    {

        [ValidateNever]
        public int? ProductID { get; set; }
        [Required(ErrorMessage = "Required field")]
        [StringLength(40, MinimumLength =2, ErrorMessage = "Product name should be between 2 and 40 characters")]
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }      
        public string QuantityPerUnit { get; set; }
        [ValidateNever]
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        [Required(ErrorMessage = "Required field")]
        public bool Discontinued { get; set; }       

        public Product GetProduct(Product product = null)
        {
            if (product == null)
                product = new Product();
            if(ProductID != null)
            product.ProductID = ProductID.Value;
            product.ProductName = ProductName;
            product.SupplierID = SupplierID;
            product.CategoryID = CategoryID;
            product.QuantityPerUnit = QuantityPerUnit;
            product.UnitPrice = UnitPrice;
            product.UnitsInStock = UnitsInStock;
            product.UnitsOnOrder = UnitsOnOrder;
            product.ReorderLevel = ReorderLevel;
            product.Discontinued = Discontinued;
            return product;
        }
        public void SetPropFromProduct(Product product)
        {            
            ProductID = product.ProductID;
            ProductName = product.ProductName;
            SupplierID = product.SupplierID;
            CategoryID = product.CategoryID;    
            QuantityPerUnit = product.QuantityPerUnit;
            UnitPrice = product.UnitPrice;
            UnitsInStock = product.UnitsInStock;
            UnitsOnOrder = product.UnitsOnOrder;
            ReorderLevel = product.ReorderLevel;
            Discontinued = product.Discontinued;          
        }
    }
}
