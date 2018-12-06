using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConfiguradorRackPadrao
{
    class Arquivo
    {
        public string GetNomeCompletoArquivo(string codigo)
        {
            var arquivos = Directory.GetFiles(@"C:\ELETROFRIO\ENGENHARIA SMR", ".", SearchOption.AllDirectories);

            arquivos
            
        }
    }
}
