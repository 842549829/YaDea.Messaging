using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Validation;
using YaDea.Messaging.Constants;
using YaDea.Messaging.Entities;
using YaDea.Messaging.Repositories;

namespace YaDea.Messaging.Managers
{
    /// <summary>
    /// 消息管理
    /// </summary>
    public class MessageManager : DomainService, IMessageManager
    {
        /// <summary>
        /// 消息仓储接口
        /// </summary>
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageRepository">消息仓储接口</param>
        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>返回消息</returns>
        public async Task<Message> CreateAsync(Message message, CancellationToken cancellationToken = default)
        {
            await ValidateAsync(message);
            return await _messageRepository.InsertAsync(message, true, cancellationToken);
        }

        /// <summary>
        /// 修改消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>返回消息</returns>
        public async Task<Message> UpdateAsync(Message message, CancellationToken cancellationToken = default)
        {
            return await _messageRepository.UpdateAsync(message, true, cancellationToken);
        }

        /// <summary>
        /// 消息验证
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <returns>标识一个异步方法</returns>
        /// <exception cref="AbpValidationException">验证出错异常</exception>
        protected virtual Task ValidateAsync(Message message)
        {
            if (message.ApplicationName.IsNullOrWhiteSpace())
            {
                throw new AbpValidationException(
                    "新消息需要ApplicationName!",
                    new List<ValidationResult>
                    {
                        new(
                            "ApplicationName不能为空!",
                            new[] {nameof(Message.ApplicationName)}
                        )
                    }
                );
            }

            if (message.ApplicationName.Length > MessageConstants.MaxApplicationNameLength)
            {
                var errorMessage =
                    $"ApplicationName最大长度{MessageConstants.MaxApplicationNameLength}!";

                throw new AbpValidationException(
                    errorMessage, new List<ValidationResult>
                    {
                        new(
                            errorMessage, new[] {nameof(Message.ApplicationName)}
                        )
                    }
                );
            }

            if (message.Title.IsNullOrWhiteSpace())
            {
                throw new AbpValidationException(
                    "新消息需要Title!",
                    new List<ValidationResult>
                    {
                        new(
                            "Title can not be empty!",
                            new[] {nameof(Message.Title)}
                        )
                    }
                );
            }

            if (message.Title.Length > MessageConstants.MaxTitleLength)
            {
                var errorMessage =
                    $"Title最大长度{MessageConstants.MaxTitleLength}!";

                throw new AbpValidationException(
                    errorMessage, new List<ValidationResult>
                    {
                        new(
                            errorMessage, new[] {nameof(Message.Title)}
                        )
                    }
                );
            }

            if (message.Content.IsNullOrWhiteSpace())
            {
                throw new AbpValidationException(
                    "新消息需要Content!",
                    new List<ValidationResult>
                    {
                        new(
                            "Content不能为空!",
                            new[] {nameof(Message.Content)}
                        )
                    }
                );
            }

            if (message.Content.Length > MessageConstants.MaxContentLength)
            {
                var errorMessage =
                    $"Content最大长度{MessageConstants.MaxContentLength}!";

                throw new AbpValidationException(
                    errorMessage, new List<ValidationResult>
                    {
                        new(
                            errorMessage, new[] {nameof(Message.Content)}
                        )
                    }
                );
            }

            if (message.SendUserName.Length > MessageConstants.MaxSendUserNameLength)
            {
                var errorMessage =
                    $"SendUserName最大长度{MessageConstants.MaxSendUserNameLength}!";

                throw new AbpValidationException(
                    errorMessage, new List<ValidationResult>
                    {
                        new(
                            errorMessage, new[] {nameof(Message.SendUserName)}
                        )
                    }
                );
            }

            if (message.Scopes.IsNullOrEmpty())
            {
                throw new AbpValidationException(
                    "新消息需要Scopes!",
                    new List<ValidationResult>
                    {
                        new(
                            "Scopes不能为空!",
                            new[] {nameof(Message.Scopes)}
                        )
                    }
                );
            }

            return Task.CompletedTask;
        }
    }
}