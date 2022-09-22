using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using FoodFinder_WebApp.ViewModels;
using FoodFinder_WebApp.Helpers;

namespace FoodFinder_WebApp.Controllers
{
    public class PratoDoDiumController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;
        private readonly IHostEnvironment _he;

        public PratoDoDiumController(FoodFinder_WebAppContext context, IHostEnvironment e)
        {
            _context = context;
            _he = e;

        }


        public async Task<IActionResult> Index()
        {
            var listaHistorico = from l in _context.AdicionarPratoDoDia where l.Restaurante.RestauranteId == HttpContext.Session.GetString("user") && (l.Ativado == false || l.DataPrato.Date != DateTime.Today) select l.Prato;
            var result = listaHistorico.Distinct();
           
            return View(await result.ToListAsync());
        }


        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> Create()
        {   
            return View(); 
        }


        [LogActionFilter(aut = "restaurante")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Tipo")] PratoDoDium pratoDoDium, IFormFile file)
        {
            Settings settings = new Settings();

            if (ModelState.IsValid)
            {
                TempData["NovoPrato"] = JsonConvert.SerializeObject(pratoDoDium);

                string destination = Path.Combine(_he.ContentRootPath, settings.ImagensPratosPathWWW, Path.GetFileName("new.jpg"));
                FileStream fs = new FileStream(destination, FileMode.Create);
                file.CopyTo(fs);
                fs.Close();
              
                return RedirectToAction("Create", "AdicionarPratoDoDium");
            }
            return View(pratoDoDium);
        }


        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> Usar(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pratoDoDium = await _context.PratoDoDia.FindAsync(id);
            if (pratoDoDium == null)
            {
                return NotFound();
            }
            return View(pratoDoDium);
        }



        [LogActionFilter(aut = "restaurante")]
        public async Task<IActionResult> Usar2(long id)
        {
            var Prato = _context.PratoDoDia.First(x=>x.PratoId == id);

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(pratoDoDium);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PratoDoDiumExists(Prato.PratoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                TempData["NovoPrato"] = JsonConvert.SerializeObject(Prato);

                return RedirectToAction("Create", "AdicionarPratoDoDium");
            }
            return View(Prato);
        }

      
        private bool PratoDoDiumExists(long id)
        {
            return _context.PratoDoDia.Any(e => e.PratoId == id);
        }
    }
}
