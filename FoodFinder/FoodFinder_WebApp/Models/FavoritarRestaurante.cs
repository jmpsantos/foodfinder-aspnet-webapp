using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Favoritar_Restaurante")]
    public partial class FavoritarRestaurante
    {
        [Key]
        [Column("restaurante_id")]
        [StringLength(75)]
        public string RestauranteId { get; set; }
        [Key]
        [Column("cliente_id")]
        [StringLength(75)]
        public string ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        [InverseProperty("FavoritarRestaurantes")]
        public virtual Cliente Cliente { get; set; }
        [ForeignKey(nameof(RestauranteId))]
        [InverseProperty("FavoritarRestaurantes")]
        public virtual Restaurante Restaurante { get; set; }
    }
}
