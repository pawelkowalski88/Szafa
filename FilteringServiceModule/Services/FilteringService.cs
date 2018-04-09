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

        FilteringConditions currentFilter;
        FilteringConditions currentTypeFilter;
        IEventAggregator eventAggregator;
        ITypeFilteringConditionsService typeFilteringConditionsService;
        public List<FilteringConditions> FilterTabs { get; private set; }
        public List<FilteringConditions> TypesFilterList { get; private set; }
        
        public event EventHandler TypesFilterListUpdated;

        public FilteringService(IEventAggregator eventAggregator, ITypeFilteringConditionsService typeFilteringConditionsService)
        {
            this.eventAggregator = eventAggregator;
            this.typeFilteringConditionsService = typeFilteringConditionsService;

            FilterTabs = FilteringSortingConditions.GenerateStandardConditions();
            currentFilter = FilterTabs[0];

            TypesFilterList = typeFilteringConditionsService.Conditions;
            typeFilteringConditionsService.FilteringConditionsUpdated += TypeFilteringConditionsService_FilteringConditionsUpdated;
            currentTypeFilter = TypesFilterList[0];
        }

        private void TypeFilteringConditionsService_FilteringConditionsUpdated(object sender, EventArgs e)
        {
            TypesFilterList = typeFilteringConditionsService.Conditions;
            currentTypeFilter = TypesFilterList[0];
            TypesFilterListUpdated(this, new EventArgs());
        }

        public void RefreshFiltering(FilteringConditions filter)
        {
            currentFilter = filter;
            eventAggregator.GetEvent<RefreshClothesFilteringEvent>().Publish();
        }

        public void RefreshTypeFiltering(FilteringConditions filter)
        {
            currentTypeFilter = filter;
            eventAggregator.GetEvent<RefreshClothesFilteringEvent>().Publish();
        }

        public List<PieceOfClothing> ProcessAll(List<PieceOfClothing> list)
        {
            var tempList = currentTypeFilter.Process(list);
            return currentFilter.Process(tempList);
        }
    }
}
