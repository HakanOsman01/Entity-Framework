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
            string path = File.ReadAllText("../../../Datasets/suppliers.json");
            Console.WriteLine(ImportSuppliers(context,path));
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
    }
}