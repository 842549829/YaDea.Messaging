using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;
using YaDea.Messaging.Enums;
using YaDea.Messaging.EventData;
using YaDea.Messaging.Managers;
using YaDea.Messaging.Migrations;
using YaDea.Messaging.Models;
using YaDea.Messaging.Repositories;

namespace YaDea.Messaging.Controllers
{
    /// <summary>
    /// 消息
    /// </summary>
    [Route("[controller]")]
    public class MessageController : AbpController
    {
        private readonly IDistributedEventBus _eventBus;

        public MessageController(IDistributedEventBus eventBus, IMessageRepository messageManager)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task<string> SendMessageAsync([FromBody] MessageViewDto input)
        {
            //发送系统消息
            await _eventBus.PublishAsync(new MessageGenerateEto
            {
                MessageId = Guid.NewGuid(),
                ApplicationName = "雅迪测试系统",
                MessageType = MessageType.Default,
                PushProviderCode = PushProviderCode.System,
                Title = input.Title,
                Content = input.Content,
                DelayedSend = true,
                SendTime = DateTime.Now.AddMinutes(5),
                SendUserId = Guid.NewGuid(),
                SendUserName = "测试",
                Scopes = new[]
                {
                    new MessageScopeModel
                    {
                        ProviderName = "U",
                        ProviderKey = "a797304b-3cc2-11ee-9413-08bfb83e8436"
                    }
                }
            });

            return "OK";
        }
    }
}
