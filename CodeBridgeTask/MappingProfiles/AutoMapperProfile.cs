using AutoMapper;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DTOs;

namespace CodeBridgeTask.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DogDTO, Dog>().ReverseMap();
        CreateMap<QueryParamsDTO, QueryParams>().ReverseMap();
    }
}