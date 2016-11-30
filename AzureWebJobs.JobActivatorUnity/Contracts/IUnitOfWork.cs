using System.Threading;
using System.Threading.Tasks;

namespace AzureWebJobs.JobActivatorUnity.Contracts
{
    public interface IUnitOfWork
    {
        Task DoWork(CancellationToken ct, string message);
    }
}
