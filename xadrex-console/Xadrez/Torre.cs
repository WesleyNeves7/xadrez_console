using System;
using xadrex_console.Tabuleiro;

namespace xadrex_console.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "T";
        }
    }
}