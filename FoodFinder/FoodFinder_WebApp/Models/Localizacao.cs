using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Localizacao")]
    public partial class Localizacao
    {
        public Localizacao()
        {
            Restaurantes = new HashSet<Restaurante>();
        }

        [Key]
        [Column("localizacao_id")]
        public long LocalizacaoId { get; set; }
        [Required]
        [Column("codigo_postal")]
        [StringLength(15)]
        public string CodigoPostal { get; set; }
        [Required]
        [Column("morada")]
        [StringLength(75)]
        public string Morada { get; set; }
        [Required]
        [Column("localidade")]
        [StringLength(75)]
        public string Localidade { get; set; }
        [Column("gps_Latitude")]
        public double GpsLatitude { get; set; }
        [Column("gps_Longitude")]
        public double GpsLongitude { get; set; }

        [InverseProperty(nameof(Restaurante.Localizacao))]
        public virtual ICollection<Restaurante> Restaurantes { get; set; }
    }
}
