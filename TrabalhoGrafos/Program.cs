using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void PreencherGrafo(List<Vertice> grafo)
        {
            for (int i = 0; i < 10; i++)
            {
                grafo.Add(new Vertice()
                {
                    Nome = $"V{i}",
                    Adjacencias = new List<Vertice>()
                });
            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (i != j)
                    {
                        grafo[i].Adjacencias.Add(grafo[j]);
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

            if (vertices.Nome == nomeDoVertice)
            {
                return vertices;
            }
            else
            {
                foreach (var vertice in vertices.Adjacencias)
                {
                    pilha.Push(vertice);

                    if (vertice.Adjacencias.Count == 0)
                    {
                        pilha.Pop();
                        encontrado = BuscaProfundidade(pilha.Peek(), nomeDoVertice);
                    }
                    else if(!visitados.Contains(vertice))
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

            // Preencher o grafo automaticamente, apenas para testes
            PreencherGrafo(grafo);

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

                }
                else
                {

                }

                opcao = Menu();
            }
        }
    }
}
