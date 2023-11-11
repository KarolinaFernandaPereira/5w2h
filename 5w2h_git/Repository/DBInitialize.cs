using Bussines;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DBInitialize
    {
        public static void Initialize(Context context) 
        {

            if (context.Database.EnsureCreated())
            {
                
                Quadro qBase1 = new Quadro();
                Quadro qBase2 = new Quadro();
                Quadro qBase3 = new Quadro();

                Funcionario admin = new Funcionario();

                Login login = new Login();

                admin.Gestor = 1;
                admin.Ativo = 1;
                admin.NomeCompleto = "Admin";
                admin.CPF = "3216549710";
                context.Funcionario.Add(admin);
                context.SaveChanges();

                login.Email = "admin";
                login.Senha = "admin";
                login.CodigoFuncionario = admin.Codigo;
                context.Login.Add(login);

                qBase1.Nome = "A Fazer";
                qBase2.Nome = "Em Progresso";
                qBase3.Nome = "Finalizada";

                context.Quadro.Add(qBase1);
                context.Quadro.Add(qBase2);
                context.Quadro.Add(qBase3);

                context.SaveChanges();
                
            } 
        }
    }
}
