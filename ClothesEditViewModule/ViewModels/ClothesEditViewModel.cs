﻿using ClothesService.Enumerators;
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
using ClothesService.Services;
using Microsoft.Win32;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesEditViewModule.ViewModels
{
    public class ClothesEditViewModel : PropertyChangedImplementation
    {
        public ClothesEditViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IUnityContainer container, IClothesServices clothesService)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.container = container;
            this.clothesService = clothesService;
            Initialize();
        }

        private void Initialize()
        {
            actionType = container.Resolve<EditActionType>();
            if (actionType == EditActionType.Create)
            {
                Title = "Nowy przedmiot";
                InitializeTypesService();
                CurrentItem = new PieceOfClothing()
                {
                    Name = "",
                    InUse = false,
                    PicturePath = "",
                    TimesOn = 0
                };
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
        private void OnEditElementAdded(PieceOfClothing obj)
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
            clothesService.UpdatePieceOfClothing(CurrentItem);
        }

        private void SaveCurrentItemAsNew()
        {
            clothesService.AddPieceOfClothing(CurrentItem);
        }

        //saving the changes
        private void OnEditOK()
        {
            if (actionType == EditActionType.Create)
            {
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
        PieceOfClothing currentItem;
        List<ClothingType> typesList;
        TypesService.Services.TypesService typesService;
        IClothesServices clothesService;

        public PieceOfClothing CurrentItem
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

        public List<ClothingType> TypesList
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
            if (OpenDialog.FileName != null || OpenDialog.FileName != "")
            {
                CurrentItem.PicturePath = OpenDialog.FileName;
                InvokePropertyChanged("CurrentItem");
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
