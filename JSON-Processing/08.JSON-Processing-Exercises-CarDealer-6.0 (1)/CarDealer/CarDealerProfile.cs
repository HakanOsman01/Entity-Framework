using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuuplierDto, Supplier>();
            CreateMap<PartDto, Part>();
            CreateMap<CarDto, Car>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<SalesDto,Sale>();
            CreateMap<CarDto, Car>();
            CreateMap<Customer, ExportOrderDto>();
            CreateMap<Car,ExportToyotaCarsDto>();
            CreateMap<Car,ExportCarDto>();
        }
    }
}
