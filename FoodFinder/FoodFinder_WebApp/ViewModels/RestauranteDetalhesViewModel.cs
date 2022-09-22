using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder_WebApp.Models;

namespace FoodFinder_WebApp.ViewModels
{
    public class RestauranteDetalhesViewModel
    {
        public List<string> ImagensPath { get; set; }
        public List<Restaurante> Restaurantes { get; set; }
        public List<Localizacao> Localizacoes { get; set; }
        public List<Utilizador> UtilizadoresRestaurantes { get; set; }
        public List<ComentarioRestaurante> ComentariosRestaurantes { get; set; }
    }
}
