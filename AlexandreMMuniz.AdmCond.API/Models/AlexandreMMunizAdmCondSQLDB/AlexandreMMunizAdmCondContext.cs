using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlexandreMMuniz.AdmCond.API.Models.AlexandreMMunizAdmCondSQLDB
{
    public partial class AlexandreMMunizAdmCondContext : DbContext
    {
        public AlexandreMMunizAdmCondContext()
        {
        }

        public AlexandreMMunizAdmCondContext(DbContextOptions<AlexandreMMunizAdmCondContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administradoras> Administradoras { get; set; }
        public virtual DbSet<Assuntos> Assuntos { get; set; }
        public virtual DbSet<Condominios> Condominios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-T28UCSE\\SQLEXPRESS;Initial Catalog=AlexandreMMunizAdmCond;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assuntos>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Condominios>(entity =>
            {
                entity.Property(e => e.Nome).IsFixedLength();

                entity.HasOne(d => d.IdAdministradoraNavigation)
                    .WithMany(p => p.Condominios)
                    .HasForeignKey(d => d.IdAdministradora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Condominios_Administradoras");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasOne(d => d.IdCondominioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdCondominio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuarios_Condominios");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
