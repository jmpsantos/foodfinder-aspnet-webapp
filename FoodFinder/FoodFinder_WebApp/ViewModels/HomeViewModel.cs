using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder_WebApp.Models;

namespace FoodFinder_WebApp.ViewModels
{
    public class HomeViewModel
    {
        public PratoDetalhesViewModel PratoDetalhesViewModel { get; set; }
        public RestauranteDetalhesViewModel RestauranteDetalhesViewModel { get; set; }

    }
}
