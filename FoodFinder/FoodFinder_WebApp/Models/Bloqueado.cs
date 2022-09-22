using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Bloqueado")]
    public partial class Bloqueado
    {
        public Bloqueado()
        {
            Utilizadors = new HashSet<Utilizador>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("motivo")]
        [StringLength(200)]
        public string Motivo { get; set; }

        [InverseProperty(nameof(Utilizador.Bloqueado))]
        public virtual ICollection<Utilizador> Utilizadors { get; set; }
    }
}
