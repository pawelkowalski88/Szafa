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
        public ClothesDetailedViewModel(IEventAggregator eventAggregator, IUnityContainer container, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            PieceOfClothingChangedEvent evt =
                eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Subscribe(OnCurrentItemChanged, true);
            this.container = container;
            this.regionManager = regionManager;
        }

        private void OnCurrentItemChanged(PieceOfClothing obj)
        {
            CurrentItem = obj;
        }

        private void OnEdit()
        {
            container.RegisterInstance<EditActionType>(EditActionType.Edit);
            IRegion region = regionManager.Regions["MainDetailsRegion"];
            ClothesEditView newView = container.Resolve<ClothesEditView>();
            region.Add(newView);
            region.Activate(newView);
            ClothesEditButtonClickedEvent evt = eventAggregator.GetEvent<ClothesEditButtonClickedEvent>();
            evt.Publish(CurrentItem);
        }

        IUnityContainer container;
        IRegionManager regionManager;
        private PieceOfClothing currentItem;
        private IEventAggregator eventAggregator;

        public PieceOfClothing CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; InvokePropertyChanged("CurrentItem"); }
        }

        ICommand editCommand;
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
