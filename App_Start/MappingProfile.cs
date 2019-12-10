using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.EF;
using Vidly.Dtos;

namespace Vidly.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto,  Customer>();
            Mapper.CreateMap<Movy, MovieDto>();
            Mapper.CreateMap<MovieDto, Movy>();
            Mapper.CreateMap<Rental, NewRentalDto>();
            Mapper.CreateMap<NewRentalDto, Rental>();

           
        }
    }
}