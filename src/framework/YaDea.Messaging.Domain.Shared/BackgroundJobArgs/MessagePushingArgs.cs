using System;
using System.ComponentModel.DataAnnotations;
using YaDea.Messaging.Enums;
using YaDea.Messaging.Models;

namespace YaDea.Messaging.BackgroundJobArgs
{
    /// <summary>
    /// 消息任务
    /// </summary>
    public class MessagePushingArgs
    {
        /// <summary>
        /// id
        /// </summary>
        [Required]
#pragma warning disable CS8618
        public string Id { get; set; }
#pragma warning restore CS8618

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
#pragma warning disable CS8618
        public string Title { get; set; }
#pragma warning restore CS8618

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
#pragma warning disable CS8618
        public string Content { get; set; }
#pragma warning restore CS8618

        /// <summary>
        /// 通知范围 key：providerName，value：providerKey
        /// </summary>
        [Required]
#pragma warning disable CS8618
        public MessageScopeModel[] Scopes { get; set; }
#pragma warning restore CS8618

        /// <summary>
        /// 关联提供商类型（1：系统消息，2：短信，4：典字邮件）
        /// </summary>
        [Required]
        public PushProviderCode ProviderCode { get; set; }

        /// <summary>
        /// 发送人Id
        /// </summary>
        [Required]
        public Guid SendUserId { get; set; }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        [Required]
#pragma warning disable CS8618
        public string SendUserName { get; set; }
#pragma warning restore CS8618
    }
}