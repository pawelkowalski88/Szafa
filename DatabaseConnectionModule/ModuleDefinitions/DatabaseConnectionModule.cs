using DatabaseConnectionModule.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DatabaseConnectionModule.ModuleDefinitions
{
    public class DatabaseConnectionModule : IModule
    {
        public DatabaseConnectionModule(IRegionManager regionManager,
            IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterInstance<DatabaseConnectionService>(new DatabaseConnectionService());
        }

        IRegionManager regionManager;
        IUnityContainer container;
    }
}
