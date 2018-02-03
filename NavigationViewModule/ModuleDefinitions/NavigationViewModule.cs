using Microsoft.Practices.Unity;
using NavigationViewModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationViewModule.ModuleDefinitions
{
    public class NavigationViewModule : IModule
    {
        public NavigationViewModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            IRegion region = regionManager.Regions["NavigationRegion"];
            var newView = container.Resolve<NavigaitonView>();
            region.Add(newView);
            region.Activate(newView);
        }
        IRegionManager regionManager;
        IUnityContainer container;
    }
}
