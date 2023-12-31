﻿using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YaDea.Messaging.Entities;
using YaDea.Messaging.EntityFrameworkCore.DbContext;
using YaDea.Messaging.Repositories;

namespace YaDea.Messaging.EntityFrameworkCore.Repositories
{
    public class MessageRepository : EfCoreRepository<IMessageDbContext, Message, Guid>, IMessageRepository
    {
        public MessageRepository(IDbContextProvider<IMessageDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}