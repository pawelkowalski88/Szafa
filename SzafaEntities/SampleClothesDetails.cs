namespace SzafaEntities
{
    public class SampleClothesDetails
    {
        public PieceOfClothing CurrentItem
        {
            get
            {
                return new PieceOfClothing()
                {
                    Name = "Biała koszula",
                    TimesOn= 5,
                    Description = "Biała koszula na specjalne okazje.",
                    Type = new ClothingType() { Name = "shirts"}
                };
            }
        }
    }
}
