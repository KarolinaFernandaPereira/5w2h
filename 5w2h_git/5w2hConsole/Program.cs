using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using Azure.Identity;
using Bussines;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Controle;

public class Program
{
    static void Main(string[] args)
    {


        iniciarQuadros();


        var continuar = true;
        while (continuar)
        {
            Console.Write("Escolha uma das opções: \n" +
                           "1 - Funcionário\n" +
                           "2 - Login\n" +
                           "3 - Tarefa\n" +
                           "4 - Quadro\n" +
                           "5 - Sair\n" +
                           "Sua escolha: "
            );

            var opc = Console.ReadLine();
            Console.Clear();

            if (opc.ToString().Equals("1"))
            {
                menuFuncionario();
            }
            else if (opc.ToString().Equals("2"))
            {
                menuLogin();
            }
            else if (opc.ToString().Equals("4"))
            {
                menuQuadro();
            }
            else if (opc.ToString().Equals("3"))
            {
                menuTarefa();
            }
            else if (opc.ToString().Equals("5"))
            {
                continuar = false;
            }
            else
            {
                Console.WriteLine("Opção invalida");
            }
        }


        static void menuFuncionario()
        {
            DbContextOptionsBuilder<Context> optionBuilder = new DbContextOptionsBuilder<Context>();

            optionBuilder.UseSqlServer(" server=DESKTOP-FEB5GB5\\SQLEXPRESS; database = 5W2H; trusted_connection = true; Encrypt = false");
            Context context = new Context(optionBuilder.Options);
            context.Database.EnsureCreated();

            FuncionarioRepository funcionarioRepository = new FuncionarioRepository(context);
            LoginRepository loginFuncRepository = new LoginRepository(context);

            var funcMenu = true;

            while (funcMenu)
            {
                Console.Clear();
                Console.WriteLine("Funcionário menu de controle!");
                Console.Write("1 - Cadastrar\n" +
                    "2 - Consultar Ativos\n" +
                    "3 - Consultar Geral\n" +
                    "4 - Inativar\n" +
                    "5 - Ativar\n" +
                    "6 - Update\n" +
                    "7 - Testar Login\n" +
                    "8 - Sair\n" +
                    "Sua escolha: ");


                var opc = Console.ReadLine();
                Console.Clear();

                if (opc.ToString().Equals("1"))
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Informe o Nome do Funcionario: ");
                    var nome = Console.ReadLine();

                    Console.WriteLine("Informe o CPF: ");
                    var cpf = Console.ReadLine();

                    Console.WriteLine("É um gestor S ou N :");
                    var gestor = Console.ReadLine();

                    Funcionario f = new Funcionario();
                    f.NomeCompleto = nome;
                    f.CPF = cpf;

                    if (gestor == "S" || gestor == "s")
                    {
                        f.Gestor = 1;
                    }
                    else
                    {
                        f.Gestor = 0;
                    }

                    funcionarioRepository.Criar(f);
                    funcionarioRepository.Salvar();
                    Console.Write("Deseja cadastrar o login deste Funcionário, S ou N: ");
                    var loginRes = Console.ReadLine();

                    if (loginRes == "S" || loginRes == "s")
                    {
                        Console.Write(" ");
                        Login novol = new Login();
                        novol.CodigoFuncionario = f.Codigo;

                        Console.Write("Qual o Email para o login: ");
                        var email = Console.ReadLine();

                        Console.Write("Qual a senha para o login: ");
                        var senha = Console.ReadLine();

                        novol.Senha = senha;
                        novol.Email = email;
                        loginFuncRepository.Criar(novol);
                        loginFuncRepository.Salvar();

                        Console.Write("Login Cadastrado com sucesso \nPressione Enter para continuar! ");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Cadastro realizado com sucesso \nPressione Enter para continuar!");
                        Console.ReadKey();
                    }


                }
                else if (opc.ToString().Equals("2"))
                {

                    List<Funcionario> func = funcionarioRepository.BuscarAtivos();
                    foreach (Funcionario f in func)
                    {
                            var sitcao = "";
                            var gestor = "";
                            if (f.Ativo == -1)
                            {
                                sitcao = "Inativo";
                            }
                            else
                            {
                                sitcao = "Ativo";
                            }

                            if (f.Gestor == 1)
                            {
                                gestor = "Sim";
                            }
                            else
                            {
                                gestor = "Não";
                            }
                            Console.WriteLine("\nCódigo: " + f.Codigo);
                            Console.Write("Funcionário: " + f.NomeCompleto);
                            Console.Write("  Situação: ");
                            Console.ForegroundColor = f.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.Write(sitcao);
                            Console.ResetColor();
                            Console.Write("\nCPF: " + f.CPF);
                            Console.Write("\nÉ gestor: ");
                            Console.ForegroundColor = f.Gestor != 0 ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.Write(gestor + "\n");
                            Console.ResetColor();
                        
                    }
                    Console.WriteLine("\n\nPressione Enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("4"))
                {
                    Console.WriteLine("Funcionários disponivéis: ");
                    List<Funcionario> funcGeral = funcionarioRepository.BuscarAtivos();
                    foreach (Funcionario fDesativar in funcGeral)
                    {



                        var sitcao = "";
                        var gestor = "";
                        if (fDesativar.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        if (fDesativar.Gestor == 1)
                        {
                            gestor = "Sim";
                        }
                        else
                        {
                            gestor = "Não";
                        }
                        Console.WriteLine("\nCódigo: " + fDesativar.Codigo);
                        Console.Write("Funcionário: " + fDesativar.NomeCompleto);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = fDesativar.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nCPF: " + fDesativar.CPF);
                        Console.Write("\nÉ gestor: ");
                        Console.ForegroundColor = fDesativar.Gestor != 0 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(gestor + "\n");
                        Console.ResetColor();
                    }

                    var id = "";
                    while (id == "")
                    {
                        Console.Write("\n\nQual o código do Funcionario que deseja inativar: ");
                        id = Console.ReadLine();
                    }

                    var idCovertido = Convert.ToInt32(id);

                    Funcionario f = funcionarioRepository.BuscarPorCodigo(idCovertido);
                    funcionarioRepository.Inativar(id: f.Codigo);
                    funcionarioRepository.Salvar();
                    Console.WriteLine("Desativado com Sucesso \nPressione Enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("3"))
                {
                    Console.WriteLine("Funcionários disponivéis: ");
                    List<Funcionario> funcGeral = funcionarioRepository.BuscarTodos();
                    foreach (Funcionario f in funcGeral)
                    {
                       

                        
                        var sitcao = "";
                        var gestor = "";
                        if (f.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        if (f.Gestor == 1)
                        {
                            gestor = "Sim";
                        } 
                        else
                        {
                            gestor = "Não";
                        }
                        Console.WriteLine("\nCódigo: " + f.Codigo);
                        Console.Write("Funcionário: " + f.NomeCompleto);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = f.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nCPF: " + f.CPF);
                        Console.Write("\nÉ gestor: ");
                        Console.ForegroundColor = f.Gestor != 0 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(gestor + "\n");
                        Console.ResetColor();
                    }

                    Console.Write("\n\nPressione enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("5"))
                {
                    Console.WriteLine("Funcionários Inativos: ");
                    List<Funcionario> funcGeral = funcionarioRepository.BuscarTodos();
                    foreach (Funcionario fAtivar in funcGeral)
                    {


                        if(fAtivar.Ativo == -1)
                        {
                            var sitcao = "";
                            var gestor = "";
                            if (fAtivar.Ativo == -1)
                            {
                                sitcao = "Inativo";
                            }
                            else
                            {
                                sitcao = "Ativo";
                            }

                            if (fAtivar.Gestor == 1)
                            {
                                gestor = "Sim";
                            }
                            else
                            {
                                gestor = "Não";
                            }
                            Console.WriteLine("\nCódigo: " + fAtivar.Codigo);
                            Console.Write("Funcionário: " + fAtivar.NomeCompleto);
                            Console.Write("  Situação: ");
                            Console.ForegroundColor = fAtivar.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.Write(sitcao);
                            Console.ResetColor();
                            Console.Write("\nCPF: " +   fAtivar.CPF);
                            Console.Write("\nÉ gestor: ");
                            Console.ForegroundColor = fAtivar.Gestor != 0 ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.Write(gestor + "\n");
                            Console.ResetColor();
                        } else
                        {
                            continue;
                        }
                        
                    }
                    
                  
                    var idAtivar = "";
                    while (idAtivar == "")
                    {
                        Console.Write("Qual o código do funcionario que deseja ativar: ");
                        idAtivar = Console.ReadLine();
                    }

                    

                    var idCovertidoAtivar = Convert.ToInt32(idAtivar);

                    Funcionario f = funcionarioRepository.BuscarPorCodigo(idCovertidoAtivar);
                    funcionarioRepository.Ativar(f.Codigo);
                    funcionarioRepository.Salvar();
                    Console.WriteLine("Ativado com sucesso \nPressione Enter para continuar!");
                    Console.ReadKey();

                }
                else if (opc.ToString().Equals("6"))
                {

                    List<Funcionario> func = funcionarioRepository.BuscarAtivos();
                    foreach (Funcionario f in func)
                    {
                        var sitcao = "";
                        var gestorPrint = "";
                        if (f.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        if (f.Gestor == 1)
                        {
                            gestorPrint = "Sim";
                        }
                        else
                        {
                            gestorPrint = "Não";
                        }
                        Console.WriteLine("\nCódigo: " + f.Codigo);
                        Console.Write("Funcionário: " + f.NomeCompleto);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = f.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nCPF: " + f.CPF);
                        Console.Write("\nÉ gestor: ");
                        Console.ForegroundColor = f.Gestor != 0 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(gestorPrint + "\n");
                        Console.ResetColor();

                    }

                  
                    var idAtulizar = "";
                    while (idAtulizar == "")
                    {
                        Console.Write("\n\nQual o id do funcionario que deseja Atualizar: ");
                        idAtulizar = Console.ReadLine();
                    }

                    var idCovertidoAtualizar = Convert.ToInt32(idAtulizar);

                    Funcionario updateF = funcionarioRepository.BuscarPorCodigo(idCovertidoAtualizar);
                    Console.Clear();

                    Console.Write("Atualizando o funcionário: " + updateF.NomeCompleto);
                    Console.Write("\nNome do funcionário: ");
                    var novoNome = Console.ReadLine();

                    Console.Write("\nInforme o CPF: ");
                    var cpf = Console.ReadLine();

                    Console.Write("\nÉ um gestor S ou N :");
                    var gestor = Console.ReadLine();

                    updateF.NomeCompleto = novoNome;
                    updateF.CPF = cpf;

                    if (gestor == "S" || gestor == "s")
                    {
                        updateF.Gestor = 1;
                    }
                    else
                    {
                        updateF.Gestor = 0;
                    }

                    funcionarioRepository.Atualizar(updateF);
                    funcionarioRepository.Salvar();
                    var gestorC = "";

                    if (updateF.Gestor == 1)
                    {
                        gestorC = "Sim";
                    }
                    else
                    {
                        gestorC = "Não";
                    }

                    Console.Clear();
                    Console.WriteLine("Fucnionário atualizado com sucesso\n");
                    Console.WriteLine("Id: " + updateF.Codigo +
                        "\nNome: " + updateF.NomeCompleto.ToString() +
                        "\nCPF: " + updateF.CPF.ToString() +
                        "\nGestor: " + gestorC);

                    Console.WriteLine("\n\nPressione Enter para continaur!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("7"))
                {
                    Console.Write("Teste de login!");
                    Console.Write("\n\nEscreve seu email: ");
                    var loginEmail = Console.ReadLine();
                    
                    Console.Write("\nEscreve sua senha: ");
                    var loginSenha = Console.ReadLine();

                    loginFuncRepository.Logar(loginSenha, loginEmail);
                    

                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("8"))
                {
                    funcMenu = false;
                }
                {
                    Console.WriteLine("Opção Inválida");
                }

            }




        }

        static void menuLogin()
        {
            DbContextOptionsBuilder<Context> optionBuilder = new DbContextOptionsBuilder<Context>();

            optionBuilder.UseSqlServer(" server=DESKTOP-FEB5GB5\\SQLEXPRESS; database = 5W2H; trusted_connection = true; Encrypt = false");
            Context context = new Context(optionBuilder.Options);
            context.Database.EnsureCreated();

            FuncionarioRepository funcionarioRepository = new FuncionarioRepository(context);
            LoginRepository loginFuncRepository = new LoginRepository(context);

            var loginMenu = true;
            while (loginMenu)
            {
                Console.Clear();
                Console.WriteLine("Login, menu de controle!");
                Console.Write("1 - Cadastrar\n" +
                    "2 - Consultar Login\n" +
                    "3 - Update\n" +
                    "4 - Inativar\n" +
                    "5 - Ativar\n" +
                    "6 - Sair\n" +
                    "Sua escolha: ");

                var opc = Console.ReadLine();
                Console.Clear();

                if (opc.ToString().Equals("1"))
                {
                    Console.WriteLine("Lista de Funconários Ativos e sem Login: ");
                    List<Funcionario> func = funcionarioRepository.BuscarAtivos();

                    var semLogin = false;
                    foreach (Funcionario f in func)
                    {
                        if (loginFuncRepository.BuscarLoginFuncionario(f.Codigo) == null)
                        {

                            Console.WriteLine("\nId: " + f.Codigo + " \nNome: " + f.NomeCompleto.ToString() + "\n CPF: " + f.CPF.ToString());
                            semLogin = true;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if(semLogin)
                    {

                        Console.WriteLine("Deseja cadastrar o Login para qual id: ");
                        var loginFuncId = Console.ReadLine();

                        var loginIdConvertido = Convert.ToInt32(loginFuncId);

                        Console.Clear();



                        Funcionario loginSuporte = funcionarioRepository.BuscarPorCodigo(loginIdConvertido);
                        Console.WriteLine("Cadastrando login para: " + loginSuporte.NomeCompleto.ToString());




                        Console.Write("Informe o Email: ");
                        var novoLoginEmail = Console.ReadLine();

                        Console.Write("Informe a senha: ");
                        var novoLoginSenha = Console.ReadLine();

                        Login novoL = new Login();

                        novoL.Senha = novoLoginSenha;
                        novoL.CodigoFuncionario = loginIdConvertido;
                        novoL.Email = novoLoginEmail;

                        loginFuncRepository.Criar(novoL);
                        loginFuncRepository.Salvar();

                        Console.WriteLine("Login Cadastrado com sucesso \n Pressione Enter para continuar!");
                        Console.ReadKey();
                    } else
                    {
                        Console.Write("Todos os funcionários já possuem logins!\nPressione enter para continuar");
                        Console.ReadKey();
                    }
                    

                }
                else if (opc.ToString().Equals("2"))
                {
                    Console.Write("Logins cadastrados: ");
                    List<Login> listaLogin = loginFuncRepository.BuscarTodos();
                    foreach (Login login in listaLogin)
                    {
                        Funcionario f = funcionarioRepository.BuscarPorCodigo(login.CodigoFuncionario);
                        var sitcao = "";
                        if (f.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nLogin do Usuário: " + f.NomeCompleto);
                        Console.Write(" Situação: ");
                        Console.ForegroundColor = f.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nEmail: " + login.Email + "\nSenha: " + login.Senha);
                    }
                    Console.Write("\nPressione Enter parar continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("3"))
                {
                    List<Login> listaLogin = loginFuncRepository.BuscarTodos();
                    Console.Write("Logins Ativos: \n");
                    foreach (Login login in listaLogin)
                    {
                        Funcionario f = funcionarioRepository.BuscarPorCodigo(login.CodigoFuncionario);
                        Console.Write("\n\nLogin do Usuário: " + f.NomeCompleto + " Id: " + f.Codigo +
                            "\nEmail: " + login.Email +
                            "\nSenha: " + login.Senha);
                    }
                    
                   

                    var idFuncLogin = "";
                    while (idFuncLogin == "")
                    {
                        Console.Write("\nId do funcinário que deseja alterar o login: ");
                        idFuncLogin = Console.ReadLine();
                    }

                    var convertidoLoginId = Convert.ToInt32(idFuncLogin);

                    Login updateLogin = loginFuncRepository.BuscarLoginFuncionario(convertidoLoginId);

                    Console.Clear();

                    Console.Write("Novo Email: ");
                    var updateEmail = Console.ReadLine();

                    Console.Write("Nova Senha: ");
                    var updateSenha = Console.ReadLine();

                    updateLogin.Senha = updateSenha;
                    updateLogin.Email = updateEmail;

                    loginFuncRepository.Atualizar(updateLogin);
                    loginFuncRepository.Salvar();

                    Console.WriteLine("Login Atualizado com sucesso \n Pressione enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("4"))
                {
                    List<Login> listaLogin = loginFuncRepository.BuscarTodos();
                    Console.Write("Logins Ativos: \n");
                    foreach (Login login in listaLogin)
                    {
                        Funcionario f = funcionarioRepository.BuscarPorCodigo(login.CodigoFuncionario);
                        Console.Write("\n\nLogin do Usuário: " + f.NomeCompleto + " Id: " + f.Codigo +
                            "\nEmail: " + login.Email +
                            "\nSenha: " + login.Senha);
                    }


                    var idFuncLogin = "";
                    while (idFuncLogin == "")
                    {
                        Console.Write("\n\nATENÇÃO: O funcionário será desativado junto!! \nId do funcinário que deseja inativer o login: ");
                        idFuncLogin = Console.ReadLine();
                    }

                    var deleteID = Convert.ToInt32(idFuncLogin);

                    loginFuncRepository.Inativar(deleteID, context);
                    funcionarioRepository.Salvar();
                    Console.WriteLine("Desativado com sucesso! \n Pressione Enter para continuar");
                    Console.ReadKey();

                }
                else if (opc.ToString().Equals("5"))
                {
                    List<Login> listaLogin = loginFuncRepository.BuscarTodos();
                    Console.Write("Logins Inativos: \n");
                    foreach (Login login in listaLogin)
                    {
                        Funcionario f = funcionarioRepository.BuscarPorCodigo(login.CodigoFuncionario);
                        if (f.Ativo == -1)
                        {
                            Console.Write("\n\nLogin do Usuário: " + f.NomeCompleto + " Código: " + f.Codigo +
                                "\nEmail: " + login.Email +
                                "\nSenha: " + login.Senha);

                        }
                        else
                        {
                            continue;
                        }
                    }


                    var idFuncLogin = "";
                    while (idFuncLogin == "")
                    {
                        Console.Write("\n\nCodigo do funcinário que deseja ativar o login: ");
                        idFuncLogin = Console.ReadLine();
                    }

                    var deleteID = Convert.ToInt32(idFuncLogin);

                    loginFuncRepository.Ativar(deleteID, context);
                    funcionarioRepository.Salvar();
                    Console.WriteLine("Ativado com sucesso! \n Pressione Enter para continuar");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("6"))
                {
                    loginMenu = false;
                }
                else
                {
                    Console.WriteLine("Opção Inválida");
                }

            }
        }

        static void menuQuadro()
        {
            DbContextOptionsBuilder<Context> optionBuilder = new DbContextOptionsBuilder<Context>();

            optionBuilder.UseSqlServer(" server=DESKTOP-FEB5GB5\\SQLEXPRESS; database = 5W2H; trusted_connection = true; Encrypt = false");
            Context context = new Context(optionBuilder.Options);
            context.Database.EnsureCreated();


            QuadroRepository quadroRepository = new QuadroRepository(context);
            TarefaRepository tarefaRepository = new TarefaRepository(context);  

            var quadroMenu = true;

            while (quadroMenu)
            {
                Console.Clear();
                Console.WriteLine("Quadro, menu de controle!");
                Console.Write("1 - Cadastrar\n" +
                    "2 - Consultar Ativos\n" +
                    "3 - Consultar Geral\n" +
                    "4 - Remover\n" +
                    "5 - Atualizar\n" +
                    "6 - Consultar Tarefas dos quadros\n" +
                    "7 - Sair\n" +
                    "Sua escolha: ");


                var opc = Console.ReadLine();
                Console.Clear();

                if (opc.ToString().Equals("1"))
                {
                    Quadro novoQuadro = new Quadro();

                    Console.WriteLine("Cadastrando novo Quadro!");
                    Console.Write("\nQual o nome do quadro: ");
                    var nomeQuadro = Console.ReadLine();

                    novoQuadro.Nome = nomeQuadro;

                    quadroRepository.Criar(novoQuadro);
                    quadroRepository.Salvar();


                    Console.WriteLine("Quadro Cadastrado com sucesso \n Pressione Enter para continuar!");
                    Console.ReadKey();

                }
                else if (opc.ToString().Equals("2"))
                {
                    Console.WriteLine("Todos os quadros ativos: ");
                    List<Quadro> quad = quadroRepository.BuscarAtivos();
                    foreach (Quadro q in quad)
                    {
                        var sitcao = "";
                        if (q.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nCódigo do quadro: " + q.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = q.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nNome do quadro: " + q.Nome);
                    }
                    Console.WriteLine("\n Pressione Enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("4"))
                {
                    Console.WriteLine("Quadros disponíveis: ");
                    List<Quadro> quad = quadroRepository.BuscarAtivos();
                    foreach (Quadro q in quad)
                    {

                        var sitcao = "";
                        if (q.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nCódigo do quadro: " + q.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = q.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nNome do quadro: " + q.Nome);
                    }

                    Console.Write("\n\nQual o id do quadro que deseja remover: ");
                    var removeQuadro = Console.ReadLine();

                    var removeConvertido = Convert.ToInt32(removeQuadro);

                    quadroRepository.Remover(removeConvertido);
                    quadroRepository.Salvar();
                    Console.Write("\n\nDesativado com sucesso\nPressione enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("3"))
                {
                    List<Quadro> quad = quadroRepository.BuscarTodos();
                    foreach (Quadro q in quad)
                    {
                        var sitcao = "";
                        if (q.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nCódigo do quadro: " + q.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = q.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nNome do quadro: " + q.Nome);

                    }
                    Console.WriteLine("\n\nPressione Enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("5"))
                {
                    Console.WriteLine("Todos os quadros ativos: ");
                    List<Quadro> quad = quadroRepository.BuscarAtivos();
                    foreach (Quadro q in quad)
                    {

                        var sitcao = "";
                        if (q.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nCódigo do quadro: " + q.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = q.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nNome do quadro: " + q.Nome);
                    }

                    
                    

                    var atualiazarQuadro = "";

                    while (atualiazarQuadro == "")
                    {
                        Console.Write("\n\nQual codigo do quadro que deseja atualizar: ");
                        atualiazarQuadro = Console.ReadLine();

                    }
                    var atualiazarQuadroConvertido = Convert.ToInt32(atualiazarQuadro);


                    Console.Clear();

                    Quadro updateQ = quadroRepository.BuscarCodigo(atualiazarQuadroConvertido);
                    Console.Write("\nAtualizando o quadro: " + updateQ.Nome);

                    Console.Write("\n\nNovo nome: ");
                    var novoNome = Console.ReadLine();

                    updateQ.Nome = novoNome;

                    quadroRepository.Atualizar(updateQ);
                    quadroRepository.Salvar();

                    Console.WriteLine("\n\nAtualizado com sucesso \nPressione Enter para continuar!");
                    Console.ReadKey();

                }
                else if (opc.ToString().Equals("6"))
                {
                    Console.WriteLine("Todos os quadros disponíveis: ");
                    List<Quadro> quad = quadroRepository.BuscarAtivos();
                    foreach (Quadro q in quad)
                    {

                        var sitcao = "";
                        if (q.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nCódigo do quadro: " + q.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = q.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nNome do quadro: " + q.Nome);
                    }




                    var atualiazarQuadro = "";

                    while (atualiazarQuadro == "")
                    {
                        Console.Write("\n\nQual codigo do quadro que deseja ver as tarefas: ");
                        atualiazarQuadro = Console.ReadLine();

                    }
                    var atualiazarQuadroConvertido = Convert.ToInt32(atualiazarQuadro);

                    List<Tarefa> tarefaPorQuadro = tarefaRepository.BuscarTodos();
                    Quadro verTarefa = quadroRepository.BuscarCodigo(atualiazarQuadroConvertido);

                    Console.Clear();
                    Console.WriteLine("Tarefas do quadro: " + verTarefa.Nome);
                    foreach(Tarefa t in tarefaPorQuadro)
                    {
                        if(t.CodigoQuadro == atualiazarQuadroConvertido)
                        {
                            Console.Write("\n\nCodigo da Tarefa: " + t.Codigo);
                            Console.Write("  Situação: ");
                            Console.ForegroundColor = t.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.Write(t.Ativo != -1 ? "Ativo" : "Inativo");
                            Console.ResetColor();
                            Console.Write("\nNome: " + t.Nome);
                        }
                    }

                    Console.Write("\n\nPressione enter para continuar!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("7"))
                {
                    quadroMenu = false;
                }
                else
                {
                    Console.WriteLine("Opção Inválida");
                }

            }
        }

        static void menuTarefa()
        {
            DbContextOptionsBuilder<Context> optionBuilder = new DbContextOptionsBuilder<Context>();

            optionBuilder.UseSqlServer(" server=DESKTOP-FEB5GB5\\SQLEXPRESS; database = 5W2H; trusted_connection = true; Encrypt = false");
            Context context = new Context(optionBuilder.Options);
            context.Database.EnsureCreated();

            FuncionarioRepository funcionarioRepository = new FuncionarioRepository(context);
            TarefaRepository tarefaRepository = new TarefaRepository(context);
            QuadroRepository quadroRepository = new QuadroRepository(context);


            var tarefaMenu = true;

            while (tarefaMenu)
            {
                Console.Clear();
                Console.WriteLine("Tarefa, menu de controle!");
                Console.Write("1 - Cadastrar\n" +
                    "2 - Consultar Ativas\n" +
                    "3 - Consultar Geral\n" +
                    "4 - Remover\n" +
                    "5 - Atualizar\n" +
                    "6 - Sair\n" +
                    "Sua escolha: ");


                var opc = Console.ReadLine();
                Console.Clear();

                if (opc.ToString().Equals("1"))
                {
                    Tarefa t = new Tarefa();

                    Console.WriteLine("Cadastrando nova tarefa!\n");
                    Console.Write("Nome da tarefa: ");
                    var nomeTarefa = Console.ReadLine();

                    Console.Write("Descrição da tarefa: ");
                    var descricaoTarefa = Console.ReadLine();

                    Console.Clear();    

                    Console.WriteLine("\n--- Campos 5W2H! --- \n");

                    Console.Write("O que: ");
                    var oque = Console.ReadLine();

                    Console.Write("Onde: ");
                    var onde = Console.ReadLine();

                    Console.Write("Quando: ");
                    var quando = Console.ReadLine();

                    var quandoConvertido = Convert.ToDateTime(quando);

                    Console.Write("Como: ");
                    var como = Console.ReadLine();

                    Console.Write("Quanto: ");
                    var quanto = Console.ReadLine();

                    Console.Write("Quem: ");
                    var quem = Console.ReadLine();

                    Console.Write("Por Que: ");
                    var porQue = Console.ReadLine();

                    t.Nome = nomeTarefa;
                    t.Descricao = descricaoTarefa;
                    t.Oque = oque;
                    t.Onde = onde;
                    t.Quando = quandoConvertido;
                    t.Como = como;
                    t.Quanto = quanto;
                    t.Quem = quem;
                    t.PorQue = porQue;

                    Console.Write("\n\nDeseja adicionar Membros a tarfa, S ou N: ");
                    var resMembro = Console.ReadLine();

                    if (resMembro == "S" || resMembro == "s")
                    {
                        var adicionarMembros = "S";
                        while (adicionarMembros == "S" || adicionarMembros == "s")
                        {

                            Console.Clear();
                            Console.WriteLine("Usuário disponiveis: ");
                            List<Funcionario> func = funcionarioRepository.BuscarAtivos();
                            foreach (Funcionario funcT in func)
                            {
                                var sitcao = "";
                                var gestorPrint = "";
                                if (funcT.Ativo == -1)
                                {
                                    sitcao = "Inativo";
                                }
                                else
                                {
                                    sitcao = "Ativo";
                                }

                                if (funcT.Gestor == 1)
                                {
                                    gestorPrint = "Sim";
                                }
                                else
                                {
                                    gestorPrint = "Não";
                                }
                                Console.WriteLine("\nCódigo: " + funcT.Codigo);
                                Console.Write("Funcionário: " + funcT.NomeCompleto);
                                Console.Write("  Situação: ");
                                Console.ForegroundColor = funcT.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                                Console.Write(sitcao);
                                Console.ResetColor();
                                Console.Write("\nCPF: " + funcT.CPF);
                                Console.Write("\nÉ gestor: ");
                                Console.ForegroundColor = funcT.Gestor != 0 ? ConsoleColor.Green : ConsoleColor.Red;
                                Console.Write(gestorPrint + "\n");
                                Console.ResetColor();
                            }
                            var codFunc = "";
                            while(codFunc == "")
                            {

                                Console.Write("\n\nCódigo do funcionário que deseja adicinar a tarefa: ");
                                codFunc = Console.ReadLine();
                            }

                            var codFuncConvertido = Convert.ToInt32(codFunc);

                            Funcionario f = funcionarioRepository.BuscarPorCodigo(codFuncConvertido);
                            t.Funcionario.Add(f);
                            f.Tarefa.Add(t);

                            Console.Write("Deseja adicinar mais membros, S ou N: ");
                            adicionarMembros = Console.ReadLine();
                        }
                    }

                    Console.Clear();
                    Console.Write("Quadros disponiveis: ");
                    List<Quadro> quad = quadroRepository.BuscarAtivos();
                    foreach (Quadro q in quad)
                    {

                        var sitcao = "";
                        if (q.Ativo == -1)
                        {
                            sitcao = "Inativo";
                        }
                        else
                        {
                            sitcao = "Ativo";
                        }

                        Console.Write("\n\nCódigo do quadro: " + q.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = q.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(sitcao);
                        Console.ResetColor();
                        Console.Write("\nNome do quadro: " + q.Nome);
                    }
                    Console.Write("\n\nCódigo do quadro que sua tarefa será adicionada: ");
                    var codigoQ = Console.ReadLine();

                    var codigoQC = Convert.ToInt32(codigoQ);
                    Quadro qTarefa = quadroRepository.BuscarCodigo(codigoQC);
                    t.CodigoQuadro = qTarefa.Codigo;
                    t.Quadro = qTarefa;
                    qTarefa.Tarefas.Add(t);

                    tarefaRepository.Criar(t);
                    tarefaRepository.Salvar();

                    Console.Write("\n\nTarefa Cadastrada com sucesso \nPressione enter para continuar!");
                    Console.ReadKey();


                }
                else if (opc.ToString().Equals("2"))
                {
                    Console.WriteLine("Tarefas Ativas!");
                    List<Tarefa> tarefasAtivas = context.Tarefa.Include(t => t.Funcionario).Where(t=>t.Ativo != -1).ToList();
                    foreach (Tarefa tarefa in tarefasAtivas)
                    {
                        Console.Write("\n\nCodigo da Tarefa: " + tarefa.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = tarefa.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(tarefa.Ativo != -1 ? "Ativo" : "Inativo");
                        Console.ResetColor();
                        Console.Write("\nNome: " + tarefa.Nome);
                    }

                    var codVerTarefa = "";

                    while(codVerTarefa == "")
                    {
                        Console.Write("\n\nCódigo da tarefa que deseja visualizar");
                        codVerTarefa = Console.ReadLine();

                    }
                    var codVerTarefaConvertido =  Convert.ToInt32(codVerTarefa);
                    Console.Clear();
                    
                    Tarefa verTarefa = tarefaRepository.BuscarPorCodigo(codVerTarefaConvertido);
                    Console.Write("Visualizando a tarefa: " + verTarefa.Nome);

                    Console.WriteLine("\n\nDescrição: " + verTarefa.Descricao);
                    Console.WriteLine("\n --- 5W2H ---");
                    Console.Write("\nO que: " + verTarefa.Oque +
                                  "\nOnde: " + verTarefa.Onde +
                                  "\nQuando: " + verTarefa.Quando.ToString() +
                                  "\nComo: " + verTarefa.Como +
                                  "\nQuanto: R$" + verTarefa.Quanto +
                                  "\nQuem: " + verTarefa.Quem +
                                  "\nPor Que: " + verTarefa.PorQue);


                    Console.Write("\n\nPressione enter para ver mais!");
                    Console.ReadKey();
                    Console.Clear();

                    // quadroRepository.BuscarCodigo(verTarefa.Quadro.Codigo);

                    Quadro verQuadro = quadroRepository.BuscarCodigo(verTarefa.CodigoQuadro);
                    Console.Write("Visualizando a tarefa: " + verTarefa.Nome);
                    Console.Write("\n\nPertencente ao quadro: " + verQuadro.Nome);

                    Console.WriteLine("\nFuncionários atribuidos: ");
                    foreach (Funcionario funcTarefa in verTarefa.Funcionario.ToList())
                    {
                        Console.Write("\n\nNome: " + funcTarefa.NomeCompleto);
                    }


                    Console.Write("\n\nPressione enter para continur!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("3"))
                {
                    Console.WriteLine("Todas as tarefas!");
                    List<Tarefa> tarefasAtivas = tarefaRepository.BuscarTodos();
                    foreach (Tarefa tarefa in tarefasAtivas)
                    {
                        Console.Write("\n\nCodigo da Tarefa: " + tarefa.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = tarefa.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(tarefa.Ativo != -1 ? "Ativo" : "Inativo");
                        Console.ResetColor();
                        Console.Write("\nNome: " + tarefa.Nome);
                    }

                    var codVerTarefa = "";

                    while (codVerTarefa == "")
                    {
                        Console.Write("\n\nCódigo da tarefa que deseja visualizar");
                        codVerTarefa = Console.ReadLine();

                    }
                    var codVerTarefaConvertido = Convert.ToInt32(codVerTarefa);

                    Console.Clear();
                    Tarefa verTarefa = tarefaRepository.BuscarPorCodigo(codVerTarefaConvertido);
                    Console.Write("Visualizando a tarefa: " + verTarefa.Nome);

                    Console.WriteLine("\n\nDescrição: " + verTarefa.Descricao);
                    Console.WriteLine("\n --- 5W2H ---");
                    Console.Write("\nO que: " + verTarefa.Oque +
                                  "\nOnde: " + verTarefa.Onde +
                                  "\nQuando: " + verTarefa.Quando.ToString() +
                                  "\nComo: " + verTarefa.Como +
                                  "\nQuanto: R$" + verTarefa.Quanto +
                                  "\nQuem: " + verTarefa.Quem +
                                  "\nPor Que: " + verTarefa.PorQue);


                    Console.Write("\n\nPressione enter para ver mais!");
                    Console.ReadKey();
                    Console.Clear();

                    // quadroRepository.BuscarCodigo(verTarefa.Quadro.Codigo);

                    Quadro verQuadro = quadroRepository.BuscarCodigo(verTarefa.CodigoQuadro);
                    Console.Write("Visualizando a tarefa: " + verTarefa.Nome);
                    Console.Write("\n\nPertencente ao quadro: " + verQuadro.Nome);

                    Console.WriteLine("\nFuncionários atribuidos: ");
                    foreach (Funcionario funcTarefa in verTarefa.Funcionario)
                    {
                        Console.Write("\n\nNome: " + funcTarefa.NomeCompleto);
                    }


                    Console.Write("\n\nPressione enter para continur!");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("4"))
                {
                    Console.WriteLine("Tarefas Ativas no momento!");
                    List<Tarefa> tarefasAtivas = tarefaRepository.BuscarAtivos();
                    foreach (Tarefa tarefa in tarefasAtivas)
                    {
                        Console.Write("\n\nCodigo da Tarefa: " + tarefa.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = tarefa.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(tarefa.Ativo != -1 ? "Ativo" : "Inativo");
                        Console.ResetColor();
                        Console.Write("\nNome: " + tarefa.Nome);
                    }

                    

                    var codRemoverTarefa = "";

                    while (codRemoverTarefa == "")
                    {
                        Console.Write("\n\nCódigo da tarefa que deseja visualizar");
                        codRemoverTarefa = Console.ReadLine();

                    }
                    var codRemoverTarefaConvertido = Convert.ToInt32(codRemoverTarefa);

                    tarefaRepository.Remover(codRemoverTarefaConvertido);
                    tarefaRepository.Salvar();

                    Console.WriteLine("Tarefa removida com sucesso \nPressione enter para continuar!");
                    Console.ReadKey();

                }
                else if (opc.ToString().Equals("5"))
                {
                    Console.WriteLine("Tarefas Ativas no momento!");
                    List<Tarefa> tarefasAtivas = tarefaRepository.BuscarAtivos();
                    foreach (Tarefa tarefa in tarefasAtivas)
                    {
                        Console.Write("\n\nCodigo da Tarefa: " + tarefa.Codigo);
                        Console.Write("  Situação: ");
                        Console.ForegroundColor = tarefa.Ativo != -1 ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.Write(tarefa.Ativo != -1 ? "Ativo" : "Inativo");
                        Console.ResetColor();
                        Console.Write("\nNome: " + tarefa.Nome);
                    }

                    

                    var codAtualizarTarefa = "";

                    while (codAtualizarTarefa == "")
                    {
                        Console.Write("\n\nCódigo da tarefa que deseja atualizar: ");
                        codAtualizarTarefa = Console.ReadLine();

                    }
                    var codAtualizarTarefaConvertido = Convert.ToInt32(codAtualizarTarefa);

                    Tarefa atualizaTarefa = tarefaRepository.BuscarPorCodigo(codAtualizarTarefaConvertido);
                    Console.Clear();
                    Console.WriteLine("Atulizando tarefa: " + atualizaTarefa.Nome);

                    Console.Write("\nNome da tarefa: ");
                    var nomeTarefa = Console.ReadLine();

                    Console.Write("Descrição da tarefa: ");
                    var descricaoTarefa = Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine("Atulizando tarefa: " + atualizaTarefa.Nome);
                    Console.WriteLine("\n---Campos 5W2H!---\n");

                    Console.Write("O que: ");
                    var oque = Console.ReadLine();

                    Console.Write("Onde: ");
                    var onde = Console.ReadLine();

                    Console.Write("Quando: ");
                    var quando = Console.ReadLine();

                    var quandoConvertido = Convert.ToDateTime(quando);

                    Console.Write("Como: ");
                    var como = Console.ReadLine();

                    Console.Write("Quanto: ");
                    var quanto = Console.ReadLine();

                    Console.Write("Quem: ");
                    var quem = Console.ReadLine();

                    Console.Write("Por Que: ");
                    var porQue = Console.ReadLine();

                    atualizaTarefa.Nome = nomeTarefa;
                    atualizaTarefa.Descricao = descricaoTarefa;
                    atualizaTarefa.Oque = oque;
                    atualizaTarefa.Onde = onde;
                    atualizaTarefa.Quando = quandoConvertido;
                    atualizaTarefa.Como = como;
                    atualizaTarefa.Quanto = quanto;
                    atualizaTarefa.Quem = quem;
                    atualizaTarefa.PorQue = porQue;


                    Console.Write("\n\nDeseja adicionar Membros a tarfa, S ou N: ");
                    var resMembro = Console.ReadLine();
                        
                    
                    if (resMembro == "S" || resMembro == "s")
                    {
                        var adicionarMembros = "S";
                        while (adicionarMembros == "S" || adicionarMembros == "s")
                        {

                            Console.Clear();

                            if (atualizaTarefa.Funcionario.Count == 0)
                            {
                                Console.WriteLine("Tarefa sem Funcionários, disponíves para adicionar: ");
                                List<Funcionario> func = funcionarioRepository.BuscarTodos();
                                List<Funcionario> teste = atualizaTarefa.Funcionario.ToList();
                                var _teste = func.Except(teste);
                                foreach (Funcionario funcT in _teste)
                                {
                                    Console.WriteLine("\nId: " + funcT.Codigo + " \nNome: " + funcT.NomeCompleto.ToString() + "\n CPF: " + funcT.CPF.ToString());
                                }
                                
                                

                                var codFunc = "";

                                while (codFunc == "")
                                {
                                    Console.Write("\n\nCódigo do funcionário que deseja adicinar a tarefa: ");
                                    codFunc = Console.ReadLine();

                                }
                                var codFuncConvertido = Convert.ToInt32(codFunc);

                                Funcionario f = funcionarioRepository.BuscarPorCodigo(codFuncConvertido);
                                atualizaTarefa.Funcionario.Add(f);
                                f.Tarefa.Add(atualizaTarefa);

                            }
                            else
                            {
                                Console.WriteLine(" --- Funcionários já adicionados ---");
                                List<Funcionario> func = atualizaTarefa.Funcionario.ToList();
                                foreach (Funcionario funcT in func)
                                {
                                    Console.WriteLine("\nId: " + funcT.Codigo + " \nNome: " + funcT.NomeCompleto.ToString() + "\n CPF: " + funcT.CPF.ToString());
                                }



                                Console.WriteLine("\n\nDisponíves para adicionar: ");
                                List<Funcionario> funcD = funcionarioRepository.BuscarAtivos();
                                foreach (Funcionario funcT in funcD)
                                {
                                    Console.WriteLine("\nId: " + funcT.Codigo + " \nNome: " + funcT.NomeCompleto.ToString() + "\n CPF: " + funcT.CPF.ToString());
                                }
                                var codFunc = "";

                                while (codFunc == "")
                                {
                                    Console.Write("\n\nCódigo do funcionário que deseja adicinar a tarefa: ");
                                    codFunc = Console.ReadLine();

                                }
                                var codFuncConvertido = Convert.ToInt32(codFunc);

                                Funcionario f = funcionarioRepository.BuscarPorCodigo(codFuncConvertido);
                                atualizaTarefa.Funcionario.Add(f);
                                f.Tarefa.Add(atualizaTarefa);
                            }

                            Console.Write("Deseja adicinar mais membros, S ou N: ");
                            adicionarMembros = Console.ReadLine();
                        }
                    }

                    tarefaRepository.Atualizar(atualizaTarefa);
                    tarefaRepository.Salvar();
                    Console.WriteLine("Atualizada com sucesso! \nPressione enter para continuar.");
                    Console.ReadKey();
                }
                else if (opc.ToString().Equals("6"))
                {
                    tarefaMenu = false;
                }

            }
        }

        static void iniciarQuadros()
        {
            DbContextOptionsBuilder<Context> optionBuilder = new DbContextOptionsBuilder<Context>();

            optionBuilder.UseSqlServer(" server=DESKTOP-FEB5GB5\\SQLEXPRESS; database = 5W2H; trusted_connection = true; Encrypt = false");
            Context context = new Context(optionBuilder.Options);
            context.Database.EnsureCreated();

            QuadroRepository quadroRepository = new QuadroRepository(context);

            List<Quadro> quadrosBase = quadroRepository.BuscarTodos();

            if (quadrosBase.Count == 0)
            {
                Quadro qBase1 = new Quadro();
                Quadro qBase2 = new Quadro();
                Quadro qBase3 = new Quadro();

                qBase1.Nome = "A Fazer";
                qBase2.Nome = "Em Progresso";
                qBase3.Nome = "Finalizada";

                quadroRepository.Criar(qBase1);
                quadroRepository.Criar(qBase2);
                quadroRepository.Criar(qBase3);
                quadroRepository.Salvar();
            }

        }
    }
}



