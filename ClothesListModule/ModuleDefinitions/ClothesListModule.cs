using ClothesService.Services;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
