using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace YaDea.Messaging.Data;

public interface IMessageDbSchemaMigration : ITransientDependency
{
    Task MigrateAsync();
}
