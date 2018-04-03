using DatabaseEntities;

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
            ClothesCount = t.clothes.Count;
        }

        public types Totypes()
        {
            return new types()
            {
                name = this.Name,
                id = this.Id
            };
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public long ClothesCount { get; set; }
    }
}
