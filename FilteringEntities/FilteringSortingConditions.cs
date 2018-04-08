using System;
using System.Collections.Generic;
using System.Linq;
using SzafaEntities;

namespace FilteringEntities
{
    public class FilteringSortingConditions : FilteringConditions
    {
        List<SortingConditions> sortingConditionsList;
        SortingConditions sorting;

        List<SortingOrder> sortingOrderList;
        SortingOrder order;

        public List<SortingConditions> SortingConditionsList
        {
            get
            {
                return sortingConditionsList;
            }

            set
            {
                sortingConditionsList = value;
            }
        }

        public SortingConditions Sorting
        {
            get
            {
                return sorting;
            }

            set
            {
                sorting = value;
            }
        }

        public SortingOrder Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public List<SortingOrder> SortingOrderList
        {
            get
            {
                return sortingOrderList;
            }

            set
            {
                sortingOrderList = value;
            }
        }

        public List<PieceOfClothing> Sort(List<PieceOfClothing> list)
        {
            if (Order.Direction == true)
                return ApplyFilters(list).OrderBy(Sorting.Condition).ToList();

            return ApplyFilters(list).OrderByDescending(Sorting.Condition).ToList();
        }

        public override List<PieceOfClothing> Process(List<PieceOfClothing> list)
        {
            return Sort(list);
        }

        public new static List<FilteringConditions> GenerateStandardConditions()
        {
            List<FilteringConditions> output = new List<FilteringConditions>()
            {
                new FilteringSortingConditions()
                {
                    Name = "Wszystkie",
                    Conditions = new Predicate<PieceOfClothing>(x => { return true; }),
                    SortingConditionsList = SortingConditions.GenerateNotUsedConditions(),
                    SortingOrderList = SortingOrder.GenerateStandardList()
                },
                new FilteringSortingConditions()
                {
                    Name = "Używane",
                    Conditions = new Predicate<PieceOfClothing>(x => x.InUse == true),
                    SortingConditionsList = SortingConditions.GenerateStandardConditions(),
                    SortingOrderList = SortingOrder.GenerateStandardList()
                },

                new FilteringSortingConditions()
                {
                    Name = "Nieużywane",
                    Conditions = new Predicate<PieceOfClothing>(x => x.InUse == false),
                    SortingConditionsList = SortingConditions.GenerateNotUsedConditions(),
                    SortingOrderList = SortingOrder.GenerateStandardList()
                }
            };

            foreach (FilteringSortingConditions f in output)
            {
                f.Sorting = f.SortingConditionsList[0];
                f.Order = f.SortingOrderList[0];
            }

            return output;
        }
    }
}
