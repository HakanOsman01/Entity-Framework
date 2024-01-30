using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context= new CarDealerContext();
            Console.WriteLine(GetCarsFromMakeBmw(context));
        }
        private static Mapper GetMapper()
        {
            var cfg=new MapperConfiguration
                (cnf=>cnf.AddProfile<CarDealerProfile>());
           var mapper = new Mapper(cfg);
            return mapper;

        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer=new XmlSerializer(typeof(ImportSuppliersDto[])
                ,new XmlRootAttribute("Suppliers"));
             using  var reader=new StringReader(inputXml);
            ImportSuppliersDto[] suppliersDtos = (ImportSuppliersDto[]) 
                xmlSerializer.Deserialize(reader);
            var mapper=GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(suppliersDtos);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";


        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartsDto[])
                , new XmlRootAttribute("Parts"));
            using var reader=new StringReader(inputXml);
            ImportPartsDto[]partsDtos= (ImportPartsDto[])xmlSerializer.Deserialize(reader);
            var mapper=GetMapper();
            var supliersIds=context.Suppliers
                .Select(s=>s.Id)
                .ToList();
            Part[] parts = mapper.Map<Part[]>(partsDtos
                .Where(p => supliersIds.Contains(p.SupplierId)));
                
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Length}";

        }
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCarDto[])
              , new XmlRootAttribute("Cars"));
            using var reader = new StringReader(inputXml);
            var carsDto = (ImportCarDto[])xmlSerializer.Deserialize(reader);
            var mapper = GetMapper();
            List<Car>cars=new List<Car>();
            foreach (var carDto in carsDto)
            {
                Car car = mapper.Map<Car>(carDto);
                var partsIds = carDto.Parts
                    .Select(p=>p.Id)
                    .Distinct()
                    .ToArray();
                var carParts = new List<PartCar>();
                foreach (var partId in partsIds)
                {
                    carParts.Add(new PartCar
                    {
                        Car = car,
                        PartId = partId

                    });

                }
                car.PartsCars = carParts;
                cars.Add(car);


              
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}";

        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCustomerDto[]),
                new XmlRootAttribute("Customers"));
            using var reader = new StringReader(inputXml);
            var customersDto = (ImportCustomerDto[])xmlSerializer.Deserialize(reader);
            var mapper = GetMapper();
            Customer[] customers = mapper.Map<Customer[]>(customersDto);

            context.Customers.AddRange(customers);
            context.SaveChanges();


            return $"Successfully imported {customers.Length}";

        }
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer
                (typeof(ImportSalesDto[]), new XmlRootAttribute("Sales"));
            using var reader = new StringReader(inputXml);
            var salesDtos = (ImportSalesDto[])xmlSerializer.Deserialize(reader);
            var mapper = GetMapper();
            int[] cardIds = context.Cars.Select(c => c.Id).ToArray();
            Sale[] sales = mapper.Map<Sale[]>(salesDtos
                .Where(s=>cardIds.Contains(s.CarId)));
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Length}";

        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var mapper=GetMapper();

            var cars = context.Cars
            .Where(c => c.TraveledDistance > 2000000)
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .Take(10)
            .ProjectTo<ExportCarWithDistanceDto>(mapper.ConfigurationProvider)
            .ToArray();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCarWithDistanceDto[]),
                new XmlRootAttribute("cars"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty,string.Empty);

            StringBuilder sb= new StringBuilder();
            using(StringWriter writer=new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, cars,xsn);

            }
            return sb.ToString().TrimEnd();





        }
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var mapper=GetMapper();
            var cars = context.Cars
                .Where(c=>c.Make=="BMW")
                .OrderBy(c=>c.Model)
                .ThenByDescending(c=>c.TraveledDistance)
                .ProjectTo<ExportCarBWVDto>(mapper.ConfigurationProvider)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCarBWVDto[]),
                new XmlRootAttribute("cars"));
            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            StringBuilder sb= new StringBuilder();
            using(StringWriter writer=new StringWriter(sb))
            {
                xmlSerializer.Serialize (writer, cars,xsn);
            }
            return sb.ToString().TrimEnd();

        }
    }
}