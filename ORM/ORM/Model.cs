namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model1111")
        {
        }

        public virtual DbSet<dacha> dacha { get; set; }
        public virtual DbSet<dacha_owners> dacha_owners { get; set; }
        public virtual DbSet<district> district { get; set; }
        public virtual DbSet<owners> owners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dacha>()
                .HasMany(e => e.dacha_owners)
                .WithRequired(e => e.dacha)
                .HasForeignKey(e => e.id_dacha)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<district>()
                .HasMany(e => e.dacha)
                .WithRequired(e => e.district)
                .HasForeignKey(e => e.district_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<owners>()
                .HasMany(e => e.dacha_owners)
                .WithRequired(e => e.owners)
                .HasForeignKey(e => e.id_owners)
                .WillCascadeOnDelete(false);
        }
    }
}
