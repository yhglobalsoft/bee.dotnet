using Volo.Abp.Application.Dtos;

namespace Bee.Abp.Dto;

[Serializable]
public class BeePagedResultDto<T> : PagedResultDto<T>
{

    public BeePagedResultDto()
    {

    }

    public BeePagedResultDto(long totalCount, IReadOnlyList<T> items)
        : base(totalCount, items)
    {
    }
}