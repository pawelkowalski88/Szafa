using FilteringEntities;
using System;
using System.Collections.Generic;
using SzafaEntities;

namespace SzafaInterfaces
{
    public interface IFilteringService
    {
        List<FilteringConditions> FilterTabs { get; }
        List<FilteringConditions> TypesFilterList { get; }

        List<PieceOfClothing> ProcessAll(List<PieceOfClothing> list);
        void RefreshFiltering(FilteringConditions filter);
        void RefreshTypeFiltering(FilteringConditions filter);
        event EventHandler TypesFilterListUpdated;
    }
}