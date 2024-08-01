using AutoMapper;
using Azure.Core;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Models;
/*Bu süreci kolaylaştırmak için kütüphaneyi kullanacağız AutoMapper. 
 * AutoMapper, nesneden nesneye eşlemeyi basitleştiren ve farklı nesneler arasında
 * özellikleri eşlemeyi kolaylaştıran yaygın olarak kullanılan bir kütüphanedir.*/

namespace WebApiProject.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Adresses, opt => opt.MapFrom(src => src.Adresses))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<CreateProductRequest, Product>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Name, opt => opt.Ignore())

            CreateMap<UpdateProductRequest, Product>();
            CreateMap<CreateAdressRequest, Adress>();
            CreateMap<UpdateAdressRequest, Adress>();
        }
    }
}