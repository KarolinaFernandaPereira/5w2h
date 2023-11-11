using Bussines;
using Repository.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FuncionarioService : GenericServices<Funcionario>
    {
        public FuncionarioService(GenericRepository<Funcionario> repository) : base(repository) { }

        public override bool Criar(Funcionario func)
        {
            bool resp = true;
            
            resp = base.Criar(func);
            return resp;
        }

        public bool Inativar(int id)
        {
            bool resp = true;
            Funcionario removeFuncionario = base.BuscarTodos().Where(f => f.Codigo == id).FirstOrDefault();

            if(removeFuncionario != null)
            {

                removeFuncionario.Ativo = -1;
                resp = true;
            } else
            {
                resp = false;
            }

            Repository.Salvar();

            return resp;
        }

        public bool Ativar(int id)
        {
            bool resp = true;
            Funcionario removeFuncionario = base.BuscarTodos().Where(f => f.Codigo == id).FirstOrDefault();

            if (removeFuncionario != null)
            {

                removeFuncionario.Ativo = 0;
                resp = true;
            }
            else
            {
                resp = false;
            }

            Repository.Salvar();

            return resp;
        }

        

    }
}
