using AutoMapper;
using Bee.Abp.AspNetCore.ApmAll.SampleB.Data;
using Bee.Abp.AspNetCore.ApmAll.SampleB.Services.Dtos;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB.ObjectMapping;

public class SampleBAutoMapperProfile : Profile
{
    public SampleBAutoMapperProfile()
    {
        CreateMap<Author, AuthorDto>();
    }
}
