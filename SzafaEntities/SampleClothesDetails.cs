using DatabaseConnectionSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzafaEntities
{
    public class SampleClothesDetails
    {
        public clothes CurrentItem
        {
            get
            {
                return new clothes()
                {
                    name = "Biała koszula",
                    times_on = 5,
                    description = "Biała koszula na specjalne okazje."
                };
            }
        }
    }
}
