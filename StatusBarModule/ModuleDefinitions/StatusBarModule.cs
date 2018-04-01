using Prism.Modularity;
using Prism.Regions;
using StatusBarModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusBarModule.ModuleDefinitions
{
    public class StatusBarModule : IModule
    {
        public StatusBarModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            IRegion region = regionManager.Regions["StatusBarRegion"];
            object newView = new StatusBarView(new ViewModels.StatusBarViewModel(regionManager));
            region.Add(newView);
            region.Activate(newView);
        }

        IRegionManager regionManager;
    }
}
