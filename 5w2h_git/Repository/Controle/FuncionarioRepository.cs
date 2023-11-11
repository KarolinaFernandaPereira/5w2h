using Bussines;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controle
{
    public class FuncionarioRepository : GenericRepository<Funcionario>
    {
        private Context _context;
        
        public FuncionarioRepository(Context context) : base(context) 
        {
            this._context = context;
        }

       

        public virtual Funcionario BuscarPorCodigo(int codigo) //BuscarPorCodigo
        {
            return _context.Funcionario.Find(codigo);
    
        }

        
        public virtual List<Funcionario> BuscarAtivos() //BuscarAtivos
        {
            return _context.Funcionario.Where(f => f.Ativo != -1).ToList();

        }

        public virtual void Inativar(int id)
        {
            Funcionario removeFuncionario = _context.Funcionario.Find(id);
            removeFuncionario.Ativo = -1;

        }

        public virtual void Ativar(int id)
        {
            Funcionario removeFuncionario = _context.Funcionario.Find(id);
            removeFuncionario.Ativo = 0;
        }

       

        

    }
}
