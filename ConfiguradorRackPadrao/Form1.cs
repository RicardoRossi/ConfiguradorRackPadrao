using SldWorks;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ConexaoAccess;

namespace ConfiguradorRackPadrao
{
    public partial class Form1 : Form
    {
        SldWorks.SldWorks swApp;
        ModelDoc2 swModel;
        ModelDocExtension swExt;
        CustomPropertyManager swpropMgr;

        public Form1()
        {
            InitializeComponent();
            swApp = (SldWorks.SldWorks)Marshal.GetActiveObject(@"SldWorks.Application");
            //swApp.SendMsgToUser("Conectado.");
            swModel = swApp.ActiveDoc;
            swExt = swModel.Extension;
            swpropMgr = swExt.CustomPropertyManager[""];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Propriedade.AdicionarProp(swpropMgr, "Classe C#", "C#");
            //PropTeste.SetPropriedade(swpropMgr, "Classe VB", "VB .net");
            //Propriedade.AdicionarProp(swpropMgr, "Teste DLL", "Prop DLL", "Dll Refletiu alteração");

            //ConexaoBD.Conecatr();

            AssemblyDoc swAssem = (AssemblyDoc)swModel;

            var itens = swAssem.GetComponents(false);

            foreach (var item in itens)
            {
                var comp = (Component2)item;
                var nome = comp.Name;
                var id = comp.GetID();
                var feature = comp.FeatureByName("cs_boia");

                if (!(feature is null))
                {
                    feature.Select2(false, -1); 
                }
                Console.WriteLine(nome + id);
            }



            //var caminho = @"C:\ELETROFRIO\ENGENHARIA SMR\NOVA ESTRUTURA\RACK 1\02_CAD\2048212.sldprt";
            //var cs = "cs_base_compressor";

            //Component2 item = swAssem.AddComponent4(caminho, "", 0, 0, 0);


          
            //Feature feature = item.FeatureByName(cs);
            //feature.Select2(false, -1);

        }




    }
}
