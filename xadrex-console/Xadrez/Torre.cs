using System;
using System.Xml;
using xadrex_console.TabuleiroXadrez;

namespace xadrex_console.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(cor, tab)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

            while (PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha--;
            }

            // abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

            while (PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha++;
            }

            // direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna+1);

            while (PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna++;
            }

            // esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);

            while (PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna--;
            }


            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}