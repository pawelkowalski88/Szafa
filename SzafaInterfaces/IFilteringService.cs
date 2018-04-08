using FilteringEntities;
using System;
using System.Collections.Generic;
using SzafaEntities;

namespace SzafaInterfaces
{
    public interface IFilteringService
    {
        List<FilteringConditions> FilterTabs { get; }

        List<PieceOfClothing> ProcessAll(List<PieceOfClothing> list);
        void RefreshFiltering(FilteringConditions filter);
    }
}