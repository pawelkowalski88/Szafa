using ImageServiceModuleLibrary.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using SzafaInterfaces;

namespace ImageServiceModuleLibrary.ModuleDefinitions
{
    public class ImageServiceModule : IModule
    {
        public ImageServiceModule(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<IImageService, ImageService>(new ContainerControlledLifetimeManager());
        }
        IUnityContainer container;
    }
}
