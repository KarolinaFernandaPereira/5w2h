using Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controle
{
    public class QuadroRepository : GenericRepository<Quadro>
    {
        private Context _context;

        public QuadroRepository(Context context) : base(context) 
        {
            this._context = context;
        }

        

        

        public virtual List<Quadro> BuscarAtivos() // BuscarAtivos
        {
            return _context.Quadro.Where(q => q.Ativo != -1).ToList();
        }

        public virtual Quadro BuscarCodigo(int codigo) //BuscarCodigo
        {
            return _context.Quadro.Find(codigo);

        }

        public virtual void Remover(int codigo) // Remover
        {
            Quadro removeQuadro = _context.Quadro.Find(codigo);
            removeQuadro.Ativo = -1;
        }
        

        
    }
}
