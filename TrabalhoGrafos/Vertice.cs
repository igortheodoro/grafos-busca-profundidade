using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoGrafos
{
    public class Vertice
    {
        public string Nome { get; set; }    
        public List<Vertice> Adjacencias { get; set; }
    }
}
