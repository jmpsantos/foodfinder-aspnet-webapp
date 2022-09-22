using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using FoodFinder_WebApp.Helpers;

namespace FoodFinder_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FoodFinder_WebAppContext _context;
        private readonly IHostEnvironment _he;

        public HomeController(ILogger<HomeController> logger, IHostEnvironment e, FoodFinder_WebAppContext context)
        {
            _logger = logger;
            _context = context;
            _he = e;
        }

        /// <summary>
        /// Invoca view responsável pelo home menu e trata da lista de pratos destacados e de
        /// restaurantes aleatórios a apresentar.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            HomeViewModel homeModel = new HomeViewModel();
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);
            int i = 0;

            if (_context.AdicionarPratoDoDia.Any())
            {
                // Serão apresentados os pratos classificados como destacados na base de dados.
                List<AdicionarPratoDoDium> listaPratosDestacados = _context.AdicionarPratoDoDia.Where(x => x.Destacado == true).Where(x =>
                x.DataPrato.Date == DateTime.Now.Date).Where(x => x.Ativado == true).ToList();
                List<PratoDoDium> listaPratos = new List<PratoDoDium>();

                foreach (var prato in listaPratosDestacados)
                {
                    listaPratos.Add(_context.PratoDoDia.FirstOrDefault(x => x.PratoId == prato.PratoId));
                }

                homeModel.PratoDetalhesViewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirPratoDoDia(listaPratos);
            }
            else
            {
                Console.WriteLine("Não existem pratos ativos no sistema.");
            }


            if (_context.Restaurantes.Any())
            {
                // Serão apresentados restaurantes selecionados aleatoriamente.
                List<Restaurante> listaRestaurantesAleatorios = _context.Restaurantes.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

                homeModel.RestauranteDetalhesViewModel = generalHelpers.CriarRestauranteDetalhesViewModelAPartirRestaurante(listaRestaurantesAleatorios);
            }
            else
            {
                Console.WriteLine("Não existem restaurantes ativos no sistema");
            }

            return View(homeModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Invoca view para apresentar lista de restaurantes ou pratos que possuam
        /// no seu nome o texto passado como argumento.
        /// </summary>
        /// <param name="textoPesquisa"></param>
        /// <returns></returns>
        public IActionResult Pesquisar(string textoPesquisa)
        {
            HomeViewModel homeModel = new HomeViewModel();
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);

            bool igualdadeEncontrada = false;

            if (textoPesquisa == null)
            {
                return View("Pesquisar", null);
            }

            // Vamos verificar primeiro se existem pratos com nome que contém texto para pesquisa.
            if (_context.AdicionarPratoDoDia.Any())
            {

                List<AdicionarPratoDoDium> pratosDoDiaAtivos = _context.AdicionarPratoDoDia
                    .Where(x => x.DataPrato.Date == DateTime.Now.Date)
                    .Where(x => x.Ativado == true)
                    .ToList();

                List<PratoDoDium> listaPratos = new List<PratoDoDium>();

                foreach (var prato in pratosDoDiaAtivos)
                {
                    PratoDoDium pratoEncontrado = _context.PratoDoDia.FirstOrDefault(x => x.PratoId == prato.PratoId);
                    if (pratoEncontrado.Nome.ToLower().Contains(textoPesquisa.ToLower()))
                    {
                        listaPratos.Add(pratoEncontrado);
                    }
                }

                homeModel.PratoDetalhesViewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirPratoDoDia(listaPratos);
            }

            // Caso tenhamos encontrado pratos.
            if (homeModel.PratoDetalhesViewModel != null)
            {
                igualdadeEncontrada = true;

                ViewData["tipoEntidade"] = "prato";
                ViewData["partialView"] = "~/Views/AdicionarPratoDoDium/" + "ListaPratos.cshtml";
                return View(homeModel);
            }

            // Caso não tenhamos encontrado pratos.
            if (igualdadeEncontrada == false)
            {
                // Vamos procurar restaurantes que têm titulo que contém texto a pesquisar.
                if (_context.Restaurantes.Any())
                {
                    List<Utilizador> listaUtilizadoresComNome = new List<Utilizador>();
                    List<Restaurante> listaRestaurantes = new List<Restaurante>();

                    foreach (var utilizador in _context.Utilizadors.ToList())
                    {
                        if (utilizador.Nome.ToLower().Contains(textoPesquisa.ToLower()))
                        {
                            Restaurante restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == utilizador.Username);

                            if (restaurante != null)
                            {
                                listaRestaurantes.Add(restaurante);
                            }
                        }
                    }

                    foreach (var utilizador in listaUtilizadoresComNome)
                    {
                        Restaurante restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == utilizador.Username);

                        if (restaurante != null)
                        {
                            if (utilizador.Nome.ToLower().Contains(textoPesquisa.ToLower()))
                            {
                                listaRestaurantes.Add(restaurante);
                            }
                        }
                    }

                    homeModel.RestauranteDetalhesViewModel = generalHelpers.CriarRestauranteDetalhesViewModelAPartirRestaurante(listaRestaurantes);
                }


                // Caso tenhamos encontrado restaurante.
                if (homeModel.RestauranteDetalhesViewModel != null)
                {
                    igualdadeEncontrada = true;

                    ViewData["tipoEntidade"] = "restaurante";
                    ViewData["partialView"] = "~/Views/Restaurante/" + "ListaRestaurante.cshtml";
                    return View(homeModel);
                }
            }
            return View(null);
        }

            /// <summary>
            /// Invoca view para apresentar uma lista de pratos ou de restaurantes consoante
            /// o filtro indicado no argumento.
            /// </summary>
            /// <param name="tipoEntidade">Indica se lista é de pratos ou de restaurantes.</param>
            /// <returns></returns>
            public IActionResult PesquisarComFiltragemPorTipoEntidade(string tipoEntidade)
            {
                HomeViewModel homeModel = new HomeViewModel();
                GeneralHelpers generalHelpers = new GeneralHelpers(_context);

                if (tipoEntidade != null)
                {
                    switch (tipoEntidade)
                    {
                        case "pratos":

                            List<AdicionarPratoDoDium> pratosDoDiaAtivos = new List<AdicionarPratoDoDium>();
                            List<PratoDoDium> listaPratos = new List<PratoDoDium>();
                            pratosDoDiaAtivos = _context.AdicionarPratoDoDia.Where(x =>
                    x.DataPrato.Date == DateTime.Now.Date).Where(x => x.Ativado == true).ToList();

                            foreach (var prato in pratosDoDiaAtivos)
                            {
                                listaPratos.Add(_context.PratoDoDia.FirstOrDefault(x => x.PratoId == prato.PratoId));
                            }

                            homeModel.PratoDetalhesViewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirPratoDoDia(listaPratos);

                            ViewData["tipoEntidade"] = "prato";
                            ViewData["partialView"] = "~/Views/AdicionarPratoDoDium/" + "ListaPratos.cshtml";
                            break;

                        case "restaurantes":

                            List<Utilizador> listaUtilizadores = new List<Utilizador>();
                            listaUtilizadores = _context.Utilizadors.ToList();
                            List<Restaurante> listaRestaurantes = new List<Restaurante>();

                            foreach (var utilizador in listaUtilizadores)
                            {
                                Restaurante restaurante = new Restaurante();
                                restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == utilizador.Username);

                                if (restaurante != null)
                                {
                                    listaRestaurantes.Add(restaurante);
                                }
                            }

                            homeModel.RestauranteDetalhesViewModel = generalHelpers.CriarRestauranteDetalhesViewModelAPartirRestaurante(listaRestaurantes);

                            ViewData["tipoEntidade"] = "restaurante";
                            ViewData["partialView"] = "~/Views/Restaurante/" + "ListaRestaurante.cshtml";
                            break;
                    }
                }

                return View("Pesquisar", homeModel);
            }

            /// <summary>
            /// Invoca view para apresentar uma lista de pratos ou de restaurantes consoante
            /// filtros mais detalhados.
            /// </summary>
            /// <param name="filtroTipoServico"></param>
            /// <param name="filtroTipoPrato"></param>
            /// <returns></returns>
            public IActionResult PesquisarComFiltragemDetalhada(string filtroTipoServico, string filtroTipoPrato)
            {
                HomeViewModel homeModel = new HomeViewModel();
                GeneralHelpers generalHelpers = new GeneralHelpers(_context);

                if (filtroTipoPrato != null) // se a pesquisa for filtra para apresentar pratos.
                {
                    List<AdicionarPratoDoDium> listaPratoDoDia = new List<AdicionarPratoDoDium>();
                    listaPratoDoDia = _context.AdicionarPratoDoDia.Where(x =>
                        x.DataPrato.Date == DateTime.Now.Date).Where(x => x.Ativado == true).ToList();

                    string tipo = "";

                    switch (filtroTipoPrato)
                    {
                        case "0":
                            tipo = "";
                            break;
                        case "Carne":
                            tipo = "Carne";
                            break;
                        case "Peixe":
                            tipo = "Peixe";
                            break;
                        case "Vegetariano":
                            tipo = "Vegeteriano";
                            break;
                    }

                    PratoDoDium pratoDia = new PratoDoDium();
                    List<PratoDoDium> listaPratos = new List<PratoDoDium>();
                    foreach (var prato in listaPratoDoDia)
                    {
                        pratoDia = (PratoDoDium)_context.PratoDoDia.FirstOrDefault(x => x.PratoId == prato.PratoId && x.Tipo == tipo);

                        if (pratoDia != null)
                        {
                            listaPratos.Add(pratoDia);
                        }
                    }

                    homeModel.PratoDetalhesViewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirPratoDoDia(listaPratos);

                    ViewData["tipoEntidade"] = "prato";
                    ViewData["partialView"] = "~/Views/AdicionarPratoDoDium/" + "ListaPratos.cshtml";

                }
                else if (string.Compare(filtroTipoServico, "0") != 0 && filtroTipoServico != null) // se a pesquisa for filtra para apresentar restaurantes.
                {
                    List<Restaurante> listaRestaurantes = new List<Restaurante>();

                    switch (filtroTipoServico)
                    {
                        case "0":
                            listaRestaurantes = _context.Restaurantes.ToList();
                            break;
                        case "Local":
                            listaRestaurantes = _context.Restaurantes.Where(x => x.TipoDeServico.Contains("Local")).ToList();
                            break;
                        case "Take-away":
                            listaRestaurantes = _context.Restaurantes.Where(x => x.TipoDeServico.Contains("Take-away")).ToList();
                            break;
                        case "Esplanada":
                            listaRestaurantes = _context.Restaurantes.Where(x => x.TipoDeServico.Contains("Esplanada")).ToList();
                            break;
                    }

                    homeModel.RestauranteDetalhesViewModel = generalHelpers.CriarRestauranteDetalhesViewModelAPartirRestaurante(listaRestaurantes);

                    ViewData["tipoEntidade"] = "restaurante";
                    ViewData["partialView"] = "~/Views/Restaurante/" + "ListaRestaurante.cshtml";
                }


                return View("Pesquisar", homeModel);
            }
        }
    }

