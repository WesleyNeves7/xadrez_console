﻿using xadrex_console.TabuleiroXadrez;

namespace xadrex_console.Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        
        public int Turno { get; private set; }
        
        public Cor JogadorAtual { get; private set; }
        
        public bool Terminada { get; private set; }

        public bool Xeque { get; private set; }
        
        private HashSet<Peca> Pecas;
        
        private HashSet<Peca> Capturadas;


        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
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
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            try
            {
                ValidarPosicaoDestino(origem, destino);
                ExecutaMovimento(origem, destino);

                if (EstaEmXeque(Adversario(JogadorAtual)))
                {
                    Xeque = true;
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
                Capturadas.Add(pecaCapturada);
            }


            if (EstaEmXeque(peca.Cor))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Esta jogada não pode ser executada pois colocaria o seu rei em cheque!");
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
                Capturadas.Remove(pecaCapturada);
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
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'C', 1);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'C', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'D', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'E', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'E', 1);
            ColocarNovaPeca(new Rei(Tab, Cor.Branca), 'D', 1);

            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'C', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'C', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'D', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'E', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'E', 7);
            ColocarNovaPeca(new Rei(Tab, Cor.Preta), 'D', 8);

        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            return Capturadas.Where(i => i.Cor == cor).ToHashSet();
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = Pecas.Where(i => i.Cor == cor).ToHashSet();
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

        private Cor Adversario(Cor cor)
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
