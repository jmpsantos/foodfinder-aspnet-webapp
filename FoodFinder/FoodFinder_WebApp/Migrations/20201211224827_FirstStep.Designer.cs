﻿// <auto-generated />
using System;
using FoodFinder_WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodFinder_WebApp.Migrations
{
    [DbContext(typeof(FoodFinder_WebAppContext))]
    [Migration("20201211224827_FirstStep")]
    partial class FirstStep
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("FoodFinder_WebApp.Models.AdicionarPratoDoDium", b =>
                {
                    b.Property<long>("PratoId")
                        .HasColumnType("bigint")
                        .HasColumnName("prato_id");

                    b.Property<string>("RestauranteId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("restaurante_id");

                    b.Property<DateTime>("DataPrato")
                        .HasColumnType("date")
                        .HasColumnName("data_prato");

                    b.Property<bool>("Destacado")
                        .HasColumnType("bit")
                        .HasColumnName("destacado");

                    b.Property<double>("Preco")
                        .HasColumnType("float")
                        .HasColumnName("preco");

                    b.HasKey("PratoId", "RestauranteId", "DataPrato")
                        .HasName("PK__Adiciona__522977CAF98A22AC");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Adicionar_Prato_do_Dia");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Administrador", b =>
                {
                    b.Property<string>("AdministradorId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("administrador_id");

                    b.HasKey("AdministradorId");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Bloqueado", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("motivo");

                    b.HasKey("Id");

                    b.ToTable("Bloqueado");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Cliente", b =>
                {
                    b.Property<string>("ClienteId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("cliente_id");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.ComentarioRestaurante", b =>
                {
                    b.Property<long>("ComentarioId")
                        .HasColumnType("bigint")
                        .HasColumnName("comentario_id");

                    b.Property<string>("ClienteId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("cliente_id");

                    b.Property<string>("Corpo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("corpo");

                    b.Property<DateTime>("DataComentario")
                        .HasColumnType("date")
                        .HasColumnName("data_comentario");

                    b.Property<string>("RestauranteId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("restaurante_id");

                    b.HasKey("ComentarioId")
                        .HasName("PK__Comentar__5661CA6A7722218D");

                    b.HasIndex("ClienteId");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Comentario_Restaurante");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.FavoritarPratoDoDium", b =>
                {
                    b.Property<long>("PratoId")
                        .HasColumnType("bigint")
                        .HasColumnName("prato_id");

                    b.Property<string>("ClienteId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("cliente_id");

                    b.HasKey("PratoId", "ClienteId")
                        .HasName("PK__Favorita__D83ECAC971115171");

                    b.HasIndex("ClienteId");

                    b.ToTable("Favoritar_Prato_do_Dia");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.FavoritarRestaurante", b =>
                {
                    b.Property<string>("RestauranteId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("restaurante_id");

                    b.Property<string>("ClienteId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("cliente_id");

                    b.HasKey("RestauranteId", "ClienteId")
                        .HasName("PK__Favorita__25831046647C40B4");

                    b.HasIndex("ClienteId");

                    b.ToTable("Favoritar_Restaurante");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Localizacao", b =>
                {
                    b.Property<long>("LocalizacaoId")
                        .HasColumnType("bigint")
                        .HasColumnName("localizacao_id");

                    b.Property<string>("CodigoPostal")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("codigo_postal");

                    b.Property<double>("GpsLatitude")
                        .HasColumnType("float")
                        .HasColumnName("gps_Latitude");

                    b.Property<double>("GpsLongitude")
                        .HasColumnType("float")
                        .HasColumnName("gps_Longitude");

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("localidade");

                    b.Property<string>("Morada")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("morada");

                    b.HasKey("LocalizacaoId");

                    b.ToTable("Localizacao");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.PratoDoDium", b =>
                {
                    b.Property<long>("PratoId")
                        .HasColumnType("bigint")
                        .HasColumnName("prato_id");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("descricao");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("tipo");

                    b.HasKey("PratoId")
                        .HasName("PK__Prato_do__8C40FE1F46BE0181");

                    b.ToTable("Prato_do_Dia");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Restaurante", b =>
                {
                    b.Property<string>("RestauranteId")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("restaurante_id");

                    b.Property<string>("ContactoEmail")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("contacto_email");

                    b.Property<long>("ContactoTelefone")
                        .HasColumnType("bigint")
                        .HasColumnName("contacto_telefone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("descricao");

                    b.Property<string>("DiaDeDescanso")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("dia_de_descanso");

                    b.Property<string>("HorarioFuncionamento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("horario_funcionamento");

                    b.Property<long>("LocalizacaoId")
                        .HasColumnType("bigint")
                        .HasColumnName("localizacao_id");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<string>("TipoDeServico")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tipo_de_servico");

                    b.HasKey("RestauranteId");

                    b.HasIndex("LocalizacaoId");

                    b.ToTable("Restaurante");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Utilizador", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("username");

                    b.Property<long?>("BloqueadoId")
                        .HasColumnType("bigint")
                        .HasColumnName("bloqueado_ID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nome");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("password");

                    b.Property<bool>("RegistoConfirmado")
                        .HasColumnType("bit")
                        .HasColumnName("registo_Confirmado");

                    b.HasKey("Username")
                        .HasName("PK__Utilizad__F3DBC5738F795E7B");

                    b.HasIndex("BloqueadoId");

                    b.ToTable("Utilizador");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.AdicionarPratoDoDium", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.PratoDoDium", "Prato")
                        .WithMany("AdicionarPratoDoDia")
                        .HasForeignKey("PratoId")
                        .HasConstraintName("FK__Adicionar__prato__4F7CD00D")
                        .IsRequired();

                    b.HasOne("FoodFinder_WebApp.Models.Restaurante", "Restaurante")
                        .WithMany("AdicionarPratoDoDia")
                        .HasForeignKey("RestauranteId")
                        .HasConstraintName("FK__Adicionar__resta__4E88ABD4")
                        .IsRequired();

                    b.Navigation("Prato");

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Administrador", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Utilizador", "AdministradorNavigation")
                        .WithOne("Administrador")
                        .HasForeignKey("FoodFinder_WebApp.Models.Administrador", "AdministradorId")
                        .HasConstraintName("FK__Administr__admin__45F365D3")
                        .IsRequired();

                    b.Navigation("AdministradorNavigation");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Cliente", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Utilizador", "ClienteNavigation")
                        .WithOne("Cliente")
                        .HasForeignKey("FoodFinder_WebApp.Models.Cliente", "ClienteId")
                        .HasConstraintName("FK__Cliente__cliente__4316F928")
                        .IsRequired();

                    b.Navigation("ClienteNavigation");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.ComentarioRestaurante", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Cliente", "Cliente")
                        .WithMany("ComentarioRestaurantes")
                        .HasForeignKey("ClienteId")
                        .HasConstraintName("FK__Comentari__clien__49C3F6B7")
                        .IsRequired();

                    b.HasOne("FoodFinder_WebApp.Models.Restaurante", "Restaurante")
                        .WithMany("ComentarioRestaurantes")
                        .HasForeignKey("RestauranteId")
                        .HasConstraintName("FK__Comentari__resta__48CFD27E")
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.FavoritarPratoDoDium", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Cliente", "Cliente")
                        .WithMany("FavoritarPratoDoDia")
                        .HasForeignKey("ClienteId")
                        .HasConstraintName("FK__Favoritar__clien__571DF1D5")
                        .IsRequired();

                    b.HasOne("FoodFinder_WebApp.Models.PratoDoDium", "Prato")
                        .WithMany("FavoritarPratoDoDia")
                        .HasForeignKey("PratoId")
                        .HasConstraintName("FK__Favoritar__prato__5629CD9C")
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Prato");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.FavoritarRestaurante", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Cliente", "Cliente")
                        .WithMany("FavoritarRestaurantes")
                        .HasForeignKey("ClienteId")
                        .HasConstraintName("FK__Favoritar__clien__534D60F1")
                        .IsRequired();

                    b.HasOne("FoodFinder_WebApp.Models.Restaurante", "Restaurante")
                        .WithMany("FavoritarRestaurantes")
                        .HasForeignKey("RestauranteId")
                        .HasConstraintName("FK__Favoritar__resta__52593CB8")
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Restaurante", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Localizacao", "Localizacao")
                        .WithMany("Restaurantes")
                        .HasForeignKey("LocalizacaoId")
                        .HasConstraintName("FK__Restauran__local__403A8C7D")
                        .IsRequired();

                    b.HasOne("FoodFinder_WebApp.Models.Utilizador", "RestauranteNavigation")
                        .WithOne("Restaurante")
                        .HasForeignKey("FoodFinder_WebApp.Models.Restaurante", "RestauranteId")
                        .HasConstraintName("FK__Restauran__resta__3F466844")
                        .IsRequired();

                    b.Navigation("Localizacao");

                    b.Navigation("RestauranteNavigation");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Utilizador", b =>
                {
                    b.HasOne("FoodFinder_WebApp.Models.Bloqueado", "Bloqueado")
                        .WithMany("Utilizadors")
                        .HasForeignKey("BloqueadoId")
                        .HasConstraintName("FK__Utilizado__bloqu__38996AB5");

                    b.Navigation("Bloqueado");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Bloqueado", b =>
                {
                    b.Navigation("Utilizadors");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Cliente", b =>
                {
                    b.Navigation("ComentarioRestaurantes");

                    b.Navigation("FavoritarPratoDoDia");

                    b.Navigation("FavoritarRestaurantes");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Localizacao", b =>
                {
                    b.Navigation("Restaurantes");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.PratoDoDium", b =>
                {
                    b.Navigation("AdicionarPratoDoDia");

                    b.Navigation("FavoritarPratoDoDia");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Restaurante", b =>
                {
                    b.Navigation("AdicionarPratoDoDia");

                    b.Navigation("ComentarioRestaurantes");

                    b.Navigation("FavoritarRestaurantes");
                });

            modelBuilder.Entity("FoodFinder_WebApp.Models.Utilizador", b =>
                {
                    b.Navigation("Administrador");

                    b.Navigation("Cliente");

                    b.Navigation("Restaurante");
                });
#pragma warning restore 612, 618
        }
    }
}
