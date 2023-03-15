using System;
using xadrex_console.Tabuleiro;

namespace xadrex_console
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(tabuleiro tab)
        {
            for (int i=0; i<tab.Linhas; i++)
            {
                Console.Write(8 - i + "|  ");
                for (int j=0; j<tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) != null)
                    {
                        Tela.ImprimirPeca(tab.Peca(i, j));
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(" |__________________");

            Console.WriteLine("    A B C D E F G H");
        }
        public static void ImprimirPeca (Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
