﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {

            var context = new CarDealerContext();
            string output = GetCarsFromMakeToyota(context);
            Console.WriteLine(output);



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
          
            var carsDto = JsonConvert.DeserializeObject<CarDto[]>(inputJson);

            var cars = new List<Car>();
            var carParts = new List<PartCar>();


            foreach (var carDto in carsDto)
            {

                var car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                   TraveledDistance= carDto.TraveledDistance,
                  
                   
                };

                foreach (var part in carDto.PartsId.Distinct())
                {
                    var carPart = new PartCar()
                    {
                        PartId = part,
                        Car = car
                    };

                    carParts.Add(carPart);
                }

            }

            context.Cars.AddRange(cars);

            context.PartsCars.AddRange(carParts);

            context.SaveChanges();

            return $"Successfully imported {context.Cars.Count()}.";
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
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            var mapper=new Mapper(config);

            var orderCustomersDto = context.Customers
                //.Select(s => new
                //{
                //    Name=s.Name,
                //    BirthDate=s.BirthDate,
                //    IsYoungDriver=s.IsYoungDriver

                //})
                .ProjectTo<ExportOrderDto>(mapper.ConfigurationProvider)
                .OrderBy(o=>o.BirthDate)
                .ToArray();
            for (int i = 0; i < orderCustomersDto.Length-1; i++)
            {
                if (orderCustomersDto[i].BirthDate==orderCustomersDto[i + 1].BirthDate)
                {
                    if (orderCustomersDto[i].IsYoungDriver==true 
                        && orderCustomersDto[i].IsYoungDriver == false)
                    {
                        var swap = orderCustomersDto[i];
                        orderCustomersDto[i] = orderCustomersDto[i + 1];
                        orderCustomersDto[i + 1] = swap;
                    }
                  

                }
            }
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "dd/MM/yyyy";
            jsonSettings.Formatting = Formatting.Indented;
            string jsonFormat=JsonConvert.SerializeObject(orderCustomersDto,jsonSettings);

            return jsonFormat;

        }
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var config = new MapperConfiguration(cfg 
                => cfg.AddProfile<CarDealerProfile>());
            var mapper=config.CreateMapper();
            var carsMakeByToyotaDtos=context.Cars
              .Where(c=>c.Make=="Toyota")
              .ProjectTo<ExportToyotaCarsDto>(mapper.ConfigurationProvider)
              .OrderBy(c=>c.Make)
              .ThenByDescending(c=>c.TraveledDistance)
              .ToArray();
            string jsonFormat = 
                JsonConvert.SerializeObject(carsMakeByToyotaDtos,Formatting.Indented);
            return jsonFormat;
        }


    }
}