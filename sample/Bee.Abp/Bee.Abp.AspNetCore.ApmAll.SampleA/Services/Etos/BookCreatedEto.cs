using Bee.Abp.AspNetCore.ApmAll.SampleA.Entities;
using Volo.Abp.EventBus;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.Services.Etos;

[EventName("Bee.Abp.AspNetCore.ApmAll.SampleA.Book.Created")]
public class BookCreatedEto
{
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public float Price { get; set; }
}