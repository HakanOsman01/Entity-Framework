using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context= new CarDealerContext();
            string xml = File.ReadAllText("../../../Datasets/suppliers.xml");
            Console.WriteLine(ImportSuppliers(context, xml));
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
    }
}