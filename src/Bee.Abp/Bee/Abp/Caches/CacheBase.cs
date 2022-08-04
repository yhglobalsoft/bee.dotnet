namespace Bee.Abp.Caches;

[Serializable]
public abstract class CacheBase
{
    public Guid Id { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public Guid? LastModifierId { get; set; }
}