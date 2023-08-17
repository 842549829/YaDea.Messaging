using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YaDea.Messaging.Entities;
using YaDea.Messaging.EntityFrameworkCore.Configs;

namespace YaDea.Messaging.EntityFrameworkCore.DbContext
{
    /// <summary>
    /// 消息DbContext接口
    /// </summary>
    [ConnectionStringName(MessageDbProperties.ConnectionStringName)]
    public interface IMessageDbContext : IEfCoreDbContext
    {
        /// <summary>
        /// 消息
        /// </summary>
        DbSet<Message> Messages { get; set; }
    }
}
