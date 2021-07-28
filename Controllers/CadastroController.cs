using CIMCMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIMCMVC.Controllers
{
    [Route("Cadastro")]
    public class CadastroController : Controller
    {
        Usuario UsuarioModel = new Usuario();

        [TempData]
        public string Mensagem { get; set; }
        
        [Route("Index")]
        public IActionResult Index(){
            return View();
        }
        
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection Form){
            Usuario UsuarioCadastro = new Usuario();
            UsuarioCadastro.Nome = Form["Nome"];
            UsuarioCadastro.Email = Form["Email"];
            UsuarioCadastro.Senha = Form["Senha"];
            UsuarioCadastro.IDUsuario = UsuarioModel.GerarID("Database/Usuario.csv");
            bool ConflitoEmail = false;
            foreach (Usuario item in UsuarioModel.Listar())
            {
                if (item.Email == UsuarioCadastro.Email)
                {
                    ConflitoEmail = true;
                }
            }
            if (ConflitoEmail == true)
            {
                Mensagem = "Email já existente, tente novamente";
                return LocalRedirect("~/Cadastro/Index");
            }
            else
            {
                UsuarioModel.Cadastrar(UsuarioCadastro);
                return LocalRedirect("~/Login/Index");
            }
        }
    }
}