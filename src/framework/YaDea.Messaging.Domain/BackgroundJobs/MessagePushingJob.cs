using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using YaDea.Messaging.BackgroundJobArgs;

namespace YaDea.Messaging.BackgroundJobs
{
    public class MessagePushingJob : AsyncBackgroundJob<MessagePushingArgs>, ITransientDependency
    {
        /// <summary>
        /// Executes the job with the <paramref name="args" />.
        /// </summary>
        /// <param name="args">Job arguments.</param>
        public override Task ExecuteAsync(MessagePushingArgs args)
        {
            return Task.FromResult(0);
        }
    }
}
