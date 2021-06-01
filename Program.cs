using System;

class Program
{
    static void Main()
    {
        char[,] matriz = GerarMatriz();
        
        IniciarJogo(matriz);
    }

    static char[,] GerarMatriz()
    {
        int numero = '1';
        char[,] matriz = new char[3, 3];

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                matriz[i, j] = (char) numero++;
            }
        }

        return matriz;
    }

    static void IniciarJogo(char[,] matriz)
    {
        int contador = 0;
        char posicao = '0';
        Console.Write("Digite o nome do primeiro jogador: ");
        string jogador1 = Console.ReadLine();

        Console.Write("Digite o nome do segundo jogador: ");
        string jogador2 = Console.ReadLine();

        do {
            Console.Clear();

            contador = ExibirJogo(matriz, posicao, contador);

            if (contador % 2 == 0) {
                if (!ConferirJogo(matriz)) {
                    do {
                        Console.Write("{0}, vai jogar [X] em qual posicao? ", jogador1);
                        posicao = char.Parse(Console.ReadLine().Substring(0, 1));

                        // Validação para valor inválido ou repetido
                        if (!VerificarPalpite(matriz, posicao)) {
                            Console.WriteLine("VALOR INVALIDO!");
                        }
                    } while (!VerificarPalpite(matriz, posicao));
                }
            } else {
                if (!ConferirJogo(matriz)) {
                    do {
                        Console.Write("{0}, vai jogar [O] em qual posicao? ", jogador2);
                        posicao = char.Parse(Console.ReadLine().Substring(0, 1));

                        // Validação para valor inválido ou repetido
                        if (!VerificarPalpite(matriz, posicao)) {
                            Console.WriteLine("VALOR INVALIDO!");
                        }
                    } while (!VerificarPalpite(matriz, posicao));
                }
            }
        } while (!ConferirJogo(matriz));

        Console.WriteLine("JOGO FINALIZADO!!!");

        if (contador < 9) {
            // Houve um vencedor
            if (contador % 2 == 1) {
                Console.WriteLine("JOGADOR VENCEDOR: " + jogador1);
            } else {
                Console.WriteLine("JOGADOR VENCEDOR: " + jogador2);
            }
        } else {
            Console.WriteLine("EMPATE!");
        }

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    static int ExibirJogo(char[,] matriz, char posicao, int contador)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\tJOGO DA VELHA");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\t+---+---+---+");

        for (int i = 0; i < 3; i++) {
            Console.Write("\t");

            for (int j = 0; j < 3; j++) {
                if (matriz[i, j] == posicao) {
                    matriz[i, j] = contador % 2 == 0 ? 'X' : 'O';
                    contador++;
                }
                Console.Write("|");

                if (matriz[i, j] == 'X') {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" {0} ", matriz[i, j]);
                } else if (matriz[i, j] == 'O') {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" {0} ", matriz[i, j]);
                } else {
                    Console.Write(" {0} ", matriz[i, j]);
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("|\n\t+---+---+---+");
        }

        return contador;
    }

    static bool ConferirJogo(char[,] matriz)
    {
        bool fimDeJogo = false;
        int contador = 0;

        // Diagonal
        if (matriz[0, 0] == matriz[1, 1] && matriz[1, 1] == matriz[2, 2]) {
            fimDeJogo = true;
        }

        if (matriz[0, 2] == matriz[1, 1] && matriz[1, 1] == matriz[2, 0]) {
            fimDeJogo = true;
        }

        // Horizontal
        if (matriz[0, 0] == matriz[0, 1] && matriz[0, 1] == matriz[0, 2]) {
            fimDeJogo = true;
        }

        if (matriz[1, 0] == matriz[1, 1] && matriz[1, 1] == matriz[1, 2]) {
            fimDeJogo = true;
        }

        if (matriz[2, 0] == matriz[2, 1] && matriz[2, 1] == matriz[2, 2]) {
            fimDeJogo = true;
        }

        // Vertical
        if (matriz[0, 0] == matriz[1, 0] && matriz[1, 0] == matriz[2, 0]) {
            fimDeJogo = true;
        }

        if (matriz[0, 1] == matriz[1, 1] && matriz[1, 1] == matriz[2, 1]) {
            fimDeJogo = true;
        }

        if (matriz[0, 2] == matriz[1, 2] && matriz[1, 2] == matriz[2, 2]) {
            fimDeJogo = true;
        }

        // Velha
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (matriz[i, j] == 'X' || matriz[i, j] == 'O') {
                    contador++;
                }
            }
        }

        if (contador == 9) fimDeJogo = true;

        return fimDeJogo;
    }

    static bool VerificarPalpite(char[,] matriz, char posicao)
    {
        bool palpite = false;

        if (posicao < '1' || posicao > '9') return false;

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (matriz[i, j] == posicao) {
                    palpite = true;
                }
            }
        }

        return palpite;
    }
}
