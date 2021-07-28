using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CIMCMVC.Models;
using Microsoft.AspNetCore.Http;

namespace CIMCMVC.Controllers
{
    public class HomeController : Controller
    {
        IMC IMCModel = new IMC();
        Usuario UsuarioModel = new Usuario();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IDUsuarioLogado") != null)
            {
                ViewBag.Usuarios = UsuarioModel.Listar(); 
                ViewBag.IMCs = IMCModel.Listar(); 
                return View();
            }
            else
            {
                return LocalRedirect("~/Cadastro/Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("PublicarIMC")]
        public IActionResult PublicarIMC(IFormCollection Form)
        {
            IMC IMCPublicar = new IMC();
            IMCPublicar.IDIMC = IMCModel.GerarID("Database/IMC.csv");
            IMCPublicar.IDUsuario = Int32.Parse(HttpContext.Session.GetString("IDUsuarioLogado"));
            IMCPublicar.Peso = float.Parse(Form["Peso"].ToString().Replace(".", ","));
            IMCPublicar.Altura = float.Parse(Form["Altura"].ToString().Replace(".", ","));
            IMCPublicar.ValorIMC = IMCPublicar.Peso / (IMCPublicar.Altura * IMCPublicar.Altura);
            if (IMCPublicar.ValorIMC <= 18.5)
            {
                IMCPublicar.ClassificacaoIMC = "Baixo peso";
            }
            else if (IMCPublicar.ValorIMC <= 24.9)
            {
                IMCPublicar.ClassificacaoIMC = "Peso normal";
            }
            else if (IMCPublicar.ValorIMC <= 29.9)
            {
                IMCPublicar.ClassificacaoIMC = "Sobrepeso";
            }
            else if (IMCPublicar.ValorIMC <= 34.9)
            {
                IMCPublicar.ClassificacaoIMC = "Obesidade grau 1";
            }
            else if (IMCPublicar.ValorIMC <= 39.9)
            {
                IMCPublicar.ClassificacaoIMC = "Obesidade grau 2";
            }
            else
            {
                IMCPublicar.ClassificacaoIMC = "Obesidade grau 3";
            }
            IMCModel.Cadastrar(IMCPublicar);
            return LocalRedirect("~/Home/Index");
        }
        [Route("DeletarUsuario")]
        public IActionResult DeletarUsuario(){
            IMCModel.Deletar(Int32.Parse(HttpContext.Session.GetString("IDUsuarioLogado")));
            UsuarioModel.Deletar(Int32.Parse(HttpContext.Session.GetString("IDUsuarioLogado")));
            HttpContext.Session.Remove("IDUsuarioLogado");
            return LocalRedirect("~/Cadastro/Index");
        }
    }
}