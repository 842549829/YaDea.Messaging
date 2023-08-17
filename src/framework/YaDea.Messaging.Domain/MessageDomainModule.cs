using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;

namespace YaDea.Messaging;

[DependsOn(
    typeof(MessageDomainSharedModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpEmailingModule),
    typeof(AbpEventBusRabbitMqModule)
)]
public class MessageDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // 代码配置 实际配置写在配置文件里了
        Configure<AbpRabbitMqOptions>(options =>
        {
           
        });
        Configure<AbpRabbitMqEventBusOptions>(options =>
        {
        });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }
}
