using Microsoft.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YaDea.Messaging.Entities;
using YaDea.Messaging.EntityFrameworkCore.Configs;

namespace YaDea.Messaging.EntityFrameworkCore.DbContext;

[ConnectionStringName(MessageDbProperties.ConnectionStringName)]
public class MessageDbContext : AbpDbContext<MessageDbContext>, IMessageDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public MessageDbContext(DbContextOptions<MessageDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBackgroundJobs();

        builder.ConfigureMessagingModel();
        builder.ConfigureMessagingModelRelation();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging(); //打印sql参数,测试有效
#endif
    }

    /// <summary>
    /// 消息
    /// </summary>
    public DbSet<Message> Messages { get; set; }
}
