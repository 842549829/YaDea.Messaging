using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace YaDea.Messaging;

[DependsOn(
    typeof(MessageDomainModule),
    typeof(MessageApplicationContractsModule)
    )]
public class MessageApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MessageApplicationModule>();
        });
    }
}
