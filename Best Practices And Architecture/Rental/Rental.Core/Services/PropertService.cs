using Microsoft.EntityFrameworkCore.Metadata;
using Rental.Core.Contracts;
using Rental.Core.Models;
using Rental.Infrustructer.DataBase.Comman;
using Rental.Infrustructer.DataBase.Models;

namespace Rental.Core.Services
{
    public class PropertService : IProperyService
    {
        private readonly IRepository repository;
        public PropertService(IRepository repository)
        {
            this.repository = repository;
            
        }
        public async Task<int> CreateAsync(PropertyModel model)
        {
            Property property = new Property
            {
                Area = model.Area,
                Price = model.Price,
                Location = model.Location,
            };
            await repository.AddAsync(property);
            await repository.SaveChangesAsync();
            return property.Id;

        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropertyModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PropertyModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsunc(int id, PropertyModel model)
        {
            throw new NotImplementedException();
        }
    }
}
