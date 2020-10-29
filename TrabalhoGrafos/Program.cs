using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabalhoGrafos
{
    class Program
    {
        private static int Menu()
        {
            Console.WriteLine("\n[1] - Ver grafo");
            Console.WriteLine("[2] - Buscar vértice");
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
            for (int i = 0; i < 5; i++)
            {
                grafo.Add(new Vertice()
                {
                    Nome = $"V{i}",
                    Adjacencias = new List<Vertice>()
                });

                // Do 2º vértice pra frente, adicionar para todos vértice uma adjacencia 
                //com o primeiro vértice
                if (i > 1)
                {
                    grafo[i].Adjacencias.Add(grafo[1]);
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

            

        }

        public static Vertice EncontraVertice(List<Vertice> grafo, string nome)
        {
            //var pilha = new Stack<Vertice>();
            var verticesVisitados = new List<Vertice>();

            Vertice x = null;

            foreach (var vertice in grafo)
            {
                if (!verticesVisitados.Contains(vertice))
                {
                   x = VisitaVertice(grafo, vertice, verticesVisitados, nome);
                }
            }

            return x;
        }

        public static Vertice VisitaVertice(List<Vertice> grafo, Vertice vertice, List<Vertice> verticesVisitados , string nome)
        {
            Vertice encontrado = null;

            if (vertice.Nome == nome)
            {
                return vertice;
            }
            else
            {
                foreach (var v in vertice.Adjacencias)
                {
                    verticesVisitados.Add(v);

                    if (encontrado == null && verticesVisitados.IndexOf(vertice) < 0 && verticesVisitados.Count < grafo.Count)
                    {
                        encontrado = VisitaVertice(grafo, v, verticesVisitados, nome);
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
                else if (opcao == 3)
                {
                    AdicionarVertice(grafo);
                }
                else if (opcao == 4)
                {

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
