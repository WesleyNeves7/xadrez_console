﻿using System;
using xadrex_console.Tabuleiro;

namespace xadrex_console
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(tabuleiro tab)
        {
            for (int i=0; i<tab.Linhas; i++)
            {
                for (int j=0; j<tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) != null)
                    {
                        Console.Write(tab.Peca(i, j) + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
