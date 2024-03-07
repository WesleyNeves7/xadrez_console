using xadrex_console;
using xadrex_console.TabuleiroXadrez;
using xadrex_console.Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirPartida(partida, posicoesPossiveis);

                        Console.WriteLine();

                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                        try
                        {
                            partida.RealizaJogada(origem, destino);
                        }
                        catch (TabuleiroException ex)
                        {
                            if (ex.Message.Contains("promovido"))
                            {
                            repetir:

                                Console.WriteLine();
                                Console.Write("Escolha a peça que deseja para promover o Peão [B: Bispo; C: Cavalo; D: Dama, T: Torre]:");

                                string strPecaPromovida = Console.ReadLine().ToUpper().Trim();
                                Peca pecaPromovida = null;

                                if (strPecaPromovida == "B")
                                {
                                    pecaPromovida = new Bispo(partida.Tab, partida.JogadorAtual);
                                }
                                else if (strPecaPromovida == "C")
                                {
                                    pecaPromovida = new Cavalo(partida.Tab, partida.JogadorAtual);
                                }
                                else if (strPecaPromovida == "D")
                                {
                                    pecaPromovida = new Dama(partida.Tab, partida.JogadorAtual);
                                }
                                else if (strPecaPromovida == "T")
                                {
                                    pecaPromovida = new Torre(partida.Tab, partida.JogadorAtual);
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Peça inválida!");
                                    goto repetir;
                                }

                                partida.RealizaJogada(origem, destino, pecaPromovida);
                            }
                        }
                    }
                    catch (TabuleiroException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);
                Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}