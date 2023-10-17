using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Venda_2023.model
{
    internal class Cidade
    {
        public int Codcidade { get; set; }
        public String Nomecidade { get; set; }
        public Uf Uf { get; set; }

        public Cidade() { }
    }
}
