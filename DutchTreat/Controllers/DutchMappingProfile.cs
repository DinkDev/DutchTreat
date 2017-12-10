namespace DutchTreat.Controllers
{
    using AutoMapper;
    using Data.Entities;
    using ViewModels;

    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(vm => vm.OrderId, map => map.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
