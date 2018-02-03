using DatabaseConnectionSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzafaEntities
{
    public class SampleClothesList
    {
        public List<clothes> ClothesList
        {
            get
            {
                return new List<clothes>()
                {
                    new clothes()
                    {
                        name = "Biała koszula",
                        times_on = 5
                    },
                    new clothes()
                    {
                        name = "Spodnie jeansowe",
                        times_on = 2
                    }
                };
            }
        }

        public bool Updating
        {
            get { return false; }
        }
    }
}
