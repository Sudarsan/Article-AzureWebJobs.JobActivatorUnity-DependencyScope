using System;
using AzureWebJobs.JobActivatorUnity.Dependencies;
using Microsoft.Practices.Unity;

namespace AzureWebJobs.JobActivatorUnity.Unity
{
    public class UnityJobActivatorHierarchicalDependencyResolver : IJobActivatorDependencyResolver
    {
        private readonly IUnityContainer container;

        public UnityJobActivatorHierarchicalDependencyResolver(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            this.container = container;
        }

        public IJobActivatorDependencyScope BeginScope()
        {
            return new UnityJobActivatorHierarchicalDependencyScope(this.container);
        }

        public void Dispose()
        {
            this.container.Dispose();
        }

        private sealed class UnityJobActivatorHierarchicalDependencyScope : IJobActivatorDependencyScope
        {
            private readonly IUnityContainer container;

            public UnityJobActivatorHierarchicalDependencyScope(IUnityContainer parentContainer)
            {
                this.container = parentContainer.CreateChildContainer();
            }

            public T CreateInstance<T>()
            {
                return this.container.Resolve<T>();
            }

            public void Dispose()
            {
                this.container.Dispose();
            }
        }
    }
}
