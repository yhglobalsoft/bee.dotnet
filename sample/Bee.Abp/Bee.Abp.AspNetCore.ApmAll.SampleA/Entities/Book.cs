﻿using Volo.Abp.Domain.Entities.Auditing;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.Entities;

public class Book : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public float Price { get; set; }
}