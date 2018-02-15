using DatabaseConnectionSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzafaEntities
{
    public class ClothingType
    {
        public ClothingType()
        {

        }

        public ClothingType(types t)
        {
            Id = t.id;
            Name = t.name;
        }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
