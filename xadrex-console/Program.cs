
using xadrex_console;
using xadrex_console.Tabuleiro;
using xadrex_console.Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            tabuleiro tabuleiro = new tabuleiro(8, 8);
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(2, 4));



            Tela.ImprimirTabuleiro(tabuleiro);
        }
    }
}