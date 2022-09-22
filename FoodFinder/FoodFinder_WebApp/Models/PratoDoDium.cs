using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Prato_do_Dia")]
    public partial class PratoDoDium
    {
        public PratoDoDium()
        {
            AdicionarPratoDoDia = new HashSet<AdicionarPratoDoDium>();
            FavoritarPratoDoDia = new HashSet<FavoritarPratoDoDium>();
        }

        [Key]
        [Column("prato_id")]
        public long PratoId { get; set; }
        [Required]
        [Column("descricao")]
        [StringLength(500)]
        public string Descricao { get; set; }
        [Required]
        [Column("tipo")]
        [StringLength(75)]
        public string Tipo { get; set; }
        [Required]
        [Column("nome")]
        [StringLength(75)]
        public string Nome { get; set; }

        [InverseProperty(nameof(AdicionarPratoDoDium.Prato))]
        public virtual ICollection<AdicionarPratoDoDium> AdicionarPratoDoDia { get; set; }
        [InverseProperty(nameof(FavoritarPratoDoDium.Prato))]
        public virtual ICollection<FavoritarPratoDoDium> FavoritarPratoDoDia { get; set; }
    }
}
