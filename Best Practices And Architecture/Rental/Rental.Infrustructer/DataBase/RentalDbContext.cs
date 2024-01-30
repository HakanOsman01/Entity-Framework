using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Rental.Infrustructer.DataBase
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }
        public DbSet<Property> Properties { get; set; } = null!;
       

    }
}
