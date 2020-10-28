using System;
using System.Collections.Generic;

namespace TrabalhoGrafos
{
    class Program
    {
        private static int Menu()
        {
            Console.WriteLine("\n[1] - Ver grafo");
            Console.WriteLine("[2] - Buscar vértice");
            Console.WriteLine("[3] - Remover vértice");
            Console.WriteLine("[4] - Adicionar vértice");
            Console.WriteLine("[5] - Adicionar aresta");
            Console.WriteLine("[6] - Sair");
            Console.Write("\nDigite o número correspondente a opção que deseja:");
            return int.Parse(Console.ReadLine());
        }

        public static void PreencherGrafo(List<Vertice> grafo)
        {
            for (int i = 0; i < 5; i++)
            {
                grafo.Add(new Vertice()
                {
                    Nome = $"Vértice {i}",
                    Adjacencias = new List<Vertice>()
                });

                // Do 2º vértice pra frente, adicionar para todos vértice uma adjacencia 
                //com o primeiro vértice
                if (i >= 1)
                {
                    grafo[i].Adjacencias.Add(grafo[1]);
                }
            }
        }
        static void Main(string[] args)
        {
            var opcao = Menu();

            var grafo = new List<Vertice>();

            // Preencher o grafo automaticamente, apenas para testes
            PreencherGrafo(grafo);

            while (opcao < 6)
            {
                if (opcao == 1)
                {
                    foreach (var vertice in grafo)
                    {
                        foreach (var adjacencia in vertice.Adjacencias)
                        {
                            Console.WriteLine($"Vértice: {vertice.Nome}");
                            Console.WriteLine($"Adjacencias: {adjacencia.Nome} \n");
                        }
                    }
                }
                else if (opcao == 2)
                {

                }
                else if (opcao == 3)
                {

                }
                else if (opcao == 4)
                {

                }
                else if (opcao == 5)
                {

                }

                opcao = Menu();
            }
        }
    }
}
