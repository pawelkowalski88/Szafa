using ClothesEditViewModule.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;
using System.Linq;
using ClothesEditViewModule.ViewModels;
using System;

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
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            ClothesEditView newView = new ClothesEditView(editViewModelFactory.GenerateViewModel());
            try
            {
                region.Add(newView, "NewPieceOfClothingView");
            }
            catch (InvalidOperationException)
            {
                ClothesEditView view = (ClothesEditView)region.Views.First<object>(x => x.GetType() == typeof(ClothesEditView));
                {
                    newView = view;
                }
            }
            region.Activate(newView);
        }
    }
}
