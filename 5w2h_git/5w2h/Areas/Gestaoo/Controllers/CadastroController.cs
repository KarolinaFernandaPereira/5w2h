using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using Repository.Controle;
using Bussines;
using _5w2h.util;
using System.Numerics;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace _5w2h.Areas.Gestão.Controllers
{   
    [Area("Gestaoo")]
    public class CadastroController : Controller
    {
        private Context _context;
        private FuncionarioService _funcionario;
        private LoginService _login;

        public CadastroController(Context context)
        {
            _context = context;
            _funcionario = new FuncionarioService(new GenericRepository<Funcionario>(context));
            _login = new LoginService(new GenericRepository<Login>(context));
        }


        // GET: CadastroController
        public ActionResult Index()
        {
            List<Funcionario> func = _funcionario.BuscarTodos().Where(f => f.Ativo != -1).OrderBy(f=>f.Codigo).ToList();
            return View(func);
        }

        public ActionResult Ativar()
        {
            List<Funcionario> funcInativos = _funcionario.BuscarTodos().Where(f => f.Ativo == -1).OrderBy(f => f.Codigo).ToList();
            return View(funcInativos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ativar(int id, IFormCollection collection)
        {
            try
            {
                if (_funcionario.Ativar(id))
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    Validation<Funcionario>.CopyValidation(ModelState, _funcionario);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroController/Details/5
        public ActionResult Details(int id)
        {
            Funcionario fDetalhe = _funcionario.BuscarTodos().Where(f => f.Codigo == id).FirstOrDefault();
            Login loginFunc = _login.BuscarTodos().Where(l => l.CodigoFuncionario == fDetalhe.Codigo).FirstOrDefault();
            ViewBag.loginDetlhe = loginFunc;
            return View(fDetalhe);
        }

        // GET: CadastroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Funcionario func, bool Gestor, bool Ativo, string Email, string Senha)
        {
            

            if (Gestor == true)
            {
                func.Gestor = 1;
            } else
            {
                func.Gestor = 0;    
            }

            if (Ativo == true)
            {
                func.Ativo = 1;

            }
            else
            {
                func.Ativo = -1;
            }


            try
            {

                if (_funcionario.Criar(func))
                {
                    
                    try
                    {
                        if (Email != "" && Senha != "")
                        {
                            Login l = new Login();
                            l.Email = Email;
                            l.Senha = Senha;
                            l.CodigoFuncionario = func.Codigo;

                            _login.Criar(l);

                        }
                        Validation<Funcionario>.CopyValidation(ModelState, _funcionario);
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        Validation<Funcionario>.CopyValidation(ModelState, _funcionario);
                        return RedirectToAction(nameof(Index));
                    }
                    

                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }


        }

        // GET: CadastroController/Edit/5
        public ActionResult Edit(int id)
        {
            Funcionario updateFunc = _funcionario.BuscarTodos().Where(f => f.Codigo == id).FirstOrDefault();
            ViewBag.updateF = updateFunc;
            Login loginFunc = _login.BuscarTodos().Where(l=>l.CodigoFuncionario == updateFunc.Codigo).FirstOrDefault();
            ViewBag.loginFunc = loginFunc;
            return View();
        }

        // POST: CadastroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Funcionario funcEdit, int id, bool Gestor, bool Ativo, string Email, string Senha)
        {
            
            Funcionario testeF = _funcionario.BuscarTodos().Where(f=>f.Codigo == id).FirstOrDefault();

            #region Gestor/Ativo
                if (Gestor == true)
                {
                    funcEdit.Gestor = 1;
                }
                else
                {
                    funcEdit.Gestor = 0;
                }

                if (Ativo == true)
                {
                    funcEdit.Ativo = 0;

                }
                else
                {
                    funcEdit.Ativo = -1;
                }
            #endregion

            Login loginUpdate = _login.BuscarTodos().Where(l=>l.CodigoFuncionario == testeF.Codigo).FirstOrDefault();

            if(loginUpdate == null)
            {
                if (Email != null && Senha != null)
                {

                    Login novol = new Login();
                    novol.Senha = Senha;
                    novol.Email = Email;
                    novol.CodigoFuncionario = testeF.Codigo;
                    _login.Criar(novol);
                }
                

            } else
            {
                if (Email != "" && Senha != "")
                {
                    loginUpdate.Senha = Senha;
                    loginUpdate.Email = Email;
                    _login.Atualizar(loginUpdate);
                }

            }

            if (testeF != null)
            {
                testeF.NomeCompleto = funcEdit.NomeCompleto;
                testeF.CPF = funcEdit.CPF;
                testeF.Ativo = funcEdit.Ativo;
                testeF.Gestor = funcEdit.Gestor;
            }

            try
            {
                if (_funcionario.Atualizar(testeF))
                {
                    return RedirectToAction(nameof(Index));

                }else
                {
                    Validation<Funcionario>.CopyValidation(ModelState, _funcionario);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroController/Delete/5
        public ActionResult Delete(int id)
        {
            Funcionario funcInativar = _funcionario.BuscarTodos().Where(f => f.Codigo == id).FirstOrDefault();

            return View(funcInativar);
        }

        // POST: CadastroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (_funcionario.Inativar(id))
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    Validation<Funcionario>.CopyValidation(ModelState, _funcionario);
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
