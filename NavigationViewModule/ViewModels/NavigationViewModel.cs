using ClothesEditViewModule.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;
using ClothesService.Enumerators;

namespace NavigationViewModule.ViewModels
{
    public class NavigationViewModel
    {

        public NavigationViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;  
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
            container.RegisterInstance<EditActionType>(EditActionType.Create);
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            ClothesEditView newView = container.Resolve<ClothesEditView>();
            region.Add(newView);
            region.Activate(newView);

        }
        IRegionManager regionManager;
        IUnityContainer container; 
    }
}
