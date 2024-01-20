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

            CarDealerContext contex = new CarDealerContext();
            string jsonUsers = File.ReadAllText("../../../Datasets/parts.json");
            string result=ImportParts(contex, jsonUsers);
            Console.WriteLine(result);


        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration
                 (cnf => cnf.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);
            SupplerDto[]supplerDtos=JsonConvert.DeserializeObject<SupplerDto[]>(inputJson);
            Supplier[] suppliers = mapper.Map<Supplier[]>(supplerDtos);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Length}.";



        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var config=new MapperConfiguration
                (cnf=>cnf.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);
            var partDtos = JsonConvert.DeserializeObject<PartDto[]>(inputJson);
            Part[] parts = mapper.Map<Part[]>(partDtos);
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Length}.";


        }
    }
}