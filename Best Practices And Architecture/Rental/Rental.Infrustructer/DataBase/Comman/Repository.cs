using Microsoft.EntityFrameworkCore;

namespace Rental.Infrustructer.DataBase.Comman
{
    public class Repository : IRepository
    {
        private readonly RentalDbContext context;

        public Repository(RentalDbContext rentalDbContext)
        {
            this.context = rentalDbContext;

        }
        private DbSet<T>GetDbSet<T>() where T : class 
        {
            return context.Set<T>();


        }
        public async Task  AddAsync<T>(T entity) where T : class
        {
            await this.GetDbSet<T>().AddAsync(entity);
            
        }

        public async Task AddRangeAsycn<T>(IEnumerable<T> values) where T : class
        {
            await this.GetDbSet<T>().AddRangeAsync(values);

        }

        public  IQueryable<T> All<T>() where T : class
        {
           return GetDbSet<T>().AsQueryable();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return GetDbSet<T>()
                .AsNoTracking()
                .AsQueryable();

        }

        public async Task DeleteAsync<T>(int id) where T : class
        {
            T? entity = await this.GetDbSet<T>().FindAsync(id);
            if(entity !=null)
            {

                this.GetDbSet<T>().Remove(entity);

            }
            


        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class
        {
            return await this.GetDbSet<T>().FindAsync(id);
           
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
            
        }
    }
}
