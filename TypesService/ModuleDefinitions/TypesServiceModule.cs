using Microsoft.Practices.Unity;
using Prism.Modularity;
using SzafaInterfaces;

namespace TypesService.ModuleDefinitions
{
    public class TypesServiceModule : IModule
    {
        public TypesServiceModule(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<ITypesService, TypesService.Services.TypesService>(new ContainerControlledLifetimeManager());
        }
        IUnityContainer container;
    }
}
