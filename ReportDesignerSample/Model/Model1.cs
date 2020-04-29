namespace ReportDesignerSample
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model15")
        {
        }

       // public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employe>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employe>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Employe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Report>()
                .Property(e => e.Report1)
                .IsUnicode(false);

            modelBuilder.Entity<Report>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Report>()
                .Property(e => e.ReportName)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
