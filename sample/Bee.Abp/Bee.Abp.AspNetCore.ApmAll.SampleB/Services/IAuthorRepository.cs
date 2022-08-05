using Bee.Abp.AspNetCore.ApmAll.SampleB.Data;
using Volo.Abp.Domain.Repositories;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB.Services;

public interface IAuthorRepository : IRepository<Author, Guid>
{
    Task<Author> FindByNameAsync(string name);

    Task<List<Author>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}