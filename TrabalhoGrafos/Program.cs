using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrabalhoGrafos
{
    class Program
    {
        public static List<Vertice> visitados = new List<Vertice>();
        public static Stack<Vertice> pilha = new Stack<Vertice>();

        private static int Menu()
        {
            Console.WriteLine("\n[1] - Ver grafo");
            Console.WriteLine("[2] - Verificar vértice existente");
            Console.WriteLine("[3] - Adicionar vértice");
            Console.WriteLine("[4] - Remover vértice");
            Console.WriteLine("[5] - Adicionar aresta");
            Console.WriteLine("[6] - Remover aresta");
            Console.WriteLine("[7] - Sair");
            Console.Write("\nDigite o número correspondente a opção que deseja:");
            return int.Parse(Console.ReadLine());
        }

        public static void AdicionarAresta(List<Vertice> grafo)
        {
            Console.Write("\nDigite o nome vértice que deseja inserir aresta: ");
            var nomeVertice1 = Console.ReadLine();

            Console.Write($"\nDigite o nome do vértice que será adjacente ao vértice {nomeVertice1}: ");
            var nomeVertice2 = Console.ReadLine();

            var vertice1 = EncontraVertice(grafo, nomeVertice1);
            var vertice2 = EncontraVertice(grafo, nomeVertice2);
            vertice1.Adjacencias.Add(vertice2);
            vertice2.Adjacencias.Add(vertice2);
        }

        public static void RemoverAresta(List<Vertice> grafo)
        {
            Console.Write("\nDigite o nome vértice que deseja remover aresta: ");
            var nomeVertice1 = Console.ReadLine();

            Console.Write($"\nDigite o nome do vértice seja adjacente ao vértice {nomeVertice1} que você deseja remover: ");
            var nomeVertice2 = Console.ReadLine();

            var vertice1 = EncontraVertice(grafo, nomeVertice1);
            var vertice2 = EncontraVertice(grafo, nomeVertice2);
            vertice1.Adjacencias.Remove(vertice2);
            vertice2.Adjacencias.Remove(vertice2);
        }

        public static void PreencherGrafo(List<Vertice> grafo)
        {

            var grafoTexto = new StreamReader(@"../../../grafo.txt");
            string linha = null;

            while ((linha = grafoTexto.ReadLine()) != null)
            {
                if (!linha.Contains(" ") && !linha.Contains("-"))
                {
                    grafo.Add(new Vertice()
                    {
                        Adjacencias = new List<Vertice>(),
                        Nome = linha
                    });
                }
                else if(!linha.Contains("-"))
                {
                    var vertices = Regex.Matches(linha, @"\w{2} ");

                    for (int i = 0; i < vertices.Count; i++)
                    {
                        if (i != 0)
                        {
                            grafo.FirstOrDefault(v => v.Nome == vertices[0].Value.Replace(" ", ""))
                                .Adjacencias
                                .Add(grafo.FirstOrDefault(v => v.Nome == vertices[i].Value.Replace(" ", "")));
                        }
                    }
                }
            }
        }

        public static void VerGrafo(List<Vertice> grafo)
        {
            foreach (var vertice in grafo)
            {
                Console.WriteLine($"Vértice: {vertice.Nome}");
                if (vertice.Adjacencias.Count == 0)
                {
                    Console.WriteLine($"Adjacencias: {vertice.Nome} não há vértices adjacentes!\n");
                }
                else
                {
                    foreach (var adjacencia in vertice.Adjacencias)
                    {
                        Console.WriteLine($"Adjacencias: {adjacencia.Nome} \n");
                    }
                }
            }
        }

        public static void RemoverVertice(List<Vertice> grafo)
        {
            Console.Write("\nDigite nome do vértice que deseja remover: ");
            var nome = Console.ReadLine();

            var vertice = EncontraVertice(grafo, nome);

            if (vertice == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("\nImpossível remover um vértice não existente!");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                grafo.Remove(vertice);

                foreach (var v in grafo)
                {
                    v.Adjacencias.RemoveAll(v => v.Nome == nome);
                }

                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVértice removido!");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static void VerificarVertice(List<Vertice> grafo)
        {
            Console.Write("\nDigite o nome do vértice que deseja encontrar: ");
            var nome = Console.ReadLine();

            var vertice = EncontraVertice(grafo, nome);
            if (vertice != null)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Vértice existente!");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Vértice não existente!");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static Vertice EncontraVertice(List<Vertice> grafo, string nome)
        {
            Vertice encontrado = new Vertice();
            foreach (var vertice in grafo)
            {
                encontrado = BuscaProfundidade(vertice, nome);

                if (encontrado != null)
                {
                    return encontrado;
                }
            }

            visitados = new List<Vertice>();
            pilha = new Stack<Vertice>();
            return encontrado;
        }

        public static Vertice BuscaProfundidade(Vertice vertices, string nomeDoVertice)
        {
            Vertice encontrado = null;
            pilha.Push(vertices);
            if (vertices.Nome == nomeDoVertice)
            {
                return vertices;
            }
            else
            {
                foreach (var vertice in vertices.Adjacencias)
                {

                    if (vertice.Adjacencias.Count == 0)
                    {
                        pilha.Pop();
                        visitados.Add(vertice);
                        encontrado = BuscaProfundidade(pilha.Peek(), nomeDoVertice);
                    }
                    else if (!visitados.Contains(vertice))
                    {
                        visitados.Add(vertice);
                        encontrado = BuscaProfundidade(vertice, nomeDoVertice);
                    }
                }

                return encontrado;
            }
        }

        public static void AdicionarVertice(List<Vertice> grafo)
        {
            Console.Write("\nDigite nome do vértice que deseja incluir: ");
            var nome = Console.ReadLine();

            grafo.Add(new Vertice()
            {
                Nome = nome,
                Adjacencias = new List<Vertice>()
            });
        }

        static void Main(string[] args)
        {
            var opcao = Menu();

            var grafo = new List<Vertice>();

            PreencherGrafo(grafo);

            // Preencher o grafo automaticamente, apenas para testes
            //PreencherGrafo(grafo);

            while (opcao < 7)
            {
                if (opcao == 1)
                {
                    VerGrafo(grafo);
                }
                else if (opcao == 2)
                {
                    VerificarVertice(grafo);
                }
                else if (opcao == 3)
                {
                    AdicionarVertice(grafo);
                }
                else if (opcao == 4)
                {
                    RemoverVertice(grafo);
                }
                else if (opcao == 5)
                {
                    AdicionarAresta(grafo);
                }
                else
                {
                    RemoverAresta(grafo);
                }

                opcao = Menu();
            }
        }
    }
}
