using System;
using Volo.Abp.Domain.Repositories;
using YaDea.Messaging.Entities;

namespace YaDea.Messaging.Repositories
{
    /// <summary>
    /// 消息仓储接口
    /// </summary>
    public interface IMessageRepository : IRepository<Message, Guid>
    {
    }
}