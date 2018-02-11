using ClothesService.Enumerators;
using CustomEvents;
using DatabaseConnectionSQLite;
using Microsoft.Practices.Unity;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TypesService.Services;

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
                InitializeTypesService();
            }
            else
            {
                Title = "Edytuj przedmiot";
                ClothesEditButtonClickedEvent evt = eventAggregator.GetEvent<ClothesEditButtonClickedEvent>();
                evt.Subscribe(OnEditElementAdded, true);
                InitializeTypesService();
            }
        }

        private void InitializeTypesService()
        {
            typesService = container.Resolve<TypesService.Services.TypesService>();
            typesList = typesService.TypesList;
            typesService.TypesListUpdated += TypesService_TypesListUpdated;
        }

        private void TypesService_TypesListUpdated(object sender, System.EventArgs e)
        {
            TypesList = typesService.TypesList;
        }

        private void OnEditElementAdded(clothes obj)
        {
            CurrentItem = obj;
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
        clothes currentItem;
        List<types> typesList;
        TypesService.Services.TypesService typesService;

        public clothes CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                currentItem = value;
                InvokePropertyChanged("CurrentItem");
            }
        }

        public List<types> TypesList
        {
            get
            {
                return typesList;
            }
            set
            {
                typesList = value;
                InvokePropertyChanged("TypesList");
            }
        }

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
