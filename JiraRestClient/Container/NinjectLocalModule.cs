using Ninject.Modules;

namespace JiraRestClient.Container
{
    public class NinjectLocalModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IJiraRestClient>().To<JiraRestClient>();
        }
    }
}
