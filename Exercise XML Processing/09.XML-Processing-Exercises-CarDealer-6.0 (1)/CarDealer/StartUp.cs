using AutoMapper;
using CarDealer.Data;
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
            string xml = File.ReadAllText("../../../Datasets/cars.xml");
            Console.WriteLine(ImportCars(context, xml));
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
            Car[] cars = mapper.Map<Car[]>(carsDto);


            return "";

        }
    }
}