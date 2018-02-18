using ClothesDetailedViewModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace ClothesDetailedViewModule.ModuleDefinitions
{
    public class ClothesDetailedViewModule : IModule
    {

        public ClothesDetailedViewModule(IRegionManager regionManager,
            IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            var newView = container.Resolve<ClothesDetailedView>();
            region.Add(newView);
            region.Activate(newView);

            region = regionManager.Regions["ActionButtonsRegion"];
            var newActionButtonsView = container.Resolve<ClothesDetailsActionButtonsView>();
            region.Add(newActionButtonsView);
            region.Activate(newActionButtonsView);

            region = regionManager.Regions["LowerButtonsBar"];
            var newLowerButtonsBar = container.Resolve<ClothesDetailsLowerButtonsBar>();
            region.Add(newLowerButtonsBar);
            region.Activate(newLowerButtonsBar);
        }

        private IUnityContainer container;
        private IRegionManager regionManager;
    }
}
