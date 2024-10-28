using AutoMapper;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DTOs;

namespace CodeBridgeTask.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateDogDTO, Dog>().ReverseMap();
        CreateMap<DogDTO, Dog>().ReverseMap();
        CreateMap<QueryParamsDTO, QueryParams>().ReverseMap();
    }
}