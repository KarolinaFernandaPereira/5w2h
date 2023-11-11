using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class Quadro
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public int Ativo { set; get; }
        
        public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
