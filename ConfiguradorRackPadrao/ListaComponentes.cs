using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using ConfiguradorRackPadrao.Properties;

namespace ConfiguradorRackPadrao
{
    public class ListaComponentes
    {
        static List<Componente> ListarComponentesDokit(string codigo)
        {
            var listaDeComponentesDoKit = new List<Componente>();
            // String de conexao definida na properties/settings do projeto.
            // A fonte de dados é o arquico accdb que está no PDM.
            // Provider=Microsoft.ACE.OLEDB.12.0;Data Source="C:\ELETROFRIO\ENGENHARIA SMR\produtos finais eletrofrio\mecânica\Rack padrao\CONFIGURADOR
            using (OleDbConnection conn = new OleDbConnection(Settings.Default.connAccess))
            {
                using (var command = new OleDbCommand($@"Select item, qt, scm from tbl_kit where kit_cod like {codigo} ", conn))
                {
                    try
                    {
                        conn.Open();
                        var reader = command.ExecuteReader();
                        Console.WriteLine("Itens da tabela itens do RP");

                        while (reader.Read())
                        {
                            if (reader.IsDBNull(2))
                            {
                                var k = new Componente { item = reader.GetString(0), qt = reader.GetInt32(1), scm = "cs" }; // Se a coluna scm tiver valor nulo.
                                listaDeComponentesDoKit.Add(k);
                            }
                            else
                            {
                                var k = new Componente { item = reader.GetString(0), qt = reader.GetInt32(1), scm = reader.GetString(2) };
                                listaDeComponentesDoKit.Add(k);
                            }                           
                        }
                        return listaDeComponentesDoKit;
                    }
                    catch (OleDbException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                }
            }
        }

        public static List<Componente> MontarListaDeComponentesDoKit(string codigoKit)
        {
            var kit = new List<Componente>();

            kit = ListarComponentesDokit(codigoKit);
            if (kit == null)
            {
                MessageBox.Show("Kit null, pesquisa falhou.");
            }
            return kit;
        }
    }
}