using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)")]
        public IActionResult Error(int codigo)
        {
            var modelErro = new ErrorViewModel();
            switch (codigo)
            {
                case 500:
                    modelErro.Mensagem = "Tente novamente mais tarde ou contate nosso suporte";
                    modelErro.Titulo = "Ocorreu um erro!";
                    modelErro.ErroCode = codigo;
                    break;
                case 404:
                    modelErro.Mensagem = "A página que está procurando não existe@ <br/>Em caso de dúvidas entre em contato com nosso suporte";
                    modelErro.Titulo = "Ops! Página não encontrada";
                    modelErro.ErroCode = codigo;
                    break;
                case 403:
                    modelErro.Mensagem = "Você não tem permissão para fazer isto";
                    modelErro.Titulo = "Acesso negado!";
                    modelErro.ErroCode = codigo;
                    break;
                default:
                    return StatusCode(404);
            }
            return View("Error", modelErro);
        }
    }
}
