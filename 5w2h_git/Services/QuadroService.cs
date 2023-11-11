using Repository.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines;
using Repository;

namespace Services
{
    public class QuadroService : GenericServices<Quadro>
    {
       

        public QuadroService(GenericRepository<Quadro> repository) : base(repository) { 
            
        }
        public bool Inativar(int id)
        {
            bool resp = true;
            Quadro removeQuadro= base.BuscarTodos().Where(q => q.Codigo == id).FirstOrDefault();

            if (removeQuadro != null)
            {

                removeQuadro.Ativo = -1;
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
