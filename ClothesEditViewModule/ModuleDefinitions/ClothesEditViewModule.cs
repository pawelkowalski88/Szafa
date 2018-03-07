using ClothesEditViewModule.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using SzafaInterfaces;

namespace ClothesEditViewModule.ModuleDefinitions
{
    public class ClothesEditViewModule : IModule
    {
        private IUnityContainer container;

        public ClothesEditViewModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<IClothesEditViewModel, ClothesEditViewModel>();
        }
    }
}
