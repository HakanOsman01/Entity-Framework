using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context=new CarDealerContext();
            string path = File.ReadAllText("../../../Datasets/sales.json");
            Console.WriteLine(ImportSales(context,path));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration(cnf 
                => cnf.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);
            SuuplierDto[] 
                suuplierDto = JsonConvert.DeserializeObject<SuuplierDto[]>(inputJson);

            Supplier[] suppliers = mapper.Map<Supplier[]>(suuplierDto);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var config=new MapperConfiguration
                (cnf=>cnf.AddProfile<CarDealerProfile>());
            var mapper=new Mapper(config);
            PartDto[] partDtos = JsonConvert.DeserializeObject<PartDto[]>(inputJson);
           
            Part[] parts = mapper.Map<Part[]>(partDtos);
            int[]validSuppliersIds=context.Suppliers.Select(x=>x.Id).ToArray();
            Part[] validParts = parts
                .Where(x => validSuppliersIds.Contains(x.SupplierId))
                .ToArray();
            context.Parts.AddRange(validParts);
            context.SaveChanges();
            return $"Successfully imported {parts.Length}.";
        }
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var config=new MapperConfiguration(cng=>cng.AddProfile<CarDealerProfile>());
            var mapper=new Mapper(config);
            CarDto[] carDtos = JsonConvert.DeserializeObject<CarDto[]>(inputJson);
            Car[] cars = mapper.Map<Car[]>(carDtos);
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Length}.";
        }
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var config=new MapperConfiguration(cfg=>cfg.AddProfile<CarDealerProfile>());
            var mapper=new Mapper(config);
            CustomerDto[] customerDtos = JsonConvert.DeserializeObject<CustomerDto[]>(inputJson);
            Customer[] customers = mapper.Map<Customer[]>(customerDtos);
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Length}.";
        }
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var config=new MapperConfiguration(cnf=>cnf.AddProfile<CarDealerProfile>());
            var mapper=new Mapper(config);
            SalesDto[] salesDtos = JsonConvert.DeserializeObject<SalesDto[]>(inputJson);
            Sale[] sales = mapper.Map<Sale[]>(salesDtos);
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Length}.";

        }
    }
}