using Microsoft.AspNetCore.Mvc;
using Bussines;
using Repository;
using Repository.Controle;
using Services;
using _5w2h.util;
using _5w2h.Controllers;

namespace _5w2h.Areas.Gestão.Controllers
{
    
    public class LoginController : Controller
    {
        
        private Context _context;
        private LoginService _loginService;
        private FuncionarioService _funcionarioService;

        public LoginController(Context context)
        {
            
            _context = context;
            _loginService = new LoginService(new LoginRepository(context));
            _funcionarioService = new FuncionarioService(new FuncionarioRepository(context));
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Logar(Login loginP)    
        {
            Login loginFuncionario = _loginService.Logar(loginP.Email, loginP.Senha);
            if (loginFuncionario == null)
            {
                Validation<Login>.CopyValidation(this.ModelState, _loginService);
                return View("Index");
            } else
            {
                Login login = _loginService.BuscarTodos().
                    Where(l=> l.Email.Equals(loginP.Email) && l.Senha.Equals(loginP.Senha)).FirstOrDefault();

                Funcionario f = _funcionarioService.BuscarTodos().Where(f => f.Codigo == login.CodigoFuncionario).FirstOrDefault();

                if(f.Ativo != -1)
                {

                    if(f.Gestor == 0)
                    {   
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        return RedirectToAction("Index", "teste", new { area = "Gestaoo" });
                    }
                } else
                {
                    return View("Index");
                }
            }
            
        }
    }
}
