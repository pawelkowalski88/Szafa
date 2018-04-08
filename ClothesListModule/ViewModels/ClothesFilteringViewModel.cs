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
        private ICommand selectFilterCommand, selectTypeFilterCommand, selectSortingCategoryCommand, selectSortingOrderCommand;
        private IEventAggregator eventAggregator;
        private ITypeFilteringConditionsService typeFilteringConditionsService;
        IFilteringService filteringService;

        public ClothesFilteringViewModel(IEventAggregator eventAggregator, ITypeFilteringConditionsService typeFilteringConditionsService, IFilteringService filteringService)
        {
            this.eventAggregator = eventAggregator;
            this.typeFilteringConditionsService = typeFilteringConditionsService;
            this.filteringService = filteringService;

            //FilterTabs = FilteringSortingConditions.GenerateStandardConditions();
            SelectedFilter = FilterTabs[0];

            TypesFilterList = typeFilteringConditionsService.Conditions;
            typeFilteringConditionsService.FilteringConditionsUpdated += TypeFilteringConditionsService_FilteringConditionsUpdated;
            SelectedTypeFilter = TypesFilterList[0];

            SortingCategoriesList = ((FilteringSortingConditions)SelectedFilter).SortingConditionsList;
            SelectedSortingCategory = SortingCategoriesList[0];

            SortingOrderList = SortingOrder.GenerateStandardList();
            SelectedSortingOrder = SortingOrderList[0];
        }

        private void TypeFilteringConditionsService_FilteringConditionsUpdated(object sender, EventArgs e)
        {
            TypesFilterList = typeFilteringConditionsService.Conditions;
            InvokePropertyChanged("TypesFilterList");
            SelectedTypeFilter = TypesFilterList[0];
            InvokePropertyChanged("SelectedTypeFilter");
        }

        private void OnFilterChanged(FilteringSortingConditions obj)
        {
            SelectedFilter = obj;
            InvokePropertyChanged("SelectedFilter");
            //PublishFilters();
            filteringService.RefreshFiltering(SelectedFilter);
        }

        private void OnTypeFilterChanged(FilteringSortingConditions obj)
        {
            SelectedTypeFilter = obj;
        }

        private void OnSortingCategoryChanged(SortingConditions obj)
        {
            filteringService.RefreshFiltering(SelectedFilter);
        }

        private void OnSortingOrderChanged(SortingOrder obj)
        {
            filteringService.RefreshFiltering(SelectedFilter);
        }

        public ICommand SelectFilterCommand
        {
            get
            {
                if (selectFilterCommand == null)
                {
                    selectFilterCommand = new DelegateCommand<FilteringSortingConditions>(OnFilterChanged);
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
                    selectTypeFilterCommand = new DelegateCommand<FilteringSortingConditions>(OnTypeFilterChanged);
                }
                return selectTypeFilterCommand;
            }
        }

        public ICommand SelectSortingCategoryCommand
        {
            get
            {
                if(selectSortingCategoryCommand == null)
                {
                    selectSortingCategoryCommand = new DelegateCommand<SortingConditions>(OnSortingCategoryChanged);
                }
                return selectSortingCategoryCommand;
            }
        }

        public ICommand SelectSortingOrderCommand
        {
            get
            {
                if (selectSortingOrderCommand == null)
                {
                    selectSortingOrderCommand = new DelegateCommand<SortingOrder>(OnSortingOrderChanged);
                }
                return selectSortingOrderCommand;
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
        public List<FilteringConditions> TypesFilterList { get; set; }
        public FilteringConditions SelectedTypeFilter { get; set; }

        //According to predefined list of categories
        public List<SortingConditions> SortingCategoriesList { get; set; }
        public SortingConditions SelectedSortingCategory { get; set; }

        //asc, desc
        public List<SortingOrder> SortingOrderList { get; set; }
        public SortingOrder SelectedSortingOrder { get; set; }
    }
}
