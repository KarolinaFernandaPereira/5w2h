using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class Funcionario
    {
        public int Codigo { set; get; }

        public string CPF { set; get; }

        public string NomeCompleto { set; get; }

        public int Gestor { set; get; }

        public int Ativo { set; get; }

        public virtual ICollection<Tarefa> Tarefa { set; get; } = new List<Tarefa>();

        public virtual Login Login { set; get; }
    }
}