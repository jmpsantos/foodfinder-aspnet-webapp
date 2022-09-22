using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FoodFinder_WebApp.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            ComentarioRestaurantes = new HashSet<ComentarioRestaurante>();
            FavoritarPratoDoDia = new HashSet<FavoritarPratoDoDium>();
            FavoritarRestaurantes = new HashSet<FavoritarRestaurante>();
        }

        [Key]
        [Column("cliente_id")]
        [StringLength(75)]
        public string ClienteId { get; set; }
        [Column("token_confirmacaoRegisto")]
        [StringLength(150)]
        public string TokenConfirmacaoRegisto { get; set; }

        [ForeignKey(nameof(ClienteId))]
        [InverseProperty(nameof(Utilizador.Cliente))]
        public virtual Utilizador ClienteNavigation { get; set; }
        [InverseProperty(nameof(ComentarioRestaurante.Cliente))]
        public virtual ICollection<ComentarioRestaurante> ComentarioRestaurantes { get; set; }
        [InverseProperty(nameof(FavoritarPratoDoDium.Cliente))]
        public virtual ICollection<FavoritarPratoDoDium> FavoritarPratoDoDia { get; set; }
        [InverseProperty(nameof(FavoritarRestaurante.Cliente))]
        public virtual ICollection<FavoritarRestaurante> FavoritarRestaurantes { get; set; }
    }
}
