using Bee.Abp.AspNetCore.ApmAll.SampleB.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB.Services;

public interface IAuthorAppService : IApplicationService
{
    Task<AuthorDto> GetAsync(Guid id);

    Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);

    Task<AuthorDto> CreateAsync(CreateAuthorDto input);
}