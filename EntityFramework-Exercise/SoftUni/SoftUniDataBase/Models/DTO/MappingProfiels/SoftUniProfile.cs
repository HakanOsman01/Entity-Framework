using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni.Models.DTO.MappingProfiels
{
    public class SoftUniProfile : Profile
    {
        public SoftUniProfile()
        {
            CreateMap<Employee, PersonDto>();

            
        }
    }
}
