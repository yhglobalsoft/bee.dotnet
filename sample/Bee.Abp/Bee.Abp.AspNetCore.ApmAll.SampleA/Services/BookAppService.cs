using System.Text;
using Bee.Abp.AspNetCore.ApmAll.SampleA.Entities;
using Bee.Abp.AspNetCore.ApmAll.SampleA.Services.Dtos;
using Hangfire;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Json;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.Services;

public class BookAppService :
    CrudAppService<
        Book, //The Book entity
        BookDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto>, //Used to create/update a book
    IBookAppService //implement the IBookAppService
{
    private readonly ILogger<BookAppService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IConnectionMultiplexer _connection;

    public BookAppService(
        IRepository<Book, Guid> repository,
        ILogger<BookAppService> logger,
        IHttpClientFactory httpClientFactory,
        IJsonSerializer jsonSerializer,
        IUnitOfWorkManager unitOfWorkManager,
        IDistributedCache<string> cache,
        IConnectionMultiplexer connection)
        : base(repository)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("SampleB");
        _httpClient.BaseAddress = new Uri("https://localhost:5002");
        _jsonSerializer = jsonSerializer;
        _unitOfWorkManager = unitOfWorkManager;
        _connection = connection;
    }

    public override async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
    {
        await GetCacheAsync();

        // _logger.LogInformation($"开始创建书籍,请求参数：{input.ToJsonString()}");
        // var transaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;
        // ISpan span = null;
        // if (transaction != null)
        // {
        //     span = transaction.StartSpan("CreateAsync", "Service", "ApplicationService");
        // }
        

        var dto = await base.CreateAsync(input);
        // span?.End();
        // _unitOfWorkManager.Current.OnCompleted(async () =>
        // {
        //     await RequestSampleBAsync(dto.Name);
        // });
        _logger.LogInformation("完成创建书籍.");
        return dto;
    }

    private async Task<AuthorDto> RequestSampleBAsync(string authorName)
    {
        var createAuthorDto = new CreateAuthorDto
        {
            Name = authorName,
            BirthDate = DateTime.Now,
            ShortBio = authorName
        };

        var authorJson = _jsonSerializer.Serialize(createAuthorDto);
        var requestContent = new StringContent(authorJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/app/author", requestContent);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var createdAuthor = _jsonSerializer.Deserialize<AuthorDto>(content);
        return createdAuthor;
    }

    private async Task GetCacheAsync()
    {
        var database = _connection.GetDatabase();
        await database.StringSetAsync($"string1", 1);
        await database.StringGetAsync($"string1");
    }
}