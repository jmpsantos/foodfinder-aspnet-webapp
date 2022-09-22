using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Helpers;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;
        private readonly IHostEnvironment _he;


        
        public RestauranteController(FoodFinder_WebAppContext context, IHostEnvironment e)
        {
            _context = context;
            _he = e;
        }


        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logon(IFormCollection collection, IFormFile FotoRestaurante)
        {
            Settings settings = new Settings();

            if (string.IsNullOrEmpty(collection["contactoEmail"]) == true)
            {
                ModelState.AddModelError("contactoEmail", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["contactoTelefone"]) == true)
            {
                ModelState.AddModelError("contactoTelefone", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["horarioFuncionamento"]) == true)
            {
                ModelState.AddModelError("horarioFuncionamento", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["diaDeDescanso"]) == true)
            {
                ModelState.AddModelError("diaDeDescanso", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["descricao"]) == true)
            {
                ModelState.AddModelError("descricao", "MandatoryField");
            }
            if (!collection.Files.Any())
            {
                ModelState.AddModelError("fotoRestaurante", "MandatoryField");
            }
       

            if (ModelState.IsValid)
            {
                Restaurante novoRestaurante = new Restaurante();

                novoRestaurante.ContactoEmail = collection["contactoEmail"];
                novoRestaurante.ContactoTelefone = long.Parse(collection["contactoTelefone"]);
                novoRestaurante.HorarioFuncionamento = collection["horarioFuncionamento"];
                novoRestaurante.DiaDeDescanso = collection["diaDeDescanso"];
                novoRestaurante.Rating = -1;
                novoRestaurante.Descricao = collection["descricao"];
                novoRestaurante.Localizacao = null;

                if (string.Compare(collection["tipoServico"], "0") == 0) // É cliente.
                {
                    novoRestaurante.TipoDeServico = "Local";
                }
                else if (string.Compare(collection["tipoUtilizador"], "1") == 0) // É restaurante.
                {
                    novoRestaurante.TipoDeServico = "Take-Away";
                }
                else if (string.Compare(collection["tipoUtilizador"], "2") == 0) // É restaurante.
                {
                    novoRestaurante.TipoDeServico = "Entrega";
                }

               
                // Guardar imagem no sistema de ficheiros.
                string destination = Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW, Path.GetFileName(TempData["utilizadorUsername"].ToString() + "_000" + ".jpg"));
                FileStream fs = new FileStream(destination, FileMode.Create);
                FotoRestaurante.CopyTo(fs);
                fs.Close();


                TempData["restauranteLogon"] = JsonConvert.SerializeObject(novoRestaurante);
                return RedirectToAction("Logon", "Localizacao");
            }

            return View();
        }


        public async Task<ActionResult> SaveRestauranteDBAsync()
        {
            Restaurante novoRestaurante = new Restaurante();
            Localizacao novaLocalizacao = new Localizacao();
            Utilizador novoUtilizador = new Utilizador();

            novoUtilizador = JsonConvert.DeserializeObject<Utilizador>((string)TempData["utilizadorLogon"]);

            novoRestaurante = JsonConvert.DeserializeObject<Restaurante>((string)TempData["restauranteLogon"]);

            novaLocalizacao = JsonConvert.DeserializeObject<Localizacao>((string)TempData["localizacaoLogon"]);

            novoUtilizador.Restaurante = novoRestaurante;
            novoRestaurante.Localizacao = novaLocalizacao;

            _context.Add(novoUtilizador);
            await _context.SaveChangesAsync();

            TempData.Remove("utilizadorLogon");
            TempData.Remove("restauranteLogon");
            TempData.Remove("localizacaoLogon");

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Invoca view responsável por apresentar a informação pessoal do utilizador com sessão iniciada.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut ="restaurante", aut2 = "administrador")]
        public ActionResult InformacaoPessoalRestaurante(string id)
        {
            if (id == null)
            {
                id = HttpContext.Session.GetString("user");
            }
            Restaurante r = _context.Restaurantes.SingleOrDefault(r => r.RestauranteId == id);

            Localizacao l = new Localizacao();

            l = _context.Localizacaos.SingleOrDefault(l => l.LocalizacaoId == r.LocalizacaoId);

            r.Localizacao = l;

            return View(r);
        }

  
        /// <summary>
        /// Invoca view responsável pela edição da informação pessoal do utilizador com sessão iniciada.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [LogActionFilter(aut = "restaurante")]
        public IActionResult Edit(string id)
        {
            Settings settings = new Settings();

            if (id == null)
            {
                id = HttpContext.Session.GetString("user");
            }

            Restaurante r = _context.Restaurantes.SingleOrDefault(r => r.RestauranteId == id);
            Localizacao l = new Localizacao();

            l = _context.Localizacaos.SingleOrDefault(l => l.LocalizacaoId == r.LocalizacaoId);

            ViewData["username"] = id;
            ViewBag.fCount = Directory.GetFiles(Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW), id + "*", SearchOption.TopDirectoryOnly).Length;

            r.Localizacao = l;
            return View(r);
        }

        /// <summary>
        /// Permite armazenar uma imagem passada no argumento.  
        /// </summary>
        /// <param name="fotoRestaurante">Imagem a armazenar.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogActionFilter(aut = "restaurante")]
        public ActionResult UploadImagem(IFormFile fotoRestaurante)
        {
            Settings settings = new Settings();
            if (fotoRestaurante != null)
            {
                int xyz = Directory.GetFiles(Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW), HttpContext.Session.GetString("user") + "*", SearchOption.TopDirectoryOnly).Length;

                string destination2 = Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW, Path.GetFileName(HttpContext.Session.GetString("user") + "_00" + xyz + ".jpg"));
                FileStream fs = new FileStream(destination2, FileMode.Create);
                fotoRestaurante.CopyTo(fs);
                fs.Close();
            }

            return RedirectToAction("Edit","Restaurante");
        }


        /// <summary>
        /// Permite editar as imagens armazenadas.
        /// </summary>
        /// <param name="Imagens">Imagens a editar.</param>
        /// <param name="Button">Ação a executar nas imagens.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogActionFilter(aut = "restaurante")]
        public ActionResult AtualizarImagens(string Imagens, string Button)
        {
            Settings settings = new Settings();
            if (string.Compare(Button, "Destacar") == 0)
            {
                if (string.Compare(Imagens, "0") != 0)
                {
                    string destination = Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW, Path.GetFileName(HttpContext.Session.GetString("user")) + "_00");
                    string aux = Imagens;

                    System.IO.File.Move(destination + "0.jpg", destination + "old.jpg");
                    System.IO.File.Move(destination + Imagens + ".jpg", destination + "0.jpg");
                    System.IO.File.Move(destination + "old.jpg", destination + aux + ".jpg");
                }
                return RedirectToAction("InformacaoPessoalRestaurante", "Restaurante");

            }
            else
            {
                if(string.Compare(Imagens,"0") ==0)
                {
                    ModelState.AddModelError(string.Empty, "Não pode apagar a imagem");
                    return RedirectToAction("Edit","Restaurante");  //nao pode apagar uma foto destacada
                }
                else
                {
                    int xyz = Directory.GetFiles(Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW), HttpContext.Session.GetString("user") + "*", SearchOption.TopDirectoryOnly).Length;
                    int current = Int32.Parse(Imagens);

                    if( System.IO.File.Exists(Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW, Path.GetFileName(HttpContext.Session.GetString("user")) + "_00" + Imagens+".jpg")))
                    {
                        System.IO.File.Delete(Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW, Path.GetFileName(HttpContext.Session.GetString("user")) + "_00" + Imagens + ".jpg"));
                        
                        string destination = Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW, Path.GetFileName(HttpContext.Session.GetString("user")) + "_00");

                        for (int i = current+1; i<xyz;i++)
                        {

                            System.IO.File.Move(destination+ i + ".jpg", destination + (i-1) + ".jpg");
                        }
                    }
                }
            }

            return RedirectToAction("Edit","Restaurante");
        }


        /// <summary>
        /// Armazena as alterações feitas à informação pessoal do restaurante.
        /// </summary>
        /// <param name="id">Id do restaurante a alterar informação.</param>
        /// <param name="restaurante">Informação atualizada.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> Edit( string id, [Bind("RestauranteId,ContactoEmail,ContactoTelefone,HorarioFuncionamento,DiaDeDescanso,TipoDeServico,LocalizacaoId,Rating,Descricao")] Restaurante restaurante)
        {

            if (string.IsNullOrEmpty(restaurante.ContactoEmail) == true)
            {
                ModelState.AddModelError("Email", "MandatoryField");
            }
            if (restaurante.ContactoTelefone < 100000000)
            {
                ModelState.AddModelError("Telefone", "MandatoryField");
            }
            if (string.IsNullOrEmpty(restaurante.HorarioFuncionamento) == true)
            {
                ModelState.AddModelError("Horario de Funcionamento", "MandatoryField");
            }
            if (string.IsNullOrEmpty(restaurante.DiaDeDescanso) == true)
            {
                ModelState.AddModelError("Dia de Descanso", "MandatoryField");
            }
            if (string.IsNullOrEmpty(restaurante.TipoDeServico) == true)
            {
                ModelState.AddModelError("Tipo de Serviço", "MandatoryField");
            }
            if (string.IsNullOrEmpty(restaurante.Descricao) == true)
            {
                ModelState.AddModelError("Descricao", "MandatoryField");
            }
            
         
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestauranteExists(restaurante.RestauranteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("InformacaoPessoalRestaurante","Restaurante");
            }
            
            return View();
        }


        /// <summary>
        /// Invoca view responsável por apresentar o menu área de restaurante. 
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "restaurante")]
        public ActionResult AreaRestaurante()
        {
            return View();
        }

        /// <summary>
        /// Verifica se existe um restaurante com o id passado como argumento.
        /// </summary>
        /// <param name="id">Id para o qual verificar existência de restaurante.</param>
        /// <returns></returns>
        private bool RestauranteExists(string id)
        {
            return _context.Restaurantes.Any(e => e.RestauranteId == id);
        }

        /// <summary>
        /// Marca restaurante com id passado no argumento como favorito do utilizador com sessão iniciada. 
        /// </summary>
        /// <param name="IdRestaurante">Id do restaurante a marcar como favorito.</param>
        /// <returns></returns>
        [LogActionFilter(aut = "cliente")]
        public async Task<IActionResult> AdicionarFavorito(string IdRestaurante)
        {
            Cliente u = _context.Clientes.SingleOrDefault(u => u.ClienteId== HttpContext.Session.GetString("user"));
            Restaurante r = _context.Restaurantes.SingleOrDefault(u => u.RestauranteId == IdRestaurante);

            FavoritarRestaurante f = new FavoritarRestaurante();

            f.Cliente = u;
            f.ClienteId = u.ClienteId;
            f.Restaurante = r;
            f.RestauranteId = r.RestauranteId;   
            _context.Add(f);          
            await _context.SaveChangesAsync();


            return RedirectToAction("ApresentarRestaurante", "Restaurante", new { id = IdRestaurante});
        }

        /// <summary>
        /// Remove restaurante com id passado no argumento dos favoritos do utilizador com sessão iniciada.
        /// </summary>
        /// <param name="IdRestaurante">Id do restaurante a retirar de favorito.</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoverFavorito(string IdRestaurante)
        {
            FavoritarRestaurante u = _context.FavoritarRestaurantes.SingleOrDefault(u => u.ClienteId == HttpContext.Session.GetString("user") && u.RestauranteId == IdRestaurante);
            
            _context.FavoritarRestaurantes.Remove(u);
            await _context.SaveChangesAsync();

            return RedirectToAction("ApresentarRestaurante", "Restaurante", new { id = IdRestaurante });
        }

        /// <summary>
        /// Invoca view responsável por apresentar detalhes de um restaurante,
        /// organizando e passando todos os dados para ela.
        /// </summary>
        /// <param name="id">Id do restaurante a apresentar.</param>
        /// <param name="button">Secção dos dados a apresentar.</param>
        /// <returns></returns>
        public ActionResult ApresentarRestaurante(string id, string button)
        {
            Settings settings = new Settings();

            switch (button)
            {
                case "Pratos do Dia":
                default:
                    ViewData["partialViews"] = "PratoDoDia";
                    break;
                case "Localização & Contactos":
                    ViewData["partialViews"] = "LocalizacaoInfo";
                    break;
                case "Reviews":
                    ViewData["serCliente"] = HttpContext.Session.GetString("tipo");
                    TempData["cliente"] = HttpContext.Session.GetString("user");
                    TempData["restaurante"] = id;
                    ViewData["partialViews"] = "Reviews";
                    break;
            }

            // Fornece vários métodos auxiliares uteis.
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);

            // ViewModel que guardará toda a informação para passar para a View.
            HomeViewModel homeViewModel = new HomeViewModel();


            // Formar a componente PratoDetalhesViewModel do HomeViewModel.

            List<AdicionarPratoDoDium> listaAdicionarPrato = new List<AdicionarPratoDoDium>();
            listaAdicionarPrato = _context.AdicionarPratoDoDia
                .Where(x => x.RestauranteId == id)
                .Where(x => x.Ativado == true && x.DataPrato.Date == DateTime.Now.Date)
                .ToList();

            List<PratoDoDium> listaPrato = new List<PratoDoDium>();

            foreach (var adicionarPrato in listaAdicionarPrato)
            {
                PratoDoDium prato = new PratoDoDium();
                prato = _context.PratoDoDia.FirstOrDefault(x => x.PratoId == adicionarPrato.PratoId);
                
                if (prato != null)
                {
                    listaPrato.Add(prato);
                }
            }

            if (listaPrato != null)
            {
                homeViewModel.PratoDetalhesViewModel = generalHelpers.CriarPratoDetalhesViewModelAPartirPratoDoDia(listaPrato);
            }


            // Formar a componente RestauranteDetalhesViewModel do HomeViewModel.
            List<Restaurante> listaRestaurante = new List<Restaurante>();
            listaRestaurante = _context.Restaurantes.Where(x => x.RestauranteId == id).ToList();

            if (listaRestaurante != null)
            {
                homeViewModel.RestauranteDetalhesViewModel = generalHelpers.CriarRestauranteDetalhesViewModelAPartirRestaurante(listaRestaurante);
            }


            // Necessário passar os paths de todas as imagens do restaurante.

            int numFotos = Directory.GetFiles(Path.Combine(_he.ContentRootPath, settings.ImagensRestaurantePathWWW), id + "*", SearchOption.TopDirectoryOnly).Length;


            for (int i = 1; i < numFotos; i++)
            {
                if (i < 10)
                {
                    homeViewModel.RestauranteDetalhesViewModel.ImagensPath.Add(settings.ImagensRestaurantesPath + id + "_00" + i.ToString() + ".jpg");
                }
                else
                {
                    homeViewModel.RestauranteDetalhesViewModel.ImagensPath.Add(settings.ImagensRestaurantesPath + id + "_0" + i.ToString() + ".jpg");
                }
            }


            // Verificar se utilizador cliente (caso o seja) tem restaurante como favorito ou não.
            // Determinará botão a apresentar.

            FavoritarRestaurante f = _context.FavoritarRestaurantes.SingleOrDefault(x => x.RestauranteId == id && x.ClienteId == HttpContext.Session.GetString("user"));

            if (HttpContext.Session.GetString("user") != null && string.Compare(HttpContext.Session.GetString("tipo"), "cliente") == 0)
            {
                if (f == null)
                {
                    ViewBag.restaurante = false;
                }
                else
                {
                    ViewBag.restaurante = true;
                }
            }

            return View(homeViewModel);
        }
    }
   
}
