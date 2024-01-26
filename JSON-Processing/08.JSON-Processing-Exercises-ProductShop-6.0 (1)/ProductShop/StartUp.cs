using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new ProductShopContext();
            //string path = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(context, path));
            Console.WriteLine(GetProductsInRange(context));


        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);
            if (products != null)
            {
                context.Products.AddRange(products);
                context.SaveChanges();

            }
           

            return $"Successfully imported {products.Length}";

        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var allCategories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            var validCategories = allCategories?.Where(c => c.Name is not null)
                .ToArray();
            if (validCategories != null)
            {
                context.Categories.AddRange(validCategories);
                context.SaveChanges();
                return $"Successfully imported {validCategories.Length}";
            }

            return "Successfully imported 0";

        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = 
                JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Length}";

        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var getProductsInRange=context.Products
                .Where(p=>p.Price>=500 && p.Price<=1000)
                .Select(p=> new
                {
                    name=p.Name,
                    price=p.Price,
                    seller=p.Seller.FirstName+' '+p.Seller.LastName

                })
                .OrderBy(p=>p.price)
                .ToList();
            string json=JsonConvert.SerializeObject(getProductsInRange,Formatting.Indented);
            return json;


        }
    }
}