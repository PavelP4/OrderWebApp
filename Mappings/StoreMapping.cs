using AutoMapper;
using OrderWebApp.Infrastructure.Entities;
using OrderWebApp.Models;

namespace OrderWebApp.Mappings
{
    public class StoreMapping : Profile
    {
        public StoreMapping() 
        {
            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();
        }
    }
}
