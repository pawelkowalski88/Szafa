using FilteringServiceModule.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaInterfaces;

namespace FilteringServiceModule.ModuleDefinitions
{
    public class FilteringModule : IModule
    {
        IUnityContainer container;
        public FilteringModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IFilteringService, FilteringService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITypeFilteringConditionsService, TypeFilteringConditionsService>(new ContainerControlledLifetimeManager());
        }
    }
}
