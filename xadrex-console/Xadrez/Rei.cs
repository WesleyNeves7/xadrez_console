using System;
using xadrex_console.Tabuleiro;


namespace xadrex_console.Xadrez
{
    internal class Rei : Peca
    {
        public Rei(tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
