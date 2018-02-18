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
        private PieceOfClothing currentItem;
        private IEventAggregator eventAggregator;

        public ClothesDetailedViewModel(IEventAggregator eventAggregator)
        {
            //Use the event aggregation to catch the newly selected item on the list
            this.eventAggregator = eventAggregator;
            PieceOfClothingChangedEvent evt =
                eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Subscribe(OnCurrentItemChanged, true);
        }

        private void OnCurrentItemChanged(PieceOfClothing obj)
        {
            //Point CurrentItem property onto the new target
            CurrentItem = obj;
        }

        /// <summary>
        /// Currently shown item.
        /// </summary>
        public PieceOfClothing CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; InvokePropertyChanged("CurrentItem"); }
        }
    }
}
