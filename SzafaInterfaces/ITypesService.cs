using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;

namespace SzafaInterfaces
{
    public interface ITypesService
    {
        List<ClothingType> TypesList { get; }

        event EventHandler TypesListUpdated;

        void UpdateTypesList();
        void UpdateType(ClothingType t);
    }
}
