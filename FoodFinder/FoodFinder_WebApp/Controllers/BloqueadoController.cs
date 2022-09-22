using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class BloqueadoController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public BloqueadoController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método GET que invoca a view responsável pela apresentação do motivo de bloqueio.
        /// </summary>
        /// <param name="bloqueioID"></param>
        /// <returns></returns>
        public IActionResult MensagemBloqueio(long bloqueioID)
        {
            Bloqueado bloqueio = _context.Bloqueados.First(x => x.Id == bloqueioID);
            return View(bloqueio);
        }


        /// <summary>
        /// Método responsável por invocar view para bloquear um utilizador.
        /// </summary>
        /// <param name="utilizadorUsername">Username do utilizador a bloquear.</param>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public IActionResult Bloquear(string utilizadorUsername)
        {
            ViewData["utilizadorID"] = utilizadorUsername;
            TempData["userID"] = utilizadorUsername;
            return View();
        }

        /// <summary>
        /// Marca o utilizador identificado pelo ID dos argumentos como bloqueado e
        /// adiciona o motivo também também passado como argumento.
        /// </summary>
        /// <param name="textoMotivo">Motivo do bloqueio.</param>
        /// <param name="id">ID do utilizador a bloquear.</param>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public IActionResult ConfirmarBloqueio(string textoMotivo, string id)
        {
            // Identifica o utilizador a bloquear.
            Utilizador utilizadorEncontrado = _context.Utilizadors.First(x => x.Username == id);

            // Cria nova entrada na tabela Bloqueado da BD e armazena alterações.
            Bloqueado bloqueado = new Bloqueado();
            bloqueado.Motivo = textoMotivo;

            utilizadorEncontrado.BloqueadoId = bloqueado.Id;
            utilizadorEncontrado.Bloqueado = bloqueado;

            _context.SaveChanges();

            return RedirectToAction("ListarUtilizadores", "Utilizador");
        }

        /// <summary>
        /// Marca um utilizador identificado pelo ID passado como argumento como
        /// não bloqueando, eliminando a sua respetiva entrada de Bloqueado da BD.
        /// </summary>
        /// <param name="id">ID do utilizador a desbloquear.</param>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public IActionResult Desbloquear(string id)
        {
            // Identificar utilizador a desbloquear.
            Utilizador utilizadorEncontrado = _context.Utilizadors.First(x => x.Username == id);


            // Eliminar sua relação com a entrada na tabela Bloqueado.
            utilizadorEncontrado.BloqueadoId = null;
            utilizadorEncontrado.Bloqueado = null;

            _context.SaveChanges();

            return RedirectToAction("ListarUtilizadores", "Utilizador");
        }
    }
}
