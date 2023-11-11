using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using Repository.Controle;
using Bussines;
using _5w2h.util;

namespace _5w2h.Areas.Gestão.Controllers
{   
    [Area("Gestão")]
    public class CadastroController : Controller
    {
        private Context _context;
        private FuncionarioService _funcionario;
        
        public CadastroController(Context context)
        {
            _context = context;
            _funcionario = new FuncionarioService(new GenericRepository<Funcionario>(context));
        }


        // GET: CadastroController
        public ActionResult Index()
        {
            List<Funcionario> func = _funcionario.BuscarTodos().OrderBy(f=>f.Codigo).ToList();
            return View(func);
        }

        // GET: CadastroController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Funcionario func, bool novoValor)
        {
            

            if (novoValor == true)
            {
                func.NomeCompleto = "111";
            } else
            {
                func.NomeCompleto = "2222";
            }
            try
            {
                if (_funcionario.Criar(func))
                {
                    Validation<Funcionario>.CopyValidation(ModelState, _funcionario);
                    return RedirectToAction(nameof(Index));
                } else
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
            return View();
        }

        // POST: CadastroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
