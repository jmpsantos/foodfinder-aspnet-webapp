using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Helpers;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class FavoritarRestauranteController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public FavoritarRestauranteController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }

        [LogActionFilter(aut = "cliente")]
        public IActionResult ListaRestaurantesFavoritos()
        {

            // Determinar pratos na tabela FavoritarPratoDoDia cujo o utilizador com sessão favoritou.
            // Converter esses pratos para uma lista do tipo PratoDoDia.
            string userID = HttpContext.Session.GetString("user");

            List<Restaurante> listaRestaurantesFavoritos = new List<Restaurante>();
            List<FavoritarRestaurante> listaOriginal = _context.FavoritarRestaurantes.Where(x => x.ClienteId == userID).ToList();

            foreach (var restaurante in listaOriginal)
            {
                listaRestaurantesFavoritos.Add(_context.Restaurantes.FirstOrDefault(x => x.RestauranteId == restaurante.RestauranteId));
            }


            // Classe com vários
            GeneralHelpers generalHelper = new GeneralHelpers(_context);
            // ViewModel que guardará toda a informação para passar para a View.
            RestauranteDetalhesViewModel restauranteDetalhesViewModel = new RestauranteDetalhesViewModel();

            restauranteDetalhesViewModel = generalHelper.CriarRestauranteDetalhesViewModelAPartirRestaurante(listaRestaurantesFavoritos);

            if (restauranteDetalhesViewModel != null)
            { 
                return View(restauranteDetalhesViewModel);
            }

            return View(null);
        }
    }
}

