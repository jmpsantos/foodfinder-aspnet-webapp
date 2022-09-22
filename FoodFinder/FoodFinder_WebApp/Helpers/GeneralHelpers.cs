using FoodFinder_WebApp.Data;
using FoodFinder_WebApp.Models;
using FoodFinder_WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FoodFinder_WebApp.Helpers
{
    public class GeneralHelpers 
    {
        private readonly FoodFinder_WebAppContext _context;

        public GeneralHelpers(FoodFinder_WebAppContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Envia um email para o contacto e com o assunto e corpos especificados nos
        /// argumentos do método. O email do remetente está hardcoded no corpo do método.
        /// </summary>
        /// <param name="receiverEmail">Email do contacto para o qual enviar email.</param>
        /// <param name="subject">Assunto do email a enviar.</param>
        /// <param name="body">Corpo do email a enviar.</param>
        public void EnviarEmail(string receiverEmail, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("foodfinder.lab4@gmail.com");
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.Body = body;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("foodfinder.lab4@gmail.com", "CriarPrato");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        /// <summary>
        /// Cria e retorna um objeto do tipo PratoDetalhesViewModel, através da lista
        /// de pratos passados como argumento. Permite manter toda a informação relacionada
        /// com o(s) prato(s) passados.
        /// </summary>
        /// <param name="pratos">Pratos para os quais obter informação.</param>
        /// <returns></returns>
        public PratoDetalhesViewModel CriarPratoDetalhesViewModelAPartirPratoDoDia(List<PratoDoDium> listaOriginalPratos)
        {
            Settings settings = new Settings();

            // ViewModel que guardará toda a informação para retornar.
            PratoDetalhesViewModel pratoDetalhesViewModel = new PratoDetalhesViewModel();

            // Listas necessárias para obter e filtrar as diferentes informações do context.
            List<PratoDoDium> listaPratosDoDia = new List<PratoDoDium>();
            List<AdicionarPratoDoDium> listaAdicionarPratoDoDia = new List<AdicionarPratoDoDium>();
            List<string> listaImagensPath = new List<string>();
            List<Restaurante> listaRestaurantes = new List<Restaurante>();
            List<Localizacao> localizacaos = new List<Localizacao>();
            List<Utilizador> listaUtilizadoresRestaurantes = new List<Utilizador>();

            // Indica se encontramos relações para pelo menos um prato da lista passada como argumento.
            bool igualdadeEncontrada = false;

            // Percorrer todos PratosDoDia da lista passada como argumento.
            foreach (var prato in listaOriginalPratos)
            {
                // Identificar objeto AdicionarPratoDoDia associado ao PratosDoDia atual.
                AdicionarPratoDoDium adicionarPrato = new AdicionarPratoDoDium();
                adicionarPrato = _context.AdicionarPratoDoDia.Where(x => x.PratoId == prato.PratoId).OrderByDescending(x => x.DataPrato).FirstOrDefault(); ;

                if (adicionarPrato != null)
                {
                    listaImagensPath.Add(settings.ImagensPratosPath + prato.PratoId.ToString() + ".jpg");

                    // Identificar objeto Restaurante associado ao PratosDoDia atual.
                    Restaurante restaurante = new Restaurante();
                    restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == adicionarPrato.RestauranteId);

                    // Identificar objeto Utilizador associado ao restaurante do PratosDoDia atual.
                    Utilizador restauranteUtilizador = new Utilizador();
                    restauranteUtilizador = _context.Utilizadors.FirstOrDefault(x => x.Username == restaurante.RestauranteId);

                    listaPratosDoDia.Add(prato);
                    listaAdicionarPratoDoDia.Add(adicionarPrato);
                    listaRestaurantes.Add(restaurante);
                    listaUtilizadoresRestaurantes.Add(restauranteUtilizador);

                    igualdadeEncontrada = true;
                }
            }

            // Caso tenha sido encontrada pelo menos uma relação com pelo menos um
            // prato passado como argumento.
            if (igualdadeEncontrada == true)
            {
                pratoDetalhesViewModel.PratosDoDia = listaPratosDoDia;
                pratoDetalhesViewModel.AdicionarPratoDoDia = listaAdicionarPratoDoDia;
                pratoDetalhesViewModel.ImagensPath = listaImagensPath;
                pratoDetalhesViewModel.Restaurantes = listaRestaurantes;
                pratoDetalhesViewModel.UtilizadoresRestaurantes = listaUtilizadoresRestaurantes;

                return pratoDetalhesViewModel;
            }

            return null;
        }


        public PratoDetalhesViewModel CriarPratoDetalhesViewModelAPartirAdicionarPratoDoDia(List<AdicionarPratoDoDium> listaOriginalPratos)
        {
            Settings settings = new Settings();

            // ViewModel que guardará toda a informação para retornar.
            PratoDetalhesViewModel pratoDetalhesViewModel = new PratoDetalhesViewModel();

            // Listas necessárias para obter e filtrar as diferentes informações do context.
            List<PratoDoDium> listaPratosDoDia = new List<PratoDoDium>();
            List<AdicionarPratoDoDium> listaAdicionarPratoDoDia = new List<AdicionarPratoDoDium>();
            List<string> listaImagensPath = new List<string>();
            List<Restaurante> listaRestaurantes = new List<Restaurante>();
            List<Localizacao> localizacaos = new List<Localizacao>();
            List<Utilizador> listaUtilizadoresRestaurantes = new List<Utilizador>();

            // Indica se encontramos relações para pelo menos um prato da lista passada como argumento.
            bool igualdadeEncontrada = false;

            // Percorrer todos PratosDoDia da lista passada como argumento.
            foreach (var prato in listaOriginalPratos)
            {
                // Identificar objeto AdicionarPratoDoDia associado ao PratosDoDia atual.
                PratoDoDium adicionarPrato = new PratoDoDium();
                adicionarPrato = _context.PratoDoDia.FirstOrDefault(x => x.PratoId == prato.PratoId);

                if (adicionarPrato != null)
                {
                    listaImagensPath.Add(settings.ImagensPratosPath + prato.PratoId.ToString() + ".jpg");

                    // Identificar objeto Restaurante associado ao PratosDoDia atual.
                    Restaurante restaurante = new Restaurante();
                    restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == prato.RestauranteId);

                    // Identificar objeto Utilizador associado ao restaurante do PratosDoDia atual.
                    Utilizador restauranteUtilizador = new Utilizador();
                    restauranteUtilizador = _context.Utilizadors.FirstOrDefault(x => x.Username == restaurante.RestauranteId);

                    listaPratosDoDia.Add(adicionarPrato);
                    listaAdicionarPratoDoDia.Add(prato);
                    listaRestaurantes.Add(restaurante);
                    listaUtilizadoresRestaurantes.Add(restauranteUtilizador);

                    igualdadeEncontrada = true;
                }
            }

            // Caso tenha sido encontrada pelo menos uma relação com pelo menos um
            // prato passado como argumento.
            if (igualdadeEncontrada == true)
            {
                pratoDetalhesViewModel.PratosDoDia = listaPratosDoDia;
                pratoDetalhesViewModel.AdicionarPratoDoDia = listaAdicionarPratoDoDia;
                pratoDetalhesViewModel.ImagensPath = listaImagensPath;
                pratoDetalhesViewModel.Restaurantes = listaRestaurantes;
                pratoDetalhesViewModel.UtilizadoresRestaurantes = listaUtilizadoresRestaurantes;

                return pratoDetalhesViewModel;
            }

            return null;
        }

        public RestauranteDetalhesViewModel CriarRestauranteDetalhesViewModelAPartirRestaurante(List<Restaurante> listaOriginalRestaurantes)
        {
            Settings settings = new Settings();

            // ViewModel que guardará toda a informação para passar para a View.
            RestauranteDetalhesViewModel restauranteDetalhesViewModel = new RestauranteDetalhesViewModel();

            // Listas necessárias para obter e filtrar as diferentes informações do context.
            List<string> listaImagensPath = new List<string>();
            List<Restaurante> listaRestaurantes = new List<Restaurante>();
            List<Localizacao> localizacaos = new List<Localizacao>();
            List<Utilizador> listaUtilizadoresRestaurantes = new List<Utilizador>();
            List<ComentarioRestaurante> listaComentarios = new List<ComentarioRestaurante>();

            bool igualdadeEncontrada = false;

            int i = 0;
            foreach (var novoRestaurante in listaOriginalRestaurantes)
            {
                Restaurante restaurante = new Restaurante();
                restaurante = _context.Restaurantes.FirstOrDefault(x => x.RestauranteId == novoRestaurante.RestauranteId);

                if (restaurante != null)
                {
                    string imagemPath = settings.ImagensRestaurantesPath + restaurante.RestauranteId.ToString() + "_000" + ".jpg";

                    Localizacao localizacao = new Localizacao();
                    localizacao = _context.Localizacaos.FirstOrDefault(x => x.LocalizacaoId == restaurante.LocalizacaoId);

                    Utilizador restauranteUtilizador = new Utilizador();
                    restauranteUtilizador = _context.Utilizadors.FirstOrDefault(x => x.Username == restaurante.RestauranteId);

                    ComentarioRestaurante comentarios = new ComentarioRestaurante();
                    comentarios = _context.ComentarioRestaurantes.FirstOrDefault(x => x.RestauranteId == restaurante.RestauranteId);

                    listaImagensPath.Add(imagemPath);
                    listaRestaurantes.Add(restaurante);
                    localizacaos.Add(localizacao);
                    listaUtilizadoresRestaurantes.Add(restauranteUtilizador);

                    if (comentarios != null)
                    {
                        listaComentarios.Add(comentarios);
                    }

                    igualdadeEncontrada = true;

                    i++;
                }
            }


            if (igualdadeEncontrada == true)
            {
                restauranteDetalhesViewModel.ImagensPath = listaImagensPath;
                restauranteDetalhesViewModel.Restaurantes = listaRestaurantes;
                restauranteDetalhesViewModel.Localizacoes = localizacaos;
                restauranteDetalhesViewModel.UtilizadoresRestaurantes = listaUtilizadoresRestaurantes;
                restauranteDetalhesViewModel.ComentariosRestaurantes = listaComentarios;

                return restauranteDetalhesViewModel;
            }

            return null;
        }




    }
}
