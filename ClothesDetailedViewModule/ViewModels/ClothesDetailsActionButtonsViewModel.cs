﻿using ClothesEditViewModule.ViewModels;
using ClothesEditViewModule.Views;
using CustomEvents;
using Microsoft.Practices.Unity;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Windows.Input;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesDetailedViewModule.ViewModels
{
    public class ClothesDetailsActionButtonsViewModel : PropertyChangedImplementation
    {
        private IUnityContainer container;
        private IRegionManager regionManager;
        private IClothesServices clothesService;
        private IEventAggregator eventAggregator;
        private ClothesEditViewModelFactory editViewModelFactory;
        private PieceOfClothing currentItem;
        ICommand editCommand, washCommand, useCommand;

        public ClothesDetailsActionButtonsViewModel(IEventAggregator eventAggregator,
            IUnityContainer container,
            IRegionManager regionManager,
            IClothesServices clothesService,
            ClothesEditViewModelFactory viewModelFactory)
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
            this.editViewModelFactory = viewModelFactory;
        }

        private void OnCurrentItemChanged(PieceOfClothing obj)
        {
            CurrentItem = obj;
        }

        /// <summary>
        /// Opens the editing screen for the current item.
        /// </summary>
        private void OnEdit()
        {
            //Put the edit view window on
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            
            ClothesEditView newView = new ClothesEditView(editViewModelFactory.GenerateViewModel(CurrentItem));
            region.Add(newView);
            region.Activate(newView);
        }

        /// <summary>
        /// Washes current item
        /// </summary>
        private void OnWash()
        {;
            CurrentItem.Wash();
            clothesService.UpdatePieceOfClothing(CurrentItem);
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
        }

        /// <summary>
        /// Marks current piece of clothing as currently used and increases the usage count.
        /// </summary>
        private void OnUse()
        {
            CurrentItem.Use();
            clothesService.UpdatePieceOfClothing(CurrentItem);
            eventAggregator.GetEvent<ClothesListUpdateRequestedEvent>().Publish();
        }

        public PieceOfClothing CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; InvokePropertyChanged("CurrentItem"); }
        }

        /// <summary>
        /// Fired when the user clicks Edit button.
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
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
                if (washCommand == null)
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
                if (useCommand == null)
                {
                    useCommand = new DelegateCommand(OnUse);
                }
                return useCommand;
            }
        }
    }
}
