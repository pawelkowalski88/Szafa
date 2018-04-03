using DatabaseEntities;
using System;

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
            try
            {
                ClothesCount = t.clothes.Count;
            }
            catch (Exception)
            {
                ClothesCount = 0;
            }
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
