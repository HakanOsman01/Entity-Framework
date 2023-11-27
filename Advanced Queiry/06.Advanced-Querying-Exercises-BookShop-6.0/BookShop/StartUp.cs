namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            string input = Console.ReadLine();
            string result = GetAuthorNamesEndingIn(db,input);
            Console.WriteLine(result);
        }
        public static string GetBooksByAgeRestriction
            (BookShopContext context, string command)
        {
            if(!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return "It s not a valid age restriction";
            }
           // SqlParameter sqlParameter = new("@ageRestriction", ageRestriction);
            var booktitles=context.Books
                .Where(b=>b.AgeRestriction == ageRestriction)
                 .Select(a=>new
                 {
                     a.Title

                 })
                .OrderBy(a=>a.Title)
                .ToList();

            return string.Join(Environment.NewLine, booktitles.Select(a=>a.Title));
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            var gooldEdition = EditionType.Gold;
            var goldenBooks = context.Books
                .Where(b=>b.EditionType==gooldEdition)
                .Where(b=>b.Copies< 5000)
                .Select(b=> new
                {
                    b.BookId,
                    b.Title,
                })
                .OrderBy(b=>b.BookId)
                .ToList();
            return string.Join(Environment.NewLine,goldenBooks.Select(b=>b.Title));


        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            int price = 40;
            FormattableString formattableString =
                $"SELECT * FROM Books AS b WHERE b.Price>{price} ORDER BY b.Price DESC";
            var bookByTitleAndPrice = context.Books
                .FromSqlInterpolated(formattableString);
            StringBuilder stringBuilder= new StringBuilder();
            foreach (var item in bookByTitleAndPrice)
            {
                stringBuilder.AppendLine($"{item.Title} - ${item.Price:f2}");
            }
            return stringBuilder.ToString().Trim();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
             .Where(b=>b.ReleaseDate.Value.Year!=year)
             .Select (b=>new
             {
                 b.BookId,
                 b.Title,
             }).OrderBy(b=>b.BookId)
             .ToList();
            return string.Join(Environment.NewLine,books.Select(b=>b.Title));
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categoryBooks = input.ToLower()
             .Split(' ', StringSplitOptions.RemoveEmptyEntries)
             .ToArray();
            var books = context.BooksCategories
                .Include(c => c.Category)
                .Include(b => b.Book)
                .Where(b => categoryBooks
                .Contains(b.Category.Name.ToLower()))
                .Select(t => new
                {
                    BookTitle = t.Book.Title
                })
                .OrderBy(b => b.BookTitle)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.BookTitle));

        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .Select(b => new
                {
                    TitleBook = b.Title,
                    EditionTypeOfBook = b.EditionType,
                    PriceBook = b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.TitleBook} - {book.EditionTypeOfBook} - ${book.PriceBook:f2}");
            }
            return sb.ToString().Trim();


        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + ' ' + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToArray();
            return string.Join(Environment.NewLine, authors.Select(a => a.FullName));

        }
    }
}


