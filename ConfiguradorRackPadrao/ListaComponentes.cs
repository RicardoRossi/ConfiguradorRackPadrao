using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using ConfiguradorRackPadrao.Properties;

namespace ConfiguradorRackPadrao
{
    public class ListaComponentes
    {
        public static List<Componente> ListarComponentesDokit(string codigo)
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

                        while (reader.Read())
                        {
                            if (reader.IsDBNull(2))
                            {
                                // Cria componente com info do acess
                                var componente = new Componente { item = reader.GetString(0), qt = reader.GetInt32(1), scm = "cs" }; // Se a coluna scm tiver valor nulo.
                                listaDeComponentesDoKit.Add(componente);
                            }
                            else
                            {
                                // Cria componente com info do acess
                                var componente = new Componente { item = reader.GetString(0), qt = reader.GetInt32(1), scm = reader.GetString(2) }; // O indice segue a order do select acima.
                                listaDeComponentesDoKit.Add(componente);
                            }
                        }
                        // Retorna uma lista de objetos componentes

                        foreach (var item in listaDeComponentesDoKit)
                        {
                            Console.WriteLine(item.item);
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
    }
}