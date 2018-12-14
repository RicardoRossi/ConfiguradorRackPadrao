using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SldWorks;
using SwConst;

namespace ConfiguradorRackPadrao
{
    public class Arquivo
    {
        static string[] arquivos;
        static readonly Dictionary<string, string> dicArquivos = new Dictionary<string, string>();

        public Arquivo()
        {
            arquivos = Directory.GetFiles(@"C:\ELETROFRIO\ENGENHARIA SMR", ".", SearchOption.AllDirectories);
            // Adiciona os arquivos num dicionário chave valor.
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
        }

        public static void Abrir3D(string codigo)
        {
            const string tipo = "3d";
            var nome3d = Tipo(tipo, codigo); // nome3d é uma tupla que retorna dois parãmetros
            AbrirArquivoDoSolidWorks(nome3d.Item1, nome3d.Item2);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        public static void Abrir2D(string codigo)
        {
            const string tipo = "2d";
            var nome2d = Tipo(tipo, codigo); // nome2d é uma tupla que retorna dois parãmetros
            AbrirArquivoDoSolidWorks(nome2d.Item1, nome2d.Item2);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        // O método vai filtrar a lista retornada por GetCaminhoNomeExtensaoArquivo() com base no tipo de arquivo que
        // será aberto.
        // C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\COLETOR SUCCAO\2047866.SLDDRW
        // C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\COLETOR SUCCAO\2047866.SLDASM
        // C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\COLETOR SUCCAO\2047866.SLDPRT
        // Conforme o metodo abrir invocado será retorna o caminho com a extensão correta para o tipo 2d ou 3d e o tipo
        // 1, 2, ou 3 que o OpenDoc6 requer.
        // Pode dar problema se houver o mesmo codigo com as extensão sldprt e sldasm. (Resolver)
        // ex;
        // tql200L.sldprt
        // tql200L.sldasm
        private static (string, int) Tipo(string t, string codigo) //Return com tuple
        {
            var arquivos = GetCaminhoNomeExtensaoArquivo(codigo);

            foreach (var item in arquivos)
            {
                var extensao = Path.GetExtension(item);

                if (extensao == ".SLDASM" && t == "3d")
                {
                    return (item, 2); //Retorna o fullname do arquivo e o tipo int para swDocASSEMBLY
                }
                else if (extensao == ".SLDPRT" && t == "3d")
                {
                    return (item, 1); //Retorna o fullname do arquivo e o tipo int para swPart
                }
                else if (extensao == ".SLDDRW" && t == "2d")
                {
                    return (item, 3); //Retorna o fullname do arquivo e o tipo int para swDrawing
                }
            }
            return ("", -1); // -1 é um valor qualquer diferente dos tipos do OpenDoc6
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        // Pega o fullname de todos os arquivos dos diretorios e subdiretorios
        // Retorna uma lista com o caminho de todos os arquivos que são igual
        // ao código do parãmetro.
        // EX:
        // C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\COLETOR SUCCAO\2047866.SLDDRW
        // C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\COLETOR SUCCAO\2047866.SLDASM
        // C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\COLETOR SUCCAO\2047866.SLDPRT
        private static List<string> GetCaminhoNomeExtensaoArquivo(string codigo)
        {
            var listaDeArquivos = new List<string>();
            var subset =  dicArquivos.Where(entry => entry.Value.Equals(codigo));

            foreach (var item in subset)
            {
                listaDeArquivos.Add(item.Key);
            }

            return listaDeArquivos;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        private static void AbrirArquivoDoSolidWorks(string nome, int tipo)
        {
            if (nome ==string.Empty)
            {
                return;
            }

            SldWorks.SldWorks swApp;
            ModelDoc2 swModel;
            swApp = SolidWorksSingleton.Get_swApp();
            swModel = swApp.ActiveDoc;
            var errors = 0;
            var warnings = 0;
            // Abrir arquivo invisível.
            swApp.DocumentVisible(false, (int)swDocumentTypes_e.swDocASSEMBLY);
            swApp.DocumentVisible(false, (int)swDocumentTypes_e.swDocPART);
            swApp.OpenDoc6(nome, tipo, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", (int)errors, (int)warnings); // o tipo será 1, 2, ou 3. Part, Assembly ou Drawing



            AssemblyDoc swAsm;
            Component2 swComp;
            swAsm = (AssemblyDoc)swModel;
            var r = new Random();
            swComp = swAsm.AddComponent4(nome, "",1, 0, 1);
            //swApp.ActivateDoc(nome);

            //Feature swFeature = swComp.FeatureByName("cs_base1");
            //swFeature.Select(true);
            var nome_cs = "cs_" + Path.GetFileNameWithoutExtension(nome);
            var swFeature = swComp.FeatureByName(nome_cs);

            if (swFeature != null)
            {
                swFeature.Select(true);
            }

            var nomeDoComponente = swComp.Name2; // Retorna nome com o número da instância
        }
    }
}
