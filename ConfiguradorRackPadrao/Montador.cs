using SldWorks;
using System.Runtime.InteropServices;

namespace ConfiguradorRackPadrao
{
    public class Montador
    {
        public static void MontarKit(string codigo)
        {
            ReceberListaDeComponentesDokit(codigo);
        }

        static void ReceberListaDeComponentesDokit(string codigo)
        {
            // Teste abrir todos os arquivos da lista.
            foreach (var item in ListaComponentes.MontarListaDeComponentesDoKit(codigo))
            {
                Arquivo.Abrir3D(item.item);             
            }            
        }

        /*
         // Seleciona os componentes no top level
            var componentes = swAssem.GetComponents(true);

            // Escolher o componente no top level e selecionar a feature by name
            foreach (var componente in componentes)
            {
                var comp = (Component2)componente;
                var nome = comp.Name;
                var feature = comp.FeatureByName("cs_base_compressor");

                if (!(feature is null))
                {
                    feature.Select2(false, -1);
                }
            }

            // A ideia e receber um kit com os itens
            // fazer um loop por todos os itens
            // adicionar e selecionar o cs
            // O doc deve estar na memória para usar o AddComponent
            //Component2 item = swAssem.AddComponent4(caminho, "", 0, 0, 0);


            // Para selecionar o cs do item inserido.
            //Feature feature = item.FeatureByName("cs_2025531");
            //feature.Select2(false, -1);

            // Para selecionar o cs do master model, como o master model sempre será a instancia 1
            // deixar o nome fixo "Master_part_rp-1"
            // o nome do sc virá do bd da tbl tbl_kit scm
            // cs_sero será uma variável
            // Part.Extension.SelectByID2("cs_zero@Master_part_rp-1@2300142", "COORDSYS", 0, -0, 0, False, 0, Nothing, 0)
         
         */
    }
}
