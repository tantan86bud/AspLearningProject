namespace ConsoleClient.Models
{
    public class Product
    {      
        public int productID { get; set; }      
        public string productName { get; set; }
        public int supplierID { get; set; }
        public int categoryID { get; set; }
        public string quantityPerUnit { get; set; }        
        public decimal unitPrice { get; set; }
        public short unitsInStock { get; set; }
        public short unitsOnOrder { get; set; }
        public short reorderLevel { get; set; }
        public bool discontinued { get; set; }        
        public Supplier supplier { get; set; }        
        public Category category { get; set; }

    }
}
