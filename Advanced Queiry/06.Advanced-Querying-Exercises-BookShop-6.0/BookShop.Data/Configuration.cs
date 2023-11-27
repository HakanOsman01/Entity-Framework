namespace BookShop.Data
{
    internal class Configuration
    {
        internal static string ConnectionString
            => @"Server=(LocalDB)\MSSQLLocalDB;Database
         =BookShop;Trusted_Connection = True;";
    }
}
