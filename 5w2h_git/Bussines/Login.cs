using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class Login
    {
        public int CodigoFuncionario { get; set; }
        
        public string Email { get; set; }

        public string Senha { get; set; }


        public virtual Funcionario Funcionario { get; set; }
    }
}
