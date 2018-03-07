using ClothesEditViewModule.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;
using ClothesService.Enumerators;
using ClothesEditViewModule.ViewModels;

namespace NavigationViewModule.ViewModels
{
    public class NavigationViewModel
    {
        IRegionManager regionManager;
        IUnityContainer container;
        private ClothesEditViewModelFactory editViewModelFactory;
        public NavigationViewModel(IRegionManager regionManager, IUnityContainer container, ClothesEditViewModelFactory viewModelFactory)
        {
            this.regionManager = regionManager;
            this.container = container;
            this.editViewModelFactory = viewModelFactory;
        }

        ICommand newPieceCommand;
        public ICommand NewPieceCommand
        {
            get
            {
                if(newPieceCommand == null)
                {
                    newPieceCommand = new DelegateCommand(OnNewPieceClick);
                }
                return newPieceCommand;
            }
        }
        private void OnNewPieceClick()
        {
            //container.RegisterInstance<EditActionType>(EditActionType.Create);
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            //ClothesEditView newView = container.Resolve<ClothesEditView>();
            ClothesEditView newView = new ClothesEditView(editViewModelFactory.GenerateViewModel());
            region.Add(newView);
            region.Activate(newView);
        }
    }
}
