using Microsoft.Practices.Unity;
using Prism.Modularity;
using ClothesService.Services;

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
            container.RegisterInstance<ClothesServices>(new ClothesServices(container));
        }
        IUnityContainer container;
    }
}
