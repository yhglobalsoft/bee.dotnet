using System.Threading.Tasks;

namespace Bee.Abp.DbMigrators;

public interface IBeeDbSchemaMigrator
{
    Task MigrateAsync();
        
    Type DbContextType { get; }
}