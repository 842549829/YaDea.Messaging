using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using YaDea.Messaging.Entities;
using YaDea.Messaging.Managers;
using YaDea.Messaging.Models;
using YaDea.Messaging.Pushers;

namespace YaDea.Messaging.Publishers
{
    /// <summary>
    /// 消息发布默认实现
    /// </summary>
    public class DefaultMessagePublisher : IMessagePublisher
    {
        /// <summary>
        /// 消息推送接口
        /// </summary>
        private readonly IMessagePusher _messagePusher;

        /// <summary>
        /// 消息管理接口
        /// </summary>
        private readonly IMessageManager _messageManager;

        /// <summary>
        /// 后台任务仓储接口
        /// </summary>
        private readonly IBackgroundJobStore _backgroundJobStore;

        /// <summary>
        /// 后台任务管理接口
        /// </summary>
        private readonly IBackgroundJobManager _backgroundJobManager;

        public DefaultMessagePublisher(IMessagePusher messagePusher,
            IMessageManager messageManager,
            IBackgroundJobStore backgroundJobStore,
            IBackgroundJobManager backgroundJobManager)
        {
            _messagePusher = messagePusher;
            _messageManager = messageManager;
            _backgroundJobStore = backgroundJobStore;
            _backgroundJobManager = backgroundJobManager;
        }

        /// <summary>
        /// 消息发布
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>标识一个异步</returns>
        public Task PublishAsync(Message message)
        {
            return message.DelayedSend
                ? DelayPushAsync(message)
                : InstantPushAsync(message);
        }

        /// <summary>
        /// 消息及时推送
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>标识一个异步</returns>
        private async Task InstantPushAsync(Message message)
        {
            // 消息推送
            await _messagePusher.PusherAsync(message.Title,
                message.Content,
                message.Scopes.Select(MapToMessageScopeModel).ToArray(),
                message.PushProviderCode,
                message.Id.ToString(),
                message.SendUserId,
                message.SendUserName);

            // 标记消息为已发送
            message.MarkSent();

            await _messageManager.UpdateAsync(message);
        }

        /// <summary>
        /// 消息延时推送
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>标识一个异步</returns>
        private Task DelayPushAsync(Message message)
        {
            // 可将任务放到对应的调度作业中去
            throw new NotImplementedException();
        }

        /// <summary>
        /// 消息撤回
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>标识一个异步</returns>
        public Task RecallAsync(Message message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="messageScope">范围</param>
        /// <returns>范围模型</returns>
        private static MessageScopeModel MapToMessageScopeModel(MessageScope messageScope)
        {
            return new MessageScopeModel
            {
                ProviderName = messageScope.ProviderName,
                ProviderKey = messageScope.ProviderKey
            };
        }
    }
}