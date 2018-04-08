using System;
using System.Collections.Generic;
using SzafaEntities;

namespace FilteringEntities
{
    public class FilteringConditions
    {
        string name;
        Predicate<PieceOfClothing> conditions;

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

        public List<PieceOfClothing> ApplyFilters(List<PieceOfClothing> list)
        {
            if (list == null) return new List<PieceOfClothing>();

            return list.FindAll(Conditions);
        }

        public virtual List<PieceOfClothing> Process(List<PieceOfClothing> list)
        {
            return ApplyFilters(list);
        }

        public static List<FilteringConditions> GenerateStandardConditions()
        {
            List<FilteringConditions> output = new List<FilteringConditions>()
            {
                new FilteringConditions()
                {
                    Name = "Wszystkie",
                    Conditions = new Predicate<PieceOfClothing>(x => { return true; })
                },
                new FilteringConditions()
                {
                    Name = "Używane",
                    Conditions = new Predicate<PieceOfClothing>(x => x.InUse == true)
                },

                new FilteringConditions()
                {
                    Name = "Nieużywane",
                    Conditions = new Predicate<PieceOfClothing>(x => x.InUse == false)
                }
            };

            return output;
        }
    }
}
