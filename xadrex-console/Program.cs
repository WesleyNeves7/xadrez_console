
using xadrex_console;
using xadrex_console.Tabuleiro;
using xadrex_console.Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posicao = new PosicaoXadrez('C', 7);
            Console.WriteLine(posicao);
            Console.WriteLine(posicao.ToPosicao());
        }
    }
}