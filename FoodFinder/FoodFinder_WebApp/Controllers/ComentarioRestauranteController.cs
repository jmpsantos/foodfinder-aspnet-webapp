using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Controllers
{
    public class ComentarioRestauranteController : Controller
    {
        private readonly FoodFinder_WebAppContext _context;

        public ComentarioRestauranteController(FoodFinder_WebAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Determina comentários associados a utilizador com sessão aberta e invoca view responsável
        /// pela sua listagem.
        /// </summary>
        /// <returns></returns>
        public IActionResult ListaComentariosNoRestaurante()
        {
            // Identifica utilizador restaurante com sessão aberta.
            string id = HttpContext.Session.GetString("user");

            // Identifica comentários associados a esse utilizador restaurante.
            List<ComentarioRestaurante> comentariosRestauranteIdentificado = _context.ComentarioRestaurantes.Where(x => x.RestauranteId == id).OrderBy(x => x.DataComentario).ToList();

            return View(comentariosRestauranteIdentificado);
        }


        /// <summary>
        /// Armazena um comentário na base de dados, associando-o a um cliente e restaurante.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [LogActionFilter(aut = "cliente")]
        [HttpPost]
        public IActionResult InserirComentario(IFormCollection collection)
        {
            // Verifica se o formulário foi preenchido.
            if (string.IsNullOrEmpty(collection["textoComentario"]) == true)
            {
                ModelState.AddModelError("textoComentario", "MandatoryField");
            }
            if (string.IsNullOrEmpty(collection["rating"]) == true)
            {
                ModelState.AddModelError("rating", "MandatoryField");
            }
            else if ((double.Parse(collection["rating"]) < 0.0) || double.Parse(collection["rating"]) > 5.0)
            {
                ModelState.AddModelError("rating", "IndexOutOfRange");
            }

            string clienteID = TempData["cliente"].ToString();
            string restauranteID = TempData["restaurante"].ToString();

            // Caso o formulário tenha sido devidamente preenchido.
            if (ModelState.IsValid)
            {
                string comentarioCorpo = collection["textoComentario"];
                double rating = double.Parse(collection["rating"]);

                ComentarioRestaurante novoComentario = new ComentarioRestaurante();

                // Verifica-se se já existe um comentário feito pelo cliente em questão ao restaurante. 
                // Se existe, vamos mudar esse comentário.
                // Cada utilizador apenas pode comentar uma única vez o mesmo restaurante.
                novoComentario = _context.ComentarioRestaurantes.FirstOrDefault(x => x.ClienteId == clienteID && x.RestauranteId == restauranteID);


                novoComentario.ClienteId = clienteID;
                novoComentario.RestauranteId = restauranteID;
                novoComentario.DataComentario = DateTime.Now;
                novoComentario.Corpo = comentarioCorpo;
                novoComentario.Rating = rating;

                // Cálcula a média de todas as ratings atribuidas ao restaurante.
                double somaRating = 0;
                int numero = 0;
                List<ComentarioRestaurante> numeroComentarios = _context.ComentarioRestaurantes.Where(x => x.RestauranteId == restauranteID).ToList();
                foreach (var comentario in numeroComentarios)
                {
                    somaRating += comentario.Rating;
                    numero++;
                }

                // Identifica o restaurante ao qual atribuir comentário e rating.
                Restaurante restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == restauranteID);

                restaurante.Rating = Math.Round(somaRating / numero, 2);
                novoComentario.Restaurante = restaurante;

                // Se o cliente ainda não tinha comentado este restaurante, é necessário adicionar uma nova entrada à BD.
                if (_context.ComentarioRestaurantes.FirstOrDefault(x => x.ClienteId == clienteID && x.RestauranteId == restauranteID) == null)
                {
                    _context.Add(novoComentario);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("ApresentarRestaurante", "Restaurante", new { id = restauranteID });
        }
    }
}
