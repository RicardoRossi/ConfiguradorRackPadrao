using SldWorks;
using SwConst;
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
            swModel = swApp.ActiveDoc;
            var arquivo = new Arquivo();

            // Chamar montador e passar codigo do kit
            Montador.MontarKit(txtCodigo.Text);

            swApp.DocumentVisible(true, (int)swDocumentTypes_e.swDocASSEMBLY);
            swApp.DocumentVisible(true, (int)swDocumentTypes_e.swDocPART);

            swExt = swModel.Extension;
            swExt.Rebuild((int)swRebuildOptions_e.swUpdateMates);
        }
    }
}
