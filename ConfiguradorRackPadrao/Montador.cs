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

        public static void SelecionarSCM(Componente componente)
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
    }
}
