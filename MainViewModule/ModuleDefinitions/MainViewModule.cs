using MainViewModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainViewModule.ModuleDefinitions
{
    public class MainViewModule : IModule
    {
        IRegionManager regionManager;
        IUnityContainer container;

        public MainViewModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }

        public void Initialize()
        {
            IRegionManager regionManager = container.Resolve<IRegionManager>();
            IRegion region = regionManager.Regions["MainRegion"];
            object mainView = new MainView();
            region.Add(mainView, "MainView");
            region.Activate(mainView);
        }
    }
}
