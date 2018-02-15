using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace ClothesListModule.ModuleDefinitions
{
    public class ClothesListModule : IModule
    {
        public ClothesListModule(IRegionManager regionManager,
            IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            IRegion region = regionManager.Regions["ClothesListRegion"];
            object newView = container.Resolve<Views.ClothesListView>();
            region.Add(newView);
            region.Activate(newView);
        }

        IRegionManager regionManager;
        IUnityContainer container;
    }
}
