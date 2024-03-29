﻿using xadrex_console.TabuleiroXadrez;

namespace xadrex_console.Xadrez
{
    internal class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }


        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'A');
        }
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
