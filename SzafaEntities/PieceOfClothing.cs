using DatabaseConnectionSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzafaEntities
{
    public class PieceOfClothing
    {
        public PieceOfClothing()
        {
            
        }

        public PieceOfClothing(clothes c)
        {
            Id = c.id;
            Description = c.description;
            InUse = c.in_use;
            InUseFrom = c.in_use_from;
            LastTimeOn = c.last_time_on;
            Name = c.name;
            PicturePath = c.picture_path;
            TimesOn = c.times_on;
            TypeId = c.type_id;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public Nullable<long> TypeId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> InUse { get; set; }
        public Nullable<System.DateTime> InUseFrom { get; set; }
        public Nullable<long> TimesOn { get; set; }
        public Nullable<System.DateTime> LastTimeOn { get; set; }

        public virtual ClothingType Type { get; set; }

    }
}
