
namespace xadrex_console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QteMovimentos { get; set; }
        public  tabuleiro Tab { get; set; }

        public Peca(Posicao posicao, Cor cor, tabuleiro tab)
        {
            Posicao = posicao;
            Cor = cor;
            QteMovimentos = 0;
            Tab = tab;
        }
    }
}
