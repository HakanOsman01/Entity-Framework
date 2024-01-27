using Microsoft.EntityFrameworkCore;
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
            Console.WriteLine(GetUsersWithProducts(context));


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
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts=context.Users
                .Include(p=>p.ProductsSold)
                .Include(p=>p.ProductsBought)
                .Where(p=>p.ProductsSold.Any(p=>p.BuyerId!=null))
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Select(p=> new
                {
                    firstName=p.FirstName,
                    lastName=p.LastName,
                    soldProduct=p.ProductsSold.Select(ps=> new
                    {
                        name=ps.Name,
                        price=ps.Price,
                        buyerFirstName=ps.Buyer.FirstName,
                        buyerLastName=ps.Buyer.LastName
                    })
                    .ToArray()


                })
                
                .ToArray();
            var json = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);
            return json;

        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.CategoriesProducts
               .GroupBy(gr => new { gr.CategoryId, gr.Category.Name })
               .Select(c => new
               {
                   category = c.Key.Name,
                   productsCount = c.Select(c=>c.Product).Count(),
                   averagePrice=Math.Round(c.Select(p=>p.Product.Price).Average(),2, MidpointRounding.AwayFromZero),
                   totalRevenue= Math.Round(c.Select(p => p.Product.Price).Sum(),2, MidpointRounding.AwayFromZero)
               })
               .OrderByDescending(c=>c.productsCount)
               .ToArray();
            string json = JsonConvert.SerializeObject(categories,Formatting.Indented);
            return json;


        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
              .Where(u => u.ProductsSold.Any(pb => pb.Buyer!=null))
              .Select(u => new
              {
                  u.LastName,
                  u.Age,
                  soldProducts = u.ProductsSold
                  .Where(p => p.Buyer != null)
                  .Select(ps => new
                  {
                      ps.Name,
                      ps.Price
                  })
                  .ToList()


              })
              .OrderByDescending(p => p.soldProducts.Count())
              .ToArray();
            var output = new
            {
                usersCount = users.Length,
                users = users.Select(u => new
                {
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.soldProducts.Select(sp => new
                    {
                        count = u.soldProducts.Count(),
                        products = u.soldProducts.Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price
                        })
                        .ToArray()
                    })
                })

            };
            JsonSerializerSettings serializerSettings=new JsonSerializerSettings(new JsonSerializerSettings
            {
                Formatting= Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });

            var json=JsonConvert.SerializeObject(output,serializerSettings);
            return json;



        }
    }
}