namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Text;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            // DbInitializer.ResetDatabase(db);
            string result = GetBooksByPrice(db);
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
    }
}


