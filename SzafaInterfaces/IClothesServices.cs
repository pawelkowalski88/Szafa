using SQLiteDatabaseConnection;
using System;
using System.Collections.Generic;
using SzafaEntities;

namespace SzafaInterfaces
{
    public interface IClothesServices
    {
        List<PieceOfClothing> ClothesList { get; }

        event EventHandler ClothesListUpdated;

        void AddPieceOfClothing(PieceOfClothing c);
        clothes GetPieceOfClothing(long id);
        void RefreshClothesList();
        void UpdatePieceOfClothing(PieceOfClothing c);
        void DeletePieceOfClothing(PieceOfClothing c);
    }
}