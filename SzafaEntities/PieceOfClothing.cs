using DatabaseEntities;
using SQLiteDBConnection;
using System;
using System.Windows.Media.Imaging;

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
            if (c.types != null)
            {
                Type = new ClothingType(c.types);
            }
        }
       
        public PieceOfClothing(clothes c, BitmapImage i) : this(c)
        {
            Image = i;
        }

        public clothes Toclothes()
        {
            return new clothes()
            {
                description = this.Description,
                id = this.Id,
                in_use = this.InUse,
                in_use_from = this.InUseFrom,
                last_time_on = this.LastTimeOn,
                name = this.Name,
                picture_path = (this.PicturePath!=null) ? this.PicturePath : String.Empty,
                times_on = this.TimesOn,
                type_id = (long)this.TypeId
            };
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

        public BitmapImage Image { get; set; }

    }
}
