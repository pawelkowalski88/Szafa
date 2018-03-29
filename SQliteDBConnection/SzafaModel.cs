namespace SQLiteDBConnection
{
    using System;
    using System.Data.Entity;
    using DatabaseEntities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SzafaModel : DbContext
    {
        public SzafaModel()
            : base("name=SzafaModel")
        {
        }

        public virtual DbSet<DatabaseEntities.clothes> clothes { get; set; }
        public virtual DbSet<DatabaseEntities.types> types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<clothes>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<clothes>()
                .Property(e => e.picture_path)
                .IsUnicode(false);

            modelBuilder.Entity<clothes>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<types>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<types>()
                .HasMany(e => e.clothes)
                .WithRequired(e => e.types)
                .HasForeignKey(e => e.type_id)
                .WillCascadeOnDelete(false);
        }
    }
}
