using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Security.Cryptography;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSuppliersDto, Supplier>();
            CreateMap<ImportPartsDto, Part>();
            CreateMap<ImportCarDto, Car>();
            CreateMap<CarDto, Car>();
        }
    }
}
