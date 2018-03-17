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
        private List<FilteringConditions> typesFilterList;
        private ICommand selectFilterCommand, selectTypeFilterCommand, selectSortingCategoryCommand, selectSortingOrderCommand;
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

            SortingCategoriesList = SelectedFilter.SortingConditionsList;//SortingConditions.GenerateStandardConditions();
            SelectedSortingCategory = SortingCategoriesList[0];

            SortingOrderList = SortingOrder.GenerateStandardList();
            SelectedSortingOrder = SortingOrderList[0];

            PublishFilters();
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
            //SortingCategoriesList = obj.SortingConditionsList;
            //SelectedSortingCategory = obj.Sorting;
            PublishFilters();
        }

        private void OnSortingCategoryChanged(SortingConditions obj)
        {
            SelectedSortingCategory = obj;
            PublishFilters();
        }

        private void OnSortingOrderChanged(SortingOrder obj)
        {
            SelectedSortingOrder = obj;
            PublishFilters();
        }

        private void PublishFilters()
        {
            eventAggregator.GetEvent<FilteringConditionsChangedEvent>()
                .Publish(new List<FilteringConditions>(){ SelectedFilter, SelectedTypeFilter });

            eventAggregator.GetEvent<SortingConditionsChangedEvent>()
                .Publish(Tuple.Create(SelectedSortingCategory, SelectedSortingOrder.Direction));
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

        public FilteringConditions SelectedFilter { get; set; }
        public FilteringConditions SelectedTypeFilter { get; set; }
        public SortingConditions SelectedSortingCategory { get; set; }
        public List<SortingConditions> SortingCategoriesList { get; set; }
        public List<SortingOrder> SortingOrderList { get; set; }
        public SortingOrder SelectedSortingOrder { get; set; }
        public List<FilteringConditions> FilterTabs { get; set; }
    }
}
