
namespace xadrex_console.TabuleiroXadrez
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QteMovimentos { get; set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            Tab = tab;
        }

        public void incrementarQtdMovimentos()
        {
            QteMovimentos++;
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

        public abstract bool[,] MovimentosPossiveis();
    }
}
