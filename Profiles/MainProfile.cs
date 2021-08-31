using AutoMapper;
using FutbolowaJaskinia.Models;
using FutbolowaJaskinia.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FutbolowaJaskinia.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<ApplicationUser, EditUserDTO>();
            CreateMap<EditUserDTO, EditUserInterDTO>();
            CreateMap<EditUserInterDTO, ApplicationUser>();
            CreateMap<NewsModel, NewsReadDTO>();
            CreateMap<NewsSignalDTO, NewsModel>().ReverseMap();

            CreateMap<NewsCreateDTO, NewsModel>()
                .ForMember(dest => dest.DateOfCreation, opts => opts.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Likes, opts => opts.MapFrom(src => new List<string>()))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(
                    (src, opt) =>
                    {
                        return src.PhotoUrl.Split(',').ToList();
                    }
                    ));
        }
    }
}
