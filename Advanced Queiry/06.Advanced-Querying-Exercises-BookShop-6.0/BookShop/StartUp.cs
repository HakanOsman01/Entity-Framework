namespace BookShop
{
    using BookShop.Models;
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
            //string input = Console.ReadLine();
            int result = RemoveBooks(db);
            Console.WriteLine(result);
        }
        public static string GetBooksByAgeRestriction
            (BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return "It s not a valid age restriction";
            }
            // SqlParameter sqlParameter = new("@ageRestriction", ageRestriction);
            var booktitles = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                 .Select(a => new
                 {
                     a.Title

                 })
                .OrderBy(a => a.Title)
                .ToList();

            return string.Join(Environment.NewLine, booktitles.Select(a => a.Title));
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            var gooldEdition = EditionType.Gold;
            var goldenBooks = context.Books
                .Where(b => b.EditionType == gooldEdition)
                .Where(b => b.Copies < 5000)
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                })
                .OrderBy(b => b.BookId)
                .ToList();
            return string.Join(Environment.NewLine, goldenBooks.Select(b => b.Title));


        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            int price = 40;
            FormattableString formattableString =
                $"SELECT * FROM Books AS b WHERE b.Price>{price} ORDER BY b.Price DESC";
            var bookByTitleAndPrice = context.Books
                .FromSqlInterpolated(formattableString);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in bookByTitleAndPrice)
            {
                stringBuilder.AppendLine($"{item.Title} - ${item.Price:f2}");
            }
            return stringBuilder.ToString().Trim();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
             .Where(b => b.ReleaseDate.Value.Year != year)
             .Select(b => new
             {
                 b.BookId,
                 b.Title,
             }).OrderBy(b => b.BookId)
             .ToList();
            return string.Join(Environment.NewLine, books.Select(b => b.Title));
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
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var bookTitles = context.Books
                .Where(b => EF.Functions.Like(b.Title.ToLower(), $"%{input.ToLower()}%"))
                .Select(b => new
                {
                    TitleBook = b.Title
                })
                .OrderBy(b => b.TitleBook)
                .ToArray();
            return string.Join(Environment.NewLine, bookTitles.Select(b => b.TitleBook));
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Include(b => b.Author)
                .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new
                {
                    b.BookId,
                    BookTitle = b.Title,
                    AuthorFullName = b.Author.FirstName + ' ' + b.Author.LastName

                })
                .OrderBy(b => b.BookId)
                .ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var item in books)
            {
                sb.AppendLine($"{item.BookTitle} ({item.AuthorFullName})");
            }
            return sb.ToString().Trim();


        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList();
            return books.Count;
        }
        public static string CountCopiesByAuthor(BookShopContext context)
            => string.Join(Environment.NewLine, context.Authors
                .Select(a => new
                {
                    Name = a.FirstName == null
                        ? a.LastName
                        : $"{a.FirstName} {a.LastName}",
                    Copies = a.Books
                        .Select(b => b.Copies)
                        .Sum()
                })
                .OrderByDescending(a => a.Copies)
                .Select(a => $"{a.Name} - {a.Copies}"));
        public static string GetTotalProfitByCategory(BookShopContext context)
            => string.Join(Environment.NewLine, context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    TotalProffit = c.CategoryBooks
                        .Select(cb => cb.Book.Price * cb.Book.Copies)
                        .Sum()
                })
                .OrderByDescending(c => c.TotalProffit)
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.TotalProffit:F2}"));
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories.Select(b => new
            {
                BookCategories = b.Name,
                BookTitleAndReleaseDate = b.CategoryBooks.Select(br => new
                {
                    BookTitle = br.Book.Title,
                    ReleaseDate = br.Book.ReleaseDate,
                })
                .OrderByDescending(r => r.ReleaseDate)
                .Take(3)
                .ToList()


            }).OrderBy(a=>a.BookCategories)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var bookCategory in books)
            {
                sb.AppendLine($"--{bookCategory.BookCategories}");
                foreach (var item in bookCategory.BookTitleAndReleaseDate)
                {
                    sb.AppendLine($"{item.BookTitle} ({item.ReleaseDate.Value.Year})");
                }
            }
            return sb.ToString().Trim();

        }
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
             .Where(b => b.ReleaseDate.Value.Year < 2010)
             .ToList();
            for (int i = 0; i < books.Count; i++)
            {
                books[i].Price+= 5;

            }
            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();
            for (int i = 0; i < books.Count; i++)
            {

                context.Remove(books[i]);
            }
            context.SaveChanges();
            int countRemoveBooks = books.Count;
            return countRemoveBooks;
        }

    }
}


