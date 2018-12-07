using SldWorks;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            swApp = await SolidWorksSingleton.Get_swAppAsync();

            // Chamar montador e passar codigo do kit
            Montador.MontarKit("4020001");

            //AssemblyDoc swAssem = (AssemblyDoc)swModel;

            //var itens = swAssem.GetComponents(false);

            //foreach (var item in itens)
            //{
            //    var comp = (Component2)item;
            //    var nome = comp.Name;
            //    var id = comp.GetID();
            //    var feature = comp.FeatureByName("cs_boia");

            //    if (!(feature is null))
            //    {
            //        feature.Select2(false, -1);
            //    }
            //    Console.WriteLine(nome + id);
            //}

            //var caminho = @"C:\ELETROFRIO\ENGENHARIA SMR\NOVA ESTRUTURA\RACK 1\02_CAD\2048212.sldprt";
            //var cs = "cs_base_compressor";

            //Component2 item = swAssem.AddComponent4(caminho, "", 0, 0, 0);



            //Feature feature = item.FeatureByName(cs);
            //feature.Select2(false, -1);

            //Arquivo.Abrir3D("2047866");
        }
      
    }
}
