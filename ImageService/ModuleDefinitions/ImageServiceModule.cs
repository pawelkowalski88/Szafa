using ImageServiceModuleLibrary.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;

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
            container.RegisterInstance<ImageService>(new ImageService(container));
        }
        IUnityContainer container;
    }
}
