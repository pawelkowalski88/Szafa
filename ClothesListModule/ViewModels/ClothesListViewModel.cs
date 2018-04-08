using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using CustomEvents;
using SzafaEntities;
using System.Windows;
using ClothesListModule.Filtering;
using ClothesListModule.Events;
using System.Linq;
using SzafaInterfaces;
using FilteringEntities;

namespace ClothesListModule.ViewModels
{
    public class ClothesListViewModel : PropertyChangedImplementation
    {
        IClothesServices clothesService;
        bool updating;
        ICommand selectElementCommand, refreshDataConnection;
        IEventAggregator eventAggregator;
        private PieceOfClothing currentItem, temporaryItem;

        private List<FilteringConditions> filters;
        private Tuple<SortingConditions, bool> sortingParamters;

        public ClothesListViewModel(IClothesServices clothesService, IEventAggregator eventAggregator)
        {
            this.clothesService = clothesService;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Subscribe(OnUpdateRequested, true);
            eventAggregator.GetEvent<FilteringConditionsChangedEvent>().Subscribe(OnFilterChanged, true);
            eventAggregator.GetEvent<SortingConditionsChangedEvent>().Subscribe(OnSortingConditionsChanged, true);
            eventAggregator.GetEvent<DatabaseConnectionErrorOccuredEvent>().Subscribe(OnDatabaseConnectionError, true);

            //Might be useful to use event aggregation in the future also for this
            clothesService.ClothesListUpdated += ClothesService_ClothesListUpdated;
            UpdateClothesList();
        }

        private void OnDatabaseConnectionError(string obj)
        {
            Updating = false;
            DatabaseConnectionError = true;
            InvokePropertyChanged("DatabaseConnectionError");
        }

        private void ClothesService_ClothesListUpdated(object sender, EventArgs e)
        {
            temporaryItem = CurrentItem;
            InvokePropertyChanged("ClothesList");
            Updating = false;
        }

        private void UpdateClothesList()
        {
            clothesService.RefreshClothesListAsync();
            Updating = true;
        }

        private void OnUpdateRequested()
        {
            UpdateClothesList();
        }

        private void OnElementSelected(PieceOfClothing obj)//Not very straightforward...
        {
            //if selected object is equal to null (for example when updating the list) we might try to put the previously selected item.
            if (obj == null && temporaryItem != null)
            {
                //if the previous item is on the current list then select it.
                if (ClothesList.Exists(x => x.Id == temporaryItem.Id))
                {
                    CurrentItem = ClothesList.Find(x => x.Id == temporaryItem.Id);
                }
                //if the previously selected item is not on the list then just display nothing.
                else
                {
                    CurrentItem = obj;
                }
            }
            //if currently selected object is existing the go for it.
            else
            {
                CurrentItem = obj;
            }
            //notify other objects via event aggragation.
            PieceOfClothingChangedEvent evt = eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Publish(CurrentItem);
        }

        private void OnFilterChanged(List<FilteringConditions> obj)
        {
            filters = obj;
            InvokePropertyChanged("ClothesList");
        }

        private void OnSortingConditionsChanged(Tuple<SortingConditions, bool> obj)
        {
            sortingParamters = obj;
            InvokePropertyChanged("ClothesList");
        }

        private void OnRefreshDatabaseConnection()
        {
            Updating = true;
            DatabaseConnectionError = false;
            InvokePropertyChanged("DatabaseConnectionError");
            eventAggregator.GetEvent<ClothesListRefreshRequestedEvent>().Publish();
        }

        //Zmienić na ObservableCollection
        public List<PieceOfClothing> ClothesList
        {
            get
            {
                try
                {
                    return clothesService.ClothesList;
                }
                catch(Exception e)
                {
                    //MessageBox.Show(e.ToString());
                    return new List<PieceOfClothing>();
                }
            }
        }

        public bool Updating
        {
            get
            {
                return updating;
            }
            set
            {
                updating = value;
                InvokePropertyChanged("Updating");
            }
        }

        public bool DatabaseConnectionError { get; set; }

        public ICommand SelectElementCommand
        {
            get
            {
                if(selectElementCommand == null)
                {
                    selectElementCommand = new DelegateCommand<PieceOfClothing>(OnElementSelected);
                }
                return selectElementCommand;
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

        public ICommand RefreshDataConnection
        {
            get
            {
                if(refreshDataConnection == null)
                {
                    refreshDataConnection = new DelegateCommand(OnRefreshDatabaseConnection);
                }
                return refreshDataConnection;
            }
        }
    }
}
