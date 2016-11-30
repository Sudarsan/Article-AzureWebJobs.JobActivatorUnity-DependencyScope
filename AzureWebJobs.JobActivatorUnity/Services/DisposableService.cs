using System;
using System.Threading;
using System.Threading.Tasks;
using AzureWebJobs.JobActivatorUnity.Contracts;

namespace AzureWebJobs.JobActivatorUnity.Services
{
    public class DisposableService : IDisposable, IUnitOfWork
    {
        public async Task DoWork(CancellationToken ct, string message)
        {
            Console.WriteLine("DisposableService doing work...");

            await Task.FromResult(true);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Console.WriteLine("DisposableService disposing...");
                }

                isDisposed = true;
            }
        }
    }
}