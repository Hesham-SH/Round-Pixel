using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Item, ItemDTO>()
        .ForMember(d => d.UnitOfMeasurement, o => o.MapFrom(s => s.UnitOfMeasurement.Id))
        .ForMember(d => d.UnitOfMeasurementName, o => o.MapFrom(s => s.UnitOfMeasurement.UOM));

        CreateMap<Order, OrderDTO>()
        .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.CustomerName));

        CreateMap<OrderDetails, OrderDetailsDTO>()
        .ForMember(d => d.ItemName, o => o.MapFrom(s => s.Item.ItemName));
    }
}
