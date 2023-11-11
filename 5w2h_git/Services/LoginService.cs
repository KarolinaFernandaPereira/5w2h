using Bussines;
using Repository.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginService : GenericServices<Login>
    {
        public LoginService(GenericRepository<Login> repository) : base(repository) { }

        public Login Logar(string email, string senha)
        {
            Login loginRetorno = null;

            if (email == null || email.Equals(""))
            {
                ValidationDictionary.AddError("Email", "Campo Email inválido");
            } else if (senha == null || senha.Equals("")) {
                ValidationDictionary.AddError("Senha", "Campo senha inválido");
            }else
            {

                 loginRetorno = Repository.BuscarTodos().Where(u => u.Senha.Equals(senha) && u.Email.Equals(email)).FirstOrDefault();

                if (loginRetorno == null)
                {
                    ValidationDictionary.AddError("", "Email/Senha Inválidos    ");
                }
            }


            return loginRetorno;
        }

        
    }

    
}
