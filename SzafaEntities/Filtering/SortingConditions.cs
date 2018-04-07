using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;

namespace SzafaEntities.Filtering
{
    public class SortingConditions
    {
        string name;
        Func<PieceOfClothing, object> condition;

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

        public Func<PieceOfClothing, object> Condition
        {
            get
            {
                return condition;
            }

            set
            {
                condition = value;
            }
        }

        public static List<SortingConditions> GenerateStandardConditions()
        {
            return new List<SortingConditions>()
            {
                new SortingConditions()
                {
                    Name = "Nazwa",
                    Condition = x => x.Name
                },

                new SortingConditions()
                {
                    Name = "Ile razy używane",
                    Condition = x => x.TimesOn
                },

                new SortingConditions()
                {
                    Name = "Kiedy ostatnio założone",
                    Condition = x => x.LastTimeOn
                },

                new SortingConditions()
                {
                    Name = "Odkąd używane",
                    Condition = x => x.InUseFrom
                }
            };
        }

        public static List<SortingConditions> GenerateNotUsedConditions()
        {
            return new List<SortingConditions>()
            {
                new SortingConditions()
                {
                    Name = "Nazwa",
                    Condition = x => x.Name
                },

                new SortingConditions()
                {
                    Name = "Kiedy ostatnio założone",
                    Condition = x => x.LastTimeOn
                }
            };
        }
    }
}
