using System;

namespace gabrielpetillo_d3_avaliacao
{
    class Program
    {
        static void Main(string[] args)
        {
            string menuAction;

            Console.WriteLine("========== CTEDS D3: Login ==========\n");
            Console.WriteLine("Selecione uma das ações abaixo:\n");
            Console.WriteLine("1. Acessar o sistema (Login)");
            Console.WriteLine("2. Cancelar (Exit)\n");
            Console.Write(">> Ação: ");
            menuAction = Console.ReadLine();

            Console.Write("menu: " + menuAction);
        }
    }
}
