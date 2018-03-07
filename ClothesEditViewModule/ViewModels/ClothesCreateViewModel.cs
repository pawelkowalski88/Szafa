using CustomEvents;
using Microsoft.Win32;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesEditViewModule.ViewModels
{
    class ClothesCreateViewModel : PropertyChangedImplementation, IClothesEditViewModel
    {
        public ClothesCreateViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IClothesServices clothesService, ITypesService typesService)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.clothesService = clothesService;
            this.typesService = typesService;
            typesList = typesService.TypesList;
            typesService.TypesListUpdated += TypesService_TypesListUpdated;//unsubscribe on exit!!!!
            Initialize();
        }

        private void Initialize()
        {
            Title = "Nowy przedmiot";
            CurrentItem = new PieceOfClothing()
            {
                Name = "",
                InUse = false,
                PicturePath = "",
                TimesOn = 0
            };
        }

        //private void InitializeTypesService()
        //{
        //    typesService = container.Resolve<TypesService.Services.TypesService>();
        //    typesList = typesService.TypesList;
        //    typesService.TypesListUpdated += TypesService_TypesListUpdated;
        //}

        //when the types list gets updated, update the property.
        private void TypesService_TypesListUpdated(object sender, System.EventArgs e)
        {
            TypesList = typesService.TypesList;
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

        private void SaveCurrentItemChanges()
        {
            if (CurrentItem.Name == "")
            {
                return;
            }
            clothesService.UpdatePieceOfClothing(CurrentItem);
        }

        private bool SaveCurrentItemAsNew()
        {
            if (Validate(CurrentItem))
            {
                try
                {
                    clothesService.AddPieceOfClothing(CurrentItem);
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
            if (SaveCurrentItemAsNew())
            {
                OnCancel();
                eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
            }
        }

        IEventAggregator eventAggregator;
        IRegionManager regionManager;
        string title;
        ICommand cancelCommand, editOKCommand, browseForFile;
        PieceOfClothing currentItem;
        List<ClothingType> typesList;
        IClothesServices clothesService;
        ITypesService typesService;

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
                if (browseForFile == null)
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
