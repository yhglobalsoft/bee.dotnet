using AutoMapper;
using Bee.Abp.AspNetCore.ApmAll.SampleA.Entities;
using Bee.Abp.AspNetCore.ApmAll.SampleA.Services.Dtos;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.ObjectMapping;

public class SampleAAutoMapperProfile : Profile
{
    public SampleAAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
    }
}
