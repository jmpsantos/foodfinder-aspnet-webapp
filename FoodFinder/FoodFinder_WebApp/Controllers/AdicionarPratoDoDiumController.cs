using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Helpers;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FoodFinder_WebApp.Controllers
{
    public class AdicionarPratoDoDiumController : Controller
    {

        private readonly FoodFinder_WebAppContext _context;
        private readonly IHostEnvironment _he;

        public AdicionarPratoDoDiumController(FoodFinder_WebAppContext context, IHostEnvironment e)
        {
            _context = context;
            _he = e;
        }


        /// <summary>
        /// Marca o prato com o ID especificado no argumento do método como destacado. 
        /// </summary>
        /// <param name="id">ID do prato a destacar.</param>
        /// <param name="data"></param>
        /// <returns></returns>
        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> DestacarPrato(long? id, string? data)
        {
            if (id == null)
            {
                return NotFound();
            }

            DateTime auxdata;
            DateTime.TryParse(data, out auxdata);

            var adicionarPratoDoDium = await _context.AdicionarPratoDoDia
                .Include(a => a.Prato)
                .Include(a => a.Restaurante)
                .FirstOrDefaultAsync(m => m.PratoId == id &&  m.Restaurante.RestauranteId == HttpContext.Session.GetString("user") && m.DataPrato.Date == auxdata.Date);
            
            if (adicionarPratoDoDium == null)
            {
                return NotFound();
            }

            adicionarPratoDoDium.Destacado = true;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdicionarPratoDoDium");
        }



        /// <summary>
        /// Desativa o prato com o id especificado nos parâmetros do método.
        /// </summary>
        /// <param name="id">ID do prato a destacar.</param>
        /// <param name="data"></param>
        /// <returns></returns>
        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> RemoverDestacarPrato(long? id, string? data)
        {
            if (id == null)
            {
                return NotFound();
            }

            DateTime auxdata;

            DateTime.TryParse(data, out auxdata);
            //DateTime auxdata = DateTime.ParseExact(data, "MM / dd / yyyy HH: mm:ss", CultureInfo.InvariantCulture);

            var adicionarPratoDoDium = await _context.AdicionarPratoDoDia
                .Include(a => a.Prato)
                .Include(a => a.Restaurante)
                .FirstOrDefaultAsync(m => m.PratoId == id && m.Restaurante.RestauranteId == HttpContext.Session.GetString("user") && m.DataPrato.Date == auxdata.Date);

            if (adicionarPratoDoDium == null)
            {
                return NotFound();
            }

            adicionarPratoDoDium.Destacado = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdicionarPratoDoDium");
        }


        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Adiciona uma entrada na tabela AdicionarPratoDoDia da BD com os dados passados como argumentos.
        /// </summary>
        /// <param name="adicionarPratoDoDium">Dados relativos à entrada a adicionar na tabela AdicionarPratoDoDia.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> Create([Bind("Preco,DataPrato")] AdicionarPratoDoDium adicionarPratoDoDium)
        {
            Settings settings = new Settings();

            if (ModelState.IsValid)
            {            
                adicionarPratoDoDium.Destacado = false;
                PratoDoDium novoprato = JsonConvert.DeserializeObject<PratoDoDium>((string)TempData["NovoPrato"]);
                Restaurante rest = _context.Restaurantes.SingleOrDefault(r => r.RestauranteId == HttpContext.Session.GetString("user"));
                
                adicionarPratoDoDium.Restaurante = rest;
                adicionarPratoDoDium.RestauranteId = rest.RestauranteId;
                adicionarPratoDoDium.Prato = novoprato;
                adicionarPratoDoDium.PratoId = novoprato.PratoId;
                adicionarPratoDoDium.Ativado = true;

                _context.Update(novoprato);
                _context.Add(adicionarPratoDoDium);
                
                await _context.SaveChangesAsync();
                
                string destination = settings.ImagensPratosPathWWW;
                if (System.IO.File.Exists(destination+"new.jpg"))
                {
                    System.IO.File.Move(destination + "new.jpg", destination + novoprato.PratoId + ".jpg");
                }


                EnviarEmailAcaoFavorito(rest, adicionarPratoDoDium);
                return RedirectToAction(nameof(Index));
            }

            ViewData["PratoId"] = new SelectList(_context.PratoDoDia, "PratoId", "Descricao", adicionarPratoDoDium.PratoId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "RestauranteId", "RestauranteId", adicionarPratoDoDium.RestauranteId);

            return View(adicionarPratoDoDium);
        }


        /// <summary>
        /// Constroi corpos dos emails para os clientes que têm prato e/ou restaurante marcado como favorito.
        /// Invoca métodos responsáveis por enviar emails.
        /// </summary>
        /// <param name="restaurante">Restaurante que insere novo prato.</param>
        /// <param name="adicionarPrato">AdicionarPrato referente ao novo prato.</param>
        [NonAction]
        public void EnviarEmailAcaoFavorito(Restaurante restaurante, AdicionarPratoDoDium adicionarPrato)
        {
            Settings settings = new Settings();
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);

            Utilizador utilizadorRestaurante = new Utilizador();
            utilizadorRestaurante = _context.Utilizadors.FirstOrDefault(x => x.Username == restaurante.RestauranteId);
            PratoDoDium prato = new PratoDoDium();
            prato = _context.PratoDoDia.FirstOrDefault(x => x.PratoId == adicionarPrato.PratoId);

            List<Utilizador> utilizadoresPrato = new List<Utilizador>();
            List<Utilizador> utilizadoresRestaurante = new List<Utilizador>();

            // Percorrer todos clientes com prato inserido como favorito, para enviar email a cada um deles.
            foreach (var favoritoPrato in _context.FavoritarPratoDoDia.Where(x => x.PratoId == adicionarPrato.PratoId))
            {
                Utilizador utilizadorEncontrado = new Utilizador();
                utilizadorEncontrado = _context.Utilizadors.FirstOrDefault(x => x.Username == favoritoPrato.ClienteId);
                utilizadoresPrato.Add(utilizadorEncontrado);

                string body = "O prato " + prato.Nome + ", seu favorito, do restaurante " + utilizadorRestaurante.Nome +
                    " foi disponibilizado como prato do dia. \n" + "Pode-o consultar em: " + settings.WebHostPath + 
                    "adicionarpratododium/detalhes?pratoID=" + prato.PratoId;

                generalHelpers.EnviarEmail(utilizadorEncontrado.Email, "Restaurante favorito adicionou novo prato do dia.", body);
            }

            // Percorrer todos clientes com restaurante favorito que insere prato, para enviar email a cada um deles.
            foreach (var favoritoRestaurante in _context.FavoritarRestaurantes.Where(x => x.RestauranteId == utilizadorRestaurante.Username))
            {
                if(_context.FavoritarPratoDoDia.FirstOrDefault(x => x.ClienteId != favoritoRestaurante.ClienteId && x.PratoId == prato.PratoId) == null)
                {
                    Utilizador utilizadorEncontrado = new Utilizador();
                    utilizadorEncontrado = _context.Utilizadors.FirstOrDefault(x => x.Username == favoritoRestaurante.ClienteId);
                    utilizadoresPrato.Add(utilizadorEncontrado);

                    string body = "O restaurante " + utilizadorRestaurante.Nome + ", seu favorito, adicionou como prato de dia " + 
                        prato.Nome + "\n" + "Pode-o consultar em: " + "https://localhost:44383/" +
                        "adicionarpratododium/detalhes?pratoID=" + prato.PratoId;

                    generalHelpers.EnviarEmail(utilizadorEncontrado.Email, "Restaurante favorito adicionou novo prato do dia.", body);
                }
            }
        }


        /// <summary>
        /// Método GET para apresentar informação relativas aos pratos ativos de um restaurante.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "restaurante")]
        public async Task<ActionResult> Index()
        {
            var foodFinder_WebAppContext = _context.AdicionarPratoDoDia.Where(a => a.Ativado == true && a.DataPrato.Date == DateTime.Today ).Include(a => a.Prato).Include(a => a.Restaurante).Where(a=>a.Restaurante.RestauranteId == HttpContext.Session.GetString("user"));

            List<bool> lista = new List<bool>();

            foreach(var y in foodFinder_WebAppContext)
            {
                if (y.Destacado == true)
                {
                    lista.Add(true);
                }
                else
                {
                    lista.Add(false);
                }

                ViewBag.Destacados = lista;

            }

            return View(await foodFinder_WebAppContext.ToListAsync());
        }


        /// <summary>
        /// Organiza dados sobre um prato identificado pelo ID passado como argumento e invoca view
        /// responsável pela apresentação das suas informações.
        /// </summary>
        /// <param name="pratoID">ID do prato a apresentar.</param>
        /// <returns></returns>
        public ActionResult Detalhes(long pratoID)
        {
            // Verifica se o utilizador com sessão aberta já adicionou prato como favorito ou não 
            // para apresentar o botão adequado.

            FavoritarPratoDoDium f = _context.FavoritarPratoDoDia.SingleOrDefault(x => x.PratoId == pratoID && x.ClienteId == HttpContext.Session.GetString("user"));

            if (HttpContext.Session.GetString("user") != null && string.Compare(HttpContext.Session.GetString("tipo"), "cliente") == 0)
            {
                if (f == null)
                {
                    ViewBag.Prato = false;
                }
                else
                {
                    ViewBag.Prato = true;
                }
            }

            // Identifica o prato especificado pelo ID passado como argumento.
            List<PratoDoDium> listaPratos = new List<PratoDoDium>();
            listaPratos.Add(_context.PratoDoDia.FirstOrDefault(x => x.PratoId == pratoID));

            // Invoca método para guardar todas as relações de prato e informações em outras tabelas. 
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);
            PratoDetalhesViewModel viewModel = new PratoDetalhesViewModel();
            viewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirPratoDoDia(listaPratos);


            if (viewModel != null)
            {           
                    return View(viewModel);
            }

            Console.WriteLine("Prato com ID especificado não encontrado.");

            return View("Problema");
        }


        /// <summary>
        /// Método GET para apresentar lista dos pratos com as suas respetivas informações.
        /// </summary>
        /// <returns></returns>
        public IActionResult ListaPratoItemsDetalhes()
        {
            return View();
        }


        /// <summary>
        /// Permite marcar um prato, identificado pelo ID no argumento, como favorito
        /// do utilizador com sessão iniciada.
        /// </summary>
        /// <param name="IdPrato">ID do prato a adicionar aos favoritos.</param>
        /// <returns></returns>
        /// 
        [LogActionFilter(aut = "cliente")]
        public async Task<IActionResult> AdicionarFavorito(string IdPrato)
        {
            // Identificar cliente com sessão iniciada e que adiciona prato aos seus favoritos.
            Cliente u = _context.Clientes.SingleOrDefault(u => u.ClienteId == HttpContext.Session.GetString("user"));
            
            // Identifica prato a adicionar a favoritos.
            // TODO: Verificar se é necessária esta conversão.
            long l1 = (long)Convert.ToDouble(IdPrato);
            PratoDoDium r = _context.PratoDoDia.SingleOrDefault(u => u.PratoId == l1);

            // Armazenar nova entrada na tabela FavoritarPratoDoDia na base de dados.

            FavoritarPratoDoDium f = new FavoritarPratoDoDium();

            f.Cliente = u;
            f.ClienteId = u.ClienteId;
            f.Prato = r;
            f.PratoId = r.PratoId;
            _context.Add(f);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detalhes", "AdicionarPratoDoDium", new { pratoId = (long)Convert.ToDouble(IdPrato) });
        }

        /// <summary>
        /// Permite retirar um prato, identificado pelo ID no argumento, dos favoritos
        /// do utilizador com sessão iniciada.
        /// </summary>
        /// <param name="IdPrato">ID do prato a retirar dos favoritos.</param>
        /// <returns></returns>
        /// 
        [LogActionFilter(aut = "cliente")]
        public async Task<IActionResult> RemoverFavorito(string IdPrato)
        {
            // Identificar entrada na BD referente ao cliente com sessão iniciada e ao prato a retirar dos favoritos.
            FavoritarPratoDoDium u = _context.FavoritarPratoDoDia.SingleOrDefault(u => u.ClienteId == HttpContext.Session.GetString("user") && u.PratoId == (long)Convert.ToDouble(IdPrato));

            // Remover entrada na BD.
            _context.FavoritarPratoDoDia.Remove(u);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detalhes", "AdicionarPratoDoDium", new { pratoId = (long)Convert.ToDouble(IdPrato) });     
        }


        public async Task<IActionResult> Delete (long? id, string? data)
        {
            if (id == null)
            {
                return NotFound();
            }

            DateTime auxdata;

            DateTime.TryParse(data, out auxdata);
            //DateTime auxdata = DateTime.ParseExact(data, "MM / dd / yyyy HH: mm:ss", CultureInfo.InvariantCulture);

            var adicionarPratoDoDium = await _context.AdicionarPratoDoDia
                .Include(a => a.Prato)
                .Include(a => a.Restaurante)
                .FirstOrDefaultAsync(m => m.PratoId == id && m.Restaurante.RestauranteId == HttpContext.Session.GetString("user") && m.DataPrato.Date == auxdata.Date);

            if (adicionarPratoDoDium == null)
            {
                return NotFound();
            }

            adicionarPratoDoDium.Ativado = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdicionarPratoDoDium");
        }
    }
}
