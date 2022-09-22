using FoodFinder_WebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;

namespace FoodFinder_WebApp.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public AdministradorController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Método GET que pede a apresentação da view correspondente à área de administrador.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public ActionResult AreaAdministrador()
        {
            return View();
        }


        /// <summary>
        /// Método GET que pede a apresentação da view correspondente ao formulário para criação de novo administrador.
        /// </summary>
        /// <returns></returns>
        [LogActionFilter(aut = "administrador")]
        public ActionResult AdicionarAdministrador()
        {
            TempData["tipoUtilizador"] = "Administrador";
            return View();
        }

    }
}
