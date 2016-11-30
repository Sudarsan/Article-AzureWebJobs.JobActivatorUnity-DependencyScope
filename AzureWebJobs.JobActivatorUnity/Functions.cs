using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AzureWebJobs.JobActivatorUnity.Contracts;
using AzureWebJobs.JobActivatorUnity.Dependencies;
using Microsoft.Azure.WebJobs;

namespace AzureWebJobs.JobActivatorUnity
{
    public class Functions
    {
        private readonly IJobActivatorDependencyResolver jobActivatorDependencyResolver;
        private readonly INumberService numberService;

        public Functions(IJobActivatorDependencyResolver jobActivatorDependencyResolver, INumberService numberService)
        {
            if (jobActivatorDependencyResolver == null) throw new ArgumentNullException("jobActivatorDependencyResolver");
            if (numberService == null) throw new ArgumentNullException("numberService");

            this.jobActivatorDependencyResolver = jobActivatorDependencyResolver;
            this.numberService = numberService;
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public async Task ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log, CancellationToken ct)
        {
            log.WriteLine("New random number {0} from shared number service for message: {1}", this.numberService.GetRandomNumber(), message);

            using (var scope = this.jobActivatorDependencyResolver.BeginScope())
            {
                Console.WriteLine("Beginning scope work...");

                log.WriteLine("New random number {0} from scoped number service for message: {1}", scope.CreateInstance<INumberService>().GetRandomNumber(), message);

                await scope.CreateInstance<IUnitOfWork>().DoWork(ct, message);

                Console.WriteLine("Finishing scope work...");
            }
        }
    }
}