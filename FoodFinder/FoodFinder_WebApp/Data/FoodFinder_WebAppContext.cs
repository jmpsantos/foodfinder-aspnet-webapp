using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FoodFinder_WebApp.Models;

#nullable disable

namespace FoodFinder_WebApp.Data
{
    public partial class FoodFinder_WebAppContext : DbContext
    {
        public FoodFinder_WebAppContext()
        {
        }

        public FoodFinder_WebAppContext(DbContextOptions<FoodFinder_WebAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdicionarPratoDoDium> AdicionarPratoDoDia { get; set; }
        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Bloqueado> Bloqueados { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ComentarioRestaurante> ComentarioRestaurantes { get; set; }
        public virtual DbSet<FavoritarPratoDoDium> FavoritarPratoDoDia { get; set; }
        public virtual DbSet<FavoritarRestaurante> FavoritarRestaurantes { get; set; }
        public virtual DbSet<Localizacao> Localizacaos { get; set; }
        public virtual DbSet<PratoDoDium> PratoDoDia { get; set; }
        public virtual DbSet<Restaurante> Restaurantes { get; set; }
        public virtual DbSet<Utilizador> Utilizadors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=FoodFinder_WebAppContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdicionarPratoDoDium>(entity =>
            {
                entity.HasKey(e => new { e.PratoId, e.RestauranteId, e.DataPrato })
                    .HasName("PK__Adiciona__522977CAFDA4812E");

                entity.Property(e => e.RestauranteId).IsUnicode(false);

                entity.HasOne(d => d.Prato)
                    .WithMany(p => p.AdicionarPratoDoDia)
                    .HasForeignKey(d => d.PratoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Adicionar__prato__3D5E1FD2");

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.AdicionarPratoDoDia)
                    .HasForeignKey(d => d.RestauranteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Adicionar__resta__3C69FB99");
            });

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.Property(e => e.AdministradorId).IsUnicode(false);

                entity.HasOne(d => d.AdministradorNavigation)
                    .WithOne(p => p.Administrador)
                    .HasForeignKey<Administrador>(d => d.AdministradorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Administr__admin__33D4B598");
            });

            modelBuilder.Entity<Bloqueado>(entity =>
            {
                entity.Property(e => e.Motivo).IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId).IsUnicode(false);

                entity.Property(e => e.TokenConfirmacaoRegisto).IsUnicode(false);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__cliente__30F848ED");
            });

            modelBuilder.Entity<ComentarioRestaurante>(entity =>
            {
                entity.HasKey(e => e.ComentarioId)
                    .HasName("PK__Comentar__5661CA6A19BF59F7");

                entity.Property(e => e.ClienteId).IsUnicode(false);

                entity.Property(e => e.Corpo).IsUnicode(false);

                entity.Property(e => e.RestauranteId).IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.ComentarioRestaurantes)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comentari__clien__37A5467C");

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.ComentarioRestaurantes)
                    .HasForeignKey(d => d.RestauranteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comentari__resta__36B12243");
            });

            modelBuilder.Entity<FavoritarPratoDoDium>(entity =>
            {
                entity.HasKey(e => new { e.PratoId, e.ClienteId })
                    .HasName("PK__Favorita__D83ECAC93A41FC81");

                entity.Property(e => e.ClienteId).IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.FavoritarPratoDoDia)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favoritar__clien__44FF419A");

                entity.HasOne(d => d.Prato)
                    .WithMany(p => p.FavoritarPratoDoDia)
                    .HasForeignKey(d => d.PratoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favoritar__prato__440B1D61");
            });

            modelBuilder.Entity<FavoritarRestaurante>(entity =>
            {
                entity.HasKey(e => new { e.RestauranteId, e.ClienteId })
                    .HasName("PK__Favorita__25831046859735ED");

                entity.Property(e => e.RestauranteId).IsUnicode(false);

                entity.Property(e => e.ClienteId).IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.FavoritarRestaurantes)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favoritar__clien__412EB0B6");

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.FavoritarRestaurantes)
                    .HasForeignKey(d => d.RestauranteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favoritar__resta__403A8C7D");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.Property(e => e.CodigoPostal).IsUnicode(false);

                entity.Property(e => e.Localidade).IsUnicode(false);

                entity.Property(e => e.Morada).IsUnicode(false);
            });

            modelBuilder.Entity<PratoDoDium>(entity =>
            {
                entity.HasKey(e => e.PratoId)
                    .HasName("PK__Prato_do__8C40FE1FD897CDAA");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Tipo).IsUnicode(false);
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.Property(e => e.RestauranteId).IsUnicode(false);

                entity.Property(e => e.ContactoEmail).IsUnicode(false);

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.DiaDeDescanso).IsUnicode(false);

                entity.Property(e => e.HorarioFuncionamento).IsUnicode(false);

                entity.Property(e => e.TipoDeServico).IsUnicode(false);

                entity.HasOne(d => d.Localizacao)
                    .WithMany(p => p.Restaurantes)
                    .HasForeignKey(d => d.LocalizacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__local__2E1BDC42");

                entity.HasOne(d => d.RestauranteNavigation)
                    .WithOne(p => p.Restaurante)
                    .HasForeignKey<Restaurante>(d => d.RestauranteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__resta__2D27B809");
            });

            modelBuilder.Entity<Utilizador>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Utilizad__F3DBC573340437C0");

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.Bloqueado)
                    .WithMany(p => p.Utilizadors)
                    .HasForeignKey(d => d.BloqueadoId)
                    .HasConstraintName("FK__Utilizado__bloqu__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

       

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
