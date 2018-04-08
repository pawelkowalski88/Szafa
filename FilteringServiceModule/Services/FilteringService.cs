using CustomEvents;
using FilteringEntities;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;
using SzafaInterfaces;

namespace FilteringServiceModule.Services
{
    public class FilteringService : IFilteringService
    {

        FilteringConditions currentConditions;
        IEventAggregator eventAggregator;
        public List<FilteringConditions> FilterTabs { get; private set; }

        public FilteringService(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            FilterTabs = FilteringSortingConditions.GenerateStandardConditions();
            currentConditions = FilterTabs[0];
        }

        public void RefreshFiltering(FilteringConditions filter)
        {
            currentConditions = filter;
            eventAggregator.GetEvent<RefreshClothesFilteringEvent>().Publish();
        }

        public List<PieceOfClothing> ProcessAll(List<PieceOfClothing> list)
        {
            return currentConditions.Process(list);
        }
    }
}
