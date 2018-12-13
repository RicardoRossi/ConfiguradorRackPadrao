using SldWorks;
using SwConst;
using System;
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
            foreach (var componente in ListaComponentes.ListarComponentesDokit(codigo))
            {
                Arquivo.Abrir3D(componente.item);
                SelecionarSCM(componente);
            }            
        }

        private static void SelecionarSCM(Componente componente)
        {
            SldWorks.SldWorks swApp;
            ModelDoc2 swModel;
            AssemblyDoc swAsm;
            swApp = SolidWorksSingleton.Get_swApp();
            swModel = swApp.ActiveDoc;
            swAsm = swApp.ActiveDoc;

            Component2 swComp;
            Feature swFeature;
            Mate2 swMate;
           
            object[] componentes = swAsm.GetComponents(true);
            // Metodo interno.
            void SelecionarCS(string nomeDoSistemaDeCoordenadas)
            {
                foreach (var comp in componentes)
                {
                    swComp = (Component2)comp;                    
                    swFeature = swComp.FeatureByName(nomeDoSistemaDeCoordenadas);
                    if (swFeature!=null)
                    {
                        swFeature.Select(true);
                        var id = swComp.GetID();
                        
                        continue;
                    }
                }
            }

            var cs = "cs_" + componente.item; // cs da peça, igual ao prefixo cs_ mais o codigo do item.
            //SelecionarCS(cs); // Chama metodo interno
            SelecionarCS(componente.scm); // Chama metodo interno

            swMate = swAsm.AddMate5((int)swMateType_e.swMateCOORDINATE, (int)swMateAlign_e.swAlignNONE, false, 0, 0, 0, 0, 0, 0, 0, 0, false, true, 0, out int error);
            swModel.ClearSelection();            
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
