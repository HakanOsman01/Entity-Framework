using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rental.Infrustructer.DataBase.Models;

namespace Rental.Infrustructer.DataBase
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext() {}
        public RentalDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
          
            
        }
       
        public DbSet<Models.Property> Properties { get; set; } = null!;
       

    }
}
