using AutoMapper;
using OrderWebApp.Extentions;
using OrderWebApp.Infrastructure.Entities;
using OrderWebApp.Models;

namespace OrderWebApp.Mappings
{
    public class OrderMapping : Profile
    {
        public OrderMapping() 
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom((s, d, dm, context) =>
                {
                    return context.MapEntities(s.OrderItems, d.OrderItems, sItem => sItem.Id, dItem => dItem.Id);
                }))
                ;

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
