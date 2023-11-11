using Bussines;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controle
{
    public class TarefaRepository : GenericRepository<Tarefa>
    {

        private Context _context;

        public TarefaRepository(Context context) : base(context) 
        {
            this._context = context;
        }

       

        public virtual Tarefa BuscarPorCodigo(int codigo) 
        {
            return _context.Tarefa.Find(codigo);

        }

        public virtual List<Tarefa> BuscaFuncioario()
        {
            return _context.Tarefa.Include(t => t.Funcionario).Where(t => t.Ativo != -1).ToList();
        }

        public virtual List<Tarefa> BuscarAtivos() 
        {
            return _context.Tarefa.Where(t => t.Ativo != -1).ToList();

        }

        public virtual void Remover(int codigo) 
        {
            Tarefa removeTarefa = _context.Tarefa.Find(codigo);
            removeTarefa.Ativo = -1;
        }

        
    }
}
