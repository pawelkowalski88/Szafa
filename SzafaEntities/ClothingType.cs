using DatabaseConnectionSQLite;

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
