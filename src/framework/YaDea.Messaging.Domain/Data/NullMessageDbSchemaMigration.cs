using System.Threading.Tasks;

namespace YaDea.Messaging.Data;

public class NullMessageDbSchemaMigration : IMessageDbSchemaMigration
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
