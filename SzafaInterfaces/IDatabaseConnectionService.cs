using DatabaseEntities;
using System.Collections.Generic;

namespace SzafaInterfaces
{
    public interface IDatabaseConnectionService
    {
        void AddClothes(clothes c);
        void DeletePieceOfClothing(clothes c);
        List<clothes> GetClothes();
        clothes GetPieceOfClothing(long id);
        List<types> GetTypes();
        void UpdateClothes(clothes c);
        void UpdateTypes(types t);
    }
}
