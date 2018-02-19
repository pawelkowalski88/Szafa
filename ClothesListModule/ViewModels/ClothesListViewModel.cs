using ClothesService.Services;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using CustomEvents;
using SzafaEntities;
using System.Windows;

namespace ClothesListModule.ViewModels
{
    public class ClothesListViewModel : PropertyChangedImplementation
    {
        ClothesServices clothesService;
        bool updating;
        ICommand selectElementCommand;
        IEventAggregator eventAggregator;
        private PieceOfClothing currentItem, temporaryItem;

        public ClothesListViewModel(ClothesServices clothesService, IEventAggregator eventAggregator)
        {
            this.clothesService = clothesService;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Subscribe(OnUpdateRequested, true);

            //Might be useful to use event aggregation in the future also for this
            clothesService.ClothesListUpdated += ClothesService_ClothesListUpdated;
            UpdateClothesList();
        }

        private void ClothesService_ClothesListUpdated(object sender, EventArgs e)
        {
            temporaryItem = CurrentItem;
            InvokePropertyChanged("ClothesList");
            Updating = false;
        }

        private void UpdateClothesList()
        {
            clothesService.RefreshClothesList();
            Updating = true;
        }

        private void OnUpdateRequested()
        {
            UpdateClothesList();
        }

        private void OnElementSelected(PieceOfClothing obj)
        {
            if (obj == null && temporaryItem != null)
            {
                if (ClothesList.Exists(x => x.Id == temporaryItem.Id))
                {
                    CurrentItem = ClothesList.Find(x => x.Id == temporaryItem.Id);
                }
                else
                {
                    CurrentItem = obj;
                }
            }
            else
            {
                CurrentItem = obj;
            }
            PieceOfClothingChangedEvent evt = eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Publish(CurrentItem);
        }

        public List<PieceOfClothing> ClothesList
        {
            get
            {
                return clothesService.ClothesList;
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
    }
}
