using ClothesService.Services;
using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CustomEvents;

namespace ClothesListModule.ViewModels
{
    public class ClothesListViewModel : PropertyChangedImplementation
    {
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

        private void OnElementSelected(clothes obj)
        {
            PieceOfClothingChangedEvent evt = eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Publish(obj);
        }

        public List<clothes> ClothesList
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
                    selectElementCommand = new DelegateCommand<clothes>(OnElementSelected);
                }
                return selectElementCommand;
            }
        }

        ClothesServices clothesService;
        bool updating;
        ICommand selectElementCommand;
        IEventAggregator eventAggregator;

    }
}
