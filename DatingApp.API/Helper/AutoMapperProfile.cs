using System.Linq;
using AutoMapper;
using DatingApp.API.DTOs;
using DatingApp.API.Models;

namespace DatingApp.API.Helper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListDto>()
                .ForMember(dest => dest.PhotoUrl,opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p =>p.IsMain).URL);
                })
                .ForMember(dest => dest.Age,opt =>{
                    opt.ResolveUsing(d => d.BirthDate.CalcAge());
                });
            CreateMap<User,UsersDetailsForDto>() .ForMember(dest => dest.PhotoUrl,opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p =>p.IsMain).URL);
                })
                .ForMember(dest => dest.Age,opt =>{
                    opt.ResolveUsing(d => d.BirthDate.CalcAge());
                });
            CreateMap<Photo,PhotosForDetailsDto>();
            CreateMap<UserDetailsForUpdate,User>();
        }
    }
}