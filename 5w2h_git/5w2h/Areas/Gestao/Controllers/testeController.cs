using Microsoft.AspNetCore.Mvc;
using Repository.Controle;
using Repository;
using Services;
using Bussines;

namespace _5w2h.Areas.Gestão.Controllers
{
    [Area("Gestão")]
    public class testeController : Controller
    {
        private readonly ILogger<testeController> _logger;
        private Context _context;
        private QuadroService _quadroService;

        public testeController(Context context, ILogger<testeController> logger)
        {
            _logger = logger;
            _context = context;
            _quadroService = new QuadroService(new QuadroRepository(context));
        }

        public IActionResult Index()
        {
            List<Quadro> quadros = _quadroService.BuscarTodos().OrderBy(q => q.Nome).ToList();
            ViewBag.quadros = quadros;
            return View();
        }
    }
}
