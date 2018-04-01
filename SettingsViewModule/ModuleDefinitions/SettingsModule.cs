using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SettingsViewModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsViewModule.ModuleDefinitions
{
    public class SettingsModule : IModule
    {
        IRegionManager regionManager;
        IUnityContainer container;
        public SettingsModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            IRegion region = regionManager.Regions["MainRegion"];
            SettingsView settingsView = container.Resolve<SettingsView>();
            region.Add(settingsView);
        }
    }
}
