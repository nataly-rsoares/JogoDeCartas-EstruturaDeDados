class Program
{
    static void Main()
    {
        Pilha monte = new Pilha();
        Pilha morto = new Pilha();
        Pilha jogador1 = new Pilha();
        Pilha jogador2 = new Pilha();

        GerarCartas(monte);
        EmbaralharCartas(monte);

        string[] numerosJogador1;
        string[] numerosJogador2;

        Console.WriteLine("Digite as cartas para o Jogador 1 e 2. Lembre-se as Carta do baralho são: A, 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K");
        do
        {
            Console.WriteLine("Digite as cartas para o Jogador 1 (separe por vírgula):");
            string numerosJogador1Input = Console.ReadLine();
            numerosJogador1 = numerosJogador1Input!.Split(',');

            if (numerosJogador1.Length > 5)
            {
                Console.WriteLine("Digite no máximo 5 números para o Jogador 1.");
            }
        } while (numerosJogador1.Length > 5);

        do
        {
            Console.WriteLine("Digite o número para o Jogador 2 (separe os números por vírgula):");
            string numerosJogador2Input = Console.ReadLine();
            numerosJogador2 = numerosJogador2Input!.Split(',');

            if (numerosJogador2.Length > 5)
            {
                Console.WriteLine("Digite no máximo 5 números para o Jogador 2.");
            }
        } while (numerosJogador2.Length > 5);

        SimularJogo(monte, morto, jogador1, jogador2, numerosJogador1, numerosJogador2);

        int pontuacaoJogador1 = jogador1.Empty() ? 0 : CalcularPontuacao(jogador1);
        int pontuacaoJogador2 = jogador2.Empty() ? 0 : CalcularPontuacao(jogador2);

        Console.WriteLine("Pontuação Jogador 1: {0}", pontuacaoJogador1);
        Console.WriteLine("Pontuação Jogador 2: {0}", pontuacaoJogador2);

        string vencedor = pontuacaoJogador1 > pontuacaoJogador2 ? "Jogador 1" : "Jogador 2";
        Console.WriteLine("O vencedor é: {0}", vencedor);

        Console.ReadLine();
    }

    static void GerarCartas(Pilha monte)
    {
        string[] naipes = { "Copas", "Espadas", "Ouros", "Paus" };
        string[] numeros = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };


        foreach (string numero in numeros)
        {
            foreach (string naipe in naipes)
            {
                CartaDoBaralho carta = new CartaDoBaralho(numero, naipe);
                monte.Push(carta);
            }
        }
    }

    static void EmbaralharCartas(Pilha monte)
    {
        List<CartaDoBaralho> cartas = new List<CartaDoBaralho>();
        Random random = new Random();

        while (!monte.Empty())
        {
            cartas.Add(monte.Pop());
        }

        while (cartas.Count > 0)
        {
            int index = random.Next(0, cartas.Count);
            CartaDoBaralho carta = cartas[index];
            monte.Push(carta);
            cartas.RemoveAt(index);
        }
    }

    static void SimularJogo(Pilha monte, Pilha morto, Pilha jogador1, Pilha jogador2, string[] numerosJogador1, string[] numerosJogador2)
    {
        Random random = new Random();

        while (!monte.Empty())
        {
            // Jogador 1 retira uma carta do monte
            CartaDoBaralho cartaJogador1 = monte.Pop();
            Console.WriteLine("Jogador 1: Retirou a carta {0} de {1}", cartaJogador1.Numero, cartaJogador1.Naipe);

            // Jogador 2 retira uma carta do monte
            CartaDoBaralho cartaJogador2 = monte.Pop();
            Console.WriteLine("Jogador 2: Retirou a carta {0} de {1}", cartaJogador2.Numero, cartaJogador2.Naipe);

            // Verifica se a carta do Jogador 1 corresponde ao número escolhido
            if (numerosJogador1.Contains(cartaJogador1.Numero))
            {
                jogador1.Push(cartaJogador1);
                Console.WriteLine("Jogador 1: Ficou com a carta {0} de {1}", cartaJogador1.Numero, cartaJogador1.Naipe);
            }
            else
            {
                morto.Push(cartaJogador1);
                Console.WriteLine("Jogador 1: Descartou a carta {0} de {1}", cartaJogador1.Numero, cartaJogador1.Naipe);
            }

            // Verifica se a carta do Jogador 2 corresponde ao número escolhido
            if (numerosJogador2.Contains(cartaJogador2.Numero))
            {
                jogador2.Push(cartaJogador2);
                Console.WriteLine("Jogador 2: Ficou com a carta {0} de {1}", cartaJogador2.Numero, cartaJogador2.Naipe);
            }
            else
            {
                morto.Push(cartaJogador2);
                Console.WriteLine("Jogador 2: Descartou a carta {0} de {1}", cartaJogador2.Numero, cartaJogador2.Naipe);
            }
        }
    }

    static int CalcularPontuacao(Pilha pilha)
    {
        int pontuacao = 0;

        while (!pilha.Empty())
        {
            CartaDoBaralho carta = pilha.Pop();
            switch (carta.Numero)
            {
                case "A":
                    pontuacao += 5;
                    break;
                case "J":
                    pontuacao += 4;
                    break;
                case "Q":
                    pontuacao += 3;
                    break;
                case "K":
                    pontuacao += 2;
                    break;
                default:
                    int numero;
                    if (int.TryParse(carta.Numero, out numero))
                    {
                        pontuacao += numero;
                    }
                    break;
            }
        }
        return pontuacao;
    }
}

class Pilha
{
    private List<CartaDoBaralho> cartas;

    public Pilha()
    {
        cartas = new List<CartaDoBaralho>();
    }

    public void Clean()
    {
        cartas.Clear();
    }

    public bool Empty()
    {
        return cartas.Count == 0;
    }

    public void Push(CartaDoBaralho carta)
    {
        cartas.Add(carta);
    }

    public CartaDoBaralho Pop()
    {
        if (Empty())
            throw new InvalidOperationException("A pilha está vazia.");

        int lastIndex = cartas.Count - 1;
        CartaDoBaralho carta = cartas[lastIndex];
        cartas.RemoveAt(lastIndex);

        return carta;
    }
}

class CartaDoBaralho
{
    public string Numero { get; set; }
    public string Naipe { get; set; }
    public int Pontuacao { get; set; }


    public CartaDoBaralho(string numero, string naipe, int pontuacao = 1)
    {
        Numero = numero;
        Naipe = naipe;
        Pontuacao = pontuacao;
    }

    public override string ToString()
    {
        return Numero + " de " + Naipe;
    }
}
