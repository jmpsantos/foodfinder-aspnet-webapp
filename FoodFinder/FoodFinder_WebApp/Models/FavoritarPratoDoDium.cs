using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Favoritar_Prato_do_Dia")]
    public partial class FavoritarPratoDoDium
    {
        [Key]
        [Column("prato_id")]
        public long PratoId { get; set; }
        [Key]
        [Column("cliente_id")]
        [StringLength(75)]
        public string ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        [InverseProperty("FavoritarPratoDoDia")]
        public virtual Cliente Cliente { get; set; }
        [ForeignKey(nameof(PratoId))]
        [InverseProperty(nameof(PratoDoDium.FavoritarPratoDoDia))]
        public virtual PratoDoDium Prato { get; set; }
    }
}
