using System;
using System.Collections.Generic;
using SzafaEntities;
using SzafaEntities.Filtering;

namespace SzafaInterfaces
{
    public interface IFilteringService
    {
        //All, Used, Not used
        List<FilteringConditions> FilterTabs { get; set; }
        FilteringConditions SelectedFilter { get; set; }

        //According to available types
        List<FilteringConditions> TypesFilterList { get; set; }
        FilteringConditions SelectedTypeFilter { get; set; }

        //According to predefined list of categories
        List<SortingConditions> SortingCategoriesList { get; set; }
        SortingConditions SelectedSortingCategory { get; set; }

        //asc, desc
        List<SortingOrder> SortingOrderList { get; set; }
        SortingOrder SelectedSortingOrder { get; set; }

        List<PieceOfClothing> PerformFiltering(List<PieceOfClothing> inputList);
    }
}
