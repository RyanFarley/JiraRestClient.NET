using System.Reflection;
using CommonFramework.Container;
using Ninject;

namespace JiraRestClient.Container
{
    public class NinjectBootstrap: IContainerApplicationBootstrap
    {
        public IContainerManager Initialize(Assembly rootAssembly, bool plain)
        {
            IKernel kernel = plain ? new StandardKernel() : new StandardKernel(new NinjectLocalModule());
            return new NinjectContainerManager(kernel);
        }

        public bool IsJitInitializable { get { return true; } }
    }
}
