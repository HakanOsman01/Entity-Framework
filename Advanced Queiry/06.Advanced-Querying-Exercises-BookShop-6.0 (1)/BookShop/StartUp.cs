﻿namespace BookShop
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
            Console.WriteLine(RemoveBooks(db));


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
       
        //Ex 9
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

        //Ex 10
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
     
        //Ex 11
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

        //Ex 12
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books=context.Books.Where(b=>b.Title.Length>lengthCheck).ToList();
            return books.Count;
        }

        //Ex 13
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var books = context.Books
                .Include(b => b.Author)
                .Select(b=> new
                {
                    b.AuthorId,
                    AuthorFullName=b.Author.FirstName+' '+b.Author.LastName,
                    BooksInfo=b.Copies
                })
                .GroupBy(gr=> new {gr.AuthorId,gr.AuthorFullName})
                .Select(b=> new
                {
                    AuthorName=b.Key.AuthorFullName,
                    TotalBookCopies=b.Sum(gr=>gr.BooksInfo)

                })
                .OrderByDescending(b=>b.TotalBookCopies)
                .ToList();    
               
                

            StringBuilder sb=new StringBuilder();
            foreach (var item in books)
            {
                sb.AppendLine($"{item.AuthorName} " +
                    $"- {item.TotalBookCopies}");
            }
            return sb.ToString().Trim();
        }
        //Ex 14
      
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var totalProfitByCategory = context.BooksCategories

               .Select(b => new
               {
                   b.CategoryId,
                   BookCategory = b.Category.Name,
                   BookProfit =b.Book.Price*b.Book.Copies
                 
                  

               }).GroupBy(gb => new
               {
                   gb.CategoryId,
                   gb.BookCategory,

               })
               .Select(b => new
               {
                   b.Key.BookCategory,
                   TotalProfit=b.Sum(gb=>gb.BookProfit)
               })
               .OrderByDescending(b=>b.TotalProfit)
               .ThenBy(b=>b.BookCategory) 
               .ToList();
            StringBuilder sb=new StringBuilder();
            foreach (var item in totalProfitByCategory)
            {
                sb.AppendLine($"{item.BookCategory} ${item.TotalProfit:f2}");
            }
            return sb.ToString().Trim();
               
               
        }
        //Ex 15
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoryBooks = context.BooksCategories.GroupBy(gr=> new
            {
                gr.CategoryId, gr.Category.Name,
            })
                .Select(b => new
                {
                    CategoryName = b.Key.Name,
                    MostResentBooks = b.Select(x=> new
                    {
                       TitleBook=x.Book.Title,
                       ReleaseYearBook=x.Book.ReleaseDate
                    })
                    .OrderByDescending(b=>b.ReleaseYearBook)
                    .Take(3)
                    .ToArray()
                   

                }).OrderBy(b => b.CategoryName)
                .ToList();

            StringBuilder sb=new StringBuilder();
            foreach (var item in categoryBooks)
            {
                sb.AppendLine($"--{item.CategoryName}");
                foreach (var book in item.MostResentBooks)
                {
                    sb.AppendLine($"{book.TitleBook} " +
                        $"({book.ReleaseYearBook.Value.Year})");
                }
            }
            return sb.ToString().Trim();


        }

        //Ex 16
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
              .Where(b => b.ReleaseDate.Value.Year < 2010)
              .Select(b=>b.Price+5)
              .ToList();

            context.SaveChanges();
            

        }

        //Ex 17
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Copies < 4200).ToArray();
            context.Books.RemoveRange(books);
            context.SaveChanges();
            return books.Length;
        }
    }
}


