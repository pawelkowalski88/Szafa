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
        private List<SortingConditions> sortingCategoriesList;
        private ICommand selectFilterCommand, selectTypeFilterCommand, selectSortingCategoryCommand, selectSortingOrderCommand;
        private IEventAggregator eventAggregator;
        private TypeFilteringConditionsService typeFilteringConditionsService;
        private SortingOrder sortingOrder;

        public ClothesFilteringViewModel(IEventAggregator eventAggregator, TypeFilteringConditionsService typeFilteringConditionsService)
        {
            this.eventAggregator = eventAggregator;
            this.typeFilteringConditionsService = typeFilteringConditionsService;

            FilterTabs = FilteringConditions.GenerateStandardConditions();
            SelectedFilter = FilterTabs[0];

            TypesFilterList = typeFilteringConditionsService.Conditions;
            typeFilteringConditionsService.FilteringConditionsUpdated += TypeFilteringConditionsService_FilteringConditionsUpdated;
            SelectedTypeFilter = TypesFilterList[0];

            SortingCategoriesList = SortingConditions.GenerateStandardConditions();
            SelectedSortingCategory = SortingCategoriesList[0];

            SortingOrder = SortingOrder.Rosnąco;

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
            PublishFilters();
        }

        private void OnSortingCategoryChanged(SortingConditions obj)
        {
            SelectedSortingCategory = obj;
            PublishFilters();
        }

        private void OnSortingOrderChanged(SortingOrder obj)
        {
            SortingOrder = obj;
            PublishFilters();
        }

        private void PublishFilters()
        {
            eventAggregator.GetEvent<FilteringConditionsChangedEvent>()
                .Publish(new List<FilteringConditions>(){ SelectedFilter, SelectedTypeFilter });

            if (SortingOrder == SortingOrder.Rosnąco)
                eventAggregator.GetEvent<SortingConditionsChangedEvent>()
                    .Publish(Tuple.Create(SelectedSortingCategory, false));
            else
                eventAggregator.GetEvent<SortingConditionsChangedEvent>()
                    .Publish(Tuple.Create(SelectedSortingCategory, true));

        }

        public FilteringConditions SelectedFilter { get; set; }
        public FilteringConditions SelectedTypeFilter { get; set; }
        public SortingConditions SelectedSortingCategory { get; set; }


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

        public List<SortingConditions> SortingCategoriesList
        {
            get
            {
                return sortingCategoriesList;
            }

            set
            {
                sortingCategoriesList = value;
            }
        }

        public SortingOrder SortingOrder
        {
            get
            {
                return sortingOrder;
            }
            set
            {
                sortingOrder = value;
            }
        }
    }
}
