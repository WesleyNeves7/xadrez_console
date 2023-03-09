
using xadrex_console;
using xadrex_console.Tabuleiro;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            tabuleiro tabuleiro = new tabuleiro(8, 8);
            Tela.ImprimirTabuleiro(tabuleiro);
        }
    }
}