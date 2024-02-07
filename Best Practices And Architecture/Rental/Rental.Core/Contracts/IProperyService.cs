using Rental.Core.Models;

namespace Rental.Core.Contracts
{
    public interface IProperyService
    {
        Task<int> CreateAsync(PropertyModel model);
        Task<IEnumerable<PropertyModel>> GetAllAsync();
        Task<PropertyModel> GetByIdAsync(Guid id);
        Task UpdateAsunc(int id, PropertyModel model);

        Task DeleteAsync(int id);
    }

}
