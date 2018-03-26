using SQLiteDatabaseConnectionModule.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SzafaInterfaces;

namespace SQLiteDatabaseConnectionModule.ModuleDefinitions
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
            container.RegisterType<IDatabaseConnectionService, SQLiteDatabaseConnectionService>();
        }

        IRegionManager regionManager;
        IUnityContainer container;
    }
}
