using Microsoft.Practices.Unity;
using Prism.Modularity;
using ClothesService.Services;
using ImageServiceModuleLibrary.Services;
using SzafaInterfaces;

namespace ClothesService.ModuleDefinitions
{
    public class ClothesServiceModule : IModule
    {
        public ClothesServiceModule(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<IClothesServices, ClothesServices>(new ContainerControlledLifetimeManager());
        }
        IUnityContainer container;
    }
}
