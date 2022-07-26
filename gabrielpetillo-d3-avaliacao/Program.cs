using System;
using System.Threading;
using Spectre.Console;
using gabrielpetillo_d3_avaliacao.Repositories;
using gabrielpetillo_d3_avaliacao.Models;
using System.Text;

namespace gabrielpetillo_d3_avaliacao
{
    class Program
    {
        public static string systemPrefix = "#CTEDS-LOGIN: ";

        static void Main(string[] args)
        {
            string menuAction;

            do
            {
                mainMenu();
                menuAction = Console.ReadLine();

                UserRepository userRepository = new UserRepository();
                if (menuAction == "1")
                {
                    if (userRepository.SearchAll().Count == 0)
                    {
                        Console.WriteLine("\n" + systemPrefix + "Não há nenhum usuário cadastrado!\n");
                    }
                    else
                    {
                        Console.Write("\n>> Email: ");
                        string userEmail = Console.ReadLine();
                        Console.Write("\n>> Senha: ");
                        string userPassword = Console.ReadLine();

                        User user = userRepository.SearchByEmail(userEmail);

                        try
                        {
                            if (user != null && user.Email == userEmail && user.Password == userPassword)
                            {
                                user.Create(user, "Logged in");
                                AnsiConsole.Markup("\n[lime] Login realizado com sucesso [/]\n");
                                Thread.Sleep(2000);
                                Console.Clear();
                                do
                                {
                                    AnsiConsole.Markup("[yellow]==================== CTEDS D3: Dashboard ====================[/]\n\n");
                                    Console.WriteLine(systemPrefix + "Selecione uma das ações abaixo:\n");
                                    AnsiConsole.Markup("[aqua]1. Deslogar (Logout)[/]\n");
                                    AnsiConsole.Markup("[aqua]2. Encerrar Sistema (Exit)[/]\n");
                                    AnsiConsole.Markup("[aqua]3. Deletar Usuário (Delete User)[/]\n\n");
                                    AnsiConsole.Markup("[grey]>> Ação: [/]");
                                    menuAction = Console.ReadLine();

                                    if (menuAction == "4")
                                    {
                                        if (userRepository.Delete(user.IdUser))
                                        {
                                            AnsiConsole.Markup("\n[lime] Usuário deletado com sucesso [/]\n");
                                            Thread.Sleep(2000);
                                            menuAction = "1";
                                        }
                                        else
                                        {
                                            AnsiConsole.Markup("\n[red] Erro ao deletar usuário [/]\n");
                                            Thread.Sleep(2000);
                                            menuAction = "1";
                                        }
                                    }

                                } while (menuAction != "1" && menuAction != "2");
                                user.Create(user, "Logged out");
                                Console.Clear();
                            }
                            else
                            {
                                AnsiConsole.Markup("\n[red] Credenciais inválidas!\n Tente novamente. [/]\n");
                                Thread.Sleep(2000);
                                Console.Clear();
                            }

                        }
                        catch (System.IO.IOException)
                        {
                            Console.WriteLine("\n"+systemPrefix + " Feche o arquivo 'users.csv' antes de realizar o login\n");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(systemPrefix + " Erro ao buscar usuário:\n " + e.ToString() + "\n");
                            Thread.Sleep(2000);
                            //Console.Clear();
                        }

                    }
                }
                else if (menuAction == "3")
                {
                    User createUser = new User();
                    Console.Write("\n>> Novo Nome: ");
                    createUser.Name = Console.ReadLine();
                    Console.Write("\n>> Novo Email: ");
                    createUser.Email = Console.ReadLine();
                    Console.Write("\n>> Nova Senha: ");
                    createUser.Password = Console.ReadLine();

                    if (createUser.Name.Length < 4)
                    {
                        AnsiConsole.Markup("\n[red] Nome de usuário precisa te 5 ou + caracteres [/]\n");
                        Thread.Sleep(2000);
                        menuAction = "1";
                        Console.Clear();
                    } else if (!createUser.Email.Contains('@'))
                    {
                        AnsiConsole.Markup("\n[red] Insira um email válido [/]\n");
                        Thread.Sleep(2000);
                        menuAction = "1";
                        Console.Clear();
                    }
                    else if (createUser.Password.Length < 8)
                    {
                        AnsiConsole.Markup("\n[red] Senha precisa ter mais 8 ou + caracteres  [/]\n");
                        Thread.Sleep(2000);
                        menuAction = "1";
                        Console.Clear();
                    } else if (userRepository.Create(createUser,"New User"))
                    {
                        AnsiConsole.Markup("\n[lime] Usuário criado com sucesso [/]\n");
                        Thread.Sleep(2000);
                        menuAction = "1";
                        Console.Clear();
                    }
                    else
                    {
                        AnsiConsole.Markup("\n[red] Erro ao criar usuário [/]\n");
                        Thread.Sleep(2000);
                        menuAction = "1";
                        Console.Clear();
                    }
                }
            } while (menuAction == "1" || menuAction == "3");
            AnsiConsole.Markup("\n[red] Encerrando... [/]\n");
            Thread.Sleep(2000);
        }

        static void mainMenu()
        {
            AnsiConsole.Markup("[blue]==================== CTEDS D3: Login ====================[/]\n\n");
            Console.WriteLine(systemPrefix + "Selecione uma das ações abaixo:\n");
            AnsiConsole.Markup("[aqua]1. Acessar o sistema (Login)[/]\n");
            AnsiConsole.Markup("[aqua]2. Cancelar (Exit)[/]\n");
            AnsiConsole.Markup("[aqua]3. Cadastrar Usuário (Logon)[/]\n\n");
            AnsiConsole.Markup("[grey]>> Ação: [/]");
        }
    }
}
