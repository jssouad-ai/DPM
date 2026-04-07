using Application.Categories.Commands;
using Application.DTOs;
using Application.Images.Commands;
using Application.Projects.Commands;
using AutoMapper;
using Domain;
using Domain.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class MappingProfiles

    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                // Define your mappings here
                // Example: 
                // CreateMap<SourceType, DestinationType>();

                CreateMap<Category, CategoryDTO>().ReverseMap();
                CreateMap<ProjectDTO, Project>().ReverseMap();
                CreateMap<ImageDTO, Image>().ReverseMap();
                CreateMap<DomainBase, DomainBaseDTO>().ReverseMap();

                CreateMap<UpdateCategoryCommand, Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));

                CreateMap<CreateCategoryCommand, Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));

                CreateMap<CreateImageCommand, Image>().ForMember(dest => dest.ImgURL, opt => opt.MapFrom(src => src.Url))
                                                      .ForMember(dest => dest.ImgCaption, opt => opt.MapFrom(src => src.Caption));

                CreateMap<UpdateImageCommand, Image>().ForMember(dest => dest.ImgCaption, opt => opt.MapFrom(src => src.caption));

                CreateMap<CreateProjectCommand, Project>().ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                                                      .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description))
                                                      .ForMember(dest => dest.Images, opt => opt.Ignore())
                                                      .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

                CreateMap<UpdateProjectCommand, Project >().ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                                                      .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description))
                                                     .ForMember(dest => dest.Images, opt => opt.Ignore())
                                                      .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

              

            }
        }
    }
}
