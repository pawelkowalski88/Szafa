using ClothesListModule.Filtering;
using FilteringEntities;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesListModule.Events
{
    public class SortingConditionsChangedEvent : PubSubEvent<Tuple<SortingConditions, bool>>
    {
    }
}
