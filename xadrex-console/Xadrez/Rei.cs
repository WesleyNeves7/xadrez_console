using xadrex_console.TabuleiroXadrez;


namespace xadrex_console.Xadrez
{
    internal class Rei : Peca
    {
        private PartidaDeXadrez _partida { get; set; }

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(cor, tab)
        {
            _partida = partida;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // norte
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // leste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // sul
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // oeste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // # jogada especial roque
            if (_partida.JogadorAtual == Cor)
            {
                if (PodeFazerRoquePequeno())
                {
                    pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 2);
                    mat[pos.Linha, pos.Coluna] = true;
                }
                if (PodeFazerRoqueGrande())
                {
                    pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 2);
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }

            return mat;
        }


        private bool PodeFazerRoquePequeno()
        {
            try
            {
                if (!_partida.Xeque)
                {
                    if (!_partida.CasaEstaEmXeque(Cor, new Posicao(Posicao.Linha, Posicao.Coluna + 1)) && Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna + 1)) == null
                        && !_partida.CasaEstaEmXeque(Cor, new Posicao(Posicao.Linha, Posicao.Coluna + 2)) && Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna + 2)) == null)
                    {
                        Peca peca = Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna + 3));

                        if (QteMovimentos == 0 && peca?.QteMovimentos == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        private bool PodeFazerRoqueGrande()
        {
            try
            {
                if (!_partida.Xeque)
                {
                    if (!_partida.CasaEstaEmXeque(Cor, new Posicao(Posicao.Linha, Posicao.Coluna - 1)) && Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna - 1)) == null
                        && !_partida.CasaEstaEmXeque(Cor, new Posicao(Posicao.Linha, Posicao.Coluna - 2)) && Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna - 2)) == null
                        && Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna - 3)) == null)
                    {
                        Peca peca = Tab.Peca(new Posicao(Posicao.Linha, Posicao.Coluna - 4));

                        if (QteMovimentos == 0 && peca?.QteMovimentos == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }


        public override string ToString()
        {
            return "R";
        }
    }
}
