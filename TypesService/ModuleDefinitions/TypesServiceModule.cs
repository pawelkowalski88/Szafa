using Microsoft.Practices.Unity;
using Prism.Modularity;
using TypesService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesService.ModuleDefinitions
{
    public class TypesServiceModule : IModule
    {
        public TypesServiceModule(IUnityContainer container)
        {
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterInstance<TypesService.Services.TypesService>(new TypesService.Services.TypesService(container));
        }
        IUnityContainer container;
    }
}
