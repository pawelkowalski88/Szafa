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

namespace ClothesDetailedViewModule.ViewModels
{
    public class ClothesDetailedViewModel : PropertyChangedImplementation
    {
        IUnityContainer container;
        IRegionManager regionManager;
        private PieceOfClothing currentItem;
        private IEventAggregator eventAggregator;

        public ClothesDetailedViewModel(IEventAggregator eventAggregator, IUnityContainer container, IRegionManager regionManager)
        {
            //Use the event aggregation to catch the newly selected item on the list
            this.eventAggregator = eventAggregator;
            PieceOfClothingChangedEvent evt =
                eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Subscribe(OnCurrentItemChanged, true);

            //Register the container & region manager
            this.container = container;
            this.regionManager = regionManager;
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

        /// <summary>
        /// Currently shown item.
        /// </summary>
        public PieceOfClothing CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; InvokePropertyChanged("CurrentItem"); }
        }

        ICommand editCommand;

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
    }
}
