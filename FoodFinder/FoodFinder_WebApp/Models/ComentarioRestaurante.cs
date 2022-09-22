using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Comentario_Restaurante")]
    public partial class ComentarioRestaurante
    {
        [Key]
        [Column("comentario_id")]
        public long ComentarioId { get; set; }
        [Column("data_comentario", TypeName = "datetime")]
        public DateTime DataComentario { get; set; }
        [Required]
        [Column("restaurante_id")]
        [StringLength(75)]
        public string RestauranteId { get; set; }
        [Required]
        [Column("cliente_id")]
        [StringLength(75)]
        public string ClienteId { get; set; }
        [Required]
        [Column("corpo")]
        [StringLength(750)]
        public string Corpo { get; set; }
        [Column("rating")]
        public double Rating { get; set; }

        [ForeignKey(nameof(ClienteId))]
        [InverseProperty("ComentarioRestaurantes")]
        public virtual Cliente Cliente { get; set; }
        [ForeignKey(nameof(RestauranteId))]
        [InverseProperty("ComentarioRestaurantes")]
        public virtual Restaurante Restaurante { get; set; }
    }
}
