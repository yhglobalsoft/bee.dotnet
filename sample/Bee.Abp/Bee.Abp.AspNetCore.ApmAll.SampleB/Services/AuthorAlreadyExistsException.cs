using Volo.Abp;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB.Services;

public class AuthorAlreadyExistsException : BusinessException
{
    public AuthorAlreadyExistsException(string name)
        : base("0001")
    {
        WithData("name", name);
    }
}