
using xadrex_console;
using xadrex_console.Tabuleiro;
using xadrex_console.Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                tabuleiro tabuleiro = new tabuleiro(8, 8);
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 2));
                Tela.ImprimirTabuleiro(tabuleiro);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}