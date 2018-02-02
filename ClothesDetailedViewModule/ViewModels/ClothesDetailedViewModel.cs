using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnectionSQLite;
using PresentationUtility;
using Prism.Events;
using CustomEvents;

namespace ClothesDetailedViewModule.ViewModels
{
    public class ClothesDetailedViewModel : PropertyChangedImplementation
    {
        public ClothesDetailedViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            PieceOfClothingChangedEvent evt =
                eventAggregator.GetEvent<PieceOfClothingChangedEvent>();
            evt.Subscribe(OnCurrentItemChanged, true);
        }

        private void OnCurrentItemChanged(clothes obj)
        {
            CurrentItem = obj;
        }

        private clothes currentItem;
        private IEventAggregator eventAggregator;

        public clothes CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; InvokePropertyChanged("CurrentItem"); }
        }

    }
}
