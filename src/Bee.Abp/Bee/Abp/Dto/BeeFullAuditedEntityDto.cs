using System;
using Volo.Abp.Application.Dtos;

namespace Bee.Abp.Dto;

public abstract class BeeFullAuditedEntityDto : BeeFullAuditedEntityDto<Guid>
{
}

public abstract class BeeFullAuditedEntityDto<T> : AuditedEntityDto<T>
{
}