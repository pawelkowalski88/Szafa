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

        public static List<FilteringConditions> GenerateStandardConditions()
        {
            return new List<FilteringConditions>()
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
        }
    }
}
