namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
           // DbInitializer.ResetDatabase(db);
            string command = Console.ReadLine();
            if (command != null)
            {
                string result = GetBooksByAgeRestriction(db, command);
                Console.WriteLine(result);

            }
            


        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if(!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not valid";

            }
            var titles=context.Books.Where(b=>b.AgeRestriction==ageRestriction)
                .Select(b=>b.Title)
                .OrderBy(b=>b)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var title in titles)
            {
                sb.AppendLine(title);
            }
            return sb.ToString().Trim();
      
        }

    }
}


