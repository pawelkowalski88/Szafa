using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesListModule.Filtering
{
    public class TypeFilteringConditions
    {
        string name;
        Predicate<ClothingType> conditions;
        public EventHandler FilteringConditionsUpdated;

        public TypeFilteringConditions()
        {
        }

        public Predicate<ClothingType> Conditions
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


        //public List<TypeFilteringConditions> GenerateStandardConditions(ITypesService typesService)
        //{


        //    return output;
        //}
    }
}
