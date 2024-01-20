namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Text;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            // DbInitializer.ResetDatabase(db);
            string categoryeis=Console.ReadLine();
            string result = GetBooksByCategory(db,categoryeis);
            Console.WriteLine(result);


        }
        //Ex 2
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not valid";

            }
            var titles = context.Books.Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var title in titles)
            {
                sb.AppendLine(title);
            }
            return sb.ToString().Trim();

        }
        //Ex 3
        public static string GetGoldenBooks(BookShopContext context)
        {


            var goldenBooks = context.Books
            .Where(b => (int)b.EditionType == 2 && b.Copies < 5000)
            .Select(b => new
            {
                BookId = b.BookId,
                Title = b.Title
            })
            .OrderBy(b => b.BookId)
            .ToList();
            return string.Join(Environment.NewLine, goldenBooks.Select(g => g.Title));


        }
        //Ex 4
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(b=>b.Price>40)
                .Select(b=> new
                {
                    BookTitle=b.Title,
                    BookPrice=b.Price
                })
                .OrderByDescending(b=>b.BookPrice)
                .ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var book in books)
            {
                stringBuilder.AppendLine($"{book.BookTitle} - ${book.BookPrice:f2}");

            }
            return stringBuilder.ToString().Trim();
        }
        //Ex 5
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books.Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b=> new
                {
                    BookTitle=b.Title,
                    b.BookId
                })
                .OrderBy(b=>b.BookId)
                .ToList();
            return string.Join(Environment.NewLine, books.Select(b=>b.BookTitle));
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.Split(' ',StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();
            var books = context.BooksCategories
               .Include(b => b.Category)
                .Include(b=>b.Book)

               .Select(b=> new
               {
                   BookTitle=b.Book.Title,
                   BookCategory=b.Book.BookCategories
               })
               .Where(bc=>bc.BookCategory.Any(bc=>categories.Contains
               (bc.Category.Name.ToLower())))
               .OrderBy(b=>b.BookTitle)
               .ToList();
          
            return string.Join(Environment.NewLine,books.Select(b=>b.BookTitle));
        }
    }
}


