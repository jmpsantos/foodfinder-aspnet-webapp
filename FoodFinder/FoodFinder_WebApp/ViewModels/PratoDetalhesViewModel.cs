using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder_WebApp.Models;

namespace FoodFinder_WebApp.ViewModels
{
    public class PratoDetalhesViewModel
    {
        public List<PratoDoDium> PratosDoDia { get; set; }
        public List<AdicionarPratoDoDium> AdicionarPratoDoDia { get; set; }
        public List<string> ImagensPath { get; set; }
        public List<Restaurante> Restaurantes { get; set; }
        public List<Utilizador> UtilizadoresRestaurantes { get; set; }
    }
}
