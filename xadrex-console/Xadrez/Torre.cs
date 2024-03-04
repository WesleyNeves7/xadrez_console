using System;
using xadrex_console.TabuleiroXadrez;

namespace xadrex_console.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "T";
        }
    }
}