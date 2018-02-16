using PresentationUtility;
using Prism.Events;
using CustomEvents;
using System.Windows.Input;
using Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;
using ClothesService.Enumerators;
using ClothesEditViewModule.Views;
using SzafaEntities;
using System;
using SzafaInterfaces;

namespace ClothesDetailedViewModule.ViewModels
{
    public class ClothesDetailedViewModel : PropertyChangedImplementation
    {
        private IUnityContainer container;
        private IRegionManager regionManager;
        private IClothesServices clothesService;
        private PieceOfClothing currentItem;
        private IEventAggregator eventAggregator;

        public ClothesDetailedViewModel(IEventAggregator eventAggregator, IUnityContainer container, IRegionManager regionManager, IClothesServices clothesService)
        {
            //Use the event aggregation to catch the newly selected item on the list
            this.eventAggregator = eventAggregator;
            PieceOfClothingChangedEvent evt =
                eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Subscribe(OnCurrentItemChanged, true);

            //Register the injected fields
            this.container = container;
            this.regionManager = regionManager;
            this.clothesService = clothesService;
        }

        private void OnCurrentItemChanged(PieceOfClothing obj)
        {
            //Point CurrentItem property onto the new target
            CurrentItem = obj;
        }

        /// <summary>
        /// Opens the editing screen for the current item.
        /// </summary>
        private void OnEdit()
        {
            //Put the edit view window on
            container.RegisterInstance<EditActionType>(EditActionType.Edit);
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            ClothesEditView newView = container.Resolve<ClothesEditView>();
            region.Add(newView);
            region.Activate(newView);

            //Publish the event to populate the new view with the CurrentItem data
            ClothesEditButtonClickedEvent evt = 
                eventAggregator.GetEvent<ClothesEditButtonClickedEvent>();
            evt.Publish(CurrentItem);
        }

        private void OnWash()
        {
            CurrentItem.InUse = false;
            CurrentItem.TimesOn = 0;
            clothesService.UpdatePieceOfClothing(CurrentItem);
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
        }

        private void OnUse()
        {
            if (CurrentItem.InUse == false)
            {
                CurrentItem.InUse = true;
            }
            CurrentItem.InUseFrom = DateTime.Now;
            CurrentItem.TimesOn++;
            clothesService.UpdatePieceOfClothing(CurrentItem);
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
        }

        /// <summary>
        /// Currently shown item.
        /// </summary>
        public PieceOfClothing CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; InvokePropertyChanged("CurrentItem"); }
        }

        ICommand editCommand, washCommand, useCommand;

        /// <summary>
        /// Fired when the user clicks Edit button.
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                if(editCommand == null)
                {
                    editCommand = new DelegateCommand(OnEdit);
                }
                return editCommand;
            }
        }

        public ICommand WashCommand
        {
            get
            {
                if(washCommand == null)
                {
                    washCommand = new DelegateCommand(OnWash);
                }
                return washCommand;
            }
        }

        public ICommand UseCommand
        {
            get
            {
                if(useCommand == null)
                {
                    useCommand = new DelegateCommand(OnUse);
                }
                return useCommand;
            }
        }
    }
}
