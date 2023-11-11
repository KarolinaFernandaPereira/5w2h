using _5w2h.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Repository;
using Services;
using Repository.Controle;
using Bussines;

namespace _5w2h.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Context _context;
        private QuadroService _quadroService;
        private TarefaService _trefaService;
        private FuncionarioService _funcionario;
         
        public HomeController(Context context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
            _quadroService = new QuadroService(new QuadroRepository(context));
            _trefaService = new TarefaService(new TarefaRepository(context));
            _funcionario = new FuncionarioService(new FuncionarioRepository(context));
        }

        public IActionResult Index()
        {
            List<Quadro> quadros = _quadroService.BuscarTodos().Where(q => q.Ativo != -1).OrderBy(q => q.Codigo).ToList();
            ViewBag.quadros = quadros;

            List<Tarefa> tarefas = _trefaService.BuscaFunc().Where(t => t.Ativo != -1).ToList();
            ViewBag.tarefas = tarefas;

            List<Funcionario> funcionarios = _funcionario.BuscarTodos().Where(f => f.Ativo != -1).ToList();
            ViewBag.funcionarios = funcionarios;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}