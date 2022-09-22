using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class LocalizacaoController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public LocalizacaoController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }

        public ActionResult Logon()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logon(IFormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["morada"]) == true)
            {
                ModelState.AddModelError("morada", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["codigoPostal"]) == true)
            {
                ModelState.AddModelError("codigoPostal", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["localidade"]) == true)
            {
                ModelState.AddModelError("localidade", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["latitude"]) == true)
            {
                ModelState.AddModelError("latitude", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["longitude"]) == true)
            {
                ModelState.AddModelError("longitude", "MandatoryField");
            }


            if (ModelState.IsValid)
            {
                Localizacao novaLocalizacao = new Localizacao();

                novaLocalizacao.CodigoPostal = collection["codigoPostal"];
                novaLocalizacao.Morada = collection["morada"];
                novaLocalizacao.Localidade = collection["localidade"];
                novaLocalizacao.GpsLatitude = double.Parse(collection["latitude"]);
                novaLocalizacao.GpsLongitude = double.Parse(collection["longitude"]);

                TempData["localizacaoLogon"] = JsonConvert.SerializeObject(novaLocalizacao);
                return RedirectToAction("SaveRestauranteDB", "Restaurante");
            }


            return View();
        }

    }
}
