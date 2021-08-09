using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Entities;
using WebAPI2.Models;
namespace WebAPI2
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            //mapujemy z właściwości klasy Restaurant do klasy RestaurantDto
            
            CreateMap<Restaurant, RestaurantDto>().ForMember(m => m.city, c => c.MapFrom(s => s.Address.city))
                .ForMember(m => m.street, c => c.MapFrom(s => s.Address.street))
                .ForMember(m => m.postalCode, c => c.MapFrom(s => s.Address.PostalCode));
            //takie same właściwości, mapowanie wykona się automatycznie
            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto,Restaurant>().ForMember(r=> r.Address, 
                c=> c.MapFrom(dto => new Address() 
                { city = dto.city, PostalCode=dto.postalCode, street = dto.street }));
        }

    }
}
