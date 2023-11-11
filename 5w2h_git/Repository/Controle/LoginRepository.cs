using Bussines;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controle
{
    public class LoginRepository : GenericRepository<Login>
    {

        private Context _context;


       

        public LoginRepository(Context context) : base(context)
        {
            this._context = context;
        }

        

        public virtual Login BuscarLoginFuncionario(int funid) //BuscaLoginFuncionario
        {
            return _context.Login.FirstOrDefault(l => l.CodigoFuncionario == funid);
        }

        public virtual void Inativar(int id, Context context)
        {
            FuncionarioRepository f = new FuncionarioRepository(context); 
            Funcionario desativar = f.BuscarPorCodigo(id);

            desativar.Ativo = -1;
        }

        public virtual void Ativar(int id, Context context) //Ativar
        {
            FuncionarioRepository f = new FuncionarioRepository(context);
            Funcionario ativar = f.BuscarPorCodigo(id);

            ativar.Ativo = 0;
        }

        //Logar
        public virtual void Logar(string senha, string email)
        {
            Login l1 = _context.Login.FirstOrDefault(l => l.Email == email);
            if (l1 == null)

            {
                Console.WriteLine("Email não cadastrado!");
            }

            else if (l1.Senha == senha)
            {

                Console.WriteLine("Usuário logado com sucesso!");

            }
            else
            {
                Console.WriteLine("Credenciais de login erradas !!");
            }
        }


    }
}
