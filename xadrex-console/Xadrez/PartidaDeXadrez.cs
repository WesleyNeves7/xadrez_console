using xadrex_console.TabuleiroXadrez;

namespace xadrex_console.Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }

        public int Turno { get; private set; }

        public Cor JogadorAtual { get; private set; }

        public bool Terminada { get; private set; }

        public bool Xeque { get; private set; }

        public Peca VulneravelEnPassant { get; private set; }

        private HashSet<Peca> _pecas;

        private HashSet<Peca> _capturadas;


        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Xeque = false;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();
            VulneravelEnPassant = null;
            ColocarPecas();
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            else if (JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            else if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça selecionada!");
            }
        }

        private void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            try
            {
                ValidarPosicaoDestino(origem, destino);

                Peca peca = Tab.Peca(origem);

                Peca pecaDestino = Tab.Peca(destino);

                ExecutaMovimento(origem, destino);

                #region Jogada especial roque

                // # jogada especial roque pequeno
                if (peca is Rei && destino.Coluna == origem.Coluna + 2)
                {
                    Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                    Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                    ExecutaMovimento(origemT, destinoT);
                }

                // # jogada especial roque grande
                if (peca is Rei && destino.Coluna == origem.Coluna - 2)
                {
                    Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                    Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                    ExecutaMovimento(origemT, destinoT);
                }

                #endregion

                #region Jogada especial en passant

                // # jogada especial en passant (tornar peça vulneravel)
                if (peca is Peao && (origem.Linha + 2 == destino.Linha || origem.Linha - 2 == destino.Linha))
                {
                    VulneravelEnPassant = peca;
                }
                else
                {
                    VulneravelEnPassant = null;
                }

                // # jogada especial en passant (capturar peça vulneravel)


                bool x = peca is Peao && pecaDestino == null;

                if (peca is Peao
                    && pecaDestino == null
                    && (origem.Coluna == destino.Coluna + 1)
                    || (origem.Coluna == destino.Coluna - 1))
                {
                    Peca pecaCapturada = Tab.RetirarPeca(new Posicao(origem.Linha, destino.Coluna));
                    _capturadas.Add(pecaCapturada);
                }

                #endregion

                if (EstaEmXeque(Adversario(JogadorAtual)))
                {
                    Xeque = true;
                    TesteXequeMate(Adversario(JogadorAtual));
                }
                else
                {
                    Xeque = false;
                }

                MudaJogador();
            }
            catch (TabuleiroException ex)
            {
                throw ex;
            }
        }

        private void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
            {
                _capturadas.Add(pecaCapturada);
            }

            if (EstaEmXeque(peca.Cor))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                if (Xeque)
                {
                    throw new TabuleiroException("O seu rei em cheque!");
                }
                else
                {
                    throw new TabuleiroException("Esta jogada não pode ser executada pois colocaria o seu rei em cheque!");
                }
            }
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tab.RetirarPeca(destino);
            peca.DecrementarQtdMovimentos();
            Tab.ColocarPeca(peca, origem);

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                Turno++;
                JogadorAtual = Cor.Branca;
            }
        }

        private void ColocarNovaPeca(Peca peca, char coluna, int linha)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'A', 1);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Branca), 'B', 1);
            ColocarNovaPeca(new Bispo(Tab, Cor.Branca), 'C', 1);
            ColocarNovaPeca(new Dama(Tab, Cor.Branca), 'D', 1);
            ColocarNovaPeca(new Rei(Tab, Cor.Branca, this), 'E', 1);
            ColocarNovaPeca(new Bispo(Tab, Cor.Branca), 'F', 1);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Branca), 'G', 1);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'H', 1);

            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'A', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'B', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'C', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'D', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'E', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'F', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'G', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca, this), 'H', 2);

            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'A', 8);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Preta), 'B', 8);
            ColocarNovaPeca(new Bispo(Tab, Cor.Preta), 'C', 8);
            ColocarNovaPeca(new Dama(Tab, Cor.Preta), 'D', 8);
            ColocarNovaPeca(new Rei(Tab, Cor.Preta, this), 'E', 8);
            ColocarNovaPeca(new Bispo(Tab, Cor.Preta), 'F', 8);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Preta), 'G', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'H', 8);

            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'A', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'B', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'C', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'D', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'E', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'F', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'G', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta, this), 'H', 7);
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            return _capturadas.Where(i => i.Cor == cor).ToHashSet();
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = _pecas.Where(i => i.Cor == cor).ToHashSet();
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Peca? Rei(Cor cor)
        {
            return PecasEmJogo(cor).Where(i => i is Rei).FirstOrDefault();
        }

        private bool EstaEmXeque(Cor cor)
        {
            Peca? R = Rei(cor);

            if (R == null)
            {
                throw new TabuleiroException($"Não existe um rei da cor {cor}!");
            }

            foreach (Peca peca in PecasEmJogo(Adversario(cor)))
            {
                if (peca.MovimentosPossiveis()[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool CasaEstaEmXeque(Cor cor, Posicao casa)
        {
            foreach (Peca peca in PecasEmJogo(Adversario(cor)))
            {
                if (peca.MovimentosPossiveis()[casa.Linha, casa.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        private bool TesteXequeMate(Cor cor)
        {

            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();

                for (int linha = 0; linha < Tab.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tab.Colunas; coluna++)
                    {
                        if (mat[linha, coluna])
                        {
                            try
                            {
                                if (TestaMovimentoSaidaXeque(peca.Posicao, new Posicao(linha, coluna)))
                                {
                                    return false;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            Terminada = true;
            return true;

        }

        private bool TestaMovimentoSaidaXeque(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
            {
                _capturadas.Add(pecaCapturada);
            }

            bool x = false;

            if (!EstaEmXeque(peca.Cor))
            {
                x = true;
            }

            DesfazMovimento(origem, destino, pecaCapturada);

            return x;
        }

        public Cor Adversario(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
    }
}
