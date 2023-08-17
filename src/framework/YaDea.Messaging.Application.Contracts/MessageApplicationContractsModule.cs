using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace YaDea.Messaging;

[DependsOn(
    typeof(MessageDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class MessageApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MessageDtoExtensions.Configure();
    }
}
