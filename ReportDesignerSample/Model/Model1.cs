namespace ReportDesignerSample
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employe>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employe>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            //modelBuilder.Entity<Employe>()
            //    .HasOptional(e => e.Employes1)
            //    .WithRequired(e => e.Employe1);

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Employe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
