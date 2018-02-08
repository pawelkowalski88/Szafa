using ClothesService.Enumerators;
using Microsoft.Practices.Unity;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClothesEditViewModule.ViewModels
{
    public class ClothesEditViewModel :PropertyChangedImplementation
    {
        public ClothesEditViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IUnityContainer container)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.container = container;
            Initialize();
        }

        private void Initialize()
        {
            actionType = container.Resolve<EditActionType>();
            if (actionType == EditActionType.Create)
            {
                Title = "Nowy przedmiot";
            }
        }

        private void OnCancel()
        {
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            object activeView = region.ActiveViews.First();
            region.Remove(activeView);
            region.Activate(region.Views.First());
        }

        IEventAggregator eventAggregator;
        IRegionManager regionManager;
        IUnityContainer container;
        EditActionType actionType;
        string title;
        ICommand cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new DelegateCommand(OnCancel);
                }
                return cancelCommand;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                InvokePropertyChanged("Title");
            }
        }


    }
}
