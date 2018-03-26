using CustomEvents;
using Microsoft.Practices.Unity;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using SzafaEntities;
using SzafaInterfaces;
using System;
using System.Collections.ObjectModel;

namespace ClothesEditViewModule.ViewModels
{
    public class ClothesEditViewModel : PropertyChangedImplementation, IClothesEditViewModel
    {
        IEventAggregator eventAggregator;
        IRegionManager regionManager;
        string title;
        ICommand cancelCommand, editOKCommand, browseForFile;
        PieceOfClothing currentItem;
        ObservableCollection<ClothingType> typesList;
        ITypesService typesService;
        IClothesServices clothesService;

        public ClothesEditViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IClothesServices clothesService, ITypesService typesService, PieceOfClothing pieceOfClothing)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.clothesService = clothesService;
            this.typesService = typesService;
            CurrentItem = pieceOfClothing;
            Initialize();
        }

        private void Initialize()
        {
            Title = "Edytuj przedmiot";
            //typesList = typesService.TypesList;
            TypesService_TypesListUpdated(this, null);
            typesService.TypesListUpdated += TypesService_TypesListUpdated;
        }

        //When the types list gets updated, update the property.
        private void TypesService_TypesListUpdated(object sender, System.EventArgs e)
        {
            var tempList = new ObservableCollection<ClothingType>();
            foreach (var t in typesService.TypesList)
            {
                tempList.Add(t);
            }
            TypesList = tempList;
        }

        //Fires when an element is passed to the edit view model. The element is saved as current item.
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
            typesService.TypesListUpdated -= TypesService_TypesListUpdated;
        }

        private bool SaveCurrentItemChanges()
        {
            if (Validate(CurrentItem))
            {
                try
                {
                    clothesService.UpdatePieceOfClothing(CurrentItem);
                }
                catch (Exception e)
                {
                    ShowSaveWarning = true;
                    SaveWarning = e.Message;
                    InvokePropertyChanged("ShowSaveWarning");
                    InvokePropertyChanged("SaveWarning");
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool Validate(PieceOfClothing item)
        {
            bool output = true;
            if (item.Name == "")
            {
                ShowMisingNameWarning = true;
                InvokePropertyChanged("ShowMisingNameWarning");
                output = false;
            }
            else
            {
                ShowMisingNameWarning = false;
                InvokePropertyChanged("ShowMisingNameWarning");
            }

            if (item.TypeId == null)
            {
                ShowMissingTypeWarning = true;
                InvokePropertyChanged("ShowMissingTypeWarning");
                output = false;
            }
            else
            {
                ShowMissingTypeWarning = false;
                InvokePropertyChanged("ShowMissingTypeWarning");
            }
            return output;
        }

        //saving the changes
        private void OnEditOK()
        {
            if (SaveCurrentItemChanges())
            {
                OnCancel();
                eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
            }
        }

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

        public ObservableCollection<ClothingType> TypesList
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

        public bool ShowMisingNameWarning { get; private set; }
        public bool ShowMissingTypeWarning { get; private set; }
        public bool ShowSaveWarning { get; private set; }
        public string SaveWarning { get; private set; }
    }
}
