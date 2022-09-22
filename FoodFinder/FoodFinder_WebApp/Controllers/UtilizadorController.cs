using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Helpers;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class UtilizadorController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;


        public UtilizadorController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Responsável por verificar se o token armazenado na BD para 
        /// confirmação de registo é igual ao token passado como argumento.
        /// </summary>
        /// <param name="email">Identificar utilizador para o qual confirmar registo.</param>
        /// <param name="token">Token a ser confirmado se igual ao armazenado na BD.</param>
        /// <returns></returns>
        public ActionResult ConfirmarEmail(string email, string token)
        {
            Utilizador utilizadorCliente = _context.Utilizadors.FirstOrDefault(x => x.Email == email);

            Cliente cliente = _context.Clientes.FirstOrDefault(x => x.ClienteId == utilizadorCliente.Username);

            // Caso o token passado como argumento seja igual ao armazenado na BD.
            if (string.Compare(token, cliente.TokenConfirmacaoRegisto) == 0)
            {
                // Confirma-se o registo do utilizador.
                utilizadorCliente.RegistoConfirmado = true;

                _context.SaveChanges();

                RedirectToAction("Login", "Utilizador");
            }

            return View("Denied");
        }

        /// <summary>
        /// Invoca view responsável pelo formulário de registo.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logon()
        {
            return View();
        }

        /// <summary>
        /// Prepara dados passados no argumento para a criação de um novo utilizador.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logon(IFormCollection collection)
        {
            GeneralHelpers generalHelper = new GeneralHelpers(_context);

            if (string.IsNullOrEmpty(collection["username"]) == true)
            {
                ModelState.AddModelError("username", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["nome"]) == true)
            {
                ModelState.AddModelError("nome", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["email"]) == true)
            {
                ModelState.AddModelError("email", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["password"]) == true)
            {
                ModelState.AddModelError("password", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["confirmarPassword"]) == true)
            {
                ModelState.AddModelError("confirmarPassword", "MandatoryField");
            }

            // Verificar se palavra passes coíncidem.
            if (string.Compare(collection["password"], collection["confirmarPassword"]) != 0)
            {
                ModelState.AddModelError(string.Empty, "As Passwords Sao Diferentes");
            }

            // Verificar se existem dados na base de dados.
            if (_context.Utilizadors.Any())
            {
                // Verificar se o username já se encontra utilizado.
                foreach (var utilizador in _context.Utilizadors)
                {
                    if (string.Compare(utilizador.Username, collection["username"]) == 0)
                    {
                        ModelState.AddModelError("username", "Este username já está a ser utilizado");
                    }
                }
            }

            Utilizador novoUtilizador = new Utilizador();

            novoUtilizador.Username = collection["username"];
            novoUtilizador.Email = collection["email"];
            novoUtilizador.Password = GetShaData(collection["password"]);
            novoUtilizador.Nome = collection["nome"];

            novoUtilizador.RegistoConfirmado = false;

            novoUtilizador.BloqueadoId = null;

            if (ModelState.IsValid)
            {
                if (string.Compare(collection["tipoUtilizador"], "0") == 0) // É cliente.
                {
                    Cliente novoCliente = new Cliente();
                    novoUtilizador.Restaurante = null;
                    novoUtilizador.Administrador = null;


                    novoCliente.TokenConfirmacaoRegisto = Guid.NewGuid().ToString();

                    string subject = "FoodFinder: Confirmação de registo.";

                    string body = "Por favor, confirme o seu registo em FoodFinder.\n" +
                            "https://localhost:44383/" + "utilizador/confirmaremail?" +
                            "email=" + novoUtilizador.Email + "&token=" + novoCliente.TokenConfirmacaoRegisto;


                    generalHelper.EnviarEmail(novoUtilizador.Email, subject, body);

                    TempData["utilizadorLogon"] = JsonConvert.SerializeObject(novoUtilizador);
                    TempData["clienteLogon"] = JsonConvert.SerializeObject(novoCliente);
                    return RedirectToAction("SaveClientDB", "Cliente");
                }
                else if (string.Compare(collection["tipoUtilizador"], "1") == 0) // É restaurante.
                {
                    TempData["utilizadorLogon"] = JsonConvert.SerializeObject(novoUtilizador);
                    TempData["utilizadorUsername"] = novoUtilizador.Username;
                    return RedirectToAction("Logon", "Restaurante");
                }
            }
            else
            {
                return View(novoUtilizador);
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Invoca view responsável pelo login.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Verifica se dados do formulário do login estão corretos
        /// para ser iniciada sessão.
        /// </summary>
        /// <param name="username">username do utilizador a tentar fazer login.</param>
        /// <param name="password">palavra-pass do utilizador a tentar fazer login.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            //ALTERAR PASSWORDS NA DB**************************************************
            //foreach (var utilizador in _context.Utilizadors)
            //{
            //    utilizador.Password = GetShaData(utilizador.Password);
            //    _context.Update(utilizador);
            //}
            //_context.SaveChanges();
            //ALTERAR PASSWORDS NA DB ***********************************************

            if (ModelState.IsValid)
            {
                // Verificar se existe utilizador com essa password e com esse username.
                Utilizador u = _context.Utilizadors.FirstOrDefault(u => u.Username == username && u.Password == GetShaData(password));

                // Caso não exista.
                if (u == null)
                {
                    ModelState.AddModelError(string.Empty, "Password ou username incorretos");
                    return View();
                }

                // Caso registo não se encontre confirmado.
                if (u.RegistoConfirmado == false)
                {
                    return RedirectToAction("Denied", "Utilizador");
                }

                // Verificar se utilizador encontrado está bloqueado.
                Bloqueado bloqueio = _context.Bloqueados.FirstOrDefault(x => x.Id == u.BloqueadoId);

                if (bloqueio != null)
                {
                    return RedirectToAction("MensagemBloqueio", "Bloqueado", new { bloqueioID = bloqueio.Id });
                }

                // Iniciar a sessão para utilizador.

                Cliente c = new Cliente();
                Administrador a = new Administrador();
                Restaurante r = new Restaurante();

                c = _context.Clientes.SingleOrDefault(c => c.ClienteId == username);
                u.Cliente = c;
                r = _context.Restaurantes.SingleOrDefault(r => r.RestauranteId == username);
                u.Restaurante = r;
                a = _context.Administradors.SingleOrDefault(a => a.AdministradorId == username);
                u.Administrador = a;


                if (u == null)
                {
                    ModelState.AddModelError("username", "O username ou a palavra-passe inserida não é válida.");
                }
                else
                {
                    HttpContext.Session.SetString("user", username);
                }
                if (u.Administrador != null)
                {
                    HttpContext.Session.SetString("tipo", "administrador");
                }
                if (u.Restaurante != null)
                {
                    HttpContext.Session.SetString("tipo", "restaurante");
                }
                if (u.Cliente != null)
                {
                    HttpContext.Session.SetString("tipo", "cliente");
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// Fecha a sessão do utilizador.
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout ()
        {
            HttpContext.Response.Cookies.Delete(".FoodFinderSession");
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Invoca view para apresentar informação pessoal do utilizador com sessão iniciada.
        /// </summary>
        /// <returns></returns>
        public ActionResult InformacaoPessoalUtilizador(string id)
        {
            if (id == null)
            {
                id = HttpContext.Session.GetString("user");
            }

            Utilizador r = _context.Utilizadors.SingleOrDefault(r => r.Username == id);
           
            return View(r);
        }


        /// <summary>
        /// Invoca view para apresentar lista de restaurantes com registo por confirmar, organizando
        /// e passando essa lista para a view.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public ActionResult ApresentarRestaurantesPorAceitarRegisto()
        {  
            List<Utilizador> listaUtilizadoresRestaurantes = new List<Utilizador>();

            foreach (var restaurante in _context.Restaurantes)
            {
                Utilizador restauranteEncontrado = _context.Utilizadors.First(x => x.Username == restaurante.RestauranteId);
                listaUtilizadoresRestaurantes.Add(restauranteEncontrado);
            }

            List<Utilizador> listaUtilizadoresRestaurantesPorRegistar = listaUtilizadoresRestaurantes.Where(x => x.RegistoConfirmado == false).ToList();


            return View(listaUtilizadoresRestaurantesPorRegistar);
        }

        /// <summary>
        /// Confirma o registo de um utilizador identificado pelo id passado no argumento.
        /// </summary>
        /// <param name="id">Id do utilizador a confirmar registo.</param>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public ActionResult ConfirmarRegisto(string id)
        {
            Utilizador utilizadorEncontrado = _context.Utilizadors.First(x => x.Username == id);

            utilizadorEncontrado.RegistoConfirmado = true;

            _context.SaveChanges();

            return RedirectToAction("ApresentarRestaurantesPorAceitarRegisto", "Utilizador");
        }


        /// <summary>
        /// Nega o registo de um utilizador identificado pelo id passado no argumento.
        /// </summary>
        /// <param name="id">Id do utilizador a negar registo.</param>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public ActionResult NegarRegisto(string id)
        {
            GeneralHelpers generalHelpers = new GeneralHelpers(_context);

            Utilizador utilizadorANegar = _context.Utilizadors.FirstOrDefault(x => x.Username == id);

            // Envia email a informar utilizador de negação do registo.

            string body = "Informamos que o pedido de registo do seu restaurante no FoodFinder" +
                    "não foi aceite.";
            string subject = "FoodFinder: Pedido de Registo.";

            generalHelpers.EnviarEmail(utilizadorANegar.Email, subject, body);


            // Eliminar restaurante (e utilizador) da base de dados.
            _context.Restaurantes.Remove(_context.Restaurantes.First(x => x.RestauranteId == id));

            _context.Utilizadors.Remove(_context.Utilizadors.First(x => x.Username == id));

            _context.SaveChanges();

            return RedirectToAction("ApresentarRestaurantesPorAceitarRegisto", "Utilizador");
        }


        


        /// <summary>
        /// Invoca view responsável por apresentar utilizador, passando essa lista para lá.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public ActionResult ListarUtilizadores()
        {
            return View(_context.Utilizadors.ToList());
        }

        /// <summary>
        /// Determinar através dos argumentos que filtro aplicar na lista
        /// de utilizador.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="textoPesquisa"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogActionFilter(aut = "administrador")]
        public ActionResult<List<Utilizador>> ListarUtilizadores(string button, string textoPesquisa)
        {
            List<Utilizador> listaUtilizadores = new List<Utilizador>();
       
            string utilizadorID = HttpContext.Session.GetString("user");
            ViewData["utilizadorID"] = utilizadorID;

            if (button != null)
            {
                listaUtilizadores = RecolherDadosBDAtravesTipoDeUtilizador(button);
            }
            else if (textoPesquisa != null)
            {
                listaUtilizadores = RecolherDadosBDAtravesUtilizadorID(textoPesquisa);
            }
            

            if (listaUtilizadores != null)
            {
                return View(listaUtilizadores);
            }

            return View(_context.Utilizadors.ToList());
        }

        /// <summary>
        /// Filtra lista de utilizadores através do tipo de utilizador.
        /// </summary>
        /// <param name="button">Tipo de utilizador a filtra.</param>
        /// <returns></returns>
        public List<Utilizador> RecolherDadosBDAtravesTipoDeUtilizador(string button)
        {
            List<Utilizador> listaUtilizadores = new List<Utilizador>();

            switch (button)
            {
                case "todosResultados":
                    listaUtilizadores = _context.Utilizadors.ToList();
                    break;

                case "restaurantes":

                    foreach (var restaurante in _context.Restaurantes)
                    {
                        Utilizador restauranteEncontrado = _context.Utilizadors.First(x => x.Username == restaurante.RestauranteId);
                        listaUtilizadores.Add(restauranteEncontrado);
                    }

                    break;

                case "clientes":

                    foreach (var cliente in _context.Clientes)
                    {
                        Utilizador clienteEncontrado = _context.Utilizadors.First(x => x.Username == cliente.ClienteId);
                        listaUtilizadores.Add(clienteEncontrado);
                    }

                    break;

                case "administradores":

                    foreach (var administrador in _context.Administradors)
                    {
                        Utilizador administradorEncontrado = _context.Utilizadors.First(x => x.Username == administrador.AdministradorId);
                        listaUtilizadores.Add(administradorEncontrado);
                    }

                    break;
            }

            return listaUtilizadores;
        }

        /// <summary>
        /// Filtra lista de utilizadores através do id de utilizador.
        /// </summary>
        /// <param name="utilizadorID">ID do utilizador a filtrar.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public List<Utilizador> RecolherDadosBDAtravesUtilizadorID(string utilizadorID)
        {
            List<Utilizador> listaUtilizadores = new List<Utilizador>();

            if (utilizadorID != null)
            {
                listaUtilizadores = _context.Utilizadors.Where(x => x.Username == utilizadorID).ToList();
                return listaUtilizadores;
            }

            return null;
        }

        /// <summary>
        /// Invoca view responsável por formulário para criar novo utilizador.
        /// </summary>
        /// <returns></returns>
        public ActionResult CriarUtilizador()
        {
            return View();
        }

        /// <summary>
        /// Trata os dados passados como argumento e guarda utilizador na base de dados.
        /// </summary>
        /// <param name="collection">Dados do utilizador a criar.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CriarUtilizador(IFormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["username"]) == true)
            {
                ModelState.AddModelError("username", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["nome"]) == true)
            {
                ModelState.AddModelError("nome", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["email"]) == true)
            {
                ModelState.AddModelError("email", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["password"]) == true)
            {
                ModelState.AddModelError("password", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["confirmarPassword"]) == true)
            {
                ModelState.AddModelError("confirmarPassword", "MandatoryField");
            }


            // Verificar se palavra passes coíncidem.
            if (string.Compare(collection["password"], collection["confirmarPassword"]) != 0)
            {
                ModelState.AddModelError("confirmarPassword", "AsPasswordsSaoDiferentes");
            }

            // Verificar se existem dados na base de dados.
            if (_context.Utilizadors.Any())
            {
                // Verificar se o username já se encontra utilizado.
                foreach (var utilizador in _context.Utilizadors)
                {
                    if (string.Compare(utilizador.Username, collection["username"]) == 0)
                    {
                        ModelState.AddModelError("username", "Este username já está a ser utilizado");
                    }
                }
            }

            
                Utilizador novoUtilizador = new Utilizador();

                novoUtilizador.Username = collection["username"];
                novoUtilizador.Email = collection["email"];
                novoUtilizador.Password = GetShaData(collection["password"]);
                novoUtilizador.Nome = collection["nome"];

                //TEMPORARIO: mudar para false.
                novoUtilizador.RegistoConfirmado = true;

                novoUtilizador.BloqueadoId = null;

                string tipoUtilizador = TempData["tipoUtilizador"].ToString();
                TempData.Remove("tipoUtilizador");
            if (ModelState.IsValid)
            {

                switch (tipoUtilizador)
                {
                    case "Cliente":
                        break;
                    case "Restaurante":
                        break;
                    case "Administrador":
                        Administrador novoAdministrador = new Administrador();
                        novoUtilizador.Administrador = novoAdministrador;
                        novoUtilizador.Cliente = null;
                        novoUtilizador.Restaurante = null;
                        break;
                }

                _context.Add(novoUtilizador);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("AdicionarAdministrador", "Administrador", novoUtilizador);
        }

        /// <summary>
        /// Converte uma string passada no argumento numa hash.
        /// </summary>
        /// <param name="Data">String a converteer em hash.</param>
        /// <returns></returns>
        public static string GetShaData(string Data)
        {
            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
            byte[] sha256Bytes = System.Text.Encoding.Default.GetBytes(Data);
            byte[] cryString = sha256.ComputeHash(sha256Bytes);
            string sha256Str = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X");

            }

            return sha256Str;
        }



    }
}
