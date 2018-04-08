using ClothesListModule.Filtering;
using FilteringEntities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesListModule.DesignTimeDataContexts
{
    public class FilteringDataContext
    {
        public List<FilteringConditions> FilterTabs
        {
            get
            {
                return FilteringSortingConditions.GenerateStandardConditions();
            }
        }
    }
}
