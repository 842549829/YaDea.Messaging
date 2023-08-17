using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace YaDea.Messaging.Repositories
{
    public interface IMessageReceivingRepository : IRepository, ITransientDependency
    {
    }
}