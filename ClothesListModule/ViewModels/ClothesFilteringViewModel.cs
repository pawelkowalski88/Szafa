using ClothesListModule.Events;
using ClothesListModule.Filtering;
using FilteringEntities;
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
using SzafaInterfaces;

namespace ClothesListModule.ViewModels
{ 
    public class ClothesFilteringViewModel : PropertyChangedImplementation
    {
        private ICommand selectFilterCommand, selectTypeFilterCommand, selectSortingCategoryCommand;
        private IEventAggregator eventAggregator;
        private ITypeFilteringConditionsService typeFilteringConditionsService;
        IFilteringService filteringService;

        public ClothesFilteringViewModel(IEventAggregator eventAggregator, ITypeFilteringConditionsService typeFilteringConditionsService, IFilteringService filteringService)
        {
            this.eventAggregator = eventAggregator;
            this.typeFilteringConditionsService = typeFilteringConditionsService;
            this.filteringService = filteringService;
            
            SelectedFilter = FilterTabs[0];

            filteringService.TypesFilterListUpdated += FilteringService_TypesFilterListUpdated;
            SelectedTypeFilter = TypesFilterList[0];
        }

        private void FilteringService_TypesFilterListUpdated(object sender, EventArgs e)
        {
            InvokePropertyChanged("TypesFilterList");
            SelectedTypeFilter = TypesFilterList[0];
            InvokePropertyChanged("SelectedTypeFilter");
        }

        private void OnFilterChanged(FilteringConditions obj)
        {
            InvokePropertyChanged("SelectedFilter");
            filteringService.RefreshFiltering(SelectedFilter);
        }

        private void OnTypeFilterChanged(FilteringConditions obj)
        {
            filteringService.RefreshTypeFiltering(SelectedTypeFilter);
        }

        private void OnSortingChanged()
        {
            filteringService.RefreshFiltering(SelectedFilter);
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

        public ICommand SelectSortingCommand
        {
            get
            {
                if(selectSortingCategoryCommand == null)
                {
                    selectSortingCategoryCommand = new DelegateCommand(OnSortingChanged);
                }
                return selectSortingCategoryCommand;
            }
        }

        //All, Used, Not used
        public List<FilteringConditions> FilterTabs
        {
            get
            {
                return filteringService.FilterTabs;
            }
        }
        public FilteringConditions SelectedFilter { get; set; }

        //According to available types
        public List<FilteringConditions> TypesFilterList
        {
            get
            {
                return filteringService.TypesFilterList;
            }     
        }
        public FilteringConditions SelectedTypeFilter { get; set; }
    }
}
