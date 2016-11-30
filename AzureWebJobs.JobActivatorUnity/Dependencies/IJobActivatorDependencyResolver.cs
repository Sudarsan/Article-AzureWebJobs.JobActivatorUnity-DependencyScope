using System;

namespace AzureWebJobs.JobActivatorUnity.Dependencies
{
    public interface IJobActivatorDependencyResolver : IDisposable
    {
        IJobActivatorDependencyScope BeginScope();
    }
}