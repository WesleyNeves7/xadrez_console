using System;
using xadrex_console.TabuleiroXadrez;


namespace xadrex_console.Xadrez
{
    internal class Rei : Peca
    {
        public Rei(TabuleiroXadrez.Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
