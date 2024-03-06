
namespace xadrex_console.TabuleiroXadrez
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }

        public ConsoleColor CorImpressao
        {
            get
            {
                if (Cor == Cor.Branca)
                {
                    return ConsoleColor.White;
                }
                else
                {
                    return ConsoleColor.Magenta;
                }
            }
        }

        public int QteMovimentos { get; set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            Tab = tab;
        }

        public bool MovimentoPossivel(Posicao destino)
        {
            return MovimentosPossiveis()[destino.Linha, destino.Coluna];

        }

        internal void IncrementarQtdMovimentos()
        {
            QteMovimentos++;
        }

        internal void DecrementarQtdMovimentos()
        {
            QteMovimentos--;
        }

        internal bool PodeMover(Posicao pos)
        {
            if (!Tab.PosicaoValida(pos))
            {
                return false;
            }
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            foreach (bool x in mat)
            {
                if (x)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
