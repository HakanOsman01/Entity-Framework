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
            string path = File.ReadAllText("../../../Datasets/parts.json");
            Console.WriteLine(ImportParts(context,path));
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
    }
}