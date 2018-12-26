using SldWorks;
using SwConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ConfiguradorRackPadrao
{
    public partial class Form1 : Form
    {
        SldWorks.SldWorks swApp;
        ModelDoc2 swModel;
        ModelDocExtension swExt;
        CustomPropertyManager swpropMgr;
        ModelView swView;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            swApp = SolidWorksSingleton.Get_swApp();
            swModel = swApp.ActiveDoc;
            var arquivo = new Arquivo();
            int[] codigos = Enumerable.Range(4020128, 2).ToArray();

            for (int i = 0; i < codigos.Length - 1; i++)
            {
                txtCodigo.Text = codigos[i].ToString();
                swApp.OpenDoc(@"C:\ELETROFRIO\ENGENHARIA SMR\PRODUTOS FINAIS ELETROFRIO\MECÂNICA\RACK PADRAO\template_00_rp.SLDASM",
                    (int)swDocumentTypes_e.swDocASSEMBLY);
                swModel = swApp.ActiveDoc;
                swView = swModel.ActiveView;
                swView.EnableGraphicsUpdate = false;
                // Chamar montador e passar codigo do kit
                string codigo = codigos[i].ToString();
                Montador.MontarKit(codigo);
                swModel = swApp.ActiveDoc;
                swExt = swModel.Extension;
                swApp.DocumentVisible(true, (int)swDocumentTypes_e.swDocASSEMBLY);
                swApp.DocumentVisible(true, (int)swDocumentTypes_e.swDocPART);
                string fullPath = @"C:\ELETROFRIO\ENGENHARIA SMR\PRODUTOS FINAIS ELETROFRIO\MECÂNICA\RACK PADRAO\RACK PADRAO TESTE\" + codigo + ".sldasm";
                swExt.Rebuild((int)swRebuildOptions_e.swUpdateMates);
                swModel.SaveAs(fullPath);
                swApp.CloseDoc(fullPath);
            }

            swView.EnableGraphicsUpdate = true;
        }
    }
}
