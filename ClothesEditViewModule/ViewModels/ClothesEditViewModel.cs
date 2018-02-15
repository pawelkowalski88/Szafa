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
using System;
using System.Windows;
using ClothesService.Services;
using Microsoft.Win32;

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
                CurrentItem = new clothes();
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

        //when the types list gets updated, update the property.
        private void TypesService_TypesListUpdated(object sender, System.EventArgs e)
        {
            TypesList = typesService.TypesList;
        }

        //fires when an element is passed to the edit view model. The element is saved as current item.
        private void OnEditElementAdded(clothes obj)
        {
            CurrentItem = obj;
        }

        //gettting back to the last view
        private void OnCancel()
        {
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            object activeView = region.ActiveViews.First();
            region.Remove(activeView);
            region.Activate(region.Views.First());
        }

        private void SaveCurrentItemChanges()
        {
            ClothesServices clothesService = container.Resolve<ClothesServices>();
            clothesService.UpdatePieceOfClothing(CurrentItem);
        }

        private void SaveCurrentItemAsNew()
        {
            ClothesServices clothesService = container.Resolve<ClothesServices>();
            clothesService.AddPieceOfClothing(CurrentItem);
        }

        //saving the changes
        private void OnEditOK()
        {
            if (actionType == EditActionType.Create)
            {
                MessageBox.Show("Create new");
                SaveCurrentItemAsNew();
            }
            else
            {
                SaveCurrentItemChanges();
            }
            OnCancel();
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
        }

        IEventAggregator eventAggregator;
        IRegionManager regionManager;
        IUnityContainer container;
        EditActionType actionType;
        string title;
        ICommand cancelCommand, editOKCommand, browseForFile;
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

        public ICommand EditOKCommand
        {
            get
            {
                if (editOKCommand == null)
                {
                    editOKCommand = new DelegateCommand(OnEditOK);
                }
                return editOKCommand;
            }
        }

        public ICommand BrowseForFile
        {
            get
            {
                if(browseForFile == null)
                {
                    browseForFile = new DelegateCommand(OnBrowseForFile);
                }
                return browseForFile;
            }
        }

        private void OnBrowseForFile()
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();
            OpenDialog.ShowDialog();
            if (OpenDialog.FileName != null)
            {
                CurrentItem.picture_path = OpenDialog.FileName;
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
