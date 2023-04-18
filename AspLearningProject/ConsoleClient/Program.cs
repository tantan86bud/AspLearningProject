using ConsoleClient.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleTables;

namespace HttpClientSample
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
           
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                while (true)
                {
                    await RequestListObjects();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static async Task RequestListObjects()
        {
           
                Console.WriteLine("Select the list of objects you want to get. Character c- category, p - products");
                var keyValue = Console.ReadLine();
                if (keyValue == "p")
                {

                    // Get the product
                    var products = await GetAsync($"api/product", typeof(Product));
                    if (products != null)
                        ShowProduct(products as IEnumerable<Product>);
                }
                else if (keyValue == "c")
                {
                    // Get the product
                    var categories = await GetAsync($"api/category", typeof(Category));
                    if (categories != null)
                        ShowCategory(categories as IEnumerable<Category>);

                }
                else
                {
                    Console.WriteLine("Please enter a valid request");
                }
        }
        
        static async Task<IEnumerable<object>> GetAsync(string path, Type type)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions();
                    options.IncludeFields = true;
                    if (typeof(Product) == type)
                    {
                        return JsonSerializer.Deserialize <IEnumerable<Product>> (result, options);
                    }
                    if (typeof(Category) == type)
                    {
                        return JsonSerializer.Deserialize<IEnumerable<Category>>(result, options);
                    }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

           return null;
        }
        static void ShowProduct(IEnumerable<Product> products)
        {
            var table = new ConsoleTable("id", "ProductName", "CompanyName", "CategoryName", "QuatityPerUnit", "UnitPrice", "UnitsInStock", "UnitsOnOrder", "ReorderLevel", "Discontinued");
            foreach (var product in products)
            {
                table.AddRow(product.productID, product.productName, product.supplier.companyName, product.category.categoryName,
                    product.quantityPerUnit, product.unitPrice, product.unitsInStock, product.unitsOnOrder, product.reorderLevel, product.discontinued);

            }

            table.Write();
            Console.WriteLine();

        }
        static void ShowCategory(IEnumerable<Category> categories)
        {
            var table = new ConsoleTable("CategoryID", "CategoryName", "Description");
            foreach (var category in categories)
            {
                table.AddRow(category.categoryID, category.categoryName, category.description);

            }
            table.Write();
            Console.WriteLine();

        }

    }
}