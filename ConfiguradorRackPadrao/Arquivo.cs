using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConfiguradorRackPadrao
{
    public class Arquivo
    {
        private static List<string> GetCaminhoNomeExtensaoArquivo(string codigo)
        {
            var listaDeArquivos = new List<string>();
            var arquivos = Directory.GetFiles(@"C:\ELETROFRIO\ENGENHARIA SMR", ".", SearchOption.AllDirectories);
            var nome = new List<string>();
            var dicArquivos = new Dictionary<string, string>();

            foreach (var arquivo in arquivos)
            {
                try
                {
                    dicArquivos.Add(Path.GetFullPath(arquivo).ToUpper(), Path.GetFileNameWithoutExtension(arquivo).ToUpper());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var subset = dicArquivos.Where(entry => entry.Value.Equals(codigo));
            var total = subset.Count();

            foreach (var item in subset)
            {
                listaDeArquivos.Add(item.Key);
            }
            return listaDeArquivos;
        }

        private static string Tipo(string t, string codigo)
        {
            var arquivos = GetCaminhoNomeExtensaoArquivo(codigo);

            foreach (var item in arquivos)
            {
                var extensao = Path.GetExtension(item);
                if ((extensao == ".SLDPRT" && t == "3d") || (extensao == ".SLDASM" && t == "3d"))
                {
                    return item;
                }
                else if (extensao == ".SLDDRW" && t == "2d")
                {
                    return item;
                }
            }
            return "";
        }

        public static void Abrir3D(string codigo)
        {
            var tipo = "3d";
            var nome3d = Tipo(tipo, codigo);
            AbrirArquivoDoSolidWorks(nome3d);
        }

        public static void Abrir2D(string codigo)
        {
            var tipo = "2d";
            var nome2d = Tipo(tipo, codigo);
            AbrirArquivoDoSolidWorks(nome2d);
        }

        private static void AbrirArquivoDoSolidWorks(string nome)
        {
            //OpenDoc6();
        }
    }
}
