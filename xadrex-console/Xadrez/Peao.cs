using System;
using xadrex_console.TabuleiroXadrez;


namespace xadrex_console.Xadrez
{
    internal class Peao : Peca
    {
        private PartidaDeXadrez _partida { get; set; }


        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(cor, tab)
        {
            _partida = partida;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                // acima
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                if (QteMovimentos == 0)
                {
                    pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (Tab.PosicaoValida(pos) && PodeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                    }
                }

                // capturar NE
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && PodeCapturar(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // capturar NO
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && PodeCapturar(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // # jogada especial en passant a direita
                if (PodeFazerPassantDireita())
                {
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // # jogada especial en passant a esquerda
                if (PodeFazerPassantEsquerda())
                {
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                    mat[pos.Linha, pos.Coluna] = true;
                }


            }
            else
            {
                // abaixo
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                if (QteMovimentos == 0)
                {
                    pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (Tab.PosicaoValida(pos) && PodeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                    }
                }

                // capturar SE
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && PodeCapturar(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // capturar SO
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && PodeCapturar(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // # jogada especial en passant a direita
                if (PodeFazerPassantDireita())
                {
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // # jogada especial en passant a esquerda
                if (PodeFazerPassantEsquerda())
                {
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }

            return mat;
        }

        private bool PodeFazerPassantEsquerda()
        {
            bool x = false;

            try
            {
                x = (_partida.VulneravelEnPassant == Tab.Peca(Posicao.Linha, Posicao.Coluna - 1) && _partida.VulneravelEnPassant!=null);
            }
            catch
            {
            }
            return x;
        }

        private bool PodeFazerPassantDireita()
        {
            bool x = false;

            try
            {
                x = (_partida.VulneravelEnPassant == Tab.Peca(Posicao.Linha, Posicao.Coluna + 1) && _partida.VulneravelEnPassant != null);
            }
            catch
            {
            }
            return x;
        }

        internal override bool PodeMover(Posicao pos)
        {
            if (!Tab.PosicaoValida(pos))
            {
                return false;
            }
            Peca p = Tab.Peca(pos);
            return p == null;
        }

        internal bool PodeCapturar(Posicao pos)
        {
            if (!Tab.PosicaoValida(pos))
            {
                return false;
            }
            Peca p = Tab.Peca(pos);
            return p?.Cor != Cor && p?.Cor != null;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
