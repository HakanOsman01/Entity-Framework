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
            string endName=Console.ReadLine();
            string result = GetBooksByAuthor(db,endName);
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

        //Ex 6
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
        //Ex 7
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

        //Ex 8
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var parsedDate = DateTime.Parse(date);
            var books = context.Books.Where(b => b.ReleaseDate < parsedDate)
                .Select(b => new
                {
                    BookTitle = b.Title,
                    BookEditon = b.EditionType.ToString(),
                    PriceBook = b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();
             StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.BookTitle} - {book.BookEditon} - ${book.PriceBook:f2}");
            }
            return sb.ToString();

        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authots=context.Authors
                .Select(a=> new
            {
                   a.FirstName,
                FullName=a.FirstName +' '+ a.LastName,
            })
            .Where(a=>a.FirstName.EndsWith(input))
            .OrderBy(a=>a.FullName)
            .ToList();
            return string.Join(Environment.NewLine, authots.Select(a => a.FullName));
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books=context.Books
              .Where(b=>b.Title.ToLower().Contains(input.ToLower()))
              .Select(b=> new
              {
                  b.Title
              })
              .OrderBy(b=>b.Title)
              .ToList();
            return string.Join(Environment.NewLine, books.Select(b => b.Title));


             
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksWithAuthors = context.Books
               .Include(b => b.Author)
               .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
               .Select(b => new
               {
                   b.BookId,
                   AuthorName = b.Author.FirstName + ' ' + b.Author.LastName,
                   Books=b.BookCategories.Select(b=> new
                   {
                      BookNames=b.Book.Title
                   })
                   .ToArray()


               })
               .OrderBy(b=>b.BookId)
               .ToList();
            StringBuilder sb=new StringBuilder();
            foreach (var author in booksWithAuthors)
            {
                foreach (var item in author.Books)
                {
                    sb.AppendLine($"{item.BookNames} ({author.AuthorName})");
                }
            }
            return sb.ToString().Trim();
               
        }
    }
}


