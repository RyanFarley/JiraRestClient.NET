using System;
using CommonFramework.Container;
using Ninject;

namespace JiraRestClient.Container
{
    public class NinjectContainerManager: IContainerManager
    {
        private readonly IKernel _kernel;

        public NinjectContainerManager(IKernel kernel)
        {
            _kernel = kernel;
        }

        public T GetInstance<T>(string key)
        {
            return _kernel.Get<T>(key);
        }

        public T GetInstance<T>()
        {
            return _kernel.Get<T>();
        }

        public void Register<TIface, TImpl>(string key)
            where TIface : class
            where TImpl : class, TIface
        {
            _kernel.Bind<TIface>().To<TImpl>().Named(key);
        }

        public void Register<T>(T instance) where T : class
        {
            // TODO: Ninject instance binding
            throw new NotImplementedException();
        }

        public void Register<TIface, TImpl>()
            where TIface : class
            where TImpl : class, TIface
        {
            _kernel.Bind<TIface>().To<TImpl>();
        }

        public void Register(Type iface, object instance)
        {
            // TODO: Ninject instance binding
            throw new NotImplementedException();
        }

        public void Register(Type iface, Type implementation)
        {
            _kernel.Bind(iface).To(implementation);
        }

        public void Release(object instance)
        {
            _kernel.Release(instance);
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }
    }
}
