using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
