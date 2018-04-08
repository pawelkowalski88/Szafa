using System;
using System.Collections.Generic;
using FilteringEntities;

namespace SzafaInterfaces
{
    public interface ITypeFilteringConditionsService
    {
        List<FilteringConditions> Conditions { get; set; }

        event EventHandler FilteringConditionsUpdated;
    }
}