using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Restaurante")]
    public partial class Restaurante
    {
        public Restaurante()
        {
            AdicionarPratoDoDia = new HashSet<AdicionarPratoDoDium>();
            ComentarioRestaurantes = new HashSet<ComentarioRestaurante>();
            FavoritarRestaurantes = new HashSet<FavoritarRestaurante>();
        }

        [Key]
        [Column("restaurante_id")]
        [StringLength(75)]
        public string RestauranteId { get; set; }
        [Required]
        [Column("contacto_email")]
        [StringLength(75)]
        public string ContactoEmail { get; set; }
        [Column("contacto_telefone")]
        public long ContactoTelefone { get; set; }
        [Required]
        [Column("horario_funcionamento")]
        [StringLength(150)]
        public string HorarioFuncionamento { get; set; }
        [Column("dia_de_descanso")]
        [StringLength(75)]
        public string DiaDeDescanso { get; set; }
        [Required]
        [Column("tipo_de_servico")]
        [StringLength(75)]
        public string TipoDeServico { get; set; }
        [Column("localizacao_id")]
        public long LocalizacaoId { get; set; }
        [Column("rating")]
        public double Rating { get; set; }
        [Required]
        [Column("descricao")]
        [StringLength(750)]
        public string Descricao { get; set; }

        [ForeignKey(nameof(LocalizacaoId))]
        [InverseProperty("Restaurantes")]
        public virtual Localizacao Localizacao { get; set; }
        [ForeignKey(nameof(RestauranteId))]
        [InverseProperty(nameof(Utilizador.Restaurante))]
        public virtual Utilizador RestauranteNavigation { get; set; }
        [InverseProperty(nameof(AdicionarPratoDoDium.Restaurante))]
        public virtual ICollection<AdicionarPratoDoDium> AdicionarPratoDoDia { get; set; }
        [InverseProperty(nameof(ComentarioRestaurante.Restaurante))]
        public virtual ICollection<ComentarioRestaurante> ComentarioRestaurantes { get; set; }
        [InverseProperty(nameof(FavoritarRestaurante.Restaurante))]
        public virtual ICollection<FavoritarRestaurante> FavoritarRestaurantes { get; set; }
    }
}
