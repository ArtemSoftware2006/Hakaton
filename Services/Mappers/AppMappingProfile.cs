using AutoMapper;
using Domain.Entity;
using Domain.Enum;
using Domain.Helper;
using Domain.ViewModel.Deal;
using Domain.ViewModel.User;

namespace Services.Mappers
{
    public class AppMappingProfile : Profile
    {   
        public AppMappingProfile()
		{			
			CreateMap<User, UserRegistrViewModel>().ReverseMap()
                .ForMember(dest => dest.IsVIP, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => 1000))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Role.User))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPasswordHelper.HashPassword(src.Password.Trim())));

            CreateMap<Deal, DealCardViewModel>().ReverseMap();
		}
    }
}