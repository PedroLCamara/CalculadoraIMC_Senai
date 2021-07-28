using CIMCMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIMCMVC.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        Usuario UsuarioModel = new Usuario();

        [TempData]
        public string Mensagem { get; set; }

        [Route("Index")]
        public IActionResult Index(){
            return View();
        }
        [Route("Logar")]
        public IActionResult Logar(IFormCollection Form){
            Usuario UsuarioLogin = new Usuario();
            UsuarioLogin.Email = Form["Email"];
            UsuarioLogin.Senha = Form["Senha"];
            bool EmailCorreto = false;
            bool SenhaCorreta = false;
            foreach (Usuario item in UsuarioModel.Listar())
            {
                if (UsuarioLogin.Email == item.Email)
                {
                    EmailCorreto = true;
                    if (UsuarioLogin.Senha == item.Senha)
                    {
                        SenhaCorreta = true;
                    }
                }
            }
            switch (EmailCorreto && SenhaCorreta)
            {
                case true && true:
                    HttpContext.Session.SetString("IDUsuarioLogado", UsuarioLogin.IDUsuario.ToString());
                    return LocalRedirect("~/Home/Index");
                case true && false || false && true:
                    if (EmailCorreto == false)
                    {
                        Mensagem = "Email e senha incorretos, tente novamente";
                        return LocalRedirect("~/Login/Index");
                    }
                    else
                    {
                        Mensagem = "Senha incorreta, tente novamente";
                        return LocalRedirect("~/Login/Index");
                    }
            }
        }

        [Route("Deslogar")]
        public IActionResult Deslogar(){
            HttpContext.Session.Remove("IDUsuarioLogado");
            return LocalRedirect("~/Cadastro/Index");
        }
    }
}