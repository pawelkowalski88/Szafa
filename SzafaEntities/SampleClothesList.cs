using System.Collections.Generic;

namespace SzafaEntities
{
    public class SampleClothesList
    {
        public List<PieceOfClothing> ClothesList
        {
            get
            {
                return new List<PieceOfClothing>()
                {
                    new PieceOfClothing()
                    {
                        Name = "Biała koszula",
                        TimesOn = 5,
                        Type = new ClothingType() { Name = "shirts"}
                    },
                    new PieceOfClothing()
                    {
                        Name = "Spodnie jeansowe",
                        TimesOn = 2,
                        Type = new ClothingType() { Name = "pants"}
                    }
                };
            }
        }

        public bool Updating
        {
            get { return true; }
        }

        public bool DatabaseConnectionError { get; } = true;
    }
}
