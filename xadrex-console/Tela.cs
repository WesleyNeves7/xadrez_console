using System;
using System.Runtime.Intrinsics.X86;
using xadrex_console.TabuleiroXadrez;
using xadrex_console.Xadrez;

namespace xadrex_console
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirPecasCapturadas(partida, Cor.Branca);
            Console.WriteLine();

            ImprimirTabuleiro(partida.Tab);

            Console.WriteLine();
            ImprimirPecasCapturadas(partida, Cor.Preta);

            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Jogador Atual: " + partida.JogadorAtual);
        }

        public static void ImprimirPartida(PartidaDeXadrez partida, bool[,] posicoesPossiveis)
        {
            ImprimirPecasCapturadas(partida, Cor.Branca);
            Console.WriteLine();

            ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

            Console.WriteLine();
            ImprimirPecasCapturadas(partida, Cor.Preta);

            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Jogador Atual: " + partida.JogadorAtual);
        }


        private static void ImprimirPecasCapturadas(PartidaDeXadrez partida, Cor cor)
        {
            Console.Write($"Peças capturadas (Cor: {cor}): ");
            ImprimirConjunto(partida.PecasCapturadas(cor));
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            int x = 1;
            foreach (Peca peca in conjunto)
            {
                if (x > 1)
                {
                    Console.Write(", ");
                }
                ImprimirPecaSemEspaco(peca);
                x++;
            }
            Console.WriteLine("]");
        }

        private static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "|  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPecaComEspaco(tab.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine(" |__________________");

            Console.WriteLine("    A B C D E F G H");
        }

        private static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;


            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "|  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPecaComEspaco(tab.Peca(i, j));

                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine(" |__________________");

            Console.WriteLine("    A B C D E F G H");

            Console.BackgroundColor = fundoOriginal;
        }

        private static void ImprimirPecaComEspaco(Peca peca)
        {
            if (peca != null)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = peca.CorImpressao;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            else
            {
                Console.Write("-");
            }
            Console.Write(" ");
        }

        private static void ImprimirPecaSemEspaco(Peca peca)
        {
            if (peca != null)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = peca.CorImpressao;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            else
            {
                Console.Write("-");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine().ToUpper();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
