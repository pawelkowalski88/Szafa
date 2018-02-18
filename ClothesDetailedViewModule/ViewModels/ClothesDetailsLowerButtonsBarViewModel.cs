using CustomEvents;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesDetailedViewModule.ViewModels
{
    public class ClothesDetailsLowerButtonsBarViewModel : PropertyChangedImplementation
    {
        private ICommand preDeleteCommand, confirmDeleteCommand, cancelDeleteCommand;
        private bool displayDeleteConfirmation;
        private Timer timer;
        private PieceOfClothing currentItem;
        private IClothesServices clothesService;
        private IEventAggregator eventAggregator;

        public ClothesDetailsLowerButtonsBarViewModel(IEventAggregator eventAggregator, IClothesServices clothesService)
        {
            this.clothesService = clothesService;
            this.eventAggregator = eventAggregator;

            DisplayDeleteConfirmation = false;
            InvokePropertyChanged("DisplayPreDeleteButton");

            timer = new Timer(2000);
            timer.Elapsed += Timer_Elapsed;

            PieceOfClothingChangedEvent evt =
                eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Subscribe(OnCurrentItemChanged, true);
        }

        public ICommand PreDeleteCommand
        {
            get
            {
                if(preDeleteCommand == null)
                {
                    preDeleteCommand = new DelegateCommand(OnPreDelete);
                }
                return preDeleteCommand;
            }
        }

        public ICommand ConfirmDeleteCommand
        {
            get
            {
                if (confirmDeleteCommand == null)
                {
                    confirmDeleteCommand = new DelegateCommand(OnConfirmDelete);
                }
                return confirmDeleteCommand;
            }
        }

        public ICommand CancelDeleteCommand
        {
            get
            {
                if (cancelDeleteCommand == null)
                {
                    cancelDeleteCommand = new DelegateCommand(OnCancelDelete);
                }
                return cancelDeleteCommand;
            }
        }

        public bool DisplayDeleteConfirmation
        {
            get
            {
                return displayDeleteConfirmation;
            }
            set
            {
                displayDeleteConfirmation = value;
                InvokePropertyChanged("DisplayDeleteConfirmation");
                InvokePropertyChanged("DisplayPreDeleteButton");
            }
        }

        public bool DisplayPreDeleteButton
        {
            get
            {
                return !displayDeleteConfirmation;
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

        private void OnCurrentItemChanged(PieceOfClothing obj)
        {
            CurrentItem = obj;
        }

        private void OnPreDelete()
        {
            DisplayDeleteConfirmation = true;
            Task CountDisplayConfirmationTime = new Task(() => { RunTimer(); });
            CountDisplayConfirmationTime.Start();
        }

        private void RunTimer()
        {
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DisplayDeleteConfirmation = false;
            timer.Stop();
        }

        private void OnConfirmDelete()
        {
            clothesService.DeletePieceOfClothing(CurrentItem);
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
        }

        private void OnCancelDelete()
        {
            DisplayDeleteConfirmation = false;
            timer.Stop();
        }
    }
}
