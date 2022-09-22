using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class ClienteController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public ClienteController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Método GET que invoca a view responsável por apresentar o menu
        /// da área de cliente.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "cliente")]
        public ActionResult AreaCliente()
        {
            return View();
        }
            




        /// <summary>
        /// Armazena na base de dados um utilizador cliente. Para isso espera por um objeto cliente e outro
        /// utilizador passados através de TempData.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SaveClientDB()
        {
            Utilizador novoUtilizador = new Utilizador();
            Cliente novoCliente = new Cliente();

            novoUtilizador = JsonConvert.DeserializeObject<Utilizador>((string)TempData["utilizadorLogon"]);
            novoCliente = JsonConvert.DeserializeObject<Cliente>((string)TempData["clienteLogon"]);

            novoUtilizador.Cliente = novoCliente;

            _context.Add(novoUtilizador);
            await _context.SaveChangesAsync();

            TempData.Remove("utilizadorLogon"); 
            TempData.Remove("clienteLogon");

            return RedirectToAction("Index", "Home");
        }

    }
}
