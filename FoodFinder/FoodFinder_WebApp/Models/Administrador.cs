using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Administrador")]
    public partial class Administrador
    {
        [Key]
        [Column("administrador_id")]
        [StringLength(75)]
        public string AdministradorId { get; set; }

        [ForeignKey(nameof(AdministradorId))]
        [InverseProperty(nameof(Utilizador.Administrador))]
        public virtual Utilizador AdministradorNavigation { get; set; }
    }
}
