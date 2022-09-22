using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Utilizador")]
    public partial class Utilizador
    {
        [Key]
        [Column("username")]
        [StringLength(75)]
        public string Username { get; set; }
        [Required]
        [Column("email")]
        [StringLength(75)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(75)]
        public string Password { get; set; }
        [Required]
        [Column("nome")]
        [StringLength(75)]
        public string Nome { get; set; }
        [Column("registo_Confirmado")]
        public bool RegistoConfirmado { get; set; }
        [Column("bloqueado_ID")]
        public long? BloqueadoId { get; set; }

        [ForeignKey(nameof(BloqueadoId))]
        [InverseProperty("Utilizadors")]
        public virtual Bloqueado Bloqueado { get; set; }
        [InverseProperty("AdministradorNavigation")]
        public virtual Administrador Administrador { get; set; }
        [InverseProperty("ClienteNavigation")]
        public virtual Cliente Cliente { get; set; }
        [InverseProperty("RestauranteNavigation")]
        public virtual Restaurante Restaurante { get; set; }
    }
}
