
namespace xadrex_console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QteMovimentos { get; set; }
        public  tabuleiro Tab { get; set; }

        public Peca(Cor cor, tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            Tab = tab;
        }
    }
}
