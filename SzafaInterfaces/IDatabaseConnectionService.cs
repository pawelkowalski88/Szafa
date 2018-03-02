using SQLiteDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzafaInterfaces
{
    public interface IDatabaseConnectionService
    {
        void AddClothes(clothes c);
        void DeletePieceOfClothing(clothes c);
        List<clothes> GetClothes();
        clothes GetPieceOfClothing(long id);
        List<types> GetTypes();
        bool UpdateClothes(clothes c);
    }
}
