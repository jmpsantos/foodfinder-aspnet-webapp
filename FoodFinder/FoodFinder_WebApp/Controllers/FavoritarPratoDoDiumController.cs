using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.ViewModels;
using FoodFinder_WebApp.Helpers;

namespace FoodFinder_WebApp.Controllers
{
    public class FavoritarPratoDoDiumController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public FavoritarPratoDoDiumController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }



        [LogActionFilter(aut = "cliente")]
        public IActionResult ListaPratosDoDiaFavoritos()
        {

            // Determinar pratos na tabela FavoritarPratoDoDia cujo o utilizador com sessão favoritou.
            // Converter esses pratos para uma lista do tipo PratoDoDia.
            string userID = HttpContext.Session.GetString("user");

            List<FavoritarPratoDoDium> listaOriginal = _context.FavoritarPratoDoDia.Where(x => x.ClienteId == userID).ToList();
            List<AdicionarPratoDoDium> listaAdicionar = new List<AdicionarPratoDoDium>();
            List<PratoDoDium> listaPratos = new List<PratoDoDium>();

            // Filtrar apenas 
            foreach (var prato in listaOriginal)
            {
                // Adicionar Prato mais recente.
                AdicionarPratoDoDium adicionar = _context.AdicionarPratoDoDia.Where(x => x.PratoId == prato.PratoId).OrderByDescending(x => x.DataPrato).FirstOrDefault();

                if (adicionar != null)
                {
                    listaAdicionar.Add(adicionar);
                }
            }

            // Classe que tem vários métodos auxiliares.
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);
            // ViewModel que guardará toda a informação para passar para a View.
            PratoDetalhesViewModel pratoDetalhesViewModel = new PratoDetalhesViewModel();

            pratoDetalhesViewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirAdicionarPratoDoDia(listaAdicionar);

            if (pratoDetalhesViewModel != null)
            {
                return View(pratoDetalhesViewModel);
            }

            return View(null);
        }
    }
}
