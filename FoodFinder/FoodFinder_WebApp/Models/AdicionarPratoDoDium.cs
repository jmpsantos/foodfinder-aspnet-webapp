using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Adicionar_Prato_do_Dia")]
    public partial class AdicionarPratoDoDium
    {
        [Key]
        [Column("restaurante_id")]
        [StringLength(75)]
        public string RestauranteId { get; set; }
        [Key]
        [Column("prato_id")]
        public long PratoId { get; set; }
        [Key]
        [Column("data_prato", TypeName = "datetime")]
        public DateTime DataPrato { get; set; }
        [Column("preco")]
        public double Preco { get; set; }
        [Column("destacado")]
        public bool Destacado { get; set; }
        [Column("ativado")]
        public bool Ativado { get; set; }

        [ForeignKey(nameof(PratoId))]
        [InverseProperty(nameof(PratoDoDium.AdicionarPratoDoDia))]
        public virtual PratoDoDium Prato { get; set; }
        [ForeignKey(nameof(RestauranteId))]
        [InverseProperty("AdicionarPratoDoDia")]
        public virtual Restaurante Restaurante { get; set; }
    }
}
