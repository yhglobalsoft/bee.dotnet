using Volo.Abp.Application.Dtos;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB.Services.Dtos;

public class GetAuthorListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}