using ClothesListModule.Events;
using ClothesListModule.Filtering;
using PresentationUtility;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesListModule.ViewModels
{
    public class ClothesFilteringViewModel : PropertyChangedImplementation
    {
        private List<FilteringConditions> filterTabs;
        private List<FilteringConditions> typesFilterList;
        private ICommand selectFilterCommand, selectTypeFilterCommand;
        private IEventAggregator eventAggregator;
        private TypeFilteringConditionsService typeFilteringConditionsService;

        public ClothesFilteringViewModel(IEventAggregator eventAggregator, TypeFilteringConditionsService typeFilteringConditionsService)
        {
            this.eventAggregator = eventAggregator;
            this.typeFilteringConditionsService = typeFilteringConditionsService;
            FilterTabs = FilteringConditions.GenerateStandardConditions();
            SelectedFilter = FilterTabs[0];

            TypesFilterList = typeFilteringConditionsService.Conditions;
            typeFilteringConditionsService.FilteringConditionsUpdated += TypeFilteringConditionsService_FilteringConditionsUpdated;
            SelectedTypeFilter = TypesFilterList[0];

            eventAggregator.GetEvent<FilteringConditionsChangedEvent>().Publish(new List<FilteringConditions>() { SelectedFilter, SelectedTypeFilter });
        }

        private void TypeFilteringConditionsService_FilteringConditionsUpdated(object sender, EventArgs e)
        {
            TypesFilterList = typeFilteringConditionsService.Conditions;
        }

        private void OnFilterChanged(FilteringConditions obj)
        {
            SelectedFilter = obj;
            PublishFilters();
        }

        private void OnTypeFilterChanged(FilteringConditions obj)
        {
            SelectedTypeFilter = obj;
            PublishFilters();
        }

        private void PublishFilters()
        {
            FilteringConditionsChangedEvent evt = eventAggregator.GetEvent<FilteringConditionsChangedEvent>();
            evt.Publish(new List<FilteringConditions>() { SelectedFilter, SelectedTypeFilter });
        }

        public FilteringConditions SelectedFilter { get; set; }
        public FilteringConditions SelectedTypeFilter { get; set; }

        public List<FilteringConditions> FilterTabs
        {
            get
            {
                return filterTabs;
            }

            set
            {
                filterTabs = value;
            }
        }

        public ICommand SelectFilterCommand
        {
            get
            {
                if (selectFilterCommand == null)
                {
                    selectFilterCommand = new DelegateCommand<FilteringConditions>(OnFilterChanged);
                }
                return selectFilterCommand;
            }
        }

        public ICommand SelectTypeFilterCommand
        {
            get
            {
                if (selectTypeFilterCommand == null)
                {
                    selectTypeFilterCommand = new DelegateCommand<FilteringConditions>(OnTypeFilterChanged);
                }
                return selectTypeFilterCommand;
            }
        }

        public List<FilteringConditions> TypesFilterList
        {
            get
            {
                return typesFilterList;
            }

            set
            {
                typesFilterList = value;
                InvokePropertyChanged("TypesFilterList");
            }
        }
    }
}
