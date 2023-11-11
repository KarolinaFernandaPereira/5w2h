using Bussines;
using Repository.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TarefaService : GenericServices<Tarefa>
    {
        private TarefaRepository reposoitory1;
        public TarefaService(GenericRepository<Tarefa> repository) : base(repository) 
        {
            reposoitory1 = (TarefaRepository)repository;
        }

        public List<Tarefa> BuscaFunc()
        {
            return reposoitory1.BuscaFuncioario();
        }

        
        public bool Inativar(int id)
        {
            
            bool resp = true;
            Tarefa tarefaInativar = base.BuscarTodos().Where(t => t.Codigo == id).FirstOrDefault();

            if (tarefaInativar != null)
            {

                tarefaInativar.Ativo = -1;
                resp = true;
            }
            else
            {
                resp = false;
            }

            Repository.Salvar();

            return resp;
        }
	
	    public bool CriarTarefa(Tarefa tarefa)
        {
            bool retorno = true;

            if (tarefa.Nome == null || tarefa.Nome.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Nome", "Valor em branco.");
            }

            if (tarefa.Descricao == null || tarefa.Descricao.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Descricao", "Valor em branco.");
            }
            if (tarefa.Oque == null || tarefa.Oque.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Oque", "Valor em branco.");
            }
            if (tarefa.Onde == null || tarefa.Onde.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Onde", "Valor em branco.");
            }
            if (tarefa.Como == null || tarefa.Como.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Como", "Valor em branco.");
            }
            if (tarefa.Quanto == null || tarefa.Quanto.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Quanto", "Valor em branco.");
            }
            if (tarefa.Quando == null)
            {
                retorno = false;
                ValidationDictionary.AddError("Quando", "Valor em branco.");
            }
            if (tarefa.Quem == null || tarefa.Quem.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("Quem", "Valor em branco.");
            }
            if (tarefa.PorQue == null || tarefa.PorQue.Equals(""))
            {
                retorno = false;
                ValidationDictionary.AddError("PorQue", "Valor em branco.");
            }

            

            return retorno;

        }
        
	
    }
}
