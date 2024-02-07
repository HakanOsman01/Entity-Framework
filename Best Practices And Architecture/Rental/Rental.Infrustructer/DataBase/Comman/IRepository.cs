namespace Rental.Infrustructer.DataBase.Comman
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : class;
        Task AddAsync<T>(T entity) where T : class;

        Task AddRangeAsycn<T>(IEnumerable<T> values) where T : class;
        Task DeleteAsync<T>(int id) where T: class;

        IQueryable<T>All<T>() where T : class;

        IQueryable<T>AllReadOnly<T>() where T : class;

        Task SaveChangesAsync();






    }
}
