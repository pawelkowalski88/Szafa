using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;

namespace ClothesListModule.Filtering
{
    public class FilteringConditions
    {
        string name;
        Predicate<PieceOfClothing> conditions;
        List<SortingConditions> sortingConditionsList;
        SortingConditions sorting;
        List<SortingOrder> sortingOrderList;
        SortingOrder order;
        public Predicate<PieceOfClothing> Conditions
        {
            get
            {
                return conditions;
            }

            set
            {
                conditions = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

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

        public SortingOrder SelectedSortingOrder
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

        public static List<FilteringConditions> GenerateStandardConditions()
        {
            List<FilteringConditions> output = new List<FilteringConditions>()
            {
                new FilteringConditions()
                {
                    Name = "Wszystkie",
                    Conditions = new Predicate<PieceOfClothing>(x => { return true; }),
                    SortingConditionsList = SortingConditions.GenerateStandardConditions(),
                    sortingOrderList = SortingOrder.GenerateStandardList()
                },
                new FilteringConditions()
                {
                    Name = "Używane",
                    Conditions = new Predicate<PieceOfClothing>(x => x.InUse == true),
                    SortingConditionsList = SortingConditions.GenerateStandardConditions(),
                    sortingOrderList = SortingOrder.GenerateStandardList()
                },

                new FilteringConditions()
                {
                    Name = "Nieużywane",
                    Conditions = new Predicate<PieceOfClothing>(x => x.InUse == false),
                    SortingConditionsList = SortingConditions.GenerateNotUsedConditions(),
                    sortingOrderList = SortingOrder.GenerateStandardList()
                }
            };

            foreach(var f in output)
            {
                f.Sorting = f.SortingConditionsList[0];
                f.SelectedSortingOrder = f.SortingOrderList[0];
            }

            return output;
        }
    }
}
