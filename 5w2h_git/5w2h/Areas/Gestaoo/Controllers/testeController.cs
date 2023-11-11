using Microsoft.AspNetCore.Mvc;
using Repository.Controle;
using Repository;
using Services;
using Bussines;
using System;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;
using _5w2h.util;


namespace _5w2h.Areas.Gestão.Controllers
{
    [Area("Gestaoo")]
    public class testeController : Controller
    {
        private readonly ILogger<testeController> _logger;
        private Context _context;
        private QuadroService _quadroService;
        private TarefaService _tarefaService;
        private FuncionarioService _funcionario;

        public testeController(Context context, ILogger<testeController> logger)
        {
            _logger = logger;
            _context = context;
            _quadroService = new QuadroService(new QuadroRepository(context));
            _tarefaService = new TarefaService(new TarefaRepository(context));
            _funcionario = new FuncionarioService(new FuncionarioRepository(context));
        }

        public IActionResult Index()
        {
            List<Quadro> quadros = _quadroService.BuscarTodos().Where(q=>q.Ativo != -1).OrderBy(q => q.Codigo).ToList();
            ViewBag.quadros = quadros;

            List<Tarefa> tarefas = _tarefaService.BuscaFunc().Where(t => t.Ativo != -1).ToList();
            ViewBag.tarefas = tarefas;

            List<Funcionario> funcionarios = _funcionario.BuscarTodos().Where(f => f.Ativo != -1).ToList();
            ViewBag.funcionarios = funcionarios;

            
            return View();
        }

        /*[HttpPost]
        public IActionResult Membros(string nome, int tarefaID)
        {
            Funcionario f = _funcionario.BuscarTodos().Where(x=>x.NomeCompleto == nome).FirstOrDefault();
            Tarefa t = _tarefaService.BuscarTodos().Where(x=>x.Codigo == tarefaID).FirstOrDefault();
            t.Funcionario.Add(f);
            f.Tarefa.Add(t);
            
            return RedirectToAction(nameof(Index));
        }*/


        public IActionResult ControleTarefa(string acaoForm, Tarefa tar, int codigoQuadro1, int idUpdateTarefa, int idInativarTarefa,
                                            string nomeTarefa,
                                            string descricao,
                                            string oque,
                                            string onde,
                                            string quando,
                                            string como,
                                            string quanto,
                                            string quem,
                                            string porque,
                                            string membrosOcultos)
        {

            if(acaoForm == "criar")
            {

                string nomeTarefa1 = nomeTarefa;
                string descricao1 = descricao;

                string oque1 = oque;
                string onde1 = onde;
                DateTime quando1 = Convert.ToDateTime(quando);
                string como1 = como;
                string quanto1 = quanto;
                string quem1 = quem;
                string porque1 = porque;

                tar.Nome = nomeTarefa1;
                tar.Descricao = descricao1;

                tar.Oque = oque1;
                tar.Onde = onde1;
                tar.Quando = DateTime.Now;
                tar.Como = "a";
                tar.Quanto = "a";
                tar.Quem = "a";
                tar.PorQue = "a";

                tar.CodigoQuadro = codigoQuadro1;


                if(membrosOcultos != null)
                {
                    string[] dadosDoCadastro = membrosOcultos.Split(' ');
                    for (int i = 0; i < dadosDoCadastro.Length; i++)
                    {
                        if (dadosDoCadastro[i] != "")
                        {
                            Funcionario f = _funcionario.BuscarTodos().Where(x => x.NomeCompleto == dadosDoCadastro[i]).FirstOrDefault();
                            tar.Funcionario.Add(f);
                            f.Tarefa.Add(tar);

                        }
                    }
                }
                
                
                

                tar.Ativo = 1;
                bool teste = _tarefaService.CriarTarefa(tar);
                if (teste)
                {

                    List<Tarefa> brabo = _tarefaService.BuscarTodos().ToList();
                    ViewBag.tarefas = brabo;


                    return RedirectToAction(nameof(Index));
                } else
                {
                    Validation<Tarefa>.CopyValidation(this.ModelState, _tarefaService);
                    return View("Index");
                }

            }
            else if (acaoForm == "editar")
            {
                Tarefa t = _tarefaService.BuscarTodos().Where(t => t.Codigo == idUpdateTarefa).FirstOrDefault();

                string nomeUpdate = nomeTarefa;
                string descricaoUpdate = descricao;
                string oqueUpdate = oque;
                string ondeUpdate = onde;
                DateTime quandoUpdate = Convert.ToDateTime(quando);
                string comoUpdate = como;
                string quantoUpdate = quanto;
                string quemUpdate = quem;
                string porqueUpdate = porque;

                t.Nome = nomeUpdate;
                t.Descricao = descricaoUpdate;
                
                t.Oque = oqueUpdate;
                t.Onde = ondeUpdate;
                t.Quem = quemUpdate;
                t.Como = comoUpdate;
                t.Quando = quandoUpdate;
                t.Quanto = quantoUpdate;
                t.PorQue = porqueUpdate;

                t.Ativo = 1;
                t.CodigoQuadro = codigoQuadro1;

                _tarefaService.Atualizar(t);

                return RedirectToAction(nameof(Index));
            }
            else if (acaoForm == "inativar")
            {
                if(idInativarTarefa != null)
                {
                    _tarefaService.Inativar(idInativarTarefa);
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(); 
            

        }



        public IActionResult ControleQuadro(string acaoForm, int idUpdateQuadro, int idInativarQuadro, Quadro quad, string nomeQuadroUpdate)
        {
            if(acaoForm == "editar")
            {
                Quadro q = _quadroService.BuscarTodos().Where(q => q.Codigo == idUpdateQuadro).FirstOrDefault();

                string nomeQuqadroUpdate = nomeQuadroUpdate;

                q.Nome = nomeQuqadroUpdate;

                _quadroService.Atualizar(q);


                return RedirectToAction(nameof(Index));
            } else if (acaoForm == "inativar")
            {


                _quadroService.Inativar(idInativarQuadro);
                return RedirectToAction(nameof(Index));
            } else if (acaoForm == "criar")
            {
                var codigoQ = 4;
                string nomeQuqadro = nomeQuadroUpdate;


                quad.Nome = nomeQuqadro;


                if (_quadroService.Criar(quad))
                {
                    codigoQ++;
                }

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public void TrocarColuna(int tarefaId, int colunaId)
        {
            Tarefa t_troca = _tarefaService.BuscarTodos().Where(x=>x.Codigo == tarefaId).FirstOrDefault();

            t_troca.CodigoQuadro = colunaId;

            _tarefaService.Atualizar(t_troca);

            
        }

        public IActionResult visualizar(int id)
        {
            Tarefa tar = _tarefaService.BuscarTodos().Where(t=>t.Codigo == id).FirstOrDefault();

            ViewBag.verTarefa = tar;
            ViewBag.ver = true;

            return RedirectToAction(nameof(Index));
        }

        
        public ActionResult editarTarefa(int idT)
        {
            return Json(new { tarefa = _tarefaService.BuscaFunc().Where(t => t.Codigo == idT).FirstOrDefault() });

        }
    }
}
